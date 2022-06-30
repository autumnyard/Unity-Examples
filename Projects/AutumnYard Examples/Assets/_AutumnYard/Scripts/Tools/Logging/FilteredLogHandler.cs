using System;
using UnityEngine;

namespace AutumnYard.Tools.Logging
{
    public abstract class FilteredLogHandler : ILogHandler
    {
        private ILogHandler m_DefaultLogHandler = Debug.unityLogger.logHandler;

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            m_DefaultLogHandler.LogException(exception, context);
        }

        public void LogFormat(UnityEngine.LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            if (logType != UnityEngine.LogType.Log)
            {
                m_DefaultLogHandler.LogFormat(logType, context, format, args);
                return;
            }

            var (color, prefix, type) = Process(context);
            if (!CheckLogTypes(type)) return;

#if UNITY_EDITOR
            if (prefix.Equals(string.Empty))
            {
                m_DefaultLogHandler.LogFormat(logType, context, $"<color={color}>{format}</color>", args);
            }
            else
            {
                m_DefaultLogHandler.LogFormat(logType, context, $"<color={color}>[{prefix}]: {format}</color>", args);
            }
#else
            m_DefaultLogHandler.LogFormat(logType, context, format, args);
#endif
        }

        protected abstract (string color, string prefix, LogType type) Process(UnityEngine.Object obj);
        protected abstract bool CheckLogTypes(LogType type);
    }
}
