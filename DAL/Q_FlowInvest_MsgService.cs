using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class Q_FlowInvest_MsgService
    {
        public static bool IsExist(string Guid, string Type)
        {
            List<Q_FlowInvest_Msg> list = getlist("  FlowInvestGuid = '" + Guid + "' and MsgType = '" + Type + "'",0,0);
            return (list.Count > 0 ? true : false);
        }
        public static bool UpdateMac(Q_FlowInvest_Msg a)
        {
            string sql = "update Q_FlowInvest_Msg set Mac = @Mac where Id = @Id";
            SqlParameter[] parameters = {
                new SqlParameter("@Id" ,            SqlDbType.Int ,      8){ Value = a.Id },
                new SqlParameter("@Mac" ,            SqlDbType.VarChar  ){ Value = a.Mac}
            };
            return SqlHelper.ExecuteCommandIM(sql, parameters) > 0;
        }


        #region 基础维护

        public static Q_FlowInvest_Msg getOneById(int id)
        {
            return getOne("  Id = " + id);
        }

        public static bool IsExist(string Name)
        {
            return (getOne(" Name = '" + Name + "'") != null ? true : false);
        }

        public static List<Q_FlowInvest_Msg> getlist(string where, int PageSize, int CurrentPageIndex)
        {
            List<Q_FlowInvest_Msg> list = new List<Q_FlowInvest_Msg>();
            string sql = "select * " + SqlHelper.StrPage1(" Id desc") + " from Q_FlowInvest_Msg";
            sql = SqlHelper.getSql(sql, where, PageSize, CurrentPageIndex);
            using (SqlDataReader sr = SqlHelper.GetDataReaderIM(sql)) { while (sr.Read()) { list.Add(getsr(sr)); } }
            return list;
        }

        public static Q_FlowInvest_Msg getOne(string where)
        {
            Q_FlowInvest_Msg one = null;
            string sql = "select *  from Q_FlowInvest_Msg";
            sql = (where != "" ? sql + " where " + where : sql);
            using (SqlDataReader sr = SqlHelper.GetDataReaderIM(sql)) { while (sr.Read()) { one = getsr(sr); } }
            return one;
        }

        private static Q_FlowInvest_Msg getsr(SqlDataReader sr)
        {
            Q_FlowInvest_Msg a = new Q_FlowInvest_Msg();
            a.Id = (int)sr["Id"];
            a.MsgType = (string)sr["MsgType"];
            a.FlowInvestGuid = (string)sr["FlowInvestGuid"];
            a.Creator = (string)sr["Creator"];
            a.TradeDate = (string)sr["TradeDate"];
            a.Mac = (string)sr["Mac"];
            a.Msg = (string)sr["Msg"];
            return a;
        }
        public static bool Insert(Q_FlowInvest_Msg a)
        {
            string sql = "insert into Q_FlowInvest_Msg values ( @MsgType,@FlowInvestGuid,@Creator,@TradeDate,@Mac,@Msg)";
            SqlParameter[] parameters = {
                new SqlParameter("@MsgType" ,            SqlDbType.VarChar  ){ Value = a.MsgType },
                new SqlParameter("@FlowInvestGuid" ,            SqlDbType.VarChar  ){ Value = a.FlowInvestGuid },
                new SqlParameter("@Creator" ,            SqlDbType.VarChar  ){ Value = a.Creator },
                new SqlParameter("@TradeDate" ,            SqlDbType.VarChar  ){ Value = a.TradeDate },
                new SqlParameter("@Mac" ,            SqlDbType.VarChar  ){ Value = a.Mac },
                new SqlParameter("@Msg" ,            SqlDbType.VarChar  ){ Value = a.Msg}
            };
            return SqlHelper.ExecuteCommandIM(sql, parameters) > 0;
        }
        public static bool Update(Q_FlowInvest_Msg a)
        {
            string sql = "update Q_FlowInvest_Msg set MsgType = @MsgType,FlowInvestGuid = @FlowInvestGuid,Creator = @Creator,TradeDate = @TradeDate,Mac = @Mac,Msg = @Msg where Id = @Id";
            SqlParameter[] parameters = {
                new SqlParameter("@Id" ,            SqlDbType.Int ,      8){ Value = a.Id },
                new SqlParameter("@MsgType" ,            SqlDbType.VarChar  ){ Value = a.MsgType },
                new SqlParameter("@FlowInvestGuid" ,            SqlDbType.VarChar  ){ Value = a.FlowInvestGuid },
                new SqlParameter("@Creator" ,            SqlDbType.VarChar  ){ Value = a.Creator },
                new SqlParameter("@TradeDate" ,            SqlDbType.VarChar  ){ Value = a.TradeDate },
                new SqlParameter("@Mac" ,            SqlDbType.VarChar  ){ Value = a.Mac},
                new SqlParameter("@Msg" ,            SqlDbType.VarChar  ){ Value = a.Msg}
            };
            return SqlHelper.ExecuteCommandIM(sql, parameters) > 0;
        }

        public static bool Delete(int id)
        {
            string sql = string.Format("delete from Q_FlowInvest_Msg where " +
                "Id = {0}", id);
            return SqlHelper.ExecuteCommandIM(sql) > 0;
        }
        #endregion
    }
}






