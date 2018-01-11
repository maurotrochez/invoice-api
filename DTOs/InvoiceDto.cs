using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceApp.DTOs
{
    public class InvoiceDto
    {
        public System.Guid InvoiceId { get; set; }
        public string Consecutive { get; set; }
        public decimal Total { get; set; }
        public System.Guid ItemId { get; set; }
        public string ItemName { get; set; }
    }
}