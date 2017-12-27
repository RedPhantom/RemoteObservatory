using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteObservatory.Models.Astronomy
{
    public class FileModel
    {

        [Key]
        public int ID { get; set; }

        /// <summary>
        /// How to measure the gain for capture
        /// </summary>
        public enum SensetivityMethods
        {
            ISO,
            Gain
        }

        [Required]
        [Display(Description = "Select a method of digital sensetivity measurement for the image.", Name = "Sensetivity Method", Prompt = "Select a method.")]
        public SensetivityMethods SensetivityMethod { get; set; }
        //public IEnumerable<SensetivityMethods> SensetivityMethod { get; set; }

        /// <summary>
        /// How to capture color from the telescope camera.
        /// </summary>
        public enum ColorMethods
        {
            [Display(Name = "Gray-Scale")]
            GrayScale,
            [Display(Name = "RGB")]
            RGB,
            [Display(Name = "Filter")]
            Filter
        }

        [Required]
        /// <summary>
        /// Value of the gain or digital sensetivity of the capture.
        /// </summary>
        public int SensetivityValue { get; set; }

        [Required]
        public ColorMethods ColorMethod { get; set; }
        //public IEnumerable<ColorMethods> ColorMethod { get; set; }

        [Required]
        /// <summary>
        /// The vertical resolution of the image (in pixels).
        /// </summary>
        public int VerticalResolution { get; set; }

        [Required]
        /// <summary>
        /// The horizontal resolution of the image (in pixels).
        /// </summary>
        public int HorizontalResolution { get; set; }

        [Required]
        /// <summary>
        /// The offset from the top.
        /// </summary>
        public int VerticalOffset { get; set; }

        [Required]
        /// <summary>
        /// The offset from the left.
        /// </summary>
        public int HorizontalOffset { get; set; }

        [Required]
        /// <summary>
        /// Exposure time (in seconds).
        /// </summary>
        public int ExposureTime { get; set; }

        [Required]
        /// <summary>
        /// Frame rate for the capture (in frames per second).
        /// </summary>
        public float FrameRate { get; set; }

        public string OwnerID { get; set; }

        /// <summary>
        /// Returns the representation of the given capture file in JSON.
        /// </summary>
        /// <returns>A JSON string.</returns>
        public string ConvertToJson
        {
            get
            {
                return JsonConvert.SerializeObject(this);
            }
        }
    }
}
