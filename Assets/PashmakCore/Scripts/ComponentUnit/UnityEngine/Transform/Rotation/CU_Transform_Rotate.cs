using UnityEngine;

namespace Pashmak.Core.CU._UnityEngine._Transform
{
    public class CU_Transform_Rotate : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_rotateAtStart = false;
        [SerializeField] private bool m_rotateAtUpdate = true;
        [SerializeField] protected GameObject m_baseGameObject = null;
        [SerializeField] private Vector3 m_speed = new Vector3(0, 0, 1f);
        [SerializeField] private Transform m_pivot = null;
        [SerializeField] private Transform m_axis = null;
        [SerializeField] private bool m_local = true;
        [SerializeField] private bool m_rotateItself = true;


        // property________________________________________________________________
        public bool RotateAtStart { get => m_rotateAtStart; set => m_rotateAtStart = value; }
        public bool RotateAtUpdate { get => m_rotateAtUpdate; set => m_rotateAtUpdate = value; }
        public GameObject BaseGameObject { get => m_baseGameObject; set => m_baseGameObject = value; }
        public Vector3 Speed { get => m_speed; set => m_speed = value; }
        public Transform Pivot { get => m_pivot; set => m_pivot = value; }
        public Transform Axis { get => m_axis; set => m_axis = value; }
        public bool Local { get => m_local; set => m_local = value; }
        public Space RelativeTo
        {
            get
            {
                return Local ? Space.Self : Space.World;
            }
            set => Local = value == Space.Self;
        }
        public bool RotateItself { get => m_rotateItself; set => m_rotateItself = value; }


        // monoBehaviour___________________________________________________________
        void Awake()
        {
            if (!BaseGameObject)
                BaseGameObject = gameObject;
        }
        private void Start()
        {
            if (RotateAtStart)
                Rotate();
        }
        private void Update()
        {
            if (RotateAtUpdate)
                Rotate();
        }


        // function________________________________________________________________
        public void Rotate()
        {
            if (!m_isActive) return;

            // set position relative to pivot
            if (m_pivot != null)
            {
                Vector3 dir = BaseGameObject.transform.position - m_pivot.position; // get point direction relative to pivot
                dir = Quaternion.Euler(Time.deltaTime * Speed) * dir; // rotate it
                Vector3 point = dir + m_pivot.position; // calculate rotated point
                BaseGameObject.transform.position = point;
            }

            // rotate 
            if (RotateItself)
            {
                if (Axis == null)
                    BaseGameObject.transform.Rotate(Time.deltaTime * Speed, RelativeTo);
                else
                    BaseGameObject.transform.Rotate(Axis.position, Time.deltaTime * Speed.magnitude, RelativeTo);
            }
        }
        private Vector3 CalculatePivotVector(Vector3 pivotPos)
        {
            return BaseGameObject.transform.position - pivotPos;
        }
    }
}