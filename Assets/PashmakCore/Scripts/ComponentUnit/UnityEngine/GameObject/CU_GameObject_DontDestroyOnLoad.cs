using UnityEngine;


namespace Pashmak.Core.CU._UnityEngine._GameObject
{
    public class CU_GameObject_DontDestroyOnLoad : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private GameObject m_baseGameObject = null;


        // property________________________________________________________________
        public GameObject BaseGameObject { get => m_baseGameObject; set => m_baseGameObject = value; }


        // monoBehaviour___________________________________________________________
        private void Awake()
        {
            // null check
            if (BaseGameObject == null)
                BaseGameObject = this.gameObject;

            // if object has parent DontDestroyOnLoad not working.
            BaseGameObject.transform.SetParent(null);

            GameObject.DontDestroyOnLoad(BaseGameObject);
        }
    }
}