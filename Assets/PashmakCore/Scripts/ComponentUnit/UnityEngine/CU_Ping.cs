using System.Collections;
using UnityEngine;
using UnityEngine.Events;

#if !UNITY_WEBGL
namespace Pashmak.Core.CU._UnityEngine
{
    // sample of return true:
    // Reply from 0.0.0.0: Destination net unreachable.
    // Reply from 8.8.8.8: bytes=32 time=50ms TTL=45

    // sample of return false:
    // Request timed out.
    public class CU_Ping : CU_Component, IDebug
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_debugMode = false;
        [SerializeField] private bool m_checkAtStart = false;
        [SerializeField] private string m_pingAddress = "8.8.8.8";
        [SerializeField] private float m_timeout = 10f;
        [SerializeField] private UnityEvent m_on_connected = new UnityEvent();
        [SerializeField] private UnityEvent m_on_disconnected = new UnityEvent();


        // property________________________________________________________________
        bool IDebug.DebugMode { get => m_debugMode; set => m_debugMode = value; }
        public bool CheckAtStart { get => m_checkAtStart; private set => m_checkAtStart = value; }
        public bool IsConnected { get; private set; }
        public string PingAddress { get => m_pingAddress; set => m_pingAddress = value; }
        public float Timeout { get => m_timeout; set => m_timeout = value; }
        public UnityEvent On_connected { get => m_on_connected; private set => m_on_connected = value; }
        public UnityEvent On_disconnected { get => m_on_disconnected; private set => m_on_disconnected = value; }


        // monoBehaviour___________________________________________________________
        private void Start()
        {
            if (CheckAtStart)
                SendPing();
        }


        // function________________________________________________________________
        public void SendPing()
        {
            if (!IsActive) return;
            StartCoroutine(CheckConnection());
        }
        private IEnumerator CheckConnection()
        {
            IsConnected = false;
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

                    // call event
                    On_connected.Invoke();

                    IsConnected = true;
                    yield break;
                }
                if (Time.timeSinceLevelLoad - startTime > Timeout)
                {
                    // debug
                    if (m_debugMode) Debug.Log("Time out.", this);

                    // call event
                    On_disconnected.Invoke();

                    IsConnected = false;
                    yield break;
                }

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
#endif