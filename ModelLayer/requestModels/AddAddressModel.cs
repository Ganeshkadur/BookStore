using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.requestModels
{
    public class AddAddressModel
    {
        public long userId { get; set; }
        public string fullAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int type { get; set; }
    }
}
