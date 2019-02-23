using StandAlone.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandAlone
{
    public static class TeleController
    {
        public static TelescopeDictionary.MeadeLX200_16GPS Telescope { get; set; }
        public static SerialHelper Dome { get; set; }
        
    }
}
