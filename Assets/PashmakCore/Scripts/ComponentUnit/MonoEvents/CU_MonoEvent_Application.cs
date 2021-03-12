using UnityEngine;
using UnityEngine.Events;

namespace Pashmak.Core.CU.MonoEvent
{
    public class CU_MonoEvent_Application : CU_Component
    {
        // enum____________________________________________________________________
        public enum EventDetection
        {
            Focus = 0,
            Pause = 1,
            Quit = 2
        }


        // variable________________________________________________________________
        [SerializeField] private EventDetection m_detection = new EventDetection();
        [SerializeField] private bool m_enterValueFocusPause = false;
        [SerializeField] private UnityEvent m_onDetect = new UnityEvent();


        // property________________________________________________________________
        public EventDetection Detection { get => m_detection; set => m_detection = value; }
        public bool EnterValueFocusPause { get => m_enterValueFocusPause; set => m_enterValueFocusPause = value; }
        public UnityEvent OnDetect { get => m_onDetect; private set => m_onDetect = value; }


        // monoBehaviour___________________________________________________________
        private void OnApplicationFocus(bool focus)
        {
            if (IsActive && Detection == EventDetection.Focus && EnterValueFocusPause == focus)
                OnDetect.Invoke();
        }
        private void OnApplicationPause(bool pause)
        {
            if (IsActive && Detection == EventDetection.Pause && EnterValueFocusPause == pause)
                OnDetect.Invoke();
        }
        private void OnApplicationQuit()
        {
            if (IsActive && Detection == EventDetection.Quit)
                OnDetect.Invoke();
        }
    }
}