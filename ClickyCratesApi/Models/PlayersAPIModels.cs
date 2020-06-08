using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClickyCratesApi.Models
{
    public class PlayersAPIModels
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDay {get;set;}
        public string NickName { get; set; }
        public string City { get; set; }
        public bool IsOnline { get; set; }
        public string LastLogin { get; set; }
        public string HourGameScene { get; set; }
        public bool IsBanned { get; set; }
        public string BannedHour { get; set; }

    }
}