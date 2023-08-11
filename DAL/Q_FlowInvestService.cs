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
    public class Q_FlowInvestService
    {
        public static List<Q_FlowInvest> getlistTopX(int x)
        {
            List<Q_FlowInvest> list = new List<Q_FlowInvest>();
            string sql = "select top " + x +" *  from Q_FlowInvest order by Id desc";
            using (SqlDataReader sr = SqlHelper.GetDataReaderIM(sql)) { while (sr.Read()) { list.Add(getsr(sr)); } }
            return list;
        }

        #region 基础维护

        public static Q_FlowInvest getOneById(int id)
        {
            return getOne("  Id = " + id);
        }

        public static bool IsExist(string Name)
        {
            return (getOne(" Name = '" + Name + "'") != null ? true : false);
        }

        public static List<Q_FlowInvest> getlist(string where, int PageSize, int CurrentPageIndex)
        {
            List<Q_FlowInvest> list = new List<Q_FlowInvest>();
            string sql = "select * " + SqlHelper.StrPage1(" Id desc ") + " from Q_FlowInvest";
            sql = SqlHelper.getSql(sql, where, PageSize, CurrentPageIndex);
            using (SqlDataReader sr = SqlHelper.GetDataReaderIM(sql)) { while (sr.Read()) { list.Add(getsr(sr)); } }
            return list;
        }

        public static Q_FlowInvest getOne(string where)
        {
            Q_FlowInvest one = null;
            string sql = "select *  from Q_FlowInvest";
            sql = (where != "" ? sql + " where " + where : sql);
            using (SqlDataReader sr = SqlHelper.GetDataReaderIM(sql)) { while (sr.Read()) { one = getsr(sr); } }
            return one;
        }

        private static Q_FlowInvest getsr(SqlDataReader sr)
        {
            Q_FlowInvest a = new Q_FlowInvest();
            a.Id = (int)sr["Id"];
            a.Guid = (string)sr["Guid"];
            a.FundId = (int)sr["FundId"];
            a.FundMgId = (int)sr["FundMgId"];
            a.AccountId = (int)sr["AccountId"];
            a.AccountMgId = (int)sr["AccountMgId"];
            a.StockCode = (string)sr["StockCode"];
            a.StockName = (string)sr["StockName"];
            a.TradeType = (string)sr["TradeType"];
            a.PositionDescribe = (string)sr["PositionDescribe"];
            a.Amount = (string)sr["Amount"];
            a.Volume = (decimal)sr["Volume"];
            a.PriceDescribe = (string)sr["PriceDescribe"];
            a.Reason = (string)sr["Reason"];
            a.State = (string)sr["State"];
            a.Investor = (string)sr["Investor"];
            a.TradeDate = (string)sr["TradeDate"];
            a.Remark = (string)sr["Remark"];
            return a;
        }


        public static bool Delete(int id)
        {
            string sql = string.Format("delete from Q_FlowInvest where " +
                "Id = {0}", id);
            return SqlHelper.ExecuteCommandIM(sql) > 0;
        }
        #endregion
    }
}
