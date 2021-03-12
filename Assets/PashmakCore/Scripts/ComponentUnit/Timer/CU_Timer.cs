using UnityEngine;
using UnityEngine.Events;
using Pashmak.Core.Common;

namespace Pashmak.Core.CU._UnityEngine
{
    public class CU_Timer : CU_Component
    {
        // enum____________________________________________________________________
        public enum TimerRunningState
        {
            Initialize = 0,
            Start = 1,
            Stop = 2,
            Pause = 3,
            Resume = 4
        }


        // variable________________________________________________________________
        [SerializeField] private bool m_StartTimerAtStart = false;
        [SerializeField] private Order m_timerOrder = Order.Ascending;
        private bool m_isRunning = false;
        [SerializeField] private float m_duration = 10; // value is positive float number in seconds
        [SerializeField] private UnityEvent m_onStartTimer = new UnityEvent();
        [SerializeField] private UnityEvent m_onEndTimer = new UnityEvent();
        [SerializeField] private UnityEvent m_onStopTimer = new UnityEvent();
        [SerializeField] private UnityEvent m_onPauseTimer = new UnityEvent();
        [SerializeField] private UnityEvent m_onResumeTimer = new UnityEvent();
        private float m_timerValue = 0.0f;
        private TimerRunningState m_state = TimerRunningState.Initialize;


        // property________________________________________________________________
        public bool StartTimerAtStart { get => m_StartTimerAtStart; private set => m_StartTimerAtStart = value; }
        public Order TimerOrder { get => m_timerOrder; set => m_timerOrder = value; }
        public bool IsRunning { get => m_isRunning; private set => m_isRunning = value; }
        public float Duration { get => m_duration; set => m_duration = value; }
        public UnityEvent OnStartTimer { get => m_onStartTimer; private set => m_onStartTimer = value; }
        public UnityEvent OnEndTimer { get => m_onEndTimer; private set => m_onEndTimer = value; }
        public UnityEvent OnStopTimer { get => m_onStopTimer; private set => m_onStopTimer = value; }
        public UnityEvent OnPauseTimer { get => m_onPauseTimer; private set => m_onPauseTimer = value; }
        public UnityEvent OnResumeTimer { get => m_onResumeTimer; private set => m_onResumeTimer = value; }
        public float TimerValue { get => m_timerValue; private set => m_timerValue = value; }
        public TimerRunningState State { get => m_state; set => m_state = value; }


        // monoBehaviour___________________________________________________________
        void Awake()
        {
            // check duration is not zero
            if (Duration < 0)
                Duration = 0;
        }
        void Start()
        {
            // auto start timer
            if (StartTimerAtStart)
                StartTimer();
        }
        void FixedUpdate()
        {
            if (!IsActive) return;
            if (!IsRunning) return;

            // Timer
            switch (TimerOrder)
            {
                case Order.Ascending:
                    TimerValue += Time.fixedDeltaTime;
                    if (TimerValue > Duration)
                        EndTimer();
                    break;
                case Order.Descending:
                    TimerValue -= Time.fixedDeltaTime;
                    if (TimerValue < 0)
                        EndTimer();
                    break;
            }
        }


        // function________________________________________________________________
        public void StartTimer()
        {
            if (!IsActive) return;

            State = TimerRunningState.Start;
            OnStartTimer.Invoke();

            // set start value
            switch (TimerOrder)
            {
                case Order.Ascending:
                    TimerValue = 0;
                    break;
                case Order.Descending:
                    TimerValue = Duration;
                    break;
            }

            IsRunning = true;
        }
        public void StopTimer()
        {
            if (!IsActive) return;

            State = TimerRunningState.Stop;
            OnStopTimer.Invoke();
            IsRunning = false;
        }
        public void PauseTimer()
        {
            if (!IsActive) return;

            State = TimerRunningState.Pause;
            OnPauseTimer.Invoke();
            IsRunning = false;
        }
        public void ResumeTimer()
        {
            if (!IsActive) return;

            State = TimerRunningState.Resume;
            OnResumeTimer.Invoke();
            IsRunning = true;
        }
        private void EndTimer()
        {
            if (!IsActive) return;

            // set end value
            switch (TimerOrder)
            {
                case Order.Ascending:
                    TimerValue = Duration;
                    break;
                case Order.Descending:
                    TimerValue = 0;
                    break;
            }

            State = TimerRunningState.Initialize;
            OnEndTimer.Invoke();
            IsRunning = false;
        }
        public void PlusTimerValue(float value)
        {
            TimerValue += value;
        }
        public void MinusTimerValue(float value)
        {
            TimerValue -= value;
        }
        public void ChangeTimerWithDurationPercent(float value)
        {
            // value is positive between 0 and 100 or above

            if (float.IsInfinity(Duration)) return;

            float factor = value / 100;
            TimerValue += Duration * factor;
        }
        public void ChangeTimerWithLeftTimePercent(float value)
        {
            // value is positive between 0 and 100 or above

            if (float.IsInfinity(Duration)) return;

            float factor = value / 100;
            TimerValue += (Duration - TimerValue) * factor;
        }
        public void ChangeTimerWithTimerPercent(float value)
        {
            // value is positive between 0 and 100 or above

            float factor = value / 100;
            TimerValue += TimerValue * factor;
        }
    }
}