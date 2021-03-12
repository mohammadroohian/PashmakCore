using UnityEngine;
using UnityEngine.Events;


namespace Pashmak.Core.CU._UnityEngine._Transform
{
    public class CU_Event : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private UnityEvent m_customEvent = new UnityEvent();


        // property________________________________________________________________
        public UnityEvent CustomEvent { get => m_customEvent; private set => m_customEvent = value; }


        // function________________________________________________________________
        public void InvokeEvent()
        {
            if (!IsActive) return;
            CustomEvent.Invoke();
        }
    }
}