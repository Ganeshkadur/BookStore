﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.responseModel
{
    public class ResponseModel<T>
    {
        public bool Success { get; set; }
        public string Message {  get; set; }

        public T Data { get; set; }
    }
}
