using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Pashmak.Core.CU.CU_ColorFader;

namespace Pashmak.Core.CU._UnityEngine._Transform
{
    public class CU_Transform_CurvePosition : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_updatePositionAtUpdate = false;
        [SerializeField] protected GameObject m_baseGameObject = null;
        [SerializeField] private Initialize m_initializeType = Initialize.AutoPlayOnStart;
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
        [SerializeField] private bool m_y = true;
        [SerializeField] private bool m_z = false;
        [SerializeField] private Vector3 m_offset = Vector3.zero;
        [SerializeField] private float m_timeOffset = 0;
        private float m_baseTime = 0;
        private float m_timer = 0;


        // property________________________________________________________________
        public bool UpdatePositionAtUpdate { get => m_updatePositionAtUpdate; set => m_updatePositionAtUpdate = value; }
        public GameObject BaseGameObject { get => m_baseGameObject; set => m_baseGameObject = value; }
        public Initialize InitializeType { get => m_initializeType; set => m_initializeType = value; }
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
        }
        void Update()
        {
            if (UpdatePositionAtUpdate)
                UpdatePosition();
        }
        void OnEnable()
        {
            if (IsActive && InitializeType == Initialize.AutoPlayOnEnable)
                StartTranslate();
        }
        void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                if (BaseGameObject == null)
                    BaseGameObject = gameObject;
                if (X)
                    BaseGameObject.transform.localPosition = new Vector3(Offset.x,
                                                        BaseGameObject.transform.localPosition.y,
                                                        BaseGameObject.transform.localPosition.z);
                if (Y)
                    BaseGameObject.transform.localPosition = new Vector3(BaseGameObject.transform.localPosition.x,
                                                        Offset.y,
                                                        BaseGameObject.transform.localPosition.z);
                if (Z)
                    BaseGameObject.transform.localPosition = new Vector3(BaseGameObject.transform.localPosition.x,
                                                        BaseGameObject.transform.localPosition.y,
                                                        Offset.z);
            }

            Vector3 newPos = Offset;
            if (BaseGameObject.transform.parent != null)
                newPos += BaseGameObject.transform.parent.position;

            float x0 = Curve_x.keys[0].value * ValueScaleFactor;
            float x1 = Curve_x.keys[Curve_x.keys.Length - 1].value * ValueScaleFactor;

            float y0 = Curve_y.keys[0].value * ValueScaleFactor;
            float y1 = Curve_y.keys[Curve_y.keys.Length - 1].value * ValueScaleFactor;

            float z0 = Curve_z.keys[0].value * ValueScaleFactor;
            float z1 = Curve_z.keys[Curve_z.keys.Length - 1].value * ValueScaleFactor;

            Vector3 tmpVec = new Vector3(newPos.x + x0, newPos.y + y0, newPos.z + z0);
            Vector3 tmpVec2 = new Vector3(newPos.x + x1, newPos.y + y1, newPos.z + z1);

            if (X)
            {
                Gizmos.color = Color.red;
                Vector3 firstPos = new Vector3(newPos.x + x0, newPos.y, newPos.z);
                Vector3 secondPos = new Vector3(newPos.x + x1, newPos.y, newPos.z);
                Gizmos.DrawLine(firstPos, secondPos);
            }
            if (Y)
            {
                Gizmos.color = Color.green;
                Vector3 firstPos = new Vector3(newPos.x, newPos.y + y0, newPos.z);
                Vector3 secondPos = new Vector3(newPos.x, newPos.y + y1, newPos.z);
                Gizmos.DrawLine(firstPos, secondPos);
            }
            if (Z)
            {
                Gizmos.color = Color.cyan;
                Vector3 firstPos = new Vector3(newPos.x, newPos.y, newPos.z + z0);
                Vector3 secondPos = new Vector3(newPos.x, newPos.y, newPos.z + z1);
                Gizmos.DrawLine(firstPos, secondPos);
            }

            Gizmos.color = Color.yellow;
            Vector3 firstPosFinal = new Vector3();
            Vector3 secondPosFinal = new Vector3();
            if (X && Y && Z)
            {
                firstPosFinal = tmpVec;
                secondPosFinal = tmpVec2;
            }
            else if (X && Y)
            {
                firstPosFinal = new Vector3(tmpVec.x, tmpVec.y, newPos.z);
                secondPosFinal = new Vector3(tmpVec2.x, tmpVec2.y, newPos.z);
            }
            else if (X && Z)
            {
                firstPosFinal = new Vector3(tmpVec.x, newPos.y, tmpVec.z);
                secondPosFinal = new Vector3(tmpVec2.x, newPos.y, tmpVec2.z);
            }
            else if (Z && Y)
            {
                firstPosFinal = new Vector3(newPos.x, tmpVec.y, tmpVec.z);
                secondPosFinal = new Vector3(newPos.x, tmpVec2.y, tmpVec2.z);
            }
            Gizmos.DrawLine(firstPosFinal, secondPosFinal);

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
            UpdatePositionAtUpdate = true;
            IsActive = true;
        }
        public void UpdatePosition()
        {
            if (!IsActive) return;

            // Set timer.
            Timer += Time.deltaTime;

            // Get value from curve.
            float currentCurveTime = (Time.time - BaseTime) * TimeScaleFactor + TimeOffset;
            Vector3 tmpPos = new Vector3();
            if (Local)
            {
                tmpPos.x = X ? Curve_x.Evaluate(currentCurveTime) * ValueScaleFactor + Offset.x : BaseGameObject.transform.localPosition.x;
                tmpPos.y = Y ? Curve_y.Evaluate(currentCurveTime) * ValueScaleFactor + Offset.y : BaseGameObject.transform.localPosition.y;
                tmpPos.z = Z ? Curve_z.Evaluate(currentCurveTime) * ValueScaleFactor + Offset.z : BaseGameObject.transform.localPosition.z;
                transform.localPosition = tmpPos;
            }
            else
            {
                tmpPos.x = X ? Curve_x.Evaluate(currentCurveTime) * ValueScaleFactor + Offset.x : BaseGameObject.transform.position.x;
                tmpPos.y = Y ? Curve_y.Evaluate(currentCurveTime) * ValueScaleFactor + Offset.y : BaseGameObject.transform.position.y;
                tmpPos.z = Z ? Curve_z.Evaluate(currentCurveTime) * ValueScaleFactor + Offset.z : BaseGameObject.transform.position.z;
                transform.position = tmpPos;
            }
        }
    }
}