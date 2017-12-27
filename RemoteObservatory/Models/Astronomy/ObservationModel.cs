using Newtonsoft.Json;
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
        public string OwnerID { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name ="Observation Start Date")]
        [Required]
        /// <summary>
        /// When the observation starts.
        /// </summary>
        public DateTime ObservationStart { get; set; }

        [Required]
        /// <summary>
        /// The ID of the object within the internal catalogue.
        /// </summary>
        public long ObjectID { get; set; }

        public string ObjectName { get; set; }

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
