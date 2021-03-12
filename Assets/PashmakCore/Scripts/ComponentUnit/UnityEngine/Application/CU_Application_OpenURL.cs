using UnityEngine;


namespace Pashmak.Core.CU._UnityEngine._Application
{
    public class CU_Application_OpenURL : CU_Component, IDefaultExcute
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_openUrlAtStart = false;
        [SerializeField] private string m_url;


        // property________________________________________________________________
        public bool OpenUrlAtStart { get => m_openUrlAtStart; private set => m_openUrlAtStart = value; }
        public string Url { get => m_url; set => m_url = value; }


        // monoBehaviour___________________________________________________________
        private void Start()
        {
            if (OpenUrlAtStart)
                OpenURL();
        }


        // override________________________________________________________________
        public override string GetDetails(string gameObjectName, string componentName, string methodName, string methodParams)
        {
            methodName = "OpenURL";
            methodParams = string.Format("\"{0}\"", m_url);
            componentName = "";
            return base.GetDetails(gameObjectName, componentName, methodName, methodParams);
        }


        // implement_______________________________________________________________
        void IDefaultExcute.DefaultExecute() => OpenURL();


        // function________________________________________________________________
        public void OpenURL()
        {
            if (!IsActive) return;
            Application.OpenURL(Url);
        }
    }
}