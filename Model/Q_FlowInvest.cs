using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Q_FlowInvest
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public int FundId { get; set; }
        public int FundMgId { get; set; }
        public int AccountId { get; set; }
        public int AccountMgId { get; set; }
        public string StockCode { get; set; }
        public string StockName { get; set; }
        public string TradeType { get; set; }
        public string PositionDescribe { get; set; }
        public string Amount { get; set; }
        public decimal Volume { get; set; }
        public string PriceDescribe { get; set; }
        public string Reason { get; set; }
        public string State { get; set; }
        public string Investor { get; set; }
        public string TradeDate { get; set; }
        public string Remark { get; set; }
    }
}

