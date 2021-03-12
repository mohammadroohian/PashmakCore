using UnityEngine;
using NaughtyAttributes;

namespace Pashmak.Core.CU._UnityEngine._Transform
{
    public class CU_Transform_Dragable : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] protected GameObject m_baseGameObject = null;
        [SerializeField] private bool m_stickToFirstPosition = true;

        [SerializeField] [BoxGroup("Axis")] private bool m_x = true;
        [SerializeField] [BoxGroup("Axis")] private bool m_y = true;
        [SerializeField] [BoxGroup("Axis")] private bool m_z = true;
        [SerializeField] [BoxGroup("Axis")] [ShowIf("m_z")] private int m_zValue = 0;

        [SerializeField]
        [BoxGroup("Override Z Axis")]
        private bool m_overrideZAtDragging = true;
        [SerializeField]
        [BoxGroup("Override Z Axis")]
        [ShowIf("m_overrideZAtDragging")]
        private int m_overrideZAtDraggingValue = -9;

        [BoxGroup("Pivot")]
        [SerializeField] private bool m_pivotIsCenter = false;
        [BoxGroup("Pivot")]
        [HideIf("m_pivotIsCenter")]
        [SerializeField] private Transform m_pivot = null;

        private Vector3 m_firstPosition = new Vector3();
        private Vector3 m_lastMouseWorldPoint;
        private bool m_clickedOn;
        private float m_zAtAwake = 0;


        // property________________________________________________________________
        public GameObject BaseGameObject { get => m_baseGameObject; set => m_baseGameObject = value; }
        public bool StickToFirstPosition { get => m_stickToFirstPosition; set => m_stickToFirstPosition = value; }
        public bool X { get => m_x; set => m_x = value; }
        public bool Y { get => m_y; set => m_y = value; }
        public bool Z { get => m_z; set => m_z = value; }
        public int ZValue { get => m_zValue; set => m_zValue = value; }
        public bool OverrideZAtDragging { get => m_overrideZAtDragging; set => m_overrideZAtDragging = value; }
        public int OverrideZAtDraggingValue { get => m_overrideZAtDraggingValue; set => m_overrideZAtDraggingValue = value; }
        public bool PivotIsCenter { get => m_pivotIsCenter; set => m_pivotIsCenter = value; }
        public Transform Pivot { get => m_pivot; set => m_pivot = value; }
        public Vector3 FirstPosition { get => m_firstPosition; set => m_firstPosition = value; }
        public bool ClickedOn { get => m_clickedOn; set => m_clickedOn = value; }


        // monoBehaviour___________________________________________________________
        void Awake()
        {
            if (!BaseGameObject)
                BaseGameObject = gameObject;

            FirstPosition = BaseGameObject.transform.position;
            m_zAtAwake = BaseGameObject.transform.position.z;
        }
        void Update()
        {
            if (ClickedOn)
                Dragging();
        }
        void OnMouseDown()
        {
            if (!m_isActive) return;
            StartDragging();
            m_lastMouseWorldPoint = GetMouseWorldPoint();
        }
        void OnMouseUp()
        {
            if (!m_isActive) return;
            EndDragging();
        }


        // function________________________________________________________________
        private void StartDragging() => ClickedOn = true;
        public void PauseDragging() => ClickedOn = false;
        public void EndDragging()
        {
            ClickedOn = false;
            if (StickToFirstPosition)
                BaseGameObject.transform.position = FirstPosition;
        }
        private void Dragging()
        {
            if (!m_isActive) return;

            Vector3 mouseWorldPoint = GetMouseWorldPoint();
            mouseWorldPoint.x = X ? mouseWorldPoint.x : BaseGameObject.transform.position.x;
            mouseWorldPoint.y = Y ? mouseWorldPoint.y : BaseGameObject.transform.position.y;
            mouseWorldPoint.z = Z ? mouseWorldPoint.z : BaseGameObject.transform.position.z;

            if (OverrideZAtDragging)
                mouseWorldPoint.z = OverrideZAtDraggingValue;

            if (Pivot || PivotIsCenter)
                BaseGameObject.transform.position = mouseWorldPoint;
            else
            {
                BaseGameObject.transform.position =
                        BaseGameObject.transform.position - (m_lastMouseWorldPoint - mouseWorldPoint);
            }

            m_lastMouseWorldPoint = mouseWorldPoint;
        }
        public void GoToFirstPosition()
        {
            if (!m_isActive) return;

            transform.position = FirstPosition;
        }
        private Vector3 GetMouseWorldPoint()
        {
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Pivot && !PivotIsCenter)
            {
                Vector3 scaledPivot = new Vector3();
                scaledPivot.x = Pivot.localPosition.x * BaseGameObject.transform.lossyScale.x;
                scaledPivot.y = Pivot.localPosition.y * BaseGameObject.transform.lossyScale.y;
                scaledPivot.z = Pivot.localPosition.z * BaseGameObject.transform.lossyScale.z;

                mouseWorldPoint -= scaledPivot;
            }

            mouseWorldPoint.z = ZValue;
            return mouseWorldPoint;
        }
    }
}