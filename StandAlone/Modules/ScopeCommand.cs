using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandAlone.Modules
{
    /// <summary>
    /// Connects between the SerialQueue and TelescopeDictionary class.
    /// </summary>
    class ScopeCommand
    {
        public enum CommandType
        {
            GetAlignmentMode,
            SetAlignmentMode,
            SyncWithCurrentObject,
            SetFocuserSpeedAndDirection,
            SetGPSStatus,
            GoToMessier,
            GoToDeepSky,
            GoTo
        }

        public object CommandValue { get; set; }
    }
}
