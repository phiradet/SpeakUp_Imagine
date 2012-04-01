using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SpeakUp
{
    public class BTconnect
    {
        /*public struct BTdevice{
            public string deviceName;
            public string deviceAddr;
            public string deviceClass;
            public int deviceCallState;
        };
        string selectedDevice;*/
        mUDPclient client;

        //------------ CONNECTION CONSTANT ------------
        const string getExistingDevice = "getExist";
        const string syncDevice = "sync";
        const string unsyncDevice = "unsync";
        const string getProperty = "getProp";
        const string makeCall = "call";
        const string endCall = "endCall";
        //const string redial = "redial";
        const string createDUN = "createDUN";
        const string closeDUN = "closeDUN";
        const string connectHF = "connectHF";
        const string endHFP = "endHF";
        const string execute = "exe";
        const string accept = "1";
        const string reject = "2";
        const string dial = "3";
        const string redial = "4";
        const string dtmf = "6";
        const string at_command = "f";
        const string HFPdisconnect = "i";

        const string error = "error";
        const string success = "success";
        //----------------------------------------------

        

        public BTconnect()
        {
            client = new mUDPclient();
        }

        ~BTconnect()
        {
            client.mClose();
        }

        public List<BTdevice> GetExistDevice(ref string errMsg)
        {
            List<String> visitedDevice = new List<string>();
            List<BTdevice> allDeviceList = new List<BTdevice>();
            string output=client.SendRecData(getExistingDevice);
            if (output==null || output.Trim() == error)
            {
                errMsg = "error";
                return null;
            }
            string[] outputList = output.Trim().Split(';');
            for (int i = 0; i < outputList.Length-1; i++)
            {
                string[] data = outputList[i].Split('|');
                string deviceName = data[0];
                string deviceAddr = data[1];
                string deviceClass = data[2];
                if (visitedDevice.Contains(deviceAddr))
                    continue;
                visitedDevice.Add(deviceAddr);
                BTdevice currDevice = new BTdevice(deviceName, deviceAddr, deviceClass);
                //currDevice.deviceName = deviceName;
                //currDevice.deviceAddr = deviceAddr;
                //currDevice.deviceClass = deviceClass;
                //allDeviceList.Add(currDevice);
            }
            return allDeviceList;
        }

        public bool ConnectHFP(string deviceAddr,ref string errMsg)
        {
            string command = String.Format("{0}#{1}",connectHF,deviceAddr);
            string output = client.SendRecData(command);
            if (output == null)
            {
                errMsg = "Error: return NULL";
                return false;
            }
            else if (output.Trim() != success)
            {
                errMsg = output.Trim();
                return false;
            }
            Debug.WriteLine(output);
            return true;
        }

        public bool TerminateHFP(ref string errMsg)
        {
            string command = String.Format("{0}",endHFP);
            string output = client.SendRecData(command);
             if (output == null)
            {
                errMsg = "Error: return NULL";
                return false;
            }
            else if (output.Trim() != success)
            {
                errMsg = output.Trim();
                return false;
            }
            Debug.WriteLine(output);
            return true;
        }

        public string ConnectDUN(string deviceAddr,ref string errMsg)
        {
            string command = String.Format("{0}#{1}", createDUN,deviceAddr);
            string portNum = client.SendRecData(command);
            if (portNum == null)
            {
                errMsg = "Error: return NULL";
                return null;
            }
            else if (portNum.Trim() != success)
            {
                errMsg = portNum.Trim();
                return null;
            }
            Debug.WriteLine(portNum);
            return portNum;
        }

       

        public bool TerminateDUN(ref string errMsg)
        {
            string command = String.Format("{0}", createDUN);
            string output = client.SendRecData(command);
            if (output == null)
            {
                errMsg = "Error: return NULL";
                return false;
            }
            else if (output.Trim() != success)
            {
                errMsg = output.Trim();
                return false;
            }
            Debug.WriteLine(output);
            return true;
        }

        public bool SyncDevice(int deviceIndex,ref string errMsg)
        {
            string command = String.Format("{0}#{1}", syncDevice, deviceIndex);
            string output = client.SendRecData(command);
            if (output == null)
            {
                errMsg = "Error: return NULL";
                return false;
            }
            else if (output.Trim() != success)
            {
                errMsg = output.Trim();
                return false;
            }
            Debug.WriteLine(output);
            return true;
        }

        public bool UnsyncDevice(ref string errMsg)
        {
            string command = String.Format("{0}", unsyncDevice);
            string output = client.SendRecData(command);
            if (output == null)
            {
                errMsg = "Error: return NULL";
                return false;
            }
            else if (output.Trim() != success)
            {
                errMsg = output.Trim();
                return false;
            }
            Debug.WriteLine(output);
            return true;
        }

        public bool DialNumber(string phoneNum,ref string errMsg)
        {
            string command = String.Format("{0}#{1}#{2}", execute ,dial, phoneNum);
            string output = client.SendRecData(command);
            if (output == null)
            {
                errMsg = "Error: return NULL";
                return false;
            }
            else if (output.Trim() != success)
            {
                errMsg = output.Trim();
                return false;
            }
            Debug.WriteLine(output);
            return true;
        }

        public bool RejectCall(ref string errMsg)
        {
            string command = String.Format("{0}#{1}", execute, reject);
            string output = client.SendRecData(command);
            if (output == null)
            {
                errMsg = "Error: return NULL";
                return false;
            }
            else if (output.Trim() != success)
            {
                errMsg = output.Trim();
                return false;
            }
            Debug.WriteLine(output);
            return true;
        }

        public bool RedialPhone(ref string errMsg)
        {
            string command = String.Format("{0}#{1}", execute,redial);
            string output = client.SendRecData(command);
            if (output == null)
            {
                errMsg = "Error: return NULL";
                return false;
            }
            else if (output.Trim() != success)
            {
                errMsg = output.Trim();
                return false;
            }
            Debug.WriteLine(output);
            return true;
        }

        public bool AcceptCall(ref string errMsg)
        {
            string command = String.Format("{0}#{1}", execute, accept);
            string output = client.SendRecData(command);
            if (output == null)
            {
                errMsg = "Error: return NULL";
                return false;
            }
            else if (output.Trim() != success)
            {
                errMsg = output.Trim();
                return false;
            }
            Debug.WriteLine(output);
            return true;
        }

        public bool DTMF(string input, ref string errMsg)
        {
            string command = String.Format("{0}#{1}#{2}", execute, dtmf, input);
            string output = client.SendRecData(command);
            if (output == null)
            {
                errMsg = "Error: return NULL";
                return false;
            }
            else if (output.Trim() != success)
            {
                errMsg = output.Trim();
                return false;
            }
            Debug.WriteLine(output);
            return true;
        }


    }
}
