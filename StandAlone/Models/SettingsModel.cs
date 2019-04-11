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
}
