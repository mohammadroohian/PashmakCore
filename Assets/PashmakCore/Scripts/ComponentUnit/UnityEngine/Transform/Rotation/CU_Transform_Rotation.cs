using UnityEngine;

namespace Pashmak.Core.CU._UnityEngine._Transform
{
    public class CU_Transform_Rotation : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_setAtStart = false;
        [SerializeField] protected GameObject m_baseGameObject = null;
        [SerializeField] private Transform m_pivot = null;
        [SerializeField] private Vector3 m_rotation = new Vector3();
        [SerializeField] private bool m_local = false;
        [SerializeField] private bool m_x = true;
        [SerializeField] private bool m_y = true;
        [SerializeField] private bool m_z = true;
        [SerializeField] private bool m_rotateItself = true;
        private Vector3 m_firstPivotVector = Vector3.zero;
        [SerializeField] private bool m_pivotIsChilde = false;


        // property________________________________________________________________
        public bool SetAtStart { get => m_setAtStart; private set => m_setAtStart = value; }
        public Transform Pivot
        {
            get => m_pivot; set
            {
                if (m_pivot != value)
                    FirstPivotVector = CalculatePivotVector(value.position);
                m_pivot = value;
            }
        }
        public GameObject BaseGameObject
        {
            get
            {
                if (m_baseGameObject == null) m_baseGameObject = gameObject;
                return m_baseGameObject;
            }
            set => m_baseGameObject = value;
        }
        public Vector3 Rotation { get => m_rotation; set => m_rotation = value; }
        public bool Local { get => m_local; set => m_local = value; }
        public bool X { get => m_x; set => m_x = value; }
        public bool Y { get => m_y; set => m_y = value; }
        public bool Z { get => m_z; set => m_z = value; }
        public bool RotateItself { get => m_rotateItself; set => m_rotateItself = value; }
        public Vector3 FirstPivotVector { get => m_firstPivotVector; private set => m_firstPivotVector = value; }


        // monoBehaviour___________________________________________________________
        private void Start()
        {
            if (SetAtStart)
                SetRotation();

            if (Pivot != null)
                FirstPivotVector = CalculatePivotVector(Pivot.position);
        }


        // function________________________________________________________________
        private void SetRotation(Vector3 newRotation)
        {
            if (!m_isActive) return;

            // rotate 
            Vector3 tmpRot = new Vector3();
            if (Local)
            {
                tmpRot.x = X ? newRotation.x : BaseGameObject.transform.localRotation.x;
                tmpRot.y = Y ? newRotation.y : BaseGameObject.transform.localRotation.y;
                tmpRot.z = Z ? newRotation.z : BaseGameObject.transform.localRotation.z;

                BaseGameObject.transform.localRotation = Quaternion.Euler(tmpRot);
            }
            else
            {
                tmpRot.x = X ? newRotation.x : BaseGameObject.transform.rotation.x;
                tmpRot.y = Y ? newRotation.y : BaseGameObject.transform.rotation.y;
                tmpRot.z = Z ? newRotation.z : BaseGameObject.transform.rotation.z;

                BaseGameObject.transform.rotation = Quaternion.Euler(tmpRot);
            }

            // set position relative to pivot
            if (m_pivot != null)
            {
                Vector3 dir = FirstPivotVector; // get point direction relative to pivot
                dir = Quaternion.Euler(tmpRot) * dir; // rotate it
                Vector3 point = m_pivotIsChilde ? dir + m_pivot.localPosition : dir + m_pivot.position; // calculate rotated point
                BaseGameObject.transform.position = point;
            }
        }
        private Vector3 CalculatePivotVector(Vector3 pivotPos)
        {
            return BaseGameObject.transform.position - pivotPos;
        }
        public void SetRotation() => SetRotation(Rotation);
        public void SetRotationToZero() => SetRotation(Vector3.zero);
    }
}