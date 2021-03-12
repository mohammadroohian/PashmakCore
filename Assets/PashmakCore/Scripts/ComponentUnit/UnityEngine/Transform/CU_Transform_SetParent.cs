using UnityEngine;


namespace Pashmak.Core.CU._UnityEngine._Transform
{
    public class CU_Transform_SetParent : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_setParentAtStart = false;
        [SerializeField] protected GameObject m_baseGameObject = null;
        [SerializeField] private Transform m_NewParent = null;


        // property________________________________________________________________
        public bool SetParentAtStart { get => m_setParentAtStart; private set => m_setParentAtStart = value; }
        public GameObject BaseGameObject { get => m_baseGameObject; set => m_baseGameObject = value; }
        public Transform NewParent { get => m_NewParent; set => m_NewParent = value; }


        // monoBehaviour___________________________________________________________
        void Awake()
        {
            if (!BaseGameObject)
                BaseGameObject = gameObject;
        }
        private void Start()
        {
            if (SetParentAtStart)
                SetParent();
        }


        // function________________________________________________________________
        public void SetParent()
        {
            if (!m_isActive) return;
            BaseGameObject.transform.SetParent(NewParent);
        }
    }
}