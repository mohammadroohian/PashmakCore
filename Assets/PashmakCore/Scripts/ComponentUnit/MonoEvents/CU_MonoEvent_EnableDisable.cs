using UnityEngine;
using UnityEngine.Events;

namespace Pashmak.Core.CU.MonoEvent
{
    public class CU_MonoEvent_EnableDisable : CU_Component
    {
        // enum____________________________________________________________________
        public enum EventDetection
        {
            Enable = 0,
            Disable = 1
        }


        // variable________________________________________________________________
        [SerializeField] private EventDetection m_detection = EventDetection.Enable;
        [SerializeField] private UnityEvent m_onDetect = new UnityEvent();


        // property________________________________________________________________
        public EventDetection Detection { get => m_detection; set => m_detection = value; }
        public UnityEvent OnDetect { get => m_onDetect; private set => m_onDetect = value; }


        // monoBehaviour___________________________________________________________
        private void OnEnable()
        {
            if (m_isActive && Detection == EventDetection.Enable)
                OnDetect.Invoke();
        }
        private void OnDisable()
        {
            if (m_isActive && Detection == EventDetection.Disable)
                OnDetect.Invoke();
        }
    }
}