using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandAlone.Models
{
    class ObservationTask
    {
        public int RecordID { get; set; }

        public int ScopeID { get; set; }

        public enum TaskStates
        {
            Pending,
            Executing,
            Complete,
            Failed
        }

        public TaskStates State { get; set; }

        public string StateDescription { get; set; }

        public CaptureSettings Settings { get; set; }


        // metadata
        public DateTimeOffset CreationTime { get; set; }
        public DateTimeOffset EditTime { get; set; }

        public int OwnerID { get; set; }
    }
}
