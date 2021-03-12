using UnityEngine;
using System;
using System.Text;
using NaughtyAttributes;

namespace Pashmak.Core.CU._UnityEngine
{
    public abstract class CU_TimerUI : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_autoUpdateUI = false;
        [ShowIf("m_autoUpdateUI")]
        [SerializeField] private float m_autoUpdateUIDelay = .1f;
        [SerializeField] private CU_Timer m_timerSource = null;
        private float m_UIUpdateTimer = 0;

        [BoxGroup("Fillter")]
        [SerializeField] private bool m_days = false;
        [BoxGroup("Fillter")]
        [SerializeField] private bool m_hours = false;
        [BoxGroup("Fillter")]
        [SerializeField] private bool m_minutes = true;
        [BoxGroup("Fillter")]
        [SerializeField] private bool m_seconds = true;
        [BoxGroup("Fillter")]
        [SerializeField] private bool m_milliSeconds = true;
        private StringBuilder strBuilder = new StringBuilder();


        // property________________________________________________________________
        public CU_Timer TimerSource { get => m_timerSource; set => m_timerSource = value; }
        public bool AutoUpdateUI { get => m_autoUpdateUI; set => m_autoUpdateUI = value; }
        public float UIUpdateTimer { get => m_UIUpdateTimer; private set => m_UIUpdateTimer = value; }
        public float AutoUpdateUIDelay { get => m_autoUpdateUIDelay; private set => m_autoUpdateUIDelay = value; }


        // monoBehaviour___________________________________________________________
        void Awake()
        {
            if (AutoUpdateUIDelay <= 0)
                AutoUpdateUIDelay = .1f;

            if (m_timerSource == null)
                m_timerSource = GetComponent<CU_Timer>();

            // events
            TimerSource.OnStartTimer.AddListener(UpdateUI);
            TimerSource.OnEndTimer.AddListener(UpdateUI);
            TimerSource.OnPauseTimer.AddListener(UpdateUI);
            TimerSource.OnResumeTimer.AddListener(UpdateUI);
            TimerSource.OnStopTimer.AddListener(UpdateUI);
        }
        private void Update()
        {
            if (!IsActive) return;
            if (!TimerSource.IsRunning) return;

            // UI
            if (AutoUpdateUI)
            {
                UIUpdateTimer += Time.deltaTime;
                if (UIUpdateTimer > AutoUpdateUIDelay)
                {
                    UIUpdateTimer = 0;
                    UpdateUI();
                }
            }
        }


        // function________________________________________________________________
        public void UpdateUI()
        {
            strBuilder.Clear();
            TimeSpan timeSpan = TimeSpan.FromSeconds(TimerSource.TimerValue);

            int days = timeSpan.Days;
            int hours = timeSpan.Hours;
            int minute = timeSpan.Minutes;
            int seconds = timeSpan.Seconds;
            int milliSeconds = timeSpan.Milliseconds;

            if (!m_days)
                hours += days * 24;
            if (!m_hours)
                minute += hours * 60;
            if (!m_minutes)
                seconds += minute * 60;
            if (!m_seconds)
                milliSeconds += seconds * 1000;

            if (m_days)
            {
                if (strBuilder.Length != 0) strBuilder.Append(":");
                strBuilder.Append(days.ToString("00"));
            }
            if (m_hours)
            {
                if (strBuilder.Length != 0) strBuilder.Append(":");
                strBuilder.Append(hours.ToString("00"));
            }
            if (m_minutes)
            {
                if (strBuilder.Length != 0) strBuilder.Append(":");
                strBuilder.Append(minute.ToString("00"));
            }
            if (m_seconds)
            {
                if (strBuilder.Length != 0) strBuilder.Append(":");
                strBuilder.Append(seconds.ToString("00"));
            }
            if (m_milliSeconds)
            {
                if (strBuilder.Length != 0) strBuilder.Append(":");
                strBuilder.Append(milliSeconds.ToString("00").Substring(0, 2));
            }

            SetUIText(strBuilder.ToString());
        }
        public virtual void SetUIText(string value)
        {
            Debug.LogError("SetUIText function not implemented!");
        }
    }
}