using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandAlone
{
    public static class OperatorService
    {
        static bool go = true;

        public static void DoWork()
        {
            while (go)
            {
                // load requests from database, sorted by score, only with status "pending".

                // execute the next request.

                // mark the request performed as "complete".
            }
        }

        public static void StopWork()
        {
            go = false;
        }
    }
}
