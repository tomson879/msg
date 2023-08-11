using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Q_FlowInvest_Msg
    {
        public int Id { get; set; }
        public string MsgType { get; set; }
        public string FlowInvestGuid { get; set; }
        public string Creator { get; set; }
        public string TradeDate { get; set; }
        public string Mac { get; set; }
        public string Msg { get; set; }
    }
}
