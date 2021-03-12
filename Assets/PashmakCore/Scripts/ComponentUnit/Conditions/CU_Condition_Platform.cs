using UnityEngine;
using UnityEngine.Events;

namespace Pashmak.Core.CU
{
    public class CU_Condition_Platform : CU_Component, ICondition
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_checkAtStart = false;
        [SerializeField] private RuntimePlatform m_platform = RuntimePlatform.Android;
        [SerializeField] private UnityEvent m_onTrue = new UnityEvent();
        [SerializeField] private UnityEvent m_onFalse = new UnityEvent();


        // property________________________________________________________________
        public bool CheckAtStart { get => m_checkAtStart; private set => m_checkAtStart = value; }
        [SerializeField] public RuntimePlatform Platform { get => m_platform; private set => m_platform = value; }
        public UnityEvent OnTrue { get => m_onTrue; private set => m_onTrue = value; }
        public UnityEvent OnFalse { get => m_onFalse; private set => m_onFalse = value; }


        // implement_______________________________________________________________
        bool ICondition.CheckCondition()
        {
            bool result = m_platform == Application.platform;

            if (result)
                OnTrue.Invoke();
            else
                OnFalse.Invoke();

            return result;
        }
        string ICondition.GetDetails() => string.Format(" {0} == {1}:", "Application.platform", m_platform);


        // monoBehaviour___________________________________________________________
        private void Start()
        {
            if (CheckAtStart)
                Check();
        }


        // function________________________________________________________________  
        public void Check()
        {
            if (!IsActive) return;

            ((ICondition)this).CheckCondition();
        }
    }
}