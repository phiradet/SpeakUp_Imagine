using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;

namespace SpeakUp
{
    class UDPserver
    {
        PageCall parent;
        public UDPserver(PageCall parent)
        {
            this.parent = parent;
        }
        
        public void Run()
        {
            byte[] data = new byte[1024];
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 12322);
            UdpClient newsock = new UdpClient(ipep);

            Debug.WriteLine("Waiting for a client...");

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);

            //data = newsock.Receive(ref sender);

            //Debug.WriteLine(String.Format("Message received from {0}:", sender.ToString()));
            //Debug.WriteLine(Encoding.ASCII.GetString(data, 0, data.Length));

            //string welcome = "Welcome to my test server";
            //data = Encoding.ASCII.GetBytes(welcome);
            //newsock.Send(data, data.Length, sender);

            while (true)
            {
                data = newsock.Receive(ref sender);
                string tmpData = Encoding.ASCII.GetString(data, 0, data.Length);
                Debug.Write(String.Format("Message received from {0}:", sender.ToString()));
                Debug.WriteLine(tmpData);
                //parent.commandList.Dispatcher.Invoke(parent.addListDelegate, new Object[] { tmpData });
                //parent.notifyLabel.Dispatcher.Invoke(parent.showNotificationDelegate, new Object[] { tmpData });
            }
        }
    }
}
