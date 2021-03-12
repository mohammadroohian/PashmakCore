using UnityEngine;
using UnityEngine.Events;


namespace Pashmak.Core.CU
{
    public class CU_Counter : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_autoReset = false;
        [SerializeField] private int m_detectionCount = 3;
        [SerializeField] private UnityEvent m_onDetect = new UnityEvent();
        [SerializeField] private UnityEvent m_onReset = new UnityEvent();
        [SerializeField] private UnityEvent m_onChange = new UnityEvent();
        [SerializeField] private UnityEvent m_onPlus = new UnityEvent();
        [SerializeField] private UnityEvent m_onMinus = new UnityEvent();
        private int m_count;
        private bool m_isDetected = false;


        // property________________________________________________________________
        public bool AutoReset { get => m_autoReset; set => m_autoReset = value; }
        public int DetectionCount { get => m_detectionCount; set => m_detectionCount = value; }
        public UnityEvent OnDetect { get => m_onDetect; private set => m_onDetect = value; }
        public UnityEvent OnReset { get => m_onReset; private set => m_onReset = value; }
        public UnityEvent OnChange { get => m_onChange; private set => m_onChange = value; }
        public UnityEvent OnPlus { get => m_onPlus; private set => m_onPlus = value; }
        public UnityEvent OnMinus { get => m_onMinus; private set => m_onMinus = value; }
        public int Count { get => m_count; private set { m_count = value; OnChange.Invoke(); } }
        public bool IsDetected { get => m_isDetected; private set => m_isDetected = value; }


        // function________________________________________________________________
        public void Plus(int value)
        {
            if (!IsActive) return;

            Count += value;
            OnPlus.Invoke();

            CheckDetection();
        }
        public void Minus(int value)
        {
            if (!IsActive) return;

            Count -= value;
            OnMinus.Invoke();

            CheckDetection();
        }
        private void CheckDetection()
        {
            if (!IsActive) return;

            if (Count < DetectionCount || IsDetected)
                return;

            IsDetected = true;
            OnDetect.Invoke();
            if (AutoReset)
                ResetCounter();
        }
        public void ResetCounter()
        {
            if (!IsActive) return;

            Count = 0;
            IsDetected = false;
            OnReset.Invoke();
        }
    }
}