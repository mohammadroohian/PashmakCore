using UnityEngine;


namespace Pashmak.Core.CU._UnityEngine._Rigidbody2D
{
    public class CU_Rigidbody_AngularVelocity : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_setAtStart = false;
        [SerializeField] private Rigidbody m_baseRigidBody = null;
        [SerializeField] private Vector3 m_velocity = Vector3.zero;


        // property________________________________________________________________
        public bool SetAtStart { get => m_setAtStart; private set => m_setAtStart = value; }
        public Rigidbody BaseRigidBody { get => m_baseRigidBody; set => m_baseRigidBody = value; }
        public Vector3 Velocity { get => m_velocity; set => m_velocity = value; }


        // monoBehaviour___________________________________________________________
        private void Awake()
        {
            if (BaseRigidBody == null)
                BaseRigidBody = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            if (SetAtStart)
                SetAngularVelocity();
        }


        // function________________________________________________________________
        private void SetAngularVelocity(Vector3 newVelocity)
        {
            if (!IsActive) return;
            BaseRigidBody.angularVelocity = newVelocity;
        }
        public void SetAngularVelocity() => SetAngularVelocity(Velocity);
        public void SetAngularVelocityToZero() => SetAngularVelocity(Vector3.zero);
    }
}