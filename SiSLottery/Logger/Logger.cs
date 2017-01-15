using Logger.Interfaces;
using System.Diagnostics;

namespace Logger
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
