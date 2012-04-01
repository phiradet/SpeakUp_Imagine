using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpeakUp
{
    public class Constants
    {
        private Constants()
        { 
        }

        
        public const int _engLanguage = 0;
        public const int _thaLanguage = 1;
        public const int _chiLanguage = 2;

        public const int MALE = 0;
        public const int FEMALE = 1;

        #region Connect to C program
        //---- CONST for connect to C program
        public const string serverIP = "127.0.0.1";
        public const int serverPort = 12321;
        #endregion

        #region PhoneState
        //+++++++++++ PHONE STATE CONST +++++++
        public const int SLEEP = 0;
        public const int INCOMING = 1;
        public const int OUTGOING = 2;
        public const int ONGOING = 3;
        //+++++++++++++++++++++++++++++++++++++
        #endregion

        #region PhoneNotify
        public const string CallTerminated = "Terminate";
        public const string CallIncomming = "calling";
        public const string CallOnGoing = "Ongoing";
        public const string CallOutGoing = "Outgoing";
        public const string CallSCOrelease = "SCOrelease";
        #endregion
    }
}
