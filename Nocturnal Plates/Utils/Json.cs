using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nocturnal.Utils
{
    internal class Json
    {
        [Serializable]
        public class PayLoad
        {
            public int Code { get; set; }
            public string PasswordCode { get; set; }
            public string UserId { get; set; }
            public Tags UserTags { get; set; }
            public WebReq WebReq { get; set; }
        }

        public class WebReq
        {
            public string Username { get; set; }
            public string AccessKey { get; set; }
        }

        public class Tags
        {
            public string[] TagArr { get; set; }
        }

        public class RecivedTag
        {

        }

    }
}
