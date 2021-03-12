using UnityEngine;
using UnityEngine.Events;



namespace Pashmak.Core.CU._UnityEngine._Input
{
    public class CU_Input_GetAxis : CU_Component
    {
        // enum____________________________________________________________________
        public enum AxisDetection { Near_1 = 0, Near_0 = 1, Near_minus_1 = 2, GraterThan_0 = 3, LessThan_0 = 4 }


        // variable________________________________________________________________
        [SerializeField] private string m_axisName = "Horizontal";
        [SerializeField] private AxisDetection m_detection = AxisDetection.GraterThan_0;
        [SerializeField] private bool m_isRawAxis = false;
        [SerializeField] private UnityEvent m_onDetectButton = new UnityEvent();
        [SerializeField] private float m_nearnessThreshold = 0.001f;


        // property________________________________________________________________
        public string AxisName { get => m_axisName; set => m_axisName = value; }
        public AxisDetection Detection { get => m_detection; set => m_detection = value; }
        public bool IsRawAxis { get => m_isRawAxis; set => m_isRawAxis = value; }
        public UnityEvent OnDetectButton { get => m_onDetectButton; private set => m_onDetectButton = value; }
        public float NearnessThreshold { get => m_nearnessThreshold; set => m_nearnessThreshold = value; }



        // monoBehaviour___________________________________________________________
        void Update()
        {
            if (!IsActive) return;
            float tmpAxisValue = 0;
            if (IsRawAxis)
                tmpAxisValue = Input.GetAxisRaw(AxisName);
            else
                tmpAxisValue = Input.GetAxis(AxisName);

            switch (Detection)
            {
                case AxisDetection.Near_1:
                    if (tmpAxisValue > 0 && Mathf.Abs(tmpAxisValue - 1) < NearnessThreshold)
                        OnDetectButton.Invoke();
                    break;
                case AxisDetection.Near_minus_1:
                    if (tmpAxisValue < 0 && Mathf.Abs(Mathf.Abs(tmpAxisValue) - 1) < NearnessThreshold)
                        OnDetectButton.Invoke();
                    break;
                case AxisDetection.Near_0:
                    if (Mathf.Abs(tmpAxisValue) < NearnessThreshold)
                        OnDetectButton.Invoke();
                    break;
                case AxisDetection.GraterThan_0:
                    if (tmpAxisValue > 0)
                        OnDetectButton.Invoke();
                    break;
                case AxisDetection.LessThan_0:
                    if (tmpAxisValue < 0)
                        OnDetectButton.Invoke();
                    break;
            }
        }
    }
}