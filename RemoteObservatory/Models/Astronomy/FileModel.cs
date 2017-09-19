using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteObservatory.Models.Astronomy
{
    public class FileModel
    {
        /// <summary>
        /// How to measure the gain for capture
        /// </summary>
        enum SensetivityMethods
        {
            ISO,
            Gain
        }

        /// <summary>
        /// How to capture color from the telescope camera.
        /// </summary>
        enum ColorMethod
        {
            BlackAndWhite,
            RGB,
            FalseColor
        }
        /// <summary>
        /// The vertical resolution of the image (in pixels).
        /// </summary>
        int VerticalResolution;

        /// <summary>
        /// The horizontal resolution of the image (in pixels).
        /// </summary>
        int HorizontalResolution;

        /// <summary>
        /// The offset from the top.
        /// </summary>
        int VerticalOffset;

        /// <summary>
        /// The offset from the left.
        /// </summary>
        int HorizontalOffser;

        /// <summary>
        /// Exposure time (in seconds).
        /// </summary>
        int Exposure;

        /// <summary>
        /// Frame rate for the capture (in frames per second).
        /// </summary>
        float FrameRate;
    }
}
