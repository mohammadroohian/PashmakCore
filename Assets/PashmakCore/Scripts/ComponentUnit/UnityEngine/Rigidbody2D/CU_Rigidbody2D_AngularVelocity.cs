using UnityEngine;


namespace Pashmak.Core.CU._UnityEngine._Rigidbody2D
{
    public class CU_Rigidbody2D_AngularVelocity : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_setAtStart = false;
        [SerializeField] private Rigidbody2D m_baseRigidBody = null;
        [SerializeField] private float m_velocity = 0;


        // property________________________________________________________________
        public bool SetAtStart { get => m_setAtStart; private set => m_setAtStart = value; }
        public Rigidbody2D BaseRigidBody { get => m_baseRigidBody; set => m_baseRigidBody = value; }
        public float Velocity { get => m_velocity; set => m_velocity = value; }


        // monoBehaviour___________________________________________________________
        private void Awake()
        {
            if (BaseRigidBody == null)
                BaseRigidBody = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            if (SetAtStart)
                SetAngularVelocity();
        }


        // function________________________________________________________________
        private void SetAngularVelocity(float newVelocity)
        {
            if (!IsActive) return;
            BaseRigidBody.angularVelocity = newVelocity;
        }
        public void SetAngularVelocity() => SetAngularVelocity(Velocity);
        public void SetAngularVelocityToZero() => SetAngularVelocity(0);
    }
}