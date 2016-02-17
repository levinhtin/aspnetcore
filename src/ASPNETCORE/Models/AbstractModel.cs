using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCORE.Models
{
    public abstract class AbstractModel
    {
        /// <summary>
        /// Id Post
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// UserId Created Post
        /// </summary>
        public string CreatedUserId { get; set; }

        /// <summary>
        /// Post created at time
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// LastModify by UserId
        /// </summary>
        public string ModifyUserId { get; set; }

        /// <summary>
        /// Last modify at time
        /// </summary>
        public DateTime ModifyTime { get; set; }
    }
}
