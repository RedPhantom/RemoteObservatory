﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteObservatory.Models.Astronomy
{
    /// <summary>
    /// This class structures the data of an observation, including all of the required information to perform an astronimical observation.
    /// </summary>
    public class ObservationModel
    {

        [Key]
        public long ID { get; set; }

        /// <summary>
        /// The user ordering the observation.
        /// </summary>
        public ApplicationUser OrderingUser { get; set; }

        [DataType(DataType.Date)]
        [Required]
        /// <summary>
        /// When the observation starts.
        /// </summary>
        public DateTime ObservationStart { get; set; }

        [DataType(DataType.Text)]
        /// <summary>
        /// These will be assigned different values based on what coordinate system is chosen.
        /// </summary>
        public string Longtitude { get; set; }
        public string Latitude { get; set; }

        [Required]
        /// <summary>
        /// The ID of the object within the internal catalogue.
        /// </summary>
        public long ObjectID { get; set; }

        public string ObjectName { get; set; }

        /// <summary>
        /// Whether to track coordinates or the name of an object.
        /// </summary>
        public enum CaptureMethods
        {
            Coordinates,
            ObjectID
        }

        [Required]
        public CaptureMethods CaptureMethod { get; set; }

        /// <summary>
        /// Current status of the observation.
        /// </summary>
        public enum ObservationStatus
        {
            PendingApproval, // observation is pending approval by the staff.
            Pending, // awating capturing date.
            Active, // is currently capturing.
            Saving, // saving data to disk.
            Processing, // processing image information.
            Complete // complete and sent to client/user.
        }

        [Required]
        public ObservationStatus Status { get; set; }

        /// <summary>
        /// The system of coordinates to observe coordinates from.
        /// <see cref="https://en.wikipedia.org/wiki/Celestial_coordinate_system"/>.
        /// Usually measured in degrees, minutes and seconds.
        /// </summary>
        public enum CoordinateSystems
        {
            Horizontal,
            Equatorial,
            Ecliptic,
            Galactic,
            Supergalactic
        }

        [Required]
        public CoordinateSystems CoordinateSystem { get; set; }

        public ICollection<FileModel> Files { get; set; }

        /// <summary>
        /// returns a JSON string for the request file.
        /// </summary>
        /// <param name="observation"></param>
        /// <returns></returns>
        public string ConvertToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public ObservationModel()
        {
            //https://stackoverflow.com/questions/23915109/why-collection-initialization-throws-nullreferenceexception
            Files = new Collection<FileModel> { };
        }

    }
}
