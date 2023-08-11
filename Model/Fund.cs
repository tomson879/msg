using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    [Serializable]
    public class Fund
    {
        int fundId;

        public int FundId
        {
            get { return fundId; }
            set { fundId = value; }
        }
        string fundName;

        public string FundName
        {
            get { return fundName; }
            set { fundName = value; }
        }

        string baseCurrency;

        public string BaseCurrency
        {
            get { return baseCurrency; }
            set { baseCurrency = value; }
        }

        decimal aBalance;

        public decimal ABalance
        {
            get { return aBalance; }
            set { aBalance = value; }
        }

        decimal hBalance;

        public decimal HBalance
        {
            get { return hBalance; }
            set { hBalance = value; }
        }

        decimal tBalance;

        public decimal TBalance
        {
            get { return tBalance; }
            set { tBalance = value; }
        }

        bool state;

        public bool State
        {
            get { return state; }
            set { state = value; }
        }

        int sortNum;

        public int SortNum
        {
            get { return sortNum; }
            set { sortNum = value; }
        }

        string closeDate;
        public string CloseDate
        {
            get { return closeDate; }
            set { closeDate = value; }
        }

    }
}
