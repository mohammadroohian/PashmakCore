using UnityEngine;

namespace Pashmak.Core.CU._UnityEngine._Transform
{
    public class CU_Transform_Position : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_setAtStart = false;
        [SerializeField] protected GameObject m_baseGameObject = null;
        [SerializeField] private Vector3 m_position = new Vector3();
        [SerializeField] private bool m_local = false;
        [SerializeField] private bool m_x = true;
        [SerializeField] private bool m_y = true;
        [SerializeField] private bool m_z = true;


        // property________________________________________________________________
        public bool SetAtStart { get => m_setAtStart; private set => m_setAtStart = value; }
        public GameObject BaseGameObject { get => m_baseGameObject; set => m_baseGameObject = value; }
        public Vector3 Position { get => m_position; set => m_position = value; }
        public bool Local { get => m_local; set => m_local = value; }
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
            if (SetAtStart)
                SetPosition();
        }


        // function________________________________________________________________
        private void SetPosition(Vector3 newPosition)
        {
            if (!m_isActive) return;
            if (Local)
            {
                float x = X ? newPosition.x : BaseGameObject.transform.localPosition.x;
                float y = Y ? newPosition.y : BaseGameObject.transform.localPosition.y;
                float z = Z ? newPosition.z : BaseGameObject.transform.localPosition.z;

                BaseGameObject.transform.localPosition = new Vector3(x, y, z);
            }
            else
            {

                float x = X ? newPosition.x : BaseGameObject.transform.position.x;
                float y = Y ? newPosition.y : BaseGameObject.transform.position.y;
                float z = Z ? newPosition.z : BaseGameObject.transform.position.z;

                BaseGameObject.transform.position = new Vector3(x, y, z);
            }
        }
        public void SetPosition() => SetPosition(Position);
        public void SetPositionToZero() => SetPosition(Vector3.zero);
    }
}