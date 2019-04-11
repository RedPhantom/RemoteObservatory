using StandAlone.Models;
using System.Runtime.Serialization;


namespace StandAlone
{
    [DataContract]
    public class Camera
    {
        [DataMember]
        public Size Resolution { get; set; }

        /// <summary>
        /// Calculates the camera's resolution in mega-pixels.
        /// </summary>
        /// <returns>A resolution value in millions of pixels.</returns>
        public double GetResolutionMP()
        {
            return Resolution.Width * Resolution.Height / 1000000.0;
        }

        [DataMember]
        public int UID { get; set; }

        [DataMember]
        public string Port { get; set; }

        [DataMember]
        public int GainMin { get; set; }
        [DataMember]
        public int GainMax { get; set; }

        [DataMember]
        public double ExposureMin { get; set; } // ms
        [DataMember]
        public double ExposureMax { get; set; } // ms

        public enum BayerFormats
        {

        }

        public BayerFormats BayerFormat { get; set; }
    }
}