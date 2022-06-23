using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Listek.Models
{
    public class ToBuy
    {
        public int Id { get; set; }

        public string Izdelek { get; set; }

        public bool Nakup { get; set; } 

        public virtual  ApplicationUser User { get; set; }
    }
}