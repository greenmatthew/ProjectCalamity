using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PC
{
    public static class Debug
    {
        #region Fields

        #region Consts Fields

        private const string Info_Color_Code = "#98999b";
        private const string Warning_Color_Code = "#b57f00";
        private const string Error_Color_Code = "#780000";
        private const string URL_Color_Code = "#663366";
        private const string Path_Color_Code = "#3366BB";
        private const string Response_Code_Color_Code = "orange";

        #endregion Consts Fields

        #region Public Fields

        public static readonly List<string> History = new List<string>();
        public static readonly Queue<string> Backlog = new Queue<string>();

        #endregion Public Fields

        #region Protected Fields
        #endregion Protected Fields

        #region Private Fields
        #endregion Private Fields

        #endregion Fields

    //----------------------------------------------------------------------------------------------------------------------

        #region Methods
    
        #region Public Methods

        public static void Log(object message)
        {
            Log(message, null);
        }

        public static void Log(object message, Object context)
        {
            if (context != null)
                UnityEngine.Debug.Log(message, context);
            else
                UnityEngine.Debug.Log(message);

            AddMessage(CreateMessage(MessageType.INFO, message));
        }

        public static void LogWarning(object message)
        {
            LogWarning(message, null);
        }

        public static void LogWarning(object message, Object context)
        {
            if (context != null)
                UnityEngine.Debug.LogWarning(message, context);
            else
                UnityEngine.Debug.LogWarning(message);

            AddMessage(CreateMessage(MessageType.WARNING, message));
        }

        public static void LogError(object message)
        {
            LogError(message, null);
        }

        public static void LogError(object message, Object context)
        {
            if (context != null)
                UnityEngine.Debug.LogError(message, context);
            else
                UnityEngine.Debug.LogError(message);

            AddMessage(CreateMessage(MessageType.ERROR, message));
        }

        public static string FormatBold(string message) => $"<b>{message}</b>";
        public static string FormatItalic(string message) => $"<i>{message}</i>";
        public static string FormatColor(string message, string colorCode) => $"<color={colorCode}>{message}</color>";
        public static string FormatURL(string url) => FormatColor(url, URL_Color_Code);
        public static string FormatPath(string path) => FormatColor(path, Path_Color_Code);
        public static string FormatResponseCode(long responseCode) => FormatColor(responseCode.ToString(), Response_Code_Color_Code);

        #endregion Public Methods

        #region Protected Methods
        #endregion Protected Methods

        #region Private Methods
    
        private static string GetTimeCode()
        {
            var time = System.DateTime.Now;
            var hhmmss = time.ToString("hh:mm:ss");
            return $"{hhmmss}";
        }

        private static string CreateMessage(MessageType type, object message)
        {
            StringBuilder sb = new StringBuilder();
            switch (type)
            {
                case MessageType.INFO:
                    sb.Append(FormatColor("[INFO]", Info_Color_Code));
                    break;
                case MessageType.WARNING:
                    sb.Append(FormatColor("[WARNING]", Warning_Color_Code));
                    break;
                case MessageType.ERROR:
                    sb.Append(FormatColor("[ERROR]", Error_Color_Code));
                    break;
            }
            sb.Append('[');
            sb.Append(GetTimeCode());
            sb.Append(']');
            sb.Append(message.ToString());

            return sb.ToString();
        }

        private static void AddMessage(string message)
        {
            History.Add(message);
            Backlog.Enqueue(message);
        }

        #endregion Private Methods

        #endregion Methods

    //----------------------------------------------------------------------------------------------------------------------

        #region Enums, Structs, Classes

        private enum MessageType
        {
            INFO, WARNING, ERROR
        }

        #endregion Enums, Structs, Classes
    }
}