using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.requestModels
{
    public class AddOrderRequest
    {
        public long bookId { get; set; }
        public int quantity { get; set; }
    }
}
