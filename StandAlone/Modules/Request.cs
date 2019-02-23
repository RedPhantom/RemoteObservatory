using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Newtonsoft.Json;

namespace StandAlone
{
    class Request
    {
        public int ID { get; set; }
        public string UUID { get; set; }
        public string JQuery { get; set; }
        
        public bool CheckForRequest(Request request)
        {
            string _filePath = AppSettings.Default.RequestFilePath;
            string _requestLogFile;

            try
            {
                _requestLogFile = System.IO.File.ReadAllText(_filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught exception: " + ex.Message);
                throw;
            } // load the text file;     string _requestLogFile

            int searchResult = _requestLogFile.IndexOf(request.UUID);

            return (searchResult != -1); // true if result is found, false if not.
        }

        /*public ObservationModel RequestToObservation(string JsonRequest)
        {
            ObservationModel obs = JsonConvert.DeserializeObject<ObservationModel>(JsonRequest);
            return obs;

        }*/

    }
}
