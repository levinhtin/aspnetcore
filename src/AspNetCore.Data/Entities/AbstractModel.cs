using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class AbstractModel
    {
        /// <summary>
        /// 
        /// </summary>
        public AbstractModel()
        {
            var now = DateTime.Now;
            this.CreatedOn = now;
            this.ModifiedOn = now;
        }

        /// <summary>
        /// Id Post
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// UserId Created Post
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Post created at time
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// LastModify by UserId
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Last modify at time
        /// </summary>
        public DateTime ModifiedOn { get; set; }
    }
}
