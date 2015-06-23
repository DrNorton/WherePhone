using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WherePhone.Api.Models
{
    public class AddBorrow
    {
        public string DeviceId { get; set; }

        public long UserId { get; set; }
        public string Description { get; set; }
    }
}
