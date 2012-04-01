using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using System.Net;
using System.Diagnostics;

namespace SpeakUp
{
    class mUDPclient
    {
        Int32 port = 12321;
        string serverIP = "127.0.0.1";
        UdpClient udpClient = new UdpClient();

        public mUDPclient()
        {
            try
            {
                udpClient.Connect(serverIP, port);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public void mClose()
        {
            udpClient.Close();
        }

        public bool Close()
        {
            udpClient.Close();
            return true;
        }


        //<summary>Send data to C program</summary>
        public string SendRecData(string inputTxt)
        {
            try
            {
              
                Byte[] sendBytes = Encoding.ASCII.GetBytes(inputTxt);
                udpClient.Send(sendBytes, sendBytes.Length);


                //IPEndPoint object will allow us to read datagrams sent from any source.
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

                // Blocks until a message returns on this socket from a remote host.
                Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                string returnData = Encoding.ASCII.GetString(receiveBytes);

                //Uses the IPEndPoint object to determine which of these two hosts responded.
                //Console.WriteLine("This is the message you received " +
                //                             returnData.ToString());
                //Console.WriteLine("This message was sent from " +
                //                            RemoteIpEndPoint.Address.ToString() +
                //                            " on their port number " +
                //                            RemoteIpEndPoint.Port.ToString());
                
                return returnData.ToString();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
