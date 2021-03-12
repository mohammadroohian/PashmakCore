using UnityEngine;

namespace Pashmak.Core.CU._UnityEngine._Transform
{
    public class CU_Transform_LookAt : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_lookAtStart = false;
        [SerializeField] private bool m_lookAtUpdate = true;
        [SerializeField] protected GameObject m_baseGameObject = null;
        [SerializeField] private Transform m_target = null;
        [SerializeField] private bool m_x = true;
        [SerializeField] private bool m_y = true;
        [SerializeField] private bool m_z = true;


        // property________________________________________________________________
        public bool LookAtStart { get => m_lookAtStart; private set => m_lookAtStart = value; }
        public bool LookAtUpdate { get => m_lookAtUpdate; set => m_lookAtUpdate = value; }
        public GameObject BaseGameObject { get => m_baseGameObject; set => m_baseGameObject = value; }
        public Transform Target { get => m_target; set => m_target = value; }
        public bool X { get => m_x; set => m_x = value; }
        public bool Y { get => m_y; set => m_y = value; }
        public bool Z { get => m_z; set => m_z = value; }


        // monoBehaviour___________________________________________________________
        void Awake()
        {
            if (!BaseGameObject)
                BaseGameObject = gameObject;
        }
        private void Start()
        {
            if (LookAtStart)
                LookAt();
        }
        void Update()
        {
            if (LookAtUpdate)
                LookAt();
        }


        // function________________________________________________________________
        public void LookAt()
        {
            if (!m_isActive) return;
            float x = X ? Target.position.x : BaseGameObject.transform.position.x;
            float y = Y ? Target.position.y : BaseGameObject.transform.position.y;
            float z = Z ? Target.position.z : BaseGameObject.transform.position.z;
            BaseGameObject.transform.LookAt(new Vector3(x, y, z));
        }
    }
}