using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;
using System.Web.Script.Serialization;

namespace MVCBlog.Account
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public string Role { get; set; }
        public string Login { get; set; }

        public UserInfo(User user)
        {
            UserId = user.Id;
            Role = user.Type.ToString();
            Login = user.UserName;
        }

        public UserInfo() { }

        public override string ToString()
        {
            var s = new JavaScriptSerializer();
            return s.Serialize(this);
        }

        public static UserInfo FromString(string info)
        {
            var s = new JavaScriptSerializer();
            UserInfo uInfo = s.Deserialize<UserInfo>(info);
            return uInfo;
        }
    }
}