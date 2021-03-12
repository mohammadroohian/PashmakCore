using UnityEngine;
using static Pashmak.Core.CU.CU_ColorFader;

namespace Pashmak.Core.CU._UnityEngine._Transform
{
    public class CU_Transform_CurveRotation : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_updateRotationAtUpdate = false;
        [SerializeField] private Initialize m_initializeType = Initialize.AutoPlayOnStart;
        [SerializeField] protected GameObject m_baseGameObject = null;
        [SerializeField] private Transform m_pivot = null;

        [SerializeField]
        private AnimationCurve m_curve_x = new AnimationCurve(
                            new Keyframe(0f, -1f),
                            new Keyframe(1f, 1f)
                        );
        [SerializeField]
        private AnimationCurve m_curve_y = new AnimationCurve(
                            new Keyframe(0f, -1f),
                            new Keyframe(1f, 1f)
                        );
        [SerializeField]
        private AnimationCurve m_curve_z = new AnimationCurve(
                            new Keyframe(0f, -1f),
                            new Keyframe(1f, 1f)
                        );
        [SerializeField] private float m_valueScaleFactor = 1;
        [SerializeField] private float m_timeScaleFactor = 1;
        [SerializeField] private bool m_local = true;
        [SerializeField] private bool m_x = false;
        [SerializeField] private bool m_y = false;
        [SerializeField] private bool m_z = true;
        [SerializeField] private Vector3 m_offset = Vector3.zero;
        [SerializeField] private float m_timeOffset = 0;
        private float m_baseTime = 0;
        private float m_timer = 0;
        [SerializeField] private bool m_rotateItself = true;
        private Vector3 m_firstPivotVector = Vector3.zero;
        [SerializeField] private bool m_pivotIsChilde = false;


        // property________________________________________________________________
        public bool UpdateRotationAtUpdate { get => m_updateRotationAtUpdate; set => m_updateRotationAtUpdate = value; }
        public Initialize InitializeType { get => m_initializeType; set => m_initializeType = value; }
        public GameObject BaseGameObject { get => m_baseGameObject; set => m_baseGameObject = value; }
        public Transform Pivot
        {
            get => m_pivot; set
            {
                if (m_pivot != value)
                    FirstPivotVector = CalculatePivotVector(value.position);
                m_pivot = value;
            }
        }
        public AnimationCurve Curve_y { get => m_curve_y; set => m_curve_y = value; }
        public AnimationCurve Curve_x { get => m_curve_x; set => m_curve_x = value; }
        public AnimationCurve Curve_z { get => m_curve_z; set => m_curve_z = value; }
        public float ValueScaleFactor { get => m_valueScaleFactor; set => m_valueScaleFactor = value; }
        public float TimeScaleFactor { get => m_timeScaleFactor; set => m_timeScaleFactor = value; }
        public bool Local { get => m_local; set => m_local = value; }
        public bool X { get => m_x; set => m_x = value; }
        public bool Y { get => m_y; set => m_y = value; }
        public bool Z { get => m_z; set => m_z = value; }
        public Vector3 Offset { get => m_offset; set => m_offset = value; }
        public float TimeOffset { get => m_timeOffset; set => m_timeOffset = value; }
        public float BaseTime { get => m_baseTime; private set => m_baseTime = value; }
        public float Timer { get => m_timer; private set => m_timer = value; }
        public bool RotateItself { get => m_rotateItself; set => m_rotateItself = value; }
        public Vector3 FirstPivotVector { get => m_firstPivotVector; private set => m_firstPivotVector = value; }


        // monoBehaviour___________________________________________________________
        void Awake()
        {
            if (!BaseGameObject)
                BaseGameObject = gameObject;
        }
        void Start()
        {
            if (IsActive && InitializeType == Initialize.AutoPlayOnStart)
                StartTranslate();

            if (Pivot != null)
                FirstPivotVector = CalculatePivotVector(Pivot.position);
        }
        void Update()
        {
            if (UpdateRotationAtUpdate)
                UpdateRotation();
        }
        void OnEnable()
        {
            if (IsActive && InitializeType == Initialize.AutoPlayOnEnable)
                StartTranslate();
        }


        // function________________________________________________________________
        public void ResetTimer()
        {
            Timer = 0;
            BaseTime = Time.time;
        }
        public void StartTranslate()
        {
            ResetTimer();
            UpdateRotationAtUpdate = true;
            IsActive = true;
        }
        public void UpdateRotation()
        {
            if (!IsActive) return;

            // Set timer.
            Timer += Time.deltaTime;

            // Get value from curve.
            float currentCurveTime = (Time.time - BaseTime) * TimeScaleFactor + TimeOffset;

            // rotate 
            Vector3 tmpRot = new Vector3();
            if (Local)
            {
                tmpRot.x = X ? Curve_x.Evaluate(currentCurveTime) * ValueScaleFactor + Offset.x : BaseGameObject.transform.localRotation.x;
                tmpRot.y = Y ? Curve_y.Evaluate(currentCurveTime) * ValueScaleFactor + Offset.y : BaseGameObject.transform.localRotation.y;
                tmpRot.z = Z ? Curve_z.Evaluate(currentCurveTime) * ValueScaleFactor + Offset.z : BaseGameObject.transform.localRotation.z;
                if (RotateItself)
                    BaseGameObject.transform.localRotation = Quaternion.Euler(tmpRot);
            }
            else
            {
                tmpRot.x = X ? Curve_x.Evaluate(currentCurveTime) * ValueScaleFactor + Offset.x : BaseGameObject.transform.rotation.x;
                tmpRot.y = Y ? Curve_y.Evaluate(currentCurveTime) * ValueScaleFactor + Offset.y : BaseGameObject.transform.rotation.y;
                tmpRot.z = Z ? Curve_z.Evaluate(currentCurveTime) * ValueScaleFactor + Offset.z : BaseGameObject.transform.rotation.z;
                if (RotateItself)
                    BaseGameObject.transform.rotation = Quaternion.Euler(tmpRot);
            }

            // set position relative to pivot
            if (m_pivot != null)
            {
                Vector3 dir = FirstPivotVector; // get point direction relative to pivot
                dir = Quaternion.Euler(tmpRot) * dir; // rotate it
                Vector3 point = m_pivotIsChilde ? dir + m_pivot.localPosition : dir + m_pivot.position; // calculate rotated point
                BaseGameObject.transform.position = point;
            }
        }
        private Vector3 CalculatePivotVector(Vector3 pivotPos)
        {
            return BaseGameObject.transform.position - pivotPos;
        }
    }
}