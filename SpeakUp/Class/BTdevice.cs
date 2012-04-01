using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakUp
{
    public class BTdevice
    {
        public string deviceName;
        public string deviceAddr;
        public string deviceClass;
        public int deviceCallState;

        public BTdevice(string name,string addr, string mClass)
        {
            deviceName = name;
            deviceAddr = addr;
            deviceClass = mClass;
            deviceCallState = Constants.SLEEP;
        }

        public void UpdatePhoneState(string rawInput)
        {
            string[] splInput = rawInput.Split('#');
            string mInput = splInput[0];
            if (this.deviceCallState == Constants.SLEEP)
            {
                if (mInput == Constants.CallIncomming)
                    this.deviceCallState = Constants.INCOMING;
                else if (mInput == Constants.CallOnGoing)
                    this.deviceCallState = Constants.ONGOING;
                else if (mInput == Constants.CallOutGoing)
                    this.deviceCallState = Constants.OUTGOING;
                else
                    throw new System.ArgumentException(String.Format("STATE FALSE {0} --> {1}",deviceCallState,mInput));
            }
            else if (this.deviceCallState == Constants.INCOMING)
            {
                if (mInput == Constants.CallTerminated)
                    this.deviceCallState = Constants.SLEEP;
                else if (mInput == Constants.CallOnGoing)
                    this.deviceCallState = Constants.ONGOING;
                else
                    throw new System.ArgumentException(String.Format("STATE FALSE {0} --> {1}",deviceCallState,mInput));
            }
            else if (this.deviceCallState == Constants.OUTGOING)
            {
                if (mInput == Constants.CallSCOrelease)
                    this.deviceCallState = Constants.SLEEP;
                else if (mInput == Constants.CallOnGoing)
                    this.deviceCallState = Constants.ONGOING;
                else
                    throw new System.ArgumentException(String.Format("STATE FALSE {0} --> {1}",deviceCallState,mInput));
            }
            else if (this.deviceCallState == Constants.ONGOING)
            {
                if (mInput == Constants.CallSCOrelease)
                    this.deviceCallState = Constants.SLEEP;
                else
                    throw new System.ArgumentException(String.Format("STATE FALSE {0} --> {1}", deviceCallState, mInput));
            }
        }
    }
}
