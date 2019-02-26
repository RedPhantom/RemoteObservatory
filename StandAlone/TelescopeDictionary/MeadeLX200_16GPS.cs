using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using StandAlone.Modules;

namespace StandAlone.TelescopeDictionary
{
    /// <summary>
    /// A command dictionary supported by the Meade LX200 16" GPS telescope.
    /// </summary>
   public class MeadeLX200_16GPS /* : IScope */
    {
        #region SerialSettings

        /// <summary>
        /// The Telescope name that appears in the configuration file.
        /// </summary>
        public const string ModelName = "MeadeLX200_16GPS";

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

        /// <summary>
        /// The ACK byte to send for alignment query as per the documentation.
        /// </summary>
        public const byte ACK = 0x06;

        /// <summary>
        /// The NAK byte that is received (within 10 ms of receiving the command terminating character) when the telescope is currently busy.
        /// </summary>
        public const byte NAK = 0x15;

        SerialHelper _helper; // scope
        SerialHelper _dome;   // dome

        #endregion

        private LogHelper _log = new LogHelper(); // registers all log operations.

        /// <summary>
        /// Initialize a TeleController object.
        /// </summary>
        /// <param name="Helper">A configured SerialHelper to send commands.</param>
        /// <returns>A TeleController object.</returns>
        public MeadeLX200_16GPS(SerialHelper helper, SerialHelper Dome)
        {
            _log.LogFileLocation = AppSettings.Default.LogFileLocation;
            _helper = helper;
        }

        /// <summary>
        /// Supported telescope alignment modes.
        /// </summary>
        public enum AlignmentModes
        {
            AltAz,
            Land,
            Polar
        }

        /// <summary>
        /// Retrieves the current alignment mode for the telescope.
        /// </summary>
        /// <returns>The current alignment mode for the telescope.</returns>
        public AlignmentModes GetAlignmentMode()
        {
            string ACK = Encoding.ASCII.GetString(new byte[] { 0x06 }); // set the ACK ascii sign as per the LX200's specs.
            string response = _helper.DoCommand(ACK);

            switch (response)
            {
                case "A":
                    _log.Write("Alignment Mode is AltAz", "ALIGN");
                    return AlignmentModes.AltAz;
                    
                case "L":
                    _log.Write("Alignment Mode is Land", "ALIGN");
                    return AlignmentModes.Land;

                case "P":
                    _log.Write("Alignment Mode is Polar", "ALIGN");
                    return AlignmentModes.Polar;

                default:
                    _log.Write("Error: invalid response from serial device.", "ALIGN", LogHelper.MessageTypes.ERROR);
                    throw new Exception();
            }
        }

        /// <summary>
        /// Sets a telescope alignment mode.
        /// </summary>
        /// <param name="AlignmentMode">An <see cref="AlignmentModes"/> to be set.</param>
        public void SetAlignmentMode(AlignmentModes AlignmentMode)
        {
            switch (AlignmentMode)
            {
                case AlignmentModes.AltAz:
                    _log.Write("Set Alignment Mode to AltAz", "ALIGN");
                    _helper.DoCommand(":AA#");
                    break;
                case AlignmentModes.Land:
                    _log.Write("Set Alignment Mode to Land", "ALIGN");
                    _helper.DoCommand(":AL#");
                    break;
                case AlignmentModes.Polar:
                    _log.Write("Set Alignment Mode to Polar", "ALIGN");
                    _helper.DoCommand(":AP#");
                    break;
                default:
                    break;
            }
        }

        // To do: add option for selenographic coordinates
        /// <summary>
        /// Syncs the telescope with the current library selected object.
        /// </summary>
        /// <returns>The selected object information terminated by a '#'.</returns>
        public string SyncTelescope()
        {
            _log.Write("Telescope synced with current object location.", "ALIGN");
            return _helper.DoCommand(":CM#");
        }

