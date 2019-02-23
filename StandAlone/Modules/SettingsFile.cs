using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StandAlone.Modules
{
    [DataContract]
    public class SettingsFile
    {
        [DataMember]
        public string TelescopeComPort { get; set; }

        /// <summary>
        /// Whether the smart dome end-unit is installed.
        /// </summary>
        [DataMember]
        public bool UseComputerizedDome { get; set; }

        [DataMember]
        public string DomeComPort { get; set; }

        [DataMember]
        public string TelescopeModel { get; set; }

        [DataMember]
        public string CameraDevice { get; set; }
    }
}
