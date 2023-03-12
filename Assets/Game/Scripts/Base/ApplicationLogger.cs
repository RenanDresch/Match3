using System;
using System.Text;
using UnityEngine;

namespace Game
{
    public enum LogLevel
    {
        /// <summary>
        ///     <para>Exceptions are always logged.</para>
        /// </summary>
        Exception = -1,

        /// <summary>
        ///     <para>Only unexpected errors and failures are logged.</para>
        /// </summary>
        Error = 0,

        /// <summary>
        ///     <para>
        ///         Abnormal situations that may result in problems are reported, in addition to anything from the LogLevel.Error
        ///         level.
        ///     </para>
        /// </summary>
        Warn = 1,

        /// <summary>
        ///     <para>High-level informational messages are reported, in addition to anything from the LogLevel.Warn level.</para>
        /// </summary>
        Info = 2,

        /// <summary>
        ///     <para>Debugging messages are reported, in addition to anything from the LogLevel.Verbose level.</para>
        /// </summary>
        Debug = 3,

        /// <summary>
        ///     <para>Detailed informational messages are reported, in addition to anything from the LogLevel.Info level.</para>
        /// </summary>
        Verbose = 4,

        /// <summary>
        ///     <para>Extremely detailed messages are reported, in addition to anything from the LogLevel.Debug level.</para>
        /// </summary>
        Silly = 5
    }

    public class LogAggregator
    {
        public readonly LogLevel Level;
        public readonly StringBuilder LogBuilder = new();

        public LogAggregator(
            LogLevel level
        )
        {
            Level = level;
            LogBuilder.Append(WithDecoration(level));
        }

        static string WithDecoration(
            LogLevel logLevel
        )
        {
            return logLevel switch
            {
                LogLevel.Error => "<color=red>",
                LogLevel.Warn => "<color=yellow>",
                LogLevel.Exception => "<color=magenta>",
                LogLevel.Info => "<color=cyan>",
                LogLevel.Debug => "<color=white>",
                LogLevel.Verbose => "<color=gray>",
                LogLevel.Silly => "<color=black>",
                _ => throw new ArgumentOutOfRangeException(
                    nameof(logLevel),
                    logLevel,
                    null)
            };
        }
    }

    public static class ApplicationLogger
    {
        static LogLevel logLevel;

        #if LOG_LEVEL_SILLY
        const LogLevel APP_LOG_LEVEL = LogLevel.Silly;
        #elif LOG_LEVEL_VERBOSE
        const LogLevel APP_LOG_LEVEL = LogLevel.Verbose;
        #elif LOG_LEVEL_DEBUG
        const LogLevel APP_LOG_LEVEL = LogLevel.Debug;
        #elif LOG_LEVEL_INFO
        const LogLevel APP_LOG_LEVEL = LogLevel.Info;
        #elif LOG_LEVEL_WARN
        const LogLevel APP_LOG_LEVEL = LogLevel.Warn;
        #else
        const LogLevel APP_LOG_LEVEL = LogLevel.Error;
        #endif
        static LogLevel LogLevel
            => APP_LOG_LEVEL;

        public static LogAggregator WithLevel(
            LogLevel logLevel
        )
        {
            LogAggregator logAggregator = null;
            if (LogLevel >= logLevel)
            {
                logAggregator = new LogAggregator(logLevel);
            }

            return logAggregator;
        }

        public static void Log(
            this LogAggregator aggregator,
            string log
        )
        {
            aggregator.LogBuilder.Append(log);
            aggregator.LogBuilder.Append("</color>");
            UnityLogger(aggregator.Level)(aggregator.LogBuilder.ToString());
        }

        public static void LogException(
            Exception exception
        )
        {
            Debug.LogException(exception);
        }

        static Action<string> UnityLogger(
            LogLevel logLevel
        )
        {
            return logLevel switch
            {
                    LogLevel.Error => Debug.LogError,
                    LogLevel.Warn  => Debug.LogWarning,
                    _              => Debug.Log
            };
        }
    }
}