using System.Collections.Generic;
using Pashmak.Core.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Pashmak.Core.CU._UnityEngine._Transform
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CU_Transform_Dragable))]
    public class CU_Transform_SnapObj : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] protected GameObject m_baseGameObject = null;
        [SerializeField] private string m_keyCode = "";
        [SerializeField] private bool m_snapAsChild = true; // set parent to snap point on snapping 
        [SerializeField] private bool m_unsnapAsFirstParentChild = true; // set parent to first parent after unsnapping 
        [SerializeField] private bool m_holdInitialLocalRotation = true;
        [SerializeField] protected Transform m_pivot = null;
        [SerializeField] private UnityEvent m_onSnap = new UnityEvent();
        [SerializeField] private UnityEvent m_onUnsnap = new UnityEvent();
        [SerializeField] private List<CU_Transform_SnapPoint> m_exceptionSnapPoints = new List<CU_Transform_SnapPoint>(); // except this snap point // common in snap point's in props
        private CU_Transform_Dragable m_dragable = null; // dragging part
        private CU_Transform_SnapPoint m_snapPoint;
        private Collider2D m_snapPointCollider;
        private Transform m_unsnapParent; // hold firs parent to back on unsnapping
        private Quaternion m_initializeLocalRotation = new Quaternion();


        // property________________________________________________________________
        public GameObject BaseGameObject { get => m_baseGameObject; set => m_baseGameObject = value; }
        protected string KeyCode { get => m_keyCode; private set => m_keyCode = value; }
        protected bool SnapAsChild { get => m_snapAsChild; private set => m_snapAsChild = value; }
        public bool UnsnapAsFirstParentChild { get => m_unsnapAsFirstParentChild; set => m_unsnapAsFirstParentChild = value; }
        public bool HoldInitialLocalRotation { get => m_holdInitialLocalRotation; set => m_holdInitialLocalRotation = value; }
        public Transform Pivot
        {
            get
            {
                if (m_pivot == null)
                    return BaseGameObject.transform;
                else return m_pivot;
            }
            set => m_pivot = value;
        }
        public UnityEvent OnSnap { get => m_onSnap; private set => m_onSnap = value; }
        public UnityEvent OnUnsnap { get => m_onUnsnap; private set => m_onUnsnap = value; }
        public List<CU_Transform_SnapPoint> ExceptionSnapPoints { get => m_exceptionSnapPoints; private set => m_exceptionSnapPoints = value; }
        public bool IsSnapped { get { return SnapPoint == null ? false : true; } }
        public CU_Transform_Dragable Dragable { get => m_dragable; private set => m_dragable = value; }
        public CU_Transform_SnapPoint SnapPoint { get => m_snapPoint; private set => m_snapPoint = value; }
        public Collider2D SnapPointCollider { get => m_snapPointCollider; private set => m_snapPointCollider = value; }
        protected Transform UnsnapParent { get => m_unsnapParent; set => m_unsnapParent = value; }


        // monoBehaviour___________________________________________________________
        protected void Awake()
        {
            if (!BaseGameObject)
                BaseGameObject = gameObject;

            // local rotaion at start
            m_initializeLocalRotation = BaseGameObject.transform.localRotation;

            Dragable = GetComponent<CU_Transform_Dragable>();

            // first parent
            UnsnapParent = transform.parent;
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!IsActive) return;
            if (IsSnapped) return;

            CU_Transform_SnapPoint tmpSnp = collider.GetComponent<CU_Transform_SnapPoint>();
            if (tmpSnp != null)
            {
                Snap(tmpSnp, collider);
            }
            else
            {
                CU_Transform_SnapablePointer pointer = collider.GetComponent<CU_Transform_SnapablePointer>();
                if (pointer != null && SnapPointIsFunctional(pointer.Snapable))
                    Snap(pointer.Snapable, collider);
            }
        }
        void OnMouseDown() => Unsnap();


        // function________________________________________________________________
        private bool SnapPointIsFunctional(CU_Transform_SnapPoint point)
        {
            if (point == null) return false;
            if (!point.IsActive) return false;
            if (point.IsFull) return false;
            if (!point.ContainsKey(KeyCode)) return false;
            if (ExceptionSnapPoints.Contains(point)) return false;

            return true;
        }
        public void Snap(CU_Transform_SnapPoint point, Collider2D collider)
        {
            if (!SnapPointIsFunctional(point)) return;

            // get snap point
            SnapPoint = point;
            SnapPointCollider = collider;
            SnapPointCollider.enabled = false;

            SnapPivots();

            // disable dragging
            Dragable.IsActive = false;

            // event
            OnSnap.Invoke();

            // set snapable object to snap point
            point.SetObj(this);
        }
        public void Unsnap()
        {
            if (!IsActive) return;
            if (!IsSnapped) return;

            // enable dragging
            Dragable.IsActive = true;
            Dragable.EndDragging();

            // return to first parent
            if (UnsnapAsFirstParentChild)
                BaseGameObject.transform.SetParent(UnsnapParent);

            // hold initial rotation
            if (HoldInitialLocalRotation)
                BaseGameObject.transform.localRotation = m_initializeLocalRotation;

            // event
            OnUnsnap.Invoke();

            // unsnap connected snap point
            SnapPointCollider.enabled = true;
            SnapPointCollider = null;
            SnapPoint.DropObj();
            SnapPoint = null;
        }
        private void SnapPivots()
        {
            if (SnapAsChild)
                BaseGameObject.transform.SetParent(SnapPoint.transform);

            TransformUtils.SnapP1ToP2(BaseGameObject.transform, Pivot, SnapPoint.Pivot);

            // hold initial rotation
            if (HoldInitialLocalRotation)
                BaseGameObject.transform.localRotation = m_initializeLocalRotation;
        }
    }
}