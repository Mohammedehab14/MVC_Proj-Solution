﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Proj.Entities
{
	public class Email 
	{
        public string Recipient { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }
}
