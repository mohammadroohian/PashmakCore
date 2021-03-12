using UnityEngine;

namespace Pashmak.Core.CU._UnityEngine._Transform
{
    public class CU_Transform : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_setAtStart = false;
        [SerializeField] protected GameObject m_baseGameObject = null;
        [SerializeField] private Transform m_destinationTransform = null;
        [SerializeField] private bool m_localPosition = false;
        [SerializeField] private bool m_localRotation = false;
        [SerializeField] private bool m_position = true;
        [SerializeField] private bool m_rotation = true;
        [SerializeField] private bool m_scale = true;


        // property________________________________________________________________
        public bool SetAtStart { get => m_setAtStart; private set => m_setAtStart = value; }
        public GameObject BaseGameObject { get => m_baseGameObject; set => m_baseGameObject = value; }
        public Transform DestinationTransform { get => m_destinationTransform; set => m_destinationTransform = value; }
        public bool LocalPosition { get => m_localPosition; set => m_localPosition = value; }
        public bool LocalRotation { get => m_localRotation; set => m_localRotation = value; }
        public bool Position { get => m_position; set => m_position = value; }
        public bool Rotation { get => m_rotation; set => m_rotation = value; }
        public bool Scale { get => m_scale; set => m_scale = value; }


        // monoBehaviour___________________________________________________________
        void Awake()
        {
            if (!BaseGameObject)
                BaseGameObject = gameObject;
        }
        private void Start()
        {
            if (SetAtStart)
                SetTransform();
        }


        // function________________________________________________________________
        public void SetTransform()
        {
            if (!IsActive) return;

            //Set position.
            if (Position)
            {
                if (LocalPosition)
                    BaseGameObject.transform.position = DestinationTransform.position;
                else
                    BaseGameObject.transform.localPosition = DestinationTransform.position;
            }

            //Set rotation.
            if (Rotation)
            {
                if (LocalRotation)
                    BaseGameObject.transform.rotation = DestinationTransform.rotation;
                else
                    BaseGameObject.transform.localRotation = DestinationTransform.rotation;
            }

            //Set scale.
            if (Scale)
            {
                BaseGameObject.transform.localScale = DestinationTransform.localScale;
            }
        }
        public void SetTransformToZero()
        {
            if (!IsActive) return;
            //Set position.
            if (Position)
            {
                if (LocalPosition)
                    BaseGameObject.transform.position = new Vector3();
                else
                    BaseGameObject.transform.localPosition = new Vector3();
            }

            //Set rotation.
            if (Rotation)
            {
                if (LocalRotation)
                    BaseGameObject.transform.rotation = Quaternion.Euler(new Vector3());
                else
                    BaseGameObject.transform.localRotation = Quaternion.Euler(new Vector3());
            }

            //Set scale.
            if (Rotation)
            {
                BaseGameObject.transform.localScale = new Vector3();
            }
        }
    }
}