using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Class_Project.Models {
    public class CaptchaResponse {
            [JsonProperty("success")]
            public bool Success {
                get;
                set;
            }
            [JsonProperty("error-codes")]
            public List<string> ErrorMessage {
                get;
                set;
            }
        }
    }