using UnityEngine;
using UnityEngine.Events;

namespace Pashmak.Core.CU.MonoEvent
{
    public class CU_MonoEvent_Update : CU_Component
    {
        // enum____________________________________________________________________
        public enum EventDetection
        {
            Update = 0,
            FixedUpdate = 1,
            LateUpdate = 2
        }


        // variable________________________________________________________________
        [SerializeField] private EventDetection m_detection = EventDetection.Update;
        [SerializeField] private UnityEvent m_onDetect = new UnityEvent();


        // property________________________________________________________________
        public EventDetection Detection { get => m_detection; set => m_detection = value; }
        public UnityEvent OnDetect { get => m_onDetect; private set => m_onDetect = value; }


        // monoBehaviour___________________________________________________________
        private void Update()
        {
            if (m_isActive && Detection == EventDetection.Update)
                OnDetect.Invoke();
        }
        private void FixedUpdate()
        {
            if (m_isActive && Detection == EventDetection.FixedUpdate)
                OnDetect.Invoke();
        }
        private void LateUpdate()
        {
            if (m_isActive && Detection == EventDetection.LateUpdate)
                OnDetect.Invoke();
        }
    }
}