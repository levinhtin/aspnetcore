using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.ViewModels.Account
{
    public class AuthenticatedViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public bool authenticated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int entityId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? tokenExpires { get; set; }
    }
}
