using StandAlone.Modules;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace StandAlone.Models
{
    // TODO: inherit the ASCOM standard in a clone of this class.

    /// <summary>
    /// One static instance that holds all information about the observatory format.
    /// </summary>
    [DataContract]
    class SettingsModel
    {
        // -- SCOPES --
        [DataMember]
        public List<Scope> Scopes { get; set; }
    }

    [DataContract]
    class Scope
    {
        [DataMember]
        public int UniqueID { get; set; }

        [DataMember]
        public string ScopeModel { get; set; }

        [DataMember]
        public string ComPort { get; set; }

        [DataMember]
        public bool HasDome { get; set; }
        [DataMember]
        public Dome Dome { get; set; } = null;

        [DataMember]
        public bool HasCamera { get; set; }
        [DataMember]
        public Camera Camera { get; set; } = null;
    }

    [DataContract]
    public class Dome
    {
        [DataMember]
        public int UniqueID { get; set; }

        [DataMember]
        public string ComPort { get; set; }

        public bool IsMoving { get; set; }

        public Hatch DomeHatch { get; set; }
        
        public class Hatch
        {
            public enum Directions
            {
                Pos,
                Neg
            }

            // todo: move the moving functionality to the arduino to reduce stress on the server.
            // the hardware end unit will aim for the target azi on its own.
            public void StartMoving(Directions direction, ref SerialHelper serialHelper)
            {
                switch (direction)
                {
                    case Directions.Pos:
                        serialHelper.DoCommand("H[+]\n");

                        break;
                    case Directions.Neg:
                        serialHelper.DoCommand("H[-]\n");

                        break;
                    default:
                        break;
                }
            }

            public void StopMoving(ref SerialHelper serialHelper)
            {
                serialHelper.DoCommand("H[0]\n");
            }
        }

        private bool _hasInit = false;
        private SerialHelper _serialHelper;
        private LogHelper log;

        public void Init(SerialHelper serialHelper)
        {
            _serialHelper = serialHelper;
            _hasInit = true;
        }

        private enum Directions
        {
            Pos,
            Neg
        }

        private void StartMoving(Directions direction)
        {
            switch (direction)
            {
                case Directions.Pos:
                    _serialHelper.DoCommand("D[+]\n");
                    log.LogVerbose("Moving dome in POS direction.");
                    break;
                case Directions.Neg:
                    _serialHelper.DoCommand("D[-]\n");
                    log.LogVerbose("Moving dome in NEG direction.");
                    break;
                default:
                    break;
            }
        }

        private void StopMoving()
        {
            _serialHelper.DoCommand("D[0]\n");
            log.LogVerbose("Stopping the dome.");
        }

        public void GoToAzimuth(double azimuth, ref double currentAzimuth)
        {
            IsMoving = true;
            
            if (azimuth - currentAzimuth > 0) // need to turn to the left = negative azimuth speed.
            {
                StartMoving(Directions.Neg); // turn in that direction...
                while (Math.Abs(azimuth - currentAzimuth) > 2) { } //... until we are at most two degrees from the target.
            } else // need to turn right, the positive azimuth speed.
            {
                StartMoving(Directions.Pos);
                while (Math.Abs(azimuth - currentAzimuth) > 2) { } // same here.
            }
            StopMoving(); // stop the dome.

            IsMoving = false;
        }
    }

    [DataContract]
    public class Size
    {
        [DataMember]
        public int Width;

        [DataMember]
        public int Height;

        public Size(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
        }

        public Size() { }
    }

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
