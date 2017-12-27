using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace StandAlone.TelescopeDictionary
{
    class MeadeLX200_16GPS
    {
        #region SerialSettings
        /// <summary>
        /// The Telescope name that appears in the configuration file.
        /// </summary>
        public const string TelescopeModel = "MeadeLX200_16GPS";
        
        public const Parity CPairity = Parity.None;
        public const StopBits CStopBits = StopBits.One;
        public const int DataBits = 8;
        public Encoding Encoding = Encoding.ASCII;
        #endregion

        private SerialHelper _helper;
        private LogHelper _log = new LogHelper();

        /// <summary>
        /// Initialize a TeleController object.
        /// </summary>
        /// <param name="Helper">A configured SerialHelper to send commands.</param>
        /// <returns>A TeleController object.</returns>
        public MeadeLX200_16GPS New(SerialHelper Helper)
        {
            this._helper = Helper;
            _log.LogFileLocation = AppSettings.Default.LogFileLocation;

            return this;
        }

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
            string ACK = ASCIIEncoding.ASCII.GetString(new byte[] { 0x06 }); // set the ACK ascii sign as per the LX200's specs.
            string response = _helper.DoCommand(ACK);

            switch (response)
            {
                case "A":
                    _log.WriteLog("Alignment Mode is AltAz");
                    return AlignmentModes.AltAz;
                    
                case "L":
                    _log.WriteLog("Alignment Mode is Land");
                    return AlignmentModes.Land;

                case "P":
                    _log.WriteLog("Alignment Mode is Polar");
                    return AlignmentModes.Polar;

                default:
                    _log.WriteLog("Error: invalid response from serial device.", LogHelper.MessageLevels.Error);
                    throw new Exception();
            }
        }

        public void SetAlignmentMode(AlignmentModes AlignmentMode)
        {
            switch (AlignmentMode)
            {
                case AlignmentModes.AltAz:
                    _log.WriteLog("Set Alignment Mode to AltAz");
                    _helper.DoCommand(":AA#");
                    break;
                case AlignmentModes.Land:
                    _log.WriteLog("Set Alignment Mode to Land");
                    _helper.DoCommand(":AL#");
                    break;
                case AlignmentModes.Polar:
                    _log.WriteLog("Set Alignment Mode to Polar");
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
            _log.WriteLog("Telescope synced with current object location.");
            return _helper.DoCommand(":CM#");
        }

        public enum FocuserDirection
        {
            Inward,
            Outward
        }
        
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
        }

        void GoToDeepSkyObject(int ID)
        {
            _helper.DoCommand(":LC" + ID.ToString() + "#");
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strength">The strength of the fart sound</param>
        public void PlayFartSound(int strength)
        {

        }

    }
}
