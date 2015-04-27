using System;
using System.IO;
using System.Text;
using System.Threading;


namespace OrdiniCatCe.Gui.Utils.Logging
{
    internal class LogFile
    {
        private readonly Object _thisLock = new Object();
        private const String _extension = ".log";

        private readonly String _logName;
        private readonly String _logPath;

        private String FullPath
        {
            get
            {
                return Path.Combine(_logPath, String.Concat(string.Format("{0}_{1}", _logName, DateTime.Now.ToString("yyyyMMdd")), _extension));
            }
        }

        internal LogFile(String logPath, String logName)
        {
            _logName = logName;
            _logPath = logPath;
        }

        private void Append(string data)
        {
            lock (_thisLock)
            {
                try
                {
                    using (StreamWriter logSW = new StreamWriter(FullPath, true, Encoding.UTF8))
                    {
                        logSW.Write(data);
                        logSW.Flush();
                    }
                }
                catch (ThreadAbortException)
                {
                    throw;
                }
                catch (Exception)
                {
                }
            }
        }

        internal void AppendLn(string data)
        {
            Append(data + Environment.NewLine);
        }
    }
}
