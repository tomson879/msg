using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class AccountMg
    {
        public int AccountMgId { get; set; }
        public string AccountMgName { get; set; }
        public string Currency { get; set; }
        public int FundId { get; set; }
        public int FundMgId { get; set; }
        public string Manager { get; set; }
        public decimal Balance { get; set; }
        public bool State { get; set; }
        public int SortNum { get; set; }
    }
}

