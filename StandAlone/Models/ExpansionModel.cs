using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandAlone.Models
{
    /// <summary>
    /// Contains all information required for an observation task.
    /// </summary>
    

    

    

    
    struct RgbPixel
    {
        public int V1;
        public int V2;
        public int V3;

        public RgbPixel(int V1, int V2, int V3)
        {
            this.V1 = V1;
            this.V2 = V2;
            this.V3 = V3;
        }
    }

    struct McPixel
    {
        public int V;

        public McPixel(int V)
        {
            this.V = V;
        }
    }

    class Frame
    {
        public Size Dimensions { get; set; }
        public int BitDepth { get; set; }
        public bool IsColor { get; set; }

        public McPixel[,] MonochomeData;
        public RgbPixel[,] RgbData;

        public DateTimeOffset CaptureTime { get; set; }

        public enum FlipModes
        {
            None,
            Vertical,
            Horizontal,
            Both
        }

        public FlipModes FlipMode { get; set; }
    }
}
