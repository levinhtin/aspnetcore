using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCORE.Models
{
    public class Article : AbstractModel
    {
        public string Title { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Tags> Tags { get; set; }

    }
}
