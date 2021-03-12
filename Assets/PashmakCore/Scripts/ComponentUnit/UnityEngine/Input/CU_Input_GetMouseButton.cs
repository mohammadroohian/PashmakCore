using UnityEngine;
using UnityEngine.Events;



namespace Pashmak.Core.CU._UnityEngine._Input
{
    public class CU_Input_GetMouseButton : CU_Component
    {
        // enum____________________________________________________________________
        public enum MouseButtonCode { m0 = 0, m1 = 1, m2 = 2 }
        public enum MouseButtonDetection { GetMouseButton = 0, GetMouseButtonDown = 1, GetUp = 2 }


        // variable________________________________________________________________
        [SerializeField] private MouseButtonCode m_button = MouseButtonCode.m0;
        [SerializeField] private MouseButtonDetection m_detection = MouseButtonDetection.GetMouseButtonDown;
        [SerializeField] private UnityEvent m_onDetectButton = new UnityEvent();


        // property________________________________________________________________
        public MouseButtonCode Button { get => m_button; set => m_button = value; }
        public MouseButtonDetection Detection { get => m_detection; set => m_detection = value; }
        public UnityEvent OnDetectButton { get => m_onDetectButton; private set => m_onDetectButton = value; }


        // monoBehaviour___________________________________________________________
        void Update()
        {
            if (!IsActive) return;
            switch (Detection)
            {
                case MouseButtonDetection.GetMouseButton:
                    if (Input.GetMouseButton((int)Button)) OnDetectButton.Invoke();
                    break;
                case MouseButtonDetection.GetMouseButtonDown:
                    if (Input.GetMouseButtonDown((int)Button)) OnDetectButton.Invoke();
                    break;
                case MouseButtonDetection.GetUp:
                    if (Input.GetMouseButtonUp((int)Button)) OnDetectButton.Invoke();
                    break;
            }
        }
    }
}