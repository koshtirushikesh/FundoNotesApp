﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommanLayer
{
    public class ForgotPassWordModel
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

    }
}
