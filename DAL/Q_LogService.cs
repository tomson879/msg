using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Model;


namespace DAL
{
    public class Q_LogService
    {

        public static void getLog(string User, string Page, string Component, string ComponentItem, string Action, string ActionDescribe, string ActionStrWhere)
        {
            Q_Log a = new Q_Log();
            a.TradeDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            a.Browser = "WebForm客户端软件";
            a.UserName = User;
            a.Url = "";
            a.Page = Page;
            a.Component = Component;
            a.ComponentItem = ComponentItem;
            a.Action = Action;
            a.ActionDescribe = ActionDescribe;
            a.HttpAgent = "";
            a.Remark1 = ActionStrWhere;
            a.Remark2 = "";

            Insert(a);
        }



        #region 基础维护

        public static Q_Log getOneById(int id)
        {
            return getOne("  Id = " + id);
        }

        public static bool IsExist(string Name)
        {
            return (getOne(" Name = '" + Name + "'") != null ? true : false);
        }

        public static List<Q_Log> getlist(string where, int PageSize, int CurrentPageIndex)
        {
            List<Q_Log> list = new List<Q_Log>();
            string sql = "select * " + SqlHelper.IdAsc() + " from Q_Log";
            sql = SqlHelper.getSql(sql, where, PageSize, CurrentPageIndex);
            using (SqlDataReader sr = SqlHelper.GetDataReaderIM(sql)) { while (sr.Read()) { list.Add(getsr(sr)); } }
            return list;
        }

        public static Q_Log getOne(string where)
        {
            Q_Log one = null;
            string sql = "select *  from Q_Log";
            sql = (where != "" ? sql + " where " + where : sql);
            using (SqlDataReader sr = SqlHelper.GetDataReaderIM(sql)) { while (sr.Read()) { one = getsr(sr); } }
            return one;
        }

        private static Q_Log getsr(SqlDataReader sr)
        {
            Q_Log a = new Q_Log();
            a.Id = (int)sr["Id"];
            a.TradeDate = (string)sr["TradeDate"];
            a.Browser = (string)sr["Browser"];
            a.UserName = (string)sr["UserName"];
            a.Url = (string)sr["Url"];
            a.Page = (string)sr["Page"];
            a.Component = (string)sr["Component"];
            a.ComponentItem = (string)sr["ComponentItem"];
            a.Action = (string)sr["Action"];
            a.ActionDescribe = (string)sr["ActionDescribe"];
            a.HttpAgent = (string)sr["HttpAgent"];
            a.Remark1 = (string)sr["Remark1"];
            a.Remark2 = (string)sr["Remark2"];
            return a;
        }
        public static bool Insert(Q_Log a)
        {
            string sql = "insert into Q_Log values ( @TradeDate,@Browser,@UserName,@Url,@Page,@Component,@ComponentItem,@Action,@ActionDescribe,@HttpAgent,@Remark1,@Remark2)";
            SqlParameter[] parameters = {
                new SqlParameter("@TradeDate" ,            SqlDbType.VarChar  ){ Value = a.TradeDate },
                new SqlParameter("@Browser" ,            SqlDbType.VarChar  ){ Value = a.Browser },
                new SqlParameter("@UserName" ,            SqlDbType.VarChar  ){ Value = a.UserName },
                new SqlParameter("@Url" ,            SqlDbType.VarChar  ){ Value = a.Url },
                new SqlParameter("@Page" ,            SqlDbType.VarChar  ){ Value = a.Page },
                new SqlParameter("@Component" ,            SqlDbType.VarChar  ){ Value = a.Component },
                new SqlParameter("@ComponentItem" ,            SqlDbType.VarChar  ){ Value = a.ComponentItem },
                new SqlParameter("@Action" ,            SqlDbType.VarChar  ){ Value = a.Action },
                new SqlParameter("@ActionDescribe" ,            SqlDbType.VarChar  ){ Value = a.ActionDescribe },
                new SqlParameter("@HttpAgent" ,            SqlDbType.VarChar  ){ Value = a.HttpAgent },
                new SqlParameter("@Remark1" ,            SqlDbType.VarChar  ){ Value = a.Remark1 },
                new SqlParameter("@Remark2" ,            SqlDbType.VarChar  ){ Value = a.Remark2 }
            };
            return SqlHelper.ExecuteCommandIM(sql, parameters) > 0;
        }
        public static bool Update(Q_Log a)
        {
            string sql = "update Q_Log set TradeDate = @TradeDate,Browser = @Browser,UserName = @UserName,Url = @Url,Page = @Page,Component = @Component,ComponentItem = @ComponentItem,Action = @Action,ActionDescribe = @ActionDescribe,HttpAgent = @HttpAgent,Remark1 = @Remark1,Remark2 = @Remark2 where Id = @Id";
            SqlParameter[] parameters = {
                new SqlParameter("@Id" ,            SqlDbType.Int ,      8){ Value = a.Id },
                new SqlParameter("@TradeDate" ,            SqlDbType.VarChar  ){ Value = a.TradeDate },
                new SqlParameter("@Browser" ,            SqlDbType.VarChar  ){ Value = a.Browser },
                new SqlParameter("@UserName" ,            SqlDbType.VarChar  ){ Value = a.UserName },
                new SqlParameter("@Url" ,            SqlDbType.VarChar  ){ Value = a.Url },
                new SqlParameter("@Page" ,            SqlDbType.VarChar  ){ Value = a.Page },
                new SqlParameter("@Component" ,            SqlDbType.VarChar  ){ Value = a.Component },
                new SqlParameter("@ComponentItem" ,            SqlDbType.VarChar  ){ Value = a.ComponentItem },
                new SqlParameter("@Action" ,            SqlDbType.VarChar  ){ Value = a.Action },
                new SqlParameter("@ActionDescribe" ,            SqlDbType.VarChar  ){ Value = a.ActionDescribe },
                new SqlParameter("@HttpAgent" ,            SqlDbType.VarChar  ){ Value = a.HttpAgent },
                new SqlParameter("@Remark1" ,            SqlDbType.VarChar  ){ Value = a.Remark1 },
                new SqlParameter("@Remark2" ,            SqlDbType.VarChar  ){ Value = a.Remark2 }
            };
            return SqlHelper.ExecuteCommandIM(sql, parameters) > 0;
        }

        public static bool Delete(int id)
        {
            string sql = string.Format("delete from Q_Log where " +
                "Id = {0}", id);
            return SqlHelper.ExecuteCommandIM(sql) > 0;
        }
        #endregion
    }
}











