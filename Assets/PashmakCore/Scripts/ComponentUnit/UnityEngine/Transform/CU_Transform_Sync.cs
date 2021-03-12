using UnityEngine;
using NaughtyAttributes;

namespace Pashmak.Core.CU._UnityEngine._Transform
{
    public class CU_Transform_Sync : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_syncAtStart = false;
        [SerializeField] private bool m_syncAtUpdate = true;
        [SerializeField] protected GameObject m_baseGameObject = null;
        [SerializeField] private Transform m_syncWithGameObject = null;

        [BoxGroup("Position")]
        [SerializeField] private bool m_position = true;
        [BoxGroup("Position")]
        [ShowIf("m_position")]
        [SerializeField] private bool m_positionX = true;
        [BoxGroup("Position")]
        [ShowIf("m_position")]
        [SerializeField] private bool m_positionY = true;
        [BoxGroup("Position")]
        [ShowIf("m_position")]
        [SerializeField] private bool m_positionZ = true;
        [BoxGroup("Position")]
        [ShowIf("m_position")]
        [SerializeField] private bool m_localPosition = false;
        [BoxGroup("Position")]
        [ShowIf("m_position")]
        [SerializeField] private bool m_holdInitialPosition = false;
        [BoxGroup("Position")]
        [ShowIf("m_position")]
        [SerializeField] private Vector3 m_positionOffset = Vector3.zero;

        [BoxGroup("Rotation")]
        [SerializeField] private bool m_rotation = false;
        [BoxGroup("Rotation")]
        [ShowIf("m_rotation")]
        [SerializeField] private bool m_localRotation = false;

        [BoxGroup("Scale")]
        [SerializeField] private bool m_scale = false;
        [BoxGroup("Scale")]
        [ShowIf("m_scale")]
        [SerializeField] private bool m_scaleX = false;
        [BoxGroup("Scale")]
        [ShowIf("m_scale")]
        [SerializeField] private bool m_scaleY = false;
        [BoxGroup("Scale")]
        [ShowIf("m_scale")]
        [SerializeField] private bool m_scaleZ = false;
        [BoxGroup("Scale")]
        [ShowIf("m_scale")]
        [SerializeField] private Vector3 m_scaleOffset = Vector3.zero;

        private Vector3 m_baseGameObjectInitialPosition = new Vector3();


        // property________________________________________________________________
        public bool SyncAtStart { get => m_syncAtStart; private set => m_syncAtStart = value; }
        public bool SyncAtUpdate { get => m_syncAtUpdate; set => m_syncAtUpdate = value; }
        public GameObject BaseGameObject { get => m_baseGameObject; set => m_baseGameObject = value; }
        public Transform SyncWithGameObject { get => m_syncWithGameObject; set => m_syncWithGameObject = value; }

        public bool Position { get => m_position; set => m_position = value; }
        public bool PositionX { get => m_positionX; set => m_positionX = value; }
        public bool PositionY { get => m_positionY; set => m_positionY = value; }
        public bool PositionZ { get => m_positionZ; set => m_positionZ = value; }
        public bool LocalPosition { get => m_localPosition; set => m_localPosition = value; }
        public Vector3 PositionOffset { get => m_positionOffset; set => m_positionOffset = value; }

        public bool Rotation { get => m_rotation; set => m_rotation = value; }
        public bool LocalRotation { get => m_localRotation; set => m_localRotation = value; }

        public bool Scale { get => m_scale; set => m_scale = value; }
        public bool ScaleX { get => m_scaleX; set => m_scaleX = value; }
        public bool ScaleY { get => m_scaleY; set => m_scaleY = value; }
        public bool ScaleZ { get => m_scaleZ; set => m_scaleZ = value; }
        public Vector3 ScaleOffset { get => m_scaleOffset; set => m_scaleOffset = value; }


        // monoBehaviour___________________________________________________________
        void Awake()
        {
            if (!BaseGameObject)
                BaseGameObject = gameObject;
        }
        private void Start()
        {
            m_baseGameObjectInitialPosition = BaseGameObject.transform.position - SyncWithGameObject.transform.position;

            if (SyncAtStart)
                Sync();
        }
        private void Update()
        {
            if (SyncAtUpdate)
                Sync();
        }


        // function________________________________________________________________
        public void Sync()
        {
            if (!m_isActive) return;

            SyncPosition();
            SyncRotations();
            SyncScale();
        }
        public void SyncPosition()
        {
            if (!m_isActive) return;
            if (!Position) return;

            Vector3 newPos = new Vector3();
            if (LocalPosition)
            {
                newPos.x = PositionX ? SyncWithGameObject.localPosition.x + PositionOffset.x : BaseGameObject.transform.localPosition.x;
                newPos.y = PositionY ? SyncWithGameObject.localPosition.y + PositionOffset.y : BaseGameObject.transform.localPosition.y;
                newPos.z = PositionZ ? SyncWithGameObject.localPosition.z + PositionOffset.z : BaseGameObject.transform.localPosition.z;
                BaseGameObject.transform.localPosition = newPos;
            }
            else
            {
                newPos.x = PositionX ? SyncWithGameObject.position.x + PositionOffset.x : BaseGameObject.transform.position.x;
                newPos.y = PositionY ? SyncWithGameObject.position.y + PositionOffset.y : BaseGameObject.transform.position.y;
                newPos.z = PositionZ ? SyncWithGameObject.position.z + PositionOffset.z : BaseGameObject.transform.position.z;
                BaseGameObject.transform.position = newPos;
            }

            if (m_holdInitialPosition)
            {
                BaseGameObject.transform.position += m_baseGameObjectInitialPosition;
            }
        }
        public void SyncRotations()
        {
            if (!m_isActive) return;
            if (!Rotation) return;

            if (LocalRotation)
                BaseGameObject.transform.rotation = SyncWithGameObject.rotation;
            else
                BaseGameObject.transform.localRotation = SyncWithGameObject.rotation;
        }
        public void SyncScale()
        {
            if (!m_isActive) return;
            if (!Scale) return;

            Vector3 newScale = new Vector3();
            newScale.x = ScaleX ? SyncWithGameObject.localScale.x + ScaleOffset.x : BaseGameObject.transform.localScale.x;
            newScale.y = ScaleY ? SyncWithGameObject.localScale.y + ScaleOffset.y : BaseGameObject.transform.localScale.y;
            newScale.z = ScaleZ ? SyncWithGameObject.localScale.z + ScaleOffset.z : BaseGameObject.transform.localScale.z;
            BaseGameObject.transform.localScale = newScale;
        }
    }
}