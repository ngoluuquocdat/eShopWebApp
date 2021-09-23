using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Utilities.Constants
{
    public class SystemConstants    // class này để lưu các constants của dự án
    {
        // tên chuỗi kết nối trong file appsetting json
        public const string MainConnectionString = "eShopSolutionDb";   

        public class AppSettings
        {
            public const string Token = "Token";
            public const string DefaultLanguageId = "DefaultLanguageId";
        }
    }
}
