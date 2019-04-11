using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandAlone.Models
{
    class CaptureOrders
    {
        public enum CaptureModes
        {
            Image,
            Video
        }

        public CaptureModes Mode { get; set; }

        public List<Frame> Frames { get; set; }

        /// <summary>
        /// Exposure time in milliseconds.
        /// </summary>
        public double ExposureTime { get; set; }

        public int Gain { get; set; }
    }
}
