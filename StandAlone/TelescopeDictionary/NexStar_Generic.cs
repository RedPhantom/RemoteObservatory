using StandAlone.Modules;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandAlone.TelescopeDictionary
{
    class NexStar_Generic
    {
        #region SerialSettings

        /// <summary>
        /// The Telescope name that appears in the configuration file.
        /// </summary>
        public const string TelescopeModel = "NexStar_Generic";
        /// <summary>
        /// The parity of the serial connection protocol.
        /// </summary>
        public const Parity CPairity = Parity.None;
        /// <summary>
        /// The stop-bits structure of the serial connection protocol.
        /// </summary>
        public const StopBits CStopBits = StopBits.One;
        /// <summary>
        /// The bit-length structure of the serial connection protocol.
        /// </summary>
        public const int DataBits = 8;
        /// <summary>
        /// The text-connection encoding of the serial connection protocol.
        /// </summary>
        public Encoding Encoding { get; internal set; } = Encoding.ASCII;

        SerialHelper _helper;
        SerialHelper _dome;

        #endregion

        private LogHelper _log = new LogHelper();

        /// <summary>
        /// Initialize a TeleController object.
        /// </summary>
        /// <param name="Helper">A configured SerialHelper to send commands.</param>
        /// <returns>A TeleController object.</returns>
        public NexStar_Generic(SerialHelper helper, SerialHelper Dome)
        {
            _log.LogFileLocation = AppSettings.Default.LogFileLocation;
            _helper = helper;
        }

        /// <summary>
        /// Supported telescope alignment modes.
        /// </summary>
        public enum CoordinateSystems
        {
            /// <summary>
            /// Relative Ascension and Declination
            /// </summary>
            RA_DEC = 1,

            /// <summary>
            /// Azimuth and Altitude
            /// </summary>
            AZM_ALT = 2
        }

        /// <summary>
        /// Supported telescope tracking modes.
        /// </summary>
        public enum TrackingModes
        {
            Off = 0,
            ALT_AZ = 1,
            EQ_NORTH = 2,
            EQ_SOUTH = 3
        }

        /// <summary>
        /// All internal device IDs on the telescope.
        /// </summary>
        public enum DeviceIDs
        {
            MainBoard      = 0x1,
            HandController = 0x4,
            AZM_Controller = 0x10,
            ALT_Controller = 0x11, 
            GPSModule      = 0xb0
        }

        public enum SlewDirections
        {
            POS,
            NEG
        }

        public CoordinateSystems CoordinateMode { get; set; }
        
        /// <summary>
        /// Retrieves the (precise?) position of the scope.
        /// </summary>
        /// <param name="highPrecision"></param>
        /// <returns>A one dimensional array contains the position in the first two elements.</returns>
        public double[] GetPosition(bool highPrecision = true)
        {
            string cmd = "";
            long denominator = -1;

            if (highPrecision)
            {
                if (CoordinateMode == CoordinateSystems.AZM_ALT)
                    cmd += "z";
                else if (CoordinateMode == CoordinateSystems.RA_DEC)
                    cmd += "e";

                denominator = 0x100000000;
            }
            else
            {
                if (CoordinateMode == CoordinateSystems.AZM_ALT)
                    cmd += "Z";
                else if (CoordinateMode == CoordinateSystems.RA_DEC)
                    cmd += "E";

                denominator = 0x10000;
            }

            string response = _helper.DoCommand(cmd);

            //if (response.Length != expected_response_length)

            string[] coordsString = response.Split(',');

            double[] coords = new double[2];

            for (int i = 0; i < 2; i++)
                coords[i] = 360.0 * Convert.ToInt32(coordsString[i], 16) / Convert.ToDouble(denominator);

            return coords;
        }

        /// <summary>
        /// Navigates the scope to the provided coordinates in the specified coordinate system.
        /// </summary>
        /// <param name="firstCoord"></param>
        /// <param name="lastCoord"></param>
        /// <param name="highPrecision"></param>
        public void GoToCoordinates(double firstCoord, double lastCoord, bool highPrecision)
        {
            string cmd = "";

            if (highPrecision)
            {
                if (CoordinateMode == CoordinateSystems.AZM_ALT)
                    cmd += "b";
                else if (CoordinateMode == CoordinateSystems.RA_DEC)
                    cmd += "r";

                double firstCoordinate = Math.Round(firstCoord / 360.0 * 0x100000000);
                double secondCoordinate = Math.Round(lastCoord / 360.0 * 0x100000000);
                cmd += string.Format("{0:8C},{1:8C}", firstCoordinate.ToString("X"), secondCoordinate.ToString("X")); // print hex of the coordinates.
            }
            else
            {
                if (CoordinateMode == CoordinateSystems.AZM_ALT)
                    cmd += "B";
                else if (CoordinateMode == CoordinateSystems.RA_DEC)
                    cmd += "R";

                double firstCoordinate = Math.Round(firstCoord / 360.0 * 0x10000);
                double secondCoordinate = Math.Round(lastCoord / 360.0 * 0x10000);
                cmd += string.Format("{0:4C},{1:4C}", firstCoordinate.ToString("X"), secondCoordinate.ToString("X")); // print hex of the coordinates.
            }

            _helper.DoCommand(cmd);
        }


        /// <summary>
        /// Retrieves the scope's current tracking mode.
        /// </summary>
        /// <returns></returns>
        public TrackingModes GetTrackingMode()
        {
            string cmd = "t";
            string res = _helper.DoCommand(cmd);

            return (TrackingModes)int.Parse(res[0].ToString());
        }

        /// <summary>
        /// Sets the scope's current tracking mode.
        /// </summary>
        /// <param name="trackingMode"></param>
        public void SetTrackingMode(CoordinateSystems trackingMode)
        {
            string cmd = "T";
            string res = _helper.DoCommand(cmd + (int)trackingMode);
        }

        /// <summary>
        /// Retrieves the scope's current geographical location.
        /// </summary>
        /// <returns></returns>
        public double[] GetLocation()
        {
            string cmd = "w";
            string res = _helper.DoCommand(cmd);

            // todo: see what we get.
            return new double[2];
        }

        /// <summary>
        /// Sets the scopes current geographical locmation.
        /// </summary>
        /// <param name="Longitude">Geographical longtitude in decimal format.</param>
        /// <param name="Latitude">Geographical latitude in decimal format.</param>
        public void SetLocation(double Longitude, double Latitude)
        {
            // Fix latitude
            int latitude_sign = -1;
            double latitude_seconds = Math.Round(Latitude * 3600);
            if (latitude_seconds >= 0)
                latitude_sign = 0;
            else
            {
                latitude_sign = 1;
                latitude_seconds = -latitude_seconds;
            }
            // Fix longitude

            int longitude_sign = -1;
            double longitude_seconds = Math.Round(Longitude * 3600);
            if (longitude_seconds >= 0)
                longitude_sign = 0;
            else
            {
                longitude_sign = 1;
                longitude_seconds = -longitude_seconds;
            }

            // Reduce to Degrees/Minutes/Seconds

            int latitude_degrees = Convert.ToInt32(latitude_seconds) / 3600; // latitude_seconds %= 3600
            int latitude_minutes = Convert.ToInt32(latitude_seconds) / 60;   // latitude_seconds %= 60

            int longitude_degrees = Convert.ToInt32(longitude_seconds) / 3600;   // longitude_seconds %= 3600
            int longitude_minutes = Convert.ToInt32(longitude_seconds) / 60;                          // longitude_seconds %= 60

            // Synthesize "W" request
            
            string cmd = "W" + latitude_degrees + latitude_minutes + latitude_seconds + latitude_sign + latitude_degrees + latitude_minutes + latitude_seconds + longitude_sign;

            _helper.DoCommand(cmd);
        }
        
        public void StopSlewing()
        {
            _helper.DoCommand("M");
        }

        /// <summary>
        /// Starts or stops the telescope.
        /// Positive movement is right or down; negative movement is left or up.
        /// </summary>
        /// <param name="Direction">Movement direction: coordinate increase or decrease.</param>
        /// <param name="DeviceID">Target device (preferably motor) internal system ID.</param>
        /// <param name="Speed">Speed from 0 (stop) to 9 (max slew rate).</param>
        public void StartSlewing(SlewDirections Direction, DeviceIDs DeviceID, byte Speed)
        {
            byte[] cmd = new byte[8];

            cmd[0] = 0x50;
            cmd[1] = 0x02;

            if (DeviceID == DeviceIDs.ALT_Controller)
                cmd[2] = (byte)DeviceID;
            else if (DeviceID == DeviceIDs.AZM_Controller)
                cmd[2] = (byte)DeviceID;
            else throw new ArgumentException("Device ID must be 0x10 or 0x11.");

            if (Direction == SlewDirections.POS)
                cmd[3] = 0x24;
            else
                cmd[3] = 0x25;

            if (Speed > 9 || Speed < 0)
                throw new ArgumentException;
            else
                cmd[4] = Speed;
        }

        // TODO!
        public void GetTime()
        {
            string cmd = "h";
            string response = _helper.DoCommand(cmd);

            byte hour = Convert.ToByte(response[0]); // 24-hr clock
            byte minute = Convert.ToByte(response[1]);
            byte second = Convert.ToByte(response[2]);
            byte month = Convert.ToByte(response[3]); // jan == 1, dec == 12
            byte day = Convert.ToByte(response[4]); // 1 .. 31
            int year = Convert.ToInt32(response[5]); // year minus 2000
            int zone = Convert.ToInt32(response[6]); // 2-complement of timezone (hour offset from UTC)
            byte dst_val = Convert.ToByte(response[7]); // 1 to enable DST, 0 for standard time.

            year += 2000;

            if (zone >= 128) // take care of negative zone offsets
                zone -= 256;

            //zone = datetime.timedelta(hours = zone)

            //bool dst = dst_val != 0;

            //print(year, month, day, hour, minute, second)

            //tzinfo = datetime.timezone(zone) # simple timezone with offset relative to UTC
            //timestamp = datetime.datetime(year, month, day, hour, minute, second, 0, tzinfo)

            //return (timestamp, dst)
        } 
    }
}
