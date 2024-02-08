using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.requestModels
{
    public class QuantityUpdateRequest
    {

        public int quantity { get; set; }
        public long bookId { get; set; }
    }
}
