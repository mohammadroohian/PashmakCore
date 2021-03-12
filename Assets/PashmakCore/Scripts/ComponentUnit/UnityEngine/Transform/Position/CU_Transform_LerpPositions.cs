using System.Collections.Generic;
using UnityEngine;


namespace Pashmak.Core.CU._UnityEngine._Transform
{
    public class CU_Transform_LerpPositions : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_translateAtStart = false;
        [SerializeField] protected GameObject m_baseGameObject = null;
        [SerializeField] private Transform m_pivot = null;
        [SerializeField] private float m_speed = 1f;
        [SerializeField] private List<Transform> m_points = new List<Transform>();
        [SerializeField] private float m_threshold = .2f;
        [SerializeField] private bool m_isLoop = false;
        private int m_currentTargetIndex = 0;
        private Transform m_currentTarget;
        private Vector3 m_pivotVector = Vector3.zero;


        // property________________________________________________________________
        public bool TranslateAtStart { get => m_translateAtStart; private set => m_translateAtStart = value; }
        public GameObject BaseGameObject { get => m_baseGameObject; set => m_baseGameObject = value; }
        public Transform Pivot { get => m_pivot; set => m_pivot = value; }
        public float Speed { get => m_speed; set => m_speed = value; }
        public List<Transform> Points { get => m_points; private set => m_points = value; }
        public float Threshold { get => m_threshold; set => m_threshold = value; }
        public bool IsLoop { get => m_isLoop; set => m_isLoop = value; }
        public int CurrentTargetIndex { get => m_currentTargetIndex; private set => m_currentTargetIndex = value; }
        public Transform CurrentTarget { get => m_currentTarget; private set => m_currentTarget = value; }
        public Vector3 PivotVector { get => m_pivotVector; private set => m_pivotVector = value; }


        // monoBehaviour___________________________________________________________
        void Awake()
        {
            if (!BaseGameObject)
                BaseGameObject = gameObject;

            if (Pivot != null)
                PivotVector = BaseGameObject.transform.position - Pivot.position;
        }
        void Start()
        {
            if (TranslateAtStart)
                Run();
        }
        void Update()
        {
            if (!m_isActive) return;
            if (CurrentTarget == null) return;

            BaseGameObject.transform.position = Vector3.Lerp(BaseGameObject.transform.position, CurrentTarget.position + PivotVector, Speed * Time.deltaTime);

            if (Vector3.Distance(CurrentTarget.position + PivotVector, BaseGameObject.transform.position) < Threshold)
                TranslateToNextPoint();
        }


        // function________________________________________________________________
        public void Run()
        {
            m_isActive = true;
            Reset();
        }
        public void End()
        {
            m_isActive = false;
            Reset();
        }
        public void Reset()
        {
            CurrentTargetIndex = 0;
            CurrentTarget = Points[CurrentTargetIndex];
        }
        public void TranslateToNextPoint()
        {
            CurrentTargetIndex++;
            if (CurrentTargetIndex >= Points.Count)
            {
                if (IsLoop)
                    Reset();
                else
                    End();
            }
            else
                CurrentTarget = Points[CurrentTargetIndex];
        }
        public void AddPont(Transform point)
        {
            Points.Add(point);
        }
        public void ClearPoint()
        {
            Points.Clear();
        }
        public void GoToPosition(Transform target)
        {
            Points.Clear();
            Points.Add(target);
            Run();
        }
    }
}