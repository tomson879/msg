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
    public class AccountMgService
    {

        public static string getManager_ByFundMgId(int FundMgId)
        {
            string Manager = "";
            string sql = "select top 1 Manager from accountmg where FundMgId = " + FundMgId + "";
            using (SqlDataReader sr = SqlHelper.GetDataReaderIM(sql)) { while (sr.Read()) { Manager = (string)sr[0]; } }
            return Manager;
        }

        #region 基础维护

        public static AccountMg getOneById(int id)
        {
            return getOne("  Id = " + id);
        }

        public static bool IsExist(string Name)
        {
            return (getOne(" Name = '" + Name + "'") != null ? true : false);
        }

        public static List<AccountMg> getlist(string where, int PageSize, int CurrentPageIndex)
        {
            List<AccountMg> list = new List<AccountMg>();
            string sql = "select * " + SqlHelper.IdAsc() + " from AccountMg";
            sql = SqlHelper.getSql(sql, where, PageSize, CurrentPageIndex);
            using (SqlDataReader sr = SqlHelper.GetDataReaderIM(sql)) { while (sr.Read()) { list.Add(getsr(sr)); } }
            return list;
        }

        public static AccountMg getOne(string where)
        {
            AccountMg one = null;
            string sql = "select *  from AccountMg";
            sql = (where != "" ? sql + " where " + where : sql);
            using (SqlDataReader sr = SqlHelper.GetDataReaderIM(sql)) { while (sr.Read()) { one = getsr(sr); } }
            return one;
        }

        private static AccountMg getsr(SqlDataReader sr)
        {
            AccountMg a = new AccountMg();
            a.AccountMgId = (int)sr["AccountMgId"];
            a.AccountMgName = (string)sr["AccountMgName"];
            a.Currency = (string)sr["Currency"];
            a.FundId = (int)sr["FundId"];
            a.FundMgId = (int)sr["FundMgId"];
            a.Manager = (string)sr["Manager"];
            a.Balance = (decimal)sr["Balance"];
            a.State = (bool)sr["State"];
            a.SortNum = (int)sr["SortNum"];
            return a;
        }
        public static bool Insert(AccountMg a)
        {
            string sql = "insert into AccountMg values ( @AccountMgName,@Currency,@FundId,@FundMgId,@Manager,@Balance,@State,@SortNum)";
            SqlParameter[] parameters = {
                new SqlParameter("@AccountMgName" ,            SqlDbType.VarChar  ){ Value = a.AccountMgName },
                new SqlParameter("@Currency" ,            SqlDbType.VarChar  ){ Value = a.Currency },
                new SqlParameter("@FundId" ,            SqlDbType.Int ,      8){ Value = a.FundId },
                new SqlParameter("@FundMgId" ,            SqlDbType.Int ,      8){ Value = a.FundMgId },
                new SqlParameter("@Manager" ,            SqlDbType.VarChar  ){ Value = a.Manager },
                new SqlParameter("@Balance" ,            SqlDbType.Decimal , 18){ Value = a.Balance },
                new SqlParameter("@State" ,            SqlDbType.Bit  ){ Value = a.State },
                new SqlParameter("@SortNum" ,            SqlDbType.Int ,      8){ Value = a.SortNum }
            };
            return SqlHelper.ExecuteCommandIM(sql, parameters) > 0;
        }
        public static bool Update(AccountMg a)
        {
            string sql = "update AccountMg set AccountMgName = @AccountMgName,Currency = @Currency,FundId = @FundId,FundMgId = @FundMgId,Manager = @Manager,Balance = @Balance,State = @State,SortNum = @SortNum where Id = @Id";
            SqlParameter[] parameters = {
                new SqlParameter("@AccountMgId" ,            SqlDbType.Int ,      8){ Value = a.AccountMgId },
                new SqlParameter("@AccountMgName" ,            SqlDbType.VarChar  ){ Value = a.AccountMgName },
                new SqlParameter("@Currency" ,            SqlDbType.VarChar  ){ Value = a.Currency },
                new SqlParameter("@FundId" ,            SqlDbType.Int ,      8){ Value = a.FundId },
                new SqlParameter("@FundMgId" ,            SqlDbType.Int ,      8){ Value = a.FundMgId },
                new SqlParameter("@Manager" ,            SqlDbType.VarChar  ){ Value = a.Manager },
                new SqlParameter("@Balance" ,            SqlDbType.Decimal , 18){ Value = a.Balance },
                new SqlParameter("@State" ,            SqlDbType.Bit  ){ Value = a.State },
                new SqlParameter("@SortNum" ,            SqlDbType.Int ,      8){ Value = a.SortNum }
            };
            return SqlHelper.ExecuteCommandIM(sql, parameters) > 0;
        }

        public static bool Delete(int id)
        {
            string sql = string.Format("delete from AccountMg where " +
                "Id = {0}", id);
            return SqlHelper.ExecuteCommandIM(sql) > 0;
        }
        #endregion
    }
}

