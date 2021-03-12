using Pashmak.Core.Condition;
using UnityEngine;
using UnityEngine.Events;


namespace Pashmak.Core.CU._UnityEngine._Application
{
    public class CU_Condition_InternetReachability : CU_Component, ICondition, IDebug
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_debugMode = false;
        [SerializeField] private bool m_checkAtStart = false;
        [SerializeField] private NetworkReachability m_status = new NetworkReachability();
        [SerializeField] private Value_conditionElement m_condition = new Value_conditionElement();
        [SerializeField] private UnityEvent m_onTrue = new UnityEvent();
        [SerializeField] private UnityEvent m_onFalse = new UnityEvent();


        // property________________________________________________________________
        public bool DebugMode { get => m_debugMode; set => m_debugMode = value; }
        public bool CheckAtStart { get => m_checkAtStart; private set => m_checkAtStart = value; }
        public NetworkReachability Status { get => m_status; set => m_status = value; }
        public Value_conditionElement Condition { get => m_condition; set => m_condition = value; }
        public UnityEvent OnTrue { get => m_onTrue; private set => m_onTrue = value; }
        public UnityEvent OnFalse { get => m_onFalse; private set => m_onFalse = value; }


        // monoBehaviour___________________________________________________________
        private void Start()
        {
            if (CheckAtStart)
                CheckInternetReachability();
        }


        // implement_______________________________________________________________
        bool ICondition.CheckCondition()
        {
            bool result = false;
            switch (Condition)
            {
                case Value_conditionElement.Equal:
                    if (Application.internetReachability == Status)
                        result = true;
                    break;
                case Value_conditionElement.NotEqual:
                    if (Application.internetReachability != Status)
                        result = true;
                    break;
            }

            if (DebugMode) Debug.Log("condition result:" +
                                        result +
                                        "\n Application.internetReachability: " +
                                        Application.internetReachability, this);

            if (result)
                OnTrue.Invoke();
            else
                OnFalse.Invoke();

            return result;
        }
        string ICondition.GetDetails()
        {
            switch (Condition)
            {
                case Value_conditionElement.Equal:
                    return string.Format("Application.internetReachability == {0}", Status);
                case Value_conditionElement.NotEqual:
                    return string.Format("Application.internetReachability != {0}", Status);
                default:
                    return "";
            }
        }


        // function________________________________________________________________  
        public void CheckInternetReachability()
        {
            if (!IsActive) return;

            ((ICondition)this).CheckCondition();
        }
    }
}