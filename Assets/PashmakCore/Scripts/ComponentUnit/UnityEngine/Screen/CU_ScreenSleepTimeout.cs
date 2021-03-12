using UnityEngine;

namespace Pashmak.Core.CU._UnityEngine._Screen
{
    public class CU_ScreenSleepTimeout : CU_Component, IDefaultExcute
    {
        // enum____________________________________________________________________
        public enum SleepTimeoutStatus
        {
            NeverSleepNever = 1,
            SystemSetting = 2,
            custom = 3
        }


        // variable________________________________________________________________
        [SerializeField] private bool m_setAtStart = false;
        [SerializeField] private SleepTimeoutStatus m_sleepStatus = SleepTimeoutStatus.NeverSleepNever;
        [SerializeField] private int m_customTimeout = 0;


        // property________________________________________________________________
        public bool SetAtStart { get => m_setAtStart; private set => m_setAtStart = value; }
        public SleepTimeoutStatus SleepStatus { get => m_sleepStatus; set => m_sleepStatus = value; }
        public int CustomTimeout { get => m_customTimeout; set => m_customTimeout = value; }


        // monoBehaviour___________________________________________________________
        void Start()
        {
            if (SetAtStart)
                SetSleepTimeout();
        }


        // override________________________________________________________________
        public override string GetDetails(string gameObjectName, string componentName, string methodName, string methodParams)
        {
            if (SleepStatus != SleepTimeoutStatus.custom)
                return string.Format(" [ {0} ] . Screen.sleepTimeout = {1}", gameObjectName, SleepStatus);
            else
                return string.Format(" [ {0} ] . Screen.sleepTimeout = {1}", gameObjectName, CustomTimeout);
        }


        // implement_______________________________________________________________
        void IDefaultExcute.DefaultExecute() => SetSleepTimeout();


        // function________________________________________________________________
        public void SetSleepTimeout()
        {
            if (!IsActive) return;
            switch (SleepStatus)
            {
                case SleepTimeoutStatus.NeverSleepNever:
                    Screen.sleepTimeout = SleepTimeout.NeverSleep;
                    break;
                case SleepTimeoutStatus.SystemSetting:
                    Screen.sleepTimeout = SleepTimeout.SystemSetting;
                    break;
                case SleepTimeoutStatus.custom:
                    Screen.sleepTimeout = CustomTimeout;
                    break;
            }
        }
    }
}