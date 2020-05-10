using System;
using System.Collections.Generic;
using System.Text;

namespace rmqfn
{
    class RMQMessage
    {
        public string deviceid;
        public float temperature;

        public RMQMessage(string deviceid, float temperature)
        {
            this.deviceid = deviceid;
            this.temperature = temperature;
        }
    }
}
