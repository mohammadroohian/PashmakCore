using UnityEngine;
using UnityEngine.Events;

namespace Pashmak.Core.CU.MonoEvent
{
    public class CU_MonoEvent_Mouse : CU_Component
    {
        // enum____________________________________________________________________
        public enum EventDetection
        {
            OnMouseDown = 0,
            OnMouseUp = 1,
            OnMouseEnter = 2,
            OnMouseExit = 3,
            OnMouseOver = 4,
            OnMouseDrag = 5,
        }


        // variable________________________________________________________________
        [SerializeField] private EventDetection m_detection = EventDetection.OnMouseDown;
        [SerializeField] private UnityEvent m_onDetect = new UnityEvent();



        // property________________________________________________________________
        public EventDetection Detection { get => m_detection; set => m_detection = value; }
        public UnityEvent OnDetect { get => m_onDetect; private set => m_onDetect = value; }


        // monoBehaviour___________________________________________________________
        void OnMouseDown()
        {
            if (IsActive && Detection == EventDetection.OnMouseDown)
                OnDetect.Invoke();
        }
        void OnMouseDrag()
        {
            if (IsActive && Detection == EventDetection.OnMouseDrag)
                OnDetect.Invoke();
        }
        void OnMouseEnter()
        {
            if (IsActive && Detection == EventDetection.OnMouseEnter)
                OnDetect.Invoke();
        }
        void OnMouseExit()
        {
            if (IsActive && Detection == EventDetection.OnMouseExit)
                OnDetect.Invoke();
        }
        void OnMouseOver()
        {
            if (IsActive && Detection == EventDetection.OnMouseOver)
                OnDetect.Invoke();
        }
        void OnMouseUp()
        {
            if (IsActive && Detection == EventDetection.OnMouseUp)
                OnDetect.Invoke();
        }
    }
}