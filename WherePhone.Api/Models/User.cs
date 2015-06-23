using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WherePhone.Api.Models
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public string Room { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public string Phone { get; set; }

        [JsonIgnore]
        public string Fio
        {
            get
            {
                var str = new StringBuilder(FirstName);
                str.Append(" ");
                str.Append(MiddleName);
                str.Append(" ");
                str.Append(LastName);
                return str.ToString();
            }
        }
    }
}
