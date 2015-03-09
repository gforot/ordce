using System;
using System.IO;


namespace OrdiniCatCe.Gui.Utils.Logging
{
    class LogHelper
    {
        private static readonly Object _lockObj = new Object();
        private const String _logName = "OrdiniCatCe";
        private static LogHelper _instance;
        private LogFile _logFile;

        public static LogHelper GetInstance()
        {
            lock (_lockObj)
            {
                return _instance ?? (_instance = new LogHelper());
            }
        }

        private LogHelper()
        {
            _logFile = new LogFile(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), _logName);
        }

        public void Log(string message)
        {
            string messageWithDateTime = String.Format("{0} {1}", DateTime.Now.ToString("F"), message);
            _logFile.AppendLn(messageWithDateTime);
        }

        public void Log(string format, string arg1)
        {
            Log(string.Format(format, arg1));
        }

        public void Log(string format, string arg1, string arg2)
        {
            Log(string.Format(format, arg1, arg2));
        }

        public void LogException(Exception exc)
        {
            Log("****************************************************************************");
            Log("********************************* EXCEPTION ********************************");
            Log("****************************************************************************");
            Log("Message : ", exc.Message);
            Log("StackTrace: ", exc.StackTrace);
            Log("***************************** END EXCEPTION ********************************");
        }
    }
}
