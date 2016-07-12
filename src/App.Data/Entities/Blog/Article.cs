using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Data.Entities.Blog
{
    public class Article : AbstractModel
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(200)]
        public string Summary { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Tags> Tags { get; set; }

    }
}
