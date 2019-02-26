using StandAlone.Modules;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandAlone.TelescopeDictionary
{
    interface IScope
    {
        /// <summary>
        /// The model display name.
        /// </summary>
        string ModelName { get; set; }

        // Serial settings:
        Parity Parity { get; set; }
        int DataBits { get; set; }
        StopBits StopBits { get; set; }

        Encoding Encoding { get; set; }

        // Serial helpers:
        /// <summary>
        /// The serial helper that handles all communication with the scope.
        /// </summary>
        SerialHelper _scope { get; set; }

        bool hasDome { get; set; }
        SerialHelper _dome { get; set; }
    }
}
