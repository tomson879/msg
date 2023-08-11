using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Model;

namespace DAL
{
    public class FundService
    {

        public static string getName(int FundId)
        {
            Fund a = getOneById(FundId);
            return (a != null ? a.FundName : "");
        }



        #region 获取基金信息
        public static Fund getOneById(int id)
        {
            return getOne("  FundId = " + id);
        }

        public static bool IsExist(string FundName)
        {
            return (getOne(" FundName = '" + FundName + "'") != null ? true : false);
        }

        public static List<Fund> getlist(string where, int PageSize, int CurrentPageIndex)
        {
            List<Fund> list = new List<Fund>();
            string sql = "select * " + SqlHelper.StrPage1("SortNum,FundId") + " from Fund";
            sql = SqlHelper.getSql(sql, where, PageSize, CurrentPageIndex);
            using (SqlDataReader sr = SqlHelper.GetDataReaderIM(sql)) { while (sr.Read()) { list.Add(getsr(sr)); } }
            return list;
        }
        public static Fund getOne(string where)
        {
            Fund one = null;
            string sql = "select *  from Fund";
            sql = (where != "" ? sql + " where " + where : sql);
            using (SqlDataReader sr = SqlHelper.GetDataReaderIM(sql)) { while (sr.Read()) { one = getsr(sr); } }
            return one;
        }
        private static Fund getsr(SqlDataReader sr)
        {
            Fund a = new Fund();
            a.FundId = (int)sr["FundId"];
            a.FundName = (string)sr["FundName"];
            a.BaseCurrency = (string)sr["BaseCurrency"];
            a.ABalance = (decimal)sr["ABalance"];
            a.HBalance = (decimal)sr["ABalance"];
            a.TBalance = (decimal)sr["ABalance"];
            a.State = (bool)sr["State"];
            a.SortNum = (int)sr["SortNum"];
            a.CloseDate = (string)sr["CloseDate"];
            return a;
        }
        #endregion


    }
}
