using System;

namespace CHXQ.XMManager
{
    /// <summary>
    /// д��־�ӿ�
    /// </summary>
    public interface ILog
    {
        void WriteMessage(string message, string logType);
        void WriteMessage(string message, string logType, LogLevel loglevel);
        void WriteError(Exception ex, string logType);          
    }
}
