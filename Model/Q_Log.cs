using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Q_Log
    {
        public int Id { get; set; }
        public string TradeDate { get; set; }
        public string Browser { get; set; }
        public string UserName { get; set; }
        public string Url { get; set; }
        public string Page { get; set; }
        public string Component { get; set; }
        public string ComponentItem { get; set; }
        public string Action { get; set; }
        public string ActionDescribe { get; set; }
        public string HttpAgent { get; set; }
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }
    }
}

