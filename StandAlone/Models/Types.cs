using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StandAlone.Models
{
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
}
