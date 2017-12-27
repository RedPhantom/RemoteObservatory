using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using StandAlone.TelescopeDictionary;

namespace StandAlone
{
    class Program
    {
        static void Main(string[] args)
        {

            MeadeLX200_16GPS telescope = new MeadeLX200_16GPS();
            telescope.PlayFartSound(100);
            
        }


        /// <summary>
        /// A continues loop for sending commands to the telescope.
        /// </summary>
        static void TestRoutine()
        {

        }

    }
}
