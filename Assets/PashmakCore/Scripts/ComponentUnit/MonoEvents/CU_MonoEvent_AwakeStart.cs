using UnityEngine;
using UnityEngine.Events;

namespace Pashmak.Core.CU.MonoEvent
{
    public class CU_MonoEvent_AwakeStart : CU_Component
    {
        // enum____________________________________________________________________
        public enum EventDetection
        {
            Awake = 0,
            Start = 1
        }


        // variable________________________________________________________________
        [SerializeField] private EventDetection m_detection = new EventDetection();
        [SerializeField] private UnityEvent m_onDetect = new UnityEvent();


        // property________________________________________________________________
        public EventDetection Detection { get => m_detection; set => m_detection = value; }
        public UnityEvent OnDetect { get => m_onDetect; private set => m_onDetect = value; }


        // monoBehaviour___________________________________________________________
        private void Awake()
        {
            if (m_isActive && Detection == EventDetection.Awake)
                OnDetect.Invoke();
        }
        private void Start()
        {
            if (m_isActive && Detection == EventDetection.Start)
                OnDetect.Invoke();
        }
    }
}