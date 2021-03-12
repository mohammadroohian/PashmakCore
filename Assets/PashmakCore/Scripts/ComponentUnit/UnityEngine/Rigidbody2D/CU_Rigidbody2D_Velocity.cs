using UnityEngine;


namespace Pashmak.Core.CU._UnityEngine._Rigidbody2D
{
    public class CU_Rigidbody2D_Velocity : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_setAtStart = false;
        [SerializeField] private Rigidbody2D m_baseRigidBody = null;
        [SerializeField] private Vector2 m_velocity = new Vector2();


        // property________________________________________________________________
        public bool SetAtStart { get => m_setAtStart; private set => m_setAtStart = value; }
        public Rigidbody2D BaseRigidBody { get => m_baseRigidBody; set => m_baseRigidBody = value; }
        public Vector2 Velocity { get => m_velocity; set => m_velocity = value; }


        // monoBehaviour___________________________________________________________
        private void Awake()
        {
            if (BaseRigidBody == null)
                BaseRigidBody = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            if (SetAtStart)
                SetVelocity();
        }


        // function________________________________________________________________
        private void SetVelocity(Vector2 newVelocity)
        {
            if (!IsActive) return;
            BaseRigidBody.velocity = newVelocity;
        }
        public void SetVelocity() => SetVelocity(Velocity);
        public void SetVelocityToZero() => SetVelocity(Vector2.zero);
    }
}