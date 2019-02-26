using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace StandAlone.TCP
{
    class RCProvider : TcpServiceProvider
    {
        private string _receivedStr;

        public override object Clone()
        {
            return new RCProvider();
        }

        public override void OnAcceptConnection(ConnectionState state)
        {
            //if (!state.Write(Encoding.UTF8.GetBytes("init\r\n"), 0, 6)) // this value has no significance
            //    state.EndConnection();
            //if write fails... then close connection
            LogHelper.WriteS($"Accepted a connection: " + state.RemoteEndPoint.ToString(), "tcp", LogHelper.MessageTypes.INFO);
        }

        Socket conn;

        public override void OnReceiveData(ConnectionState state)
        {
            byte[] buffer = new byte[1024];
            while (state.AvailableData > 0)
            {
                int readBytes = state.Read(buffer, 0, 1024);
                if (readBytes > 0)
                {
                    _receivedStr += Encoding.UTF8.GetString(buffer, 0, readBytes);

                    conn = state._conn;
                }
                else state.EndConnection();
                //If read fails then close connection
            }

            // we're here when there's no more data to read so we're parsing it now.
            ParseData(_receivedStr, state);
            // and clear the buffer:
            _receivedStr = "";
        }

        /// <summary>
        /// Parse the retrieved data.
        /// </summary>
        /// <param name="receivedStr">The retrieved data.</param>
        /// <param name="cs">The client sending the data.</param>
        private void ParseData(string receivedStr, ConnectionState cs)
        {
            
        }

        private void AddNewClient(ConnectionState cs, string DeviceID)
        {
            Program.Clients.Add(cs);
            LogHelper.WriteS("Adding new client to client pool, device ID is " + cs.DeviceID + ".", "TCP", LogHelper.MessageTypes.INFO);
        }

        public override void OnDropConnection(ConnectionState state)
        {
            Program.Clients.Remove(state);
            //Nothing to clean here
            LogHelper.WriteS("Connection dropped.", "TCP", LogHelper.MessageTypes.INFO);
        }
    }
}
