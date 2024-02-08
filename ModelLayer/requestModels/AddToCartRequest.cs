using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.requestModels
{
    public  class AddToCartRequest
    {
        public int bookId { get; set; }
        public int quantity { get; set; }
    }
}
