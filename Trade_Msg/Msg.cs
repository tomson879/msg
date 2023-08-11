using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Web;
using Model;
using DAL;
using System.Net.NetworkInformation;

namespace Trade_Msg
{
    public partial class Msg : Form
    {
        #region 基本控制
        public Msg()
        {
            InitializeComponent();
            this.lblUserName.Text = "当前用户：" + Login.UserName.ToString();
            setlbxHistInvestBind();
        }

        public static Boolean IsExit = false;


        /// 页面初始加载
        private void Msg_Load(object sender, EventArgs e) { }


        /// 双击图标
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Activate();
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
        }

        /// 退出按钮
        private void tuiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsExit = true;
            notifyIcon1.Dispose();
            Application.Exit();
        }

        /// 隐藏按钮
        private void btnHide_Click(object sender, EventArgs e) { hide(); }


        /// 隐藏程序
        protected void hide()
        {
            this.Visible = false;
            this.ShowInTaskbar = false;
            this.notifyIcon1.Visible = true;
        }

        /// 显示程序
        protected void show()
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// 定时器---每5秒扫描一次
        private void timer1_Tick(object sender, EventArgs e)
        {
            Thread t = new Thread(o => Thread.Sleep(1));
            t.Start(this);
            while (t.IsAlive)
            {
                Application.DoEvents();
            }
            //Thread t2 = new Thread(getInvest);
            getInvest();//指令处理
            //t.Abort();
        }

        #endregion

        #region 信息提示

        //自动存， 提示信息存入到数据库，然后响声音，每台主机把自己的电脑mac地址发到数据库，检测1分钟内的消息，有如果已经提醒了就不提醒，没有就提醒。
        protected void getInvest()
        {
            //1、将指令存入到消息列表
            List<Q_FlowInvest> list_New =null;
            List<Q_FlowInvest> list_Stop = null;
            try
            {
                string strWhere_New = "Guid not in (select FlowInvestGuid from q_flowinvest_msg where MsgType = '有效指令') ";
                string strWhere_Stop = "Guid not in (select FlowInvestGuid from q_flowinvest_msg where MsgType = '终止指令') and Remark like '%终止指令%'";
                list_New = Q_FlowInvestService.getlist(strWhere_New, 0, 0);
                list_Stop = Q_FlowInvestService.getlist(strWhere_Stop, 0, 0);
            }
            catch
            {
                this.lblMessage.Text = "数据库连接不成功";
            }

            if (list_New != null && list_Stop != null)

            {
                int MsgNum = list_New.Count + list_Stop.Count;
                if (MsgNum > 0)
                {
                    //清空
                    lbInvest.Items.Clear();

                    //写入
                    foreach (Q_FlowInvest a in list_New)
                    {
                        SaveToFlowInvest_Msg(a, "有效指令");
                    }
                    foreach (Q_FlowInvest a in list_Stop)
                    {
                        SaveToFlowInvest_Msg(a, "终止指令");
                    }


                }

                //读取消息列表,
                string Mac = GetMac();
                string User = Login.UserName.ToString() + "(" + Environment.MachineName + ")";
                string TradeDate = DateTime.Now.ToString("yyyy-MM-dd");
                string strWhere_Msg_New = "  tradedate > '" + TradeDate + "' and Mac not like '%" + Mac + "%' and MsgType = '有效指令'";  //筛选今天的消息，并且不包含mac地址的消息。
                string strWhere_Msg_Stop = "  tradedate > '" + TradeDate + "' and Mac not like '%" + Mac + "%' and MsgType = '终止指令'";
                List<Q_FlowInvest_Msg> list_Msg_New = Q_FlowInvest_MsgService.getlist(strWhere_Msg_New, 0, 0);
                List<Q_FlowInvest_Msg> list_Msg_Stop = Q_FlowInvest_MsgService.getlist(strWhere_Msg_Stop, 0, 0);


                if (list_Msg_New.Count > 0)
                {
                    foreach (Q_FlowInvest_Msg a in list_Msg_New)
                    {
                        this.lbInvest.Items.Add("●  " + a.Msg);
                        a.Mac = a.Mac + (a.Mac.Length > 0 ? "，" : "") + Mac + " " + User;
                        DAL.Q_FlowInvest_MsgService.UpdateMac(a);
                    }
                    playMusic_New();  //play声音
                }
                if (list_Msg_Stop.Count > 0)
                {
                    foreach (Q_FlowInvest_Msg a in list_Msg_Stop)
                    {
                        this.lbInvest.Items.Add("▷  " + a.Msg);
                        a.Mac = a.Mac + (a.Mac.Length > 0 ? "，" : "") + Mac + " " + User;
                        DAL.Q_FlowInvest_MsgService.UpdateMac(a);
                    }
                    playMusic_Stop();  //play声音
                }

                //显示出来
                if(list_Msg_New.Count > 0 || list_Msg_Stop.Count > 0)
                {
                    this.Activate();
                    this.Visible = true;
                    this.WindowState = FormWindowState.Normal;
                }
            }
        }


        protected void SaveToFlowInvest_Msg(Q_FlowInvest a, string Type)
        {
            string Msg = "【" + a.Id + "】" + Type + " " + FundService.getName(a.FundId) + "_" + AccountMgService.getManager_ByFundMgId(a.FundMgId) + "_" + a.StockName + "" + (a.StockCode != a.StockName ? a.StockCode : "") + " " + a.TradeType + " " + a.Amount + "(" + a.PositionDescribe + ") " + a.PriceDescribe;
            Q_FlowInvest_MsgService.Insert(getModel(Type, a.Guid, Msg)); //添加
                                                                         // this.lbInvest.Items.Add(Msg);  //展示到前台
        }
        #endregion

        #region 声音处理程序

        protected void playMusic_New()
        {
            playMusic("new.wav");
        }
        protected void playMusic_Stop()
        {
            playMusic("stop.wav");
        }

        /// 播放音乐
        protected void playMusic(string MusicName)
        {
            string path = System.Threading.Thread.GetDomain().BaseDirectory;
            string path2 = path + MusicName;
            PlaySound(path2, new System.IntPtr(), PlaySoundFlags.SND_SYNC);
        }

        /// 声音处理文件
        [System.Runtime.InteropServices.DllImport("winmm.DLL", EntryPoint = "PlaySound", SetLastError = true)]
        private static extern bool PlaySound(string szSound, System.IntPtr hMod, PlaySoundFlags flags);

        [System.Flags]
        public enum PlaySoundFlags : int
        {
            SND_SYNC = 0x0000,
            SND_ASYNC = 0x0001,
            SND_NODEFAULT = 0x0002,
            SND_LOOP = 0x0008,
            SND_NOSTOP = 0x0010,
            SND_NOWAIT = 0x00002000,
            SND_FILENAME = 0x00020000,
            SND_RESOURCE = 0x00040004
        }

        #endregion

        #region 附属程序

        /// <summary>
        /// 名称：setlbxHistInvestBind  绑定指令列表
        /// 逻辑：通过赋值table，绑定到gridview控件
        /// </summary>
        protected void setlbxHistInvestBind()
        {
            List<Q_FlowInvest> list = Q_FlowInvestService.getlistTopX(30);
            DataTable table = new DataTable();
            table.Columns.Add("编号", Type.GetType("System.String"));
            table.Columns.Add("日期", Type.GetType("System.String"));
            table.Columns.Add("基金", Type.GetType("System.String"));
            table.Columns.Add("证券", Type.GetType("System.String"));
            table.Columns.Add("类别", Type.GetType("System.String"));
            table.Columns.Add("量", Type.GetType("System.String"));
            table.Columns.Add("下单人", Type.GetType("System.String"));
            table.Columns.Add("是否终止", Type.GetType("System.String"));
            for (int i = 0; i < list.Count; i++)
            {
                DataRow row = table.NewRow();
                row[0] = list[i].Id;
                row[1] = list[i].TradeDate.Substring(5, 11);
                row[2] = FundService.getName(list[i].FundId) + "-" + AccountMgService.getManager_ByFundMgId(list[i].FundMgId); ;
                row[3] = (list[i].StockCode != list[i].StockName ? list[i].StockCode : "") + " " + list[i].StockName;
                row[4] = list[i].TradeType;
                row[5] = list[i].PositionDescribe + " " + list[i].Amount;
                row[6] = list[i].Investor;
                row[7] = list[i].Remark;
                table.Rows.Add(row);
            }

            gvQ_FlowInvest.DataSource = table;
            for (int i = 0; i < 8; i++)
            {
                gvQ_FlowInvest.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }


        /// <summary>
        /// 名称：getModel  获得Q_FlowInvest_Msg实体
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Guid"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        protected Q_FlowInvest_Msg getModel(string Type, string Guid, string Msg)
        {
            Q_FlowInvest_Msg a = new Q_FlowInvest_Msg();
            a.MsgType = Type;
            a.FlowInvestGuid = Guid;
            a.Creator = Login.UserName.ToString() + "(" + Environment.MachineName + ")";  //姓名+电脑名
            a.TradeDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            a.Mac = "";
            a.Msg = Msg;
            return a;
        }

        protected void ShowMsgNum(int MsgNum)
        {
            if (MsgNum > 0)
            {
                this.lblMessage.Text = "有 " + MsgNum + " 条新信息";
                show();
                setlbxHistInvestBind();
            }
        }


        /// <summary>
        /// 名称：GetMac通过NetworkInterface获取MAC地址
        /// 功能：对人的提醒，本来可以通过session解决。
        ///       面临一种情况是交易员的办公室电脑开着msg，当天在家办公，那办公室电脑提醒了，在家电脑没有提醒。
        ///       为防止在家不能提醒的情况，针对mac地址提醒，这样家和公司都会提醒。
        /// </summary>
        /// <returns></returns>
        public static string GetMac()
        {
            try
            {
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface ni in interfaces)
                {
                    return BitConverter.ToString(ni.GetPhysicalAddress().GetAddressBytes());
                }
            }
            catch (Exception)
            {
            }
            return "00-00-00-00-00-00";
        }

        #endregion

        #region 发送投资经理的邮件
        private void timer_SendMail_Tick(object sender, EventArgs e)
        {
            string strWhere = " guid not in (select ComponentItem from q_log where component = 'sendMail')";
            List<Q_FlowInvest> list = Q_FlowInvestService.getlist(strWhere, 0, 0);
            foreach (Q_FlowInvest a in list)
            {
                DAL.MailSend.sendMail(a.Guid);
            }
        }
        #endregion

        private void Msg_Load_1(object sender, EventArgs e)
        {

        }
    }
}
