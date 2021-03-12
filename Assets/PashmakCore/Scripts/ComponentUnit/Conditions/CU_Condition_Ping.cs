using System.Collections;
using UnityEngine;
using UnityEngine.Events;

#if !UNITY_WEBGL
namespace Pashmak.Core.CU
{
    public class CU_Condition_Ping : CU_Component, ICondition, IDebug
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_debugMode = false;
        [SerializeField] private bool m_checkAtStart = false;
        [SerializeField] private string m_pingAddress = "8.8.8.8";
        [SerializeField] private float m_timeout = 10f;
        [SerializeField] private UnityEvent m_onTrue = new UnityEvent();
        [SerializeField] private UnityEvent m_onFalse = new UnityEvent();


        // property________________________________________________________________
        public bool CheckAtStart { get => m_checkAtStart; private set => m_checkAtStart = value; }
        public bool DebugMode { get => m_debugMode; set => m_debugMode = value; }
        protected string PingAddress { get => m_pingAddress; set => m_pingAddress = value; }
        protected float Timeout { get => m_timeout; set => m_timeout = value; }
        public UnityEvent OnTrue { get => m_onTrue; private set => m_onTrue = value; }
        public UnityEvent OnFalse { get => m_onFalse; private set => m_onFalse = value; }
        public bool IsConnected { get; private set; }


        // implement_______________________________________________________________
        bool ICondition.CheckCondition()
        {
            Ping(PingAddress);
            return IsConnected;
        }
        string ICondition.GetDetails() => string.Format(" Ping(\"{0}\") == {1}", PingAddress, "connected");


        // monoBehaviour___________________________________________________________
        private void Start()
        {
            if (CheckAtStart)
                Check();
        }


        // function________________________________________________________________
        public void Ping(string url) => StartCoroutine(CheckConnection());
        private IEnumerator CheckConnection()
        {
            float startTime = Time.timeSinceLevelLoad;
            var ping = new Ping(PingAddress);

            while (true)
            {
                // debug
                if (m_debugMode) Debug.Log("Checking network...", this);

                // check ping
                if (ping.isDone)
                {
                    // debug
                    if (m_debugMode) Debug.Log("Network available.", this);

                    IsConnected = true;
                    OnTrue.Invoke();
                    yield break;
                }
                if (Time.timeSinceLevelLoad - startTime > Timeout)
                {
                    // debug
                    if (m_debugMode) Debug.Log("No network.", this);

                    IsConnected = false;
                    OnFalse.Invoke();
                    yield break;
                }

                yield return new WaitForEndOfFrame();
            }
        }
        public void Check()
        {
            if (!IsActive) return;

            ((ICondition)this).CheckCondition();
        }
    }
}
#endif