using System.Runtime.Serialization;

namespace StandAlone
{
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
}