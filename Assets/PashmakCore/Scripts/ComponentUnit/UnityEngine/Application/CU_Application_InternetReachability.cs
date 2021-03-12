using UnityEngine;
using UnityEngine.Events;


namespace Pashmak.Core.CU._UnityEngine._Application
{
    public class CU_Application_InternetReachability : CU_Component, IDebug
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_debugMode = false;
        [SerializeField] private bool m_checkAtStart = false;
        [SerializeField] private UnityEvent m_on_NotReachable = new UnityEvent();
        [SerializeField] private UnityEvent m_on_ReachableViaCarrierDataNetwork = new UnityEvent();
        [SerializeField] private UnityEvent m_on_ReachableViaLocalAreaNetwork = new UnityEvent();


        // property________________________________________________________________
        bool IDebug.DebugMode { get => m_debugMode; set => m_debugMode = value; }
        public bool CheckAtStart { get => m_checkAtStart; private set => m_checkAtStart = value; }
        public UnityEvent On_NotReachable { get => m_on_NotReachable; private set => m_on_NotReachable = value; }
        public UnityEvent On_ReachableViaCarrierDataNetwork { get => m_on_ReachableViaCarrierDataNetwork; private set => m_on_ReachableViaCarrierDataNetwork = value; }
        public UnityEvent On_ReachableViaLocalAreaNetwork { get => m_on_ReachableViaLocalAreaNetwork; private set => m_on_ReachableViaLocalAreaNetwork = value; }


        // monoBehaviour___________________________________________________________
        private void Start()
        {
            if (CheckAtStart)
                Check_NetworkReachability();
        }


        // function________________________________________________________________  
        public void Check_NetworkReachability()
        {
            if (!IsActive) return;
            switch (Application.internetReachability)
            {
                case NetworkReachability.NotReachable:
                    On_NotReachable.Invoke();
                    break;
                case NetworkReachability.ReachableViaCarrierDataNetwork:
                    On_ReachableViaCarrierDataNetwork.Invoke();
                    break;
                case NetworkReachability.ReachableViaLocalAreaNetwork:
                    On_ReachableViaLocalAreaNetwork.Invoke();
                    break;
            }

            if (m_debugMode) Debug.Log("Application.internetReachability: " + Application.internetReachability, this);
        }
    }
}