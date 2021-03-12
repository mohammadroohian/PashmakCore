using UnityEngine;
using UnityEngine.Events;



namespace Pashmak.Core.CU._UnityEngine._Input
{
    public class CU_Input_GetKey : CU_Component
    {
        // enum____________________________________________________________________
        public enum KeyDetection { GetKey = 0, GetKeyDown = 1, GetKeyUp = 2 }


        // variable________________________________________________________________
        [SerializeField] private KeyCode m_key = KeyCode.W;
        [SerializeField] private KeyDetection m_detection = KeyDetection.GetKeyDown;
        [SerializeField] private UnityEvent m_onDetectKey = new UnityEvent();


        // property________________________________________________________________
        public KeyCode Key { get => m_key; set => m_key = value; }
        public KeyDetection Detection { get => m_detection; set => m_detection = value; }
        public UnityEvent OnDetectKey { get => m_onDetectKey; private set => m_onDetectKey = value; }


        // monoBehaviour___________________________________________________________
        void Update()
        {
            if (!IsActive) return;
            switch (Detection)
            {
                case KeyDetection.GetKey:
                    if (Input.GetKey(Key)) OnDetectKey.Invoke();
                    break;
                case KeyDetection.GetKeyDown:
                    if (Input.GetKeyDown(Key)) OnDetectKey.Invoke();
                    break;
                case KeyDetection.GetKeyUp:
                    if (Input.GetKeyUp(Key)) OnDetectKey.Invoke();
                    break;
            }
        }
    }
}