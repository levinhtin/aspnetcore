﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Data.Entities
{
    public class Tags : AbstractModel
    {
        /// <summary>
        /// Tags Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tags Description
        /// </summary>
        public string Description { get; set; }
    }
}
