using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Role_User
    {
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public bool State { get; set; }
        public string Email { get; set; }
        public int DepaId { get; set; }
        public string CRM_IsLogin { get; set; }
        public string CRM_IsManager { get; set; }
    }
}
