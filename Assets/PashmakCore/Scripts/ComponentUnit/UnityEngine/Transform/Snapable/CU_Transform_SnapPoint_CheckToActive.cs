using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

namespace Pashmak.Core.CU._UnityEngine._Transform
{
    public class CU_Transform_SnapPoint_CheckToActive : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private CU_Transform_SnapPoint m_snapPoint = null;
        [SerializeField] private bool m_checkAtStart = false;
        [SerializeField] private bool m_checkAtUpdate = false;
        [ReorderableList]
        [SerializeField] private List<SnapableCondition> m_conditions = new List<SnapableCondition>();
        [SerializeField] private UnityEvent m_onToActive = new UnityEvent();
        [SerializeField] private UnityEvent m_onToDeactive = new UnityEvent();
        private bool m_lastCondition = false;


        // property________________________________________________________________
        public CU_Transform_SnapPoint SnapPoint { get => m_snapPoint; set => m_snapPoint = value; }
        public bool CheckAtStart { get => m_checkAtStart; set => m_checkAtStart = value; }
        public bool CheckAtUpdate { get => m_checkAtUpdate; set => m_checkAtUpdate = value; }
        public List<SnapableCondition> Conditions { get => m_conditions; set => m_conditions = value; }
        public UnityEvent OnToActive { get => m_onToActive; set => m_onToActive = value; }
        public UnityEvent OnToDeactive { get => m_onToDeactive; set => m_onToDeactive = value; }


        // monoBehaviour___________________________________________________________
        private void Awake()
        {
            if (SnapPoint == null)
                SnapPoint = GetComponent<CU_Transform_SnapPoint>();

            m_lastCondition = SnapPoint.IsActive;
        }
        private void Start()
        {
            if (CheckAtStart)
                CheckToBeActive();
        }
        private void Update()
        {
            if (CheckAtUpdate)
                CheckToBeActive();
        }


        // function________________________________________________________________
        public void CheckToBeActive()
        {
            if (!IsActive) return;
            SnapPoint.IsActive = CheckConditions();
            if (m_lastCondition != SnapPoint.IsActive)
            {
                m_lastCondition = SnapPoint.IsActive;
                if (m_lastCondition) OnToActive.Invoke();
                else OnToDeactive.Invoke();
            }
        }
        public bool CheckConditions()
        {
            for (int i = 0; i < Conditions.Count; i++)
            {
                if (Conditions[i].m_snapPoint.IsFull != Conditions[i].m_full)
                    return false;
            }
            return true;
        }


        // class___________________________________________________________________
        [System.Serializable]
        public class SnapableCondition
        {
            public bool m_full = true;
            public CU_Transform_SnapPoint m_snapPoint;
        }
    }
}