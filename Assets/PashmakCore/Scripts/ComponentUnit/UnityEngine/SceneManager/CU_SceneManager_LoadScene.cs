using UnityEngine;
using UnityEngine.SceneManagement;


namespace Pashmak.Core.CU._UnityEngine._SceneManagement
{
    public class CU_SceneManager_LoadScene : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private string m_sceneName = "";


        // property________________________________________________________________
        public string SceneName { get => m_sceneName; set => m_sceneName = value; }


        // override________________________________________________________________
        public override string GetDetails(string gameObjectName, string componentName, string methodName, string methodParams)
        {
            string parameterName = string.Format("\"{0}\"", m_sceneName);
            if (methodName.Contains("LoadScene"))
            {
                methodParams = parameterName;
            }
            return base.GetDetails(gameObjectName, componentName, methodName, methodParams);
        }


        // function________________________________________________________________     
        public void ReloadThisScene()
        {
            if (!IsActive) return;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void LoadScene()
        {
            if (!IsActive) return;
            SceneManager.LoadScene(m_sceneName);
        }
    }
}