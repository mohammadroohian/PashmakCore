using UnityEngine;

namespace Pashmak.Core.CU._UnityEngine._Transform
{
    public class CU_Transform_Scale : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_setAtStart = false;
        [SerializeField] protected GameObject m_baseGameObject = null;
        [SerializeField] private Vector3 m_scale = new Vector3(1, 1, 1);
        [SerializeField] private bool m_x = true;
        [SerializeField] private bool m_y = true;
        [SerializeField] private bool m_z = true;


        // property________________________________________________________________
        public bool SetAtStart { get => m_setAtStart; private set => m_setAtStart = value; }
        public GameObject BaseGameObject { get => m_baseGameObject; set => m_baseGameObject = value; }
        public Vector3 Scale { get => m_scale; set => m_scale = value; }
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
                SetScale();
        }


        // function________________________________________________________________
        private void SetScale(Vector3 newScale)
        {
            if (!m_isActive) return;
            float x = X ? newScale.x : BaseGameObject.transform.localScale.x;
            float y = Y ? newScale.y : BaseGameObject.transform.localScale.y;
            float z = Z ? newScale.z : BaseGameObject.transform.localScale.z;

            BaseGameObject.transform.localScale = new Vector3(x, y, z);
        }
        public void SetScale() => SetScale(Scale);
        public void SetScaleToZero() => SetScale(new Vector3());
        public void Multiply(float factor)
        {
            Vector3 newScale = BaseGameObject.transform.localScale * factor;
            SetScale(newScale);
        }
        public void Divide(float factor)
        {
            Vector3 newScale = BaseGameObject.transform.localScale / factor;
            SetScale(newScale);
        }
    }
}