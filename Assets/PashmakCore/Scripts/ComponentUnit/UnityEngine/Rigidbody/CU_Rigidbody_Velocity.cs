using UnityEngine;


namespace Pashmak.Core.CU._UnityEngine._Rigidbody2D
{
    public class CU_Rigidbody_Velocity : CU_Component
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
                SetVelocity();
        }


        // function________________________________________________________________
        private void SetVelocity(Vector3 newVelocity)
        {
            if (!IsActive) return;
            BaseRigidBody.velocity = newVelocity;
        }
        public void SetVelocity() => SetVelocity(Velocity);
        public void SetVelocityToZero() => SetVelocity(Vector3.zero);
    }
}