using UnityEngine;
using UnityEngine.Events;

namespace Pashmak.Core.CU._UnityEngine._Input
{
    public class CU_Input_AccelerationDetection : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private float m_shakeDetectionThreshold = 1.5f;
        [SerializeField] private float m_minShakeInterval = 0.2f;
        [SerializeField] private UnityEvent m_onDetectAcceleration = new UnityEvent();
        [SerializeField] private UnityEvent m_onDetectStopAcceleration = new UnityEvent();


        // property________________________________________________________________
        public bool IsAccelerating { get; private set; }
        public float ShakeDetectionThreshold
        {
            get => m_shakeDetectionThreshold;
            set
            {
                m_shakeDetectionThreshold = value;
                sqrShakeDetectionThreshold = Mathf.Pow(ShakeDetectionThreshold, 2);
            }
        }
        public float MinShakeInterval { get => m_minShakeInterval; set => m_minShakeInterval = value; }
        public float sqrShakeDetectionThreshold { get; private set; }
        public float TimeSinceLastShake { get; private set; }
        public UnityEvent OnDetectAcceleration { get => m_onDetectAcceleration; set => m_onDetectAcceleration = value; }
        public UnityEvent OnDetectStopAcceleration { get => m_onDetectStopAcceleration; set => m_onDetectStopAcceleration = value; }


        // monoBehaviour___________________________________________________________
        private void Awake()
        {
            sqrShakeDetectionThreshold = Mathf.Pow(ShakeDetectionThreshold, 2);
        }
        private void Update()
        {
            if (!IsActive) return;

            if (Time.unscaledTime >= TimeSinceLastShake + MinShakeInterval)
            {
                if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold)
                {
                    IsAccelerating = true;
                    OnDetectAcceleration.Invoke();
                    TimeSinceLastShake = Time.unscaledTime;
                }
                else
                {
                    IsAccelerating = false;
                    OnDetectStopAcceleration.Invoke();
                }
            }
        }
    }
}