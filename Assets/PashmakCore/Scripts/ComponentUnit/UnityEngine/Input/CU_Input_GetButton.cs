using UnityEngine;
using UnityEngine.Events;



namespace Pashmak.Core.CU._UnityEngine._Input
{
    public class CU_Input_GetButton : CU_Component
    {
        // enum____________________________________________________________________
        public enum ButtonDetection { GetButton = 0, GetButtonDown = 1, GetButtonUp = 2 }


        // variable________________________________________________________________
        [SerializeField] private string m_buttonName = "Jump";
        [SerializeField] private ButtonDetection m_detection = ButtonDetection.GetButtonDown;
        [SerializeField] private UnityEvent m_onDetectButton = new UnityEvent();


        // property________________________________________________________________
        public string ButtonName { get => m_buttonName; set => m_buttonName = value; }
        public ButtonDetection Detection { get => m_detection; set => m_detection = value; }
        public UnityEvent OnDetectButton { get => m_onDetectButton; private set => m_onDetectButton = value; }


        // monoBehaviour___________________________________________________________
        void Update()
        {
            if (!IsActive) return;
            switch (Detection)
            {
                case ButtonDetection.GetButton:
                    if (Input.GetButton(ButtonName)) OnDetectButton.Invoke();
                    break;
                case ButtonDetection.GetButtonDown:
                    if (Input.GetButtonDown(ButtonName)) OnDetectButton.Invoke();
                    break;
                case ButtonDetection.GetButtonUp:
                    if (Input.GetButtonUp(ButtonName)) OnDetectButton.Invoke();
                    break;
            }
        }
    }
}