using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceApp.DTOs
{
    public class ItemDto
    {
        public System.Guid ItemId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}