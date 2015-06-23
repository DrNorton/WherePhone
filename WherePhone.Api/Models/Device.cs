using System;
using Newtonsoft.Json;

namespace WherePhone.Api.Models
{
    public class Device
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public string Platform { get; set; }
        public string ImageUrl { get; set; }

        public User CurrentUser { get; set; }
        public DateTime BorrowTime { get; set; }
        
    }
}
