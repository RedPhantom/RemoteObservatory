using StandAlone.Modules;
using System.Collections.Generic;
using System.Text;

namespace StandAlone
{ 
    /// <summary>
    /// FIFO queue for commands.
    /// </summary>
    /// <typeparam name="T">Type of command structure. Usually a <see cref="string"/>.</typeparam>
    internal class SerialQueue<T>
    {
        List<T> commandList = new List<T>();

        int numTries { get; set; }
        SerialHelper helper { get; set; }
        byte NAK { get; set; }

        public void Add(T item)
        {
            commandList.Add(item);
        }

        public T GetLast()
        {
            return commandList[commandList.Count - 1];
        }

        public void Remove(T item)
        {
            commandList.Remove(item);
        }

        public bool TryLastCommand()
        {
            T cmd = this.GetLast();
            int try_ = 0;

            //return TryCommand(cmd);

            while (TryCommand(cmd) == false && try_ < this.numTries)
            {
                try_++;
                System.Threading.Thread.Sleep(500);
                LogHelper.WriteS("Tried command: <" + cmd.ToString() + ">. Failed, this is try " + try_ + " of " + this.numTries, "SERIAL", LogHelper.MessageTypes.WARNING);
            }

            if (try_ >= this.numTries)
            {
                string err = $"Exceeded number of tries on command <{cmd}>.";
                LogHelper.WriteS(err, "SERIAL", LogHelper.MessageTypes.ERROR);
                throw new System.Exception(err);
            }

            return true;
        }

        public bool TryCommand(T cmd)
        {
            byte[] result = Encoding.ASCII.GetBytes(helper.DoCommand(cmd.ToString()));

            foreach (byte byte_ in result)
            {
                if (byte_ == this.NAK)
                {
                    // the scope sent a NAK byte, which means it is busy.
                    // busy, return false.
                    return false;
                }
                else
                {
                    // not busy, got a response. Remove the command from the queue.
                    this.Remove(cmd);
                    return true;
                }
            }

            return false;
        }
    }
}