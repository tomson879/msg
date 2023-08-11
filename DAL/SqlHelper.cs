using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{
    public class SqlHelper
    {
        private static readonly string connStrIM =
            ConfigurationManager.ConnectionStrings["Trade"].ToString();

        #region 分页
        public static string StrPageDesc = ",row_number() over(order by id desc) rownum";
        public static string StrPageAsc = ",row_number() over(order by id) rownum";
        public static string StrPage1(string IdName)
        {
            return ",row_number() over(order by " + IdName + ") rownum";
        }
        public static string IdAsc()
        {
            return ",row_number() over(order by Id Asc ) rownum";
        }
        public static string StrPage2(string sql, int PageSize, int CurrentPageIndex)
        {
            return "select * from (" + sql + ") t where t.rownum>" + (CurrentPageIndex - 1) * PageSize + " and t.rownum <=" + CurrentPageIndex * PageSize;
        }
        public static int getRecordCount(string SheetName, string where)
        {
            int Num = 0;
            string sql = "select count(1) from " + SheetName + "";
            sql = (where != "" ? sql + " where " + where : sql);
            using (SqlDataReader sr = SqlHelper.GetDataReaderIM(sql)) { while (sr.Read()) { Num = (int)sr[0]; } }
            return Num;
        }
        public static string getSql(string sql, string where, int PageSize, int CurrentPageIndex)
        {
            sql = (where != "" ? sql + " where " + where : sql);
            sql = (PageSize != 0 && CurrentPageIndex != 0 ? SqlHelper.StrPage2(sql, PageSize, CurrentPageIndex) : sql);
            return sql;
        }
        #endregion

        #region command (Insert Update Delete)
        public static int ExecuteCommandIM(string sql)
        {
            using (SqlConnection conn = new SqlConnection(connStrIM))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.CommandTimeout = 720;
                int i = comm.ExecuteNonQuery();
                comm.Parameters.Clear();
                conn.Close();
                return i;
            }
        }
        public static int ExecuteCommandIM(string cmdText, SqlParameter[] commandParm)
        {
            SqlConnection conn = new SqlConnection(connStrIM);
            conn.Open();
            SqlCommand comm = new SqlCommand(cmdText, conn);
            foreach (SqlParameter p in commandParm)
            {
                comm.Parameters.Add(p);
            }
            int rowCount = 0;
            try
            {
                rowCount = comm.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                conn.Close();
            }
            return rowCount;
        }
        #endregion

        #region SqlDataReader 查询
        public static SqlDataReader GetDataReaderIM(string sql)
        {
            SqlCommand comm = new SqlCommand();
            SqlConnection conn = new SqlConnection(connStrIM);
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                comm.Connection = conn;
                comm.CommandText = sql;
                SqlDataReader reader = comm.ExecuteReader(CommandBehavior.CloseConnection);
                comm.Parameters.Clear();
                return reader;
            }
            catch (Exception)
            {
                conn.Close();
                throw;
            }
        }

        public static SqlDataReader GetDataReaderIM(string commandText, SqlParameter[] commandParm)
        {
            SqlConnection conn = new SqlConnection(connStrIM);
            SqlCommand comm = new SqlCommand(commandText, conn);
            if (commandParm != null)
            {
                foreach (SqlParameter p in commandParm)
                {
                    comm.Parameters.Add(p);
                }
            }
            try
            {
                conn.Open();
                return comm.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
                conn.Close();
                throw;
            }
        }

        #endregion

        //#region 原始sql语句执行command
        //public static int ExecuteCommand(string sql)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlCommand comm = new SqlCommand(sql, conn);
        //        int i = comm.ExecuteNonQuery();
        //        conn.Close();
        //        return i;
        //    }
        //}
        //#endregion

        //#region 原始sql语句执行SqlDataReader
        //public static SqlDataReader GetDataReader(string sql)
        //{
        //    SqlConnection conn = new SqlConnection(connectionString);
        //    SqlCommand comm = new SqlCommand(sql, conn);
        //    conn.Open();
        //    return comm.ExecuteReader(CommandBehavior.CloseConnection);
        //}
        //#endregion

        //#region 不带参的存储过程执行command
        //public static int ExecuteCommand(string sql, CommandType type)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlCommand comm = new SqlCommand(sql, conn);
        //        comm.CommandType = type;
        //        int i = comm.ExecuteNonQuery();
        //        conn.Close();
        //        return i;
        //    }
        //}
        //#endregion

        //#region 带参的存储过程执行command
        //public static int ExecuteCommand(string sql, CommandType type, params SqlParameter[] p)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlCommand comm = new SqlCommand(sql, conn);
        //        comm.CommandType = type;
        //        comm.Parameters.AddRange(p);
        //        int i = comm.ExecuteNonQuery();
        //        conn.Close();
        //        return i;
        //    }
        //}
        //#endregion

        //#region 不带参的存储过程执行SqlDataReader
        //public static SqlDataReader GetDataReader(string sql, CommandType type)
        //{
        //    SqlConnection conn = new SqlConnection(connectionString);
        //    SqlCommand comm = new SqlCommand(sql, conn);
        //    comm.CommandType = type;
        //    conn.Open();
        //    return comm.ExecuteReader(CommandBehavior.CloseConnection);
        //}
        //#endregion

        //#region 带参的存储过程执行SqlDataReader
        //public static SqlDataReader GetDataReader(string sql, CommandType type, params SqlParameter[] p)
        //{
        //    SqlConnection conn = new SqlConnection(connectionString);
        //    SqlCommand comm = new SqlCommand(sql, conn);
        //    comm.CommandType = type;
        //    comm.Parameters.AddRange(p);
        //    conn.Open();
        //    return comm.ExecuteReader(CommandBehavior.CloseConnection);
        //}
        //#endregion
    }
}
