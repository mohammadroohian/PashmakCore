using UnityEngine;

namespace Pashmak.Core.CU._UnityEngine._Transform
{
    public class CU_Transform_Position_SyncToMouse : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_syncAtStart = false;
        [SerializeField] private bool m_syncAtUpdate = true;
        [SerializeField] protected GameObject m_baseGameObject = null;
        [SerializeField] private bool m_x = true;
        [SerializeField] private bool m_y = true;
        [SerializeField] private float m_zValue = -30;


        // property________________________________________________________________
        public bool SyncAtStart { get => m_syncAtStart; private set => m_syncAtStart = value; }
        public bool SyncAtUpdate { get => m_syncAtUpdate; set => m_syncAtUpdate = value; }
        public GameObject BaseGameObject { get => m_baseGameObject; set => m_baseGameObject = value; }
        public bool X { get => m_x; set => m_x = value; }
        public bool Y { get => m_y; set => m_y = value; }
        public float ZValue { get => m_zValue; set => m_zValue = value; }


        // monoBehaviour___________________________________________________________
        void Awake()
        {
            if (!BaseGameObject)
                BaseGameObject = gameObject;
        }
        private void Start()
        {
            if (SyncAtStart)
                Sync();
        }
        private void Update()
        {
            if (SyncAtUpdate)
                Sync();
        }


        // function________________________________________________________________
        public void Sync()
        {
            if (!m_isActive) return;
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPoint.z = ZValue;
            if (!X)
                mouseWorldPoint.x = BaseGameObject.transform.position.x;
            if (!Y)
                mouseWorldPoint.y = BaseGameObject.transform.position.y;
            BaseGameObject.transform.position = mouseWorldPoint;
        }
    }
}