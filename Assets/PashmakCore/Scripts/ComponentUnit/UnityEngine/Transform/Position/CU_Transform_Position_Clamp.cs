using UnityEngine;

namespace Pashmak.Core.CU._UnityEngine._Transform
{
    public class CU_Transform_Position_Clamp : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_clampAtStart = true;
        [SerializeField] private bool m_clampAtUpdate = true;
        [SerializeField] protected GameObject m_baseGameObject = null;
        [SerializeField] private Vector3 m_maxPosition = new Vector3(0, 1, 0);
        [SerializeField] private Vector3 m_minPosition = new Vector3(0, -1, 0);
        [SerializeField] private bool m_local = false;
        [SerializeField] private bool m_x = true;
        [SerializeField] private bool m_y = true;
        [SerializeField] private bool m_z = true;


        // property________________________________________________________________
        public bool ClampAtStart { get => m_clampAtStart; set => m_clampAtStart = value; }
        public bool ClampAtUpdate { get => m_clampAtUpdate; set => m_clampAtUpdate = value; }
        public GameObject BaseGameObject { get => m_baseGameObject; set => m_baseGameObject = value; }
        public Vector3 MaxPosition { get => m_maxPosition; set => m_maxPosition = value; }
        public Vector3 MinPosition { get => m_minPosition; set => m_minPosition = value; }
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
            if (ClampAtStart)
                Clamp();
        }
        private void Update()
        {
            if (ClampAtUpdate)
                Clamp();
        }


        // function________________________________________________________________
        public void Clamp()
        {
            if (!m_isActive) return;
            if (Local)
            {
                Vector3 tmpPosition = new Vector3();
                tmpPosition.x = Mathf.Clamp(
                    BaseGameObject.transform.localPosition.x,
                    MinPosition.x,
                    MaxPosition.x
                    );
                tmpPosition.y = Mathf.Clamp(
                    BaseGameObject.transform.localPosition.y,
                    MinPosition.y,
                    MaxPosition.y
                    );
                tmpPosition.z = Mathf.Clamp(
                    BaseGameObject.transform.localPosition.z,
                    MinPosition.z,
                    MaxPosition.z
                    );

                tmpPosition.x = X ? tmpPosition.x : BaseGameObject.transform.localPosition.x;
                tmpPosition.y = Y ? tmpPosition.y : BaseGameObject.transform.localPosition.y;
                tmpPosition.z = Z ? tmpPosition.z : BaseGameObject.transform.localPosition.z;

                BaseGameObject.transform.localPosition = tmpPosition;
            }
            else
            {
                Vector3 tmpPosition = new Vector3();
                tmpPosition.x = Mathf.Clamp(
                    BaseGameObject.transform.position.x,
                    MinPosition.x,
                    MaxPosition.x
                    );
                tmpPosition.y = Mathf.Clamp(
                    BaseGameObject.transform.position.y,
                    MinPosition.y,
                    MaxPosition.y
                    );
                tmpPosition.z = Mathf.Clamp(
                    BaseGameObject.transform.position.z,
                    MinPosition.z,
                    MaxPosition.z
                    );

                tmpPosition.x = X ? tmpPosition.x : BaseGameObject.transform.position.x;
                tmpPosition.y = Y ? tmpPosition.y : BaseGameObject.transform.position.y;
                tmpPosition.z = Z ? tmpPosition.z : BaseGameObject.transform.position.z;

                BaseGameObject.transform.position = tmpPosition;
            }
        }
    }
}