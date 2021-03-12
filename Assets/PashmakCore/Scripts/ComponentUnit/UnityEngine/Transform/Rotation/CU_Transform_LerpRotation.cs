using UnityEngine;


namespace Pashmak.Core.CU._UnityEngine._Transform
{
    public class CU_Transform_LerpRotation : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] protected GameObject m_baseGameObject = null;
        [SerializeField] private Transform m_pivot = null;
        [SerializeField] private float m_speed = 1f;
        [SerializeField] private Vector3 m_destination = new Vector3();
        [SerializeField] private float m_detectionAngle = .02f;
        [SerializeField] private bool m_local = false;
        [SerializeField] private bool m_x = true;
        [SerializeField] private bool m_y = true;
        [SerializeField] private bool m_z = true;
        [SerializeField] private bool m_rotateItself = true;
        private Vector3 m_firstPivotVector = Vector3.zero;
        [SerializeField] private bool m_pivotIsChilde = false;


        // property________________________________________________________________
        public Transform Pivot { get => m_pivot; set => m_pivot = value; }
        public GameObject BaseGameObject { get => m_baseGameObject; set => m_baseGameObject = value; }
        public float Speed { get => m_speed; set => m_speed = value; }
        public Vector3 Destination { get => m_destination; private set => m_destination = value; }
        public float DetectionAngle { get => m_detectionAngle; set => m_detectionAngle = value; }
        public bool Local { get => m_local; set => m_local = value; }
        public bool X { get => m_x; set => m_x = value; }
        public bool Y { get => m_y; set => m_y = value; }
        public bool Z { get => m_z; set => m_z = value; }
        public bool RotateItself { get => m_rotateItself; set => m_rotateItself = value; }


        // monoBehaviour___________________________________________________________
        void Awake()
        {
            if (!BaseGameObject)
                BaseGameObject = gameObject;

            if (Pivot != null)
                m_firstPivotVector = CalculatePivotVector(Pivot.position);
        }
        void Update()
        {
            if (!IsActive) return;

            // rotation.
            Quaternion tmpRot = Quaternion.Lerp(BaseGameObject.transform.rotation, Quaternion.Euler(Destination), Speed * Time.deltaTime);

            // check for end.
            if (Quaternion.Angle(BaseGameObject.transform.rotation, Quaternion.Euler(Destination)) < DetectionAngle)
                IsActive = false;

            // rotate 
            if (Local)
            {
                tmpRot.x = X ? tmpRot.x : BaseGameObject.transform.localRotation.x;
                tmpRot.y = Y ? tmpRot.y : BaseGameObject.transform.localRotation.y;
                tmpRot.z = Z ? tmpRot.z : BaseGameObject.transform.localRotation.z;

                BaseGameObject.transform.localRotation = tmpRot;
            }
            else
            {
                tmpRot.x = X ? tmpRot.x : BaseGameObject.transform.rotation.x;
                tmpRot.y = Y ? tmpRot.y : BaseGameObject.transform.rotation.y;
                tmpRot.z = Z ? tmpRot.z : BaseGameObject.transform.rotation.z;

                BaseGameObject.transform.rotation = tmpRot;
            }

            // set position relative to pivot
            if (m_pivot != null)
            {
                Vector3 dir = m_firstPivotVector; // get point direction relative to pivot
                dir = tmpRot * dir; // rotate it
                Vector3 point = m_pivotIsChilde ? dir + m_pivot.localPosition : dir + m_pivot.position; // calculate rotated point
                BaseGameObject.transform.position = point;
            }
        }


        // function________________________________________________________________
        private Vector3 CalculatePivotVector(Vector3 pivotPos)
        {
            return BaseGameObject.transform.position - pivotPos;
        }
    }
}