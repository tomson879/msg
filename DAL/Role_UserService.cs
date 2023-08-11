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
    public class Role_UserService
    {

        public static bool IsExist(string LoginName, string LoginPwd)
        {
            SqlParameter[] parameters =
            {
               new SqlParameter("@LoginName" , SqlDbType.VarChar , 20){ Value = LoginName },
            };
            Role_User role_User = getOne(" LoginName= @LoginName", parameters);
            string Pwd = (role_User != null ? Pwd = role_User.LoginPwd : "");
            return (Pwd == LoginPwd && Pwd != "" ? true : false);
        }
        public static Role_User getOne(string where, SqlParameter[] parameters)
        {
            Role_User one = null;
            string sql = "select *  from Role_User";
            sql = (where != "" ? sql + " where " + where : sql);
            using (SqlDataReader sr = SqlHelper.GetDataReaderIM(sql, parameters)) { while (sr.Read()) { one = getsr(sr); } }
            return one;
        }
        public static Role_User getOneByLoginName(string LoginName)
        {
            SqlParameter[] parameters = { new SqlParameter("@LoginName", SqlDbType.VarChar, 20) { Value = LoginName } };
            return getOne("  LoginName = @LoginName", parameters);
        }

        #region 基础维护

        public static Role_User getOneById(int id)
        {
            return getOne("  Id = " + id);
        }

        public static bool IsExist(string Name)
        {
            return (getOne(" Name = '" + Name + "'") != null ? true : false);
        }

        public static List<Role_User> getlist(string where, int PageSize, int CurrentPageIndex)
        {
            List<Role_User> list = new List<Role_User>();
            string sql = "select * " + SqlHelper.IdAsc() + " from Role_User";
            sql = SqlHelper.getSql(sql, where, PageSize, CurrentPageIndex);
            using (SqlDataReader sr = SqlHelper.GetDataReaderIM(sql)) { while (sr.Read()) { list.Add(getsr(sr)); } }
            return list;
        }

        public static Role_User getOne(string where)
        {
            Role_User one = null;
            string sql = "select *  from Role_User";
            sql = (where != "" ? sql + " where " + where : sql);
            using (SqlDataReader sr = SqlHelper.GetDataReaderIM(sql)) { while (sr.Read()) { one = getsr(sr); } }
            return one;
        }

        private static Role_User getsr(SqlDataReader sr)
        {
            Role_User a = new Role_User();
            a.UserId = (int)sr["UserId"];
            a.LoginName = (string)sr["LoginName"];
            a.LoginPwd = (string)sr["LoginPwd"];
            a.UserName = (string)sr["UserName"];
            a.RoleId = (int)sr["RoleId"];
            a.State = (bool)sr["State"];
            a.Email = (string)sr["Email"];
            a.DepaId = (int)sr["DepaId"];
            a.CRM_IsLogin = (string)sr["CRM_IsLogin"];
            a.CRM_IsManager = (string)sr["CRM_IsManager"];
            return a;
        }
        public static bool Insert(Role_User a)
        {
            string sql = "insert into Role_User values ( @LoginName,@LoginPwd,@UserName,@RoleId,@State,@Email,@DepaId,@CRM_IsLogin,@CRM_IsManager)";
            SqlParameter[] parameters = {
                new SqlParameter("@LoginName" ,            SqlDbType.VarChar  ){ Value = a.LoginName },
                new SqlParameter("@LoginPwd" ,            SqlDbType.VarChar  ){ Value = a.LoginPwd },
                new SqlParameter("@UserName" ,            SqlDbType.VarChar  ){ Value = a.UserName },
                new SqlParameter("@RoleId" ,            SqlDbType.Int ,      8){ Value = a.RoleId },
                new SqlParameter("@State" ,            SqlDbType.Bit  ){ Value = a.State },
                new SqlParameter("@Email" ,            SqlDbType.VarChar  ){ Value = a.Email },
                new SqlParameter("@DepaId" ,            SqlDbType.Int ,      8){ Value = a.DepaId },
                new SqlParameter("@CRM_IsLogin" ,            SqlDbType.VarChar  ){ Value = a.CRM_IsLogin },
                new SqlParameter("@CRM_IsManager" ,            SqlDbType.VarChar  ){ Value = a.CRM_IsManager }
            };
            return SqlHelper.ExecuteCommandIM(sql, parameters) > 0;
        }
        public static bool Update(Role_User a)
        {
            string sql = "update Role_User set LoginName = @LoginName,LoginPwd = @LoginPwd,UserName = @UserName,RoleId = @RoleId,State = @State,Email = @Email,DepaId = @DepaId,CRM_IsLogin = @CRM_IsLogin,CRM_IsManager = @CRM_IsManager where Id = @Id";
            SqlParameter[] parameters = {
                new SqlParameter("@UserId" ,            SqlDbType.Int ,      8){ Value = a.UserId },
                new SqlParameter("@LoginName" ,            SqlDbType.VarChar  ){ Value = a.LoginName },
                new SqlParameter("@LoginPwd" ,            SqlDbType.VarChar  ){ Value = a.LoginPwd },
                new SqlParameter("@UserName" ,            SqlDbType.VarChar  ){ Value = a.UserName },
                new SqlParameter("@RoleId" ,            SqlDbType.Int ,      8){ Value = a.RoleId },
                new SqlParameter("@State" ,            SqlDbType.Bit  ){ Value = a.State },
                new SqlParameter("@Email" ,            SqlDbType.VarChar  ){ Value = a.Email },
                new SqlParameter("@DepaId" ,            SqlDbType.Int ,      8){ Value = a.DepaId },
                new SqlParameter("@CRM_IsLogin" ,            SqlDbType.VarChar  ){ Value = a.CRM_IsLogin },
                new SqlParameter("@CRM_IsManager" ,            SqlDbType.VarChar  ){ Value = a.CRM_IsManager }
            };
            return SqlHelper.ExecuteCommandIM(sql, parameters) > 0;
        }

        public static bool Delete(int id)
        {
            string sql = string.Format("delete from Role_User where " +
                "Id = {0}", id);
            return SqlHelper.ExecuteCommandIM(sql) > 0;
        }
        #endregion
    }
}
