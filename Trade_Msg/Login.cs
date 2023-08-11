using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Model;
using DAL;


namespace Trade_Msg
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        public static int UserId = 0;
        public static string UserName = null;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string LoginName = this.txtLoginName.Text.Trim();
            LoginName = LoginName.Replace("-", "");
            string LoginPwd = this.txtLoginPwd.Text.Trim();
            LoginPwd = LoginPwd.Replace("-", "");


            if (LoginName == "" || LoginName == null || LoginPwd == "" || LoginPwd == null)
            {
                MessageBox.Show("用户名或密码不正确!");
                return;
            }
            else
            {
                try
                {
                    Boolean flag = Role_UserService.IsExist(LoginName, LoginPwd);
                    if (flag == true)
                    {
                        Role_User a = (Role_User)Role_UserService.getOneByLoginName(LoginName);
                        UserId = a.UserId;
                        UserName = a.UserName;

                        Q_LogService.getLog(UserName, "Form", "Login", "", "", "登陆成功", "");


                        Msg m = new Msg();
                        m.Show();

                        this.Visible = false;
                    }
                    else
                    {
                        Q_LogService.getLog(UserName, "Form", "Login", "", "", "登陆失败，用户名或密码错误", "");
                        MessageBox.Show("用户名或密码错误，请重新输入");
                        this.txtLoginPwd.Text = "";
                    }
                }
                catch
                {
                    MessageBox.Show("数据库访问失败！");
                }
            }
        }
        

        private void btnCacel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