        /// <summary>
        /// A direction in which the telescope focuser (if installed) moves.
        /// </summary>
        public enum FocuserDirection
        {
            Inward,
            Outward
        }
        
        /// <summary>
        /// The speed in which the telescope focuser (if installed) moves.
        /// </summary>
        public enum FocuserSpeed
        {
            Stop,
            Slowest,
            Fastest
        }

        /// <summary>
        /// Sets the focuser speed and direction.
        /// </summary>
        /// <param name="Direction">The direction of focuser movement. Doesn't matter in case of "stop" speed.</param>
        /// <param name="Speed">The speed of focuser movement.</param>
        public void SetFocuserStatus(FocuserDirection Direction, FocuserSpeed Speed)
        {
            switch (Speed)
            {
                case FocuserSpeed.Stop:
                    _helper.DoCommand(":FQ#"); // halt motion
                    return;
                case FocuserSpeed.Slowest:
                    _helper.DoCommand(":FS#"); // speed: slowest
                    switch (Direction)
                    {
                        case FocuserDirection.Inward:
                            _helper.DoCommand(":F+#"); // inward
                            break;
                        case FocuserDirection.Outward:
                            _helper.DoCommand(":F-#"); // outward
                            break;
                        default:
                            break;
                    }
                    break;
                case FocuserSpeed.Fastest:
                    _helper.DoCommand(":FF#"); // speed: fastest
                    switch (Direction)
                    {
                        case FocuserDirection.Inward:
                            _helper.DoCommand(":F+#");
                            break;
                        case FocuserDirection.Outward:
                            _helper.DoCommand(":F-#");
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Supported statuses of the GPS unit in the LX200 16" GPS.
        /// </summary>
        public enum GPSStatus
        {
            On,
            Off
        }

        /// <summary>
        /// Controls the GPS unit.
        /// </summary>
        /// <param name="Status">The wanted GPS status.</param>
        public void SetGPSStatus(GPSStatus Status)
        {
            switch (Status)
            {
                case GPSStatus.On:
                    {
                        _helper.DoCommand(":g+#"); // turn on gps
                    }
                    break;
                case GPSStatus.Off:
                    {
                        _helper.DoCommand(":g-#"); // turn off gps
                    }
                    break;
            }
        }

        /// <summary>
        /// Object catalogues supported by the LX200 16" GPS
        /// </summary>
        public enum Catalogue
        {
            STAR,
            SAO,
            GCVS,
            Hipparcos,
            HR,
            HD
        }

        /// <summary>
        /// Sets the telescope to an object.
        /// </summary>
        /// <param name="ID">The object ID</param>
        /// <param name="Catalogue">The target catalogue.</param>
        /// <returns>True on success. False on error.</returns>
        public void GoToMessier(int ID)
        {
            _helper.DoCommand(":LM" + ID.ToString() + "#");
            //_dome.DoCommand(); 
        }

        /// <summary>
        /// Navigates the telescope to a DeepSky Catalog object.
        /// </summary>
        /// <param name="ID">The DeepSky catalog number.</param>
        void GoToDeepSkyObject(int ID)
        {
            _helper.DoCommand(":LC" + ID.ToString() + "#");
            //_dome.DoCommand();
        }

        /// <summary>
        /// Sets the current catalogue for lookup.
        /// </summary>
        /// <param name="Catalogue">Name of catalogue.</param>
        /// <returns>True on success finding the catalogue, false on faliure.</returns>
        public bool SetCurrentCatalogue(Catalogue Catalogue)
        {
            var result = _helper.DoCommand(":Ls" + Catalogue + "#");
            return (result == "1");
        }

        /// <summary>
        /// Sends the telescope to the object ID.
        /// </summary>
        /// <param name="ID">The ID of the object.</param>
        public void GoTo(int ID)
        {
            _helper.DoCommand(":LS" + ID.ToString() + "#");
            //_dome.DoCommand();
        }
    }
}
