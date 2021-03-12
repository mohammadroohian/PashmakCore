using UnityEngine;


namespace Pashmak.Core.CU._UnityEngine._Debug
{
    public class CU_Debug : CU_Component, IDefaultExcute
    {
        // enum____________________________________________________________________
        public enum LogType
        {
            log = 0,
            LogError = 1,
            LogWarning = 2
        }


        // variable________________________________________________________________
        [SerializeField] LogType m_type = LogType.log;
        [SerializeField] string m_message = "This is a debug log!";


        // property________________________________________________________________
        public LogType LType { get => m_type; set => m_type = value; }
        public string Message { get => m_message; set => m_message = value; }


        // override________________________________________________________________
        public override string GetDetails(string gameObjectName, string componentName, string methodName, string methodParams)
        {
            switch (LType)
            {
                case LogType.log:
                    methodName = "Debug.Log";
                    break;
                case LogType.LogError:
                    methodName = "Debug.LogError";
                    break;
                case LogType.LogWarning:
                    methodName = "Debug.LogWarning";
                    break;
            }

            methodParams = string.Format("\"{0}\"", Message);
            componentName = "";
            return base.GetDetails(gameObjectName, componentName, methodName, methodParams);
        }


        // implement_______________________________________________________________
        void IDefaultExcute.DefaultExecute()
        {
            if (!IsActive) return;
            ShowLog();
        }


        // Function________________________________________________________________
        public void ShowLog()
        {
            if (!IsActive) return;

            switch (LType)
            {
                case LogType.log:
                    Debug.Log(Message, this);
                    break;
                case LogType.LogError:
                    Debug.LogError(Message, this);
                    break;
                case LogType.LogWarning:
                    Debug.LogWarning(Message, this);
                    break;
            }
        }
    }
}