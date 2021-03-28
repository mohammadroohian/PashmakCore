using UnityEngine;


namespace Pashmak.Core.CU._UnityEngine._Transform
{
    public class CU_Transform_Translate : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] protected GameObject m_baseGameObject = null;
        [SerializeField] private Vector3 m_speed = new Vector3(0, 0, 1f);
        [SerializeField] private bool m_local = false;


        // property________________________________________________________________
        public Vector3 Speed { get => m_speed; set => m_speed = value; }
        public bool Local { get => m_local; set => m_local = value; }
        public GameObject BaseGameObject { get => m_baseGameObject; set => m_baseGameObject = value; }


        // monoBehaviour___________________________________________________________
        void Awake()
        {
            if (!BaseGameObject)
                BaseGameObject = gameObject;
        }
        void Update()
        {
            if (!m_isActive) return;

            if (Local)
                BaseGameObject.transform.Translate(Speed * Time.deltaTime, Space.Self);
            else
                BaseGameObject.transform.Translate(Speed * Time.deltaTime, Space.World);
        }
    }
}