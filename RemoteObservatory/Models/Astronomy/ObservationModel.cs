using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteObservatory.Models.Astronomy
{
    /// <summary>
    /// This class structures the data of an observation, including all of the required information to perform an astronimical observation.
    /// </summary>
    public class ObservationModel
    {
        /// <summary>
        /// The user ordering the observation.
        /// </summary>
        ApplicationUser OrderingUser;

        /// <summary>
        /// When the observation starts.
        /// </summary>
        DateTime ObservationStart;

        /// <summary>
        /// The raw capture file.
        /// </summary>
        FileModel file;

        /// <summary>
        /// These will be assigned different values based on what coordinate system is chosen.
        /// </summary>
        string Longtitude;
        string Latitude;
        
        /// <summary>
        /// The ID of the object within the internal catalogue.
        /// </summary>
        long ObjectID;

        /// <summary>
        /// Whether to track coordinates or the name of an object.
        /// </summary>
        enum CaptureMethods
        {
            Coordinates,
            ObjectID
        }

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
        /// Current status of the observation.
        /// </summary>
        enum ObservationStatus
        {
            PendingApproval, // observation is pending approval by the staff.
            Pending, // awating capturing date.
            Active, // is currently capturing.
            Saving, // saving data to disk.
            Processing, // processing image information.
            Complete // complete and sent to client/user.
        }

        /// <summary>
        /// The system of coordinates to observe coordinates from.
        /// <see cref="https://en.wikipedia.org/wiki/Celestial_coordinate_system"/>.
        /// Usually measured in degrees, minutes and seconds.
        /// </summary>
        enum CoordinateSystem
        {
            Horizontal, 
            Equatorial, 
            Ecliptic,     
            Galactic,
            Supergalactic
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
