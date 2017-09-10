﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSMS.Class
{
    class StructInvocieDetail
    {
        public int InvoiceNo { get; set; }
        public int PID { get; set; }
        public int Qty { get; set; }
        public float SalePrice { get; set; }
        public double Amount {
            get
            {
                return Qty * SalePrice;
            }
        }
    }
}
