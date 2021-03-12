using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Pashmak.Core.CU._UnityEngine._LineRenderer
{
    public class CU_LineRenderer : CU_Component
    {
        // variable________________________________________________________________
        public bool m_syncOnEditor = false;
        [SerializeField] private bool m_syncAtStart = false;
        [SerializeField] private bool m_syncAtUpdate = false;
        [ReorderableList]
        [SerializeField] private List<Transform> m_points = new List<Transform>();
        [SerializeField] private LineRenderer m_line = null;
        [SerializeField] private bool m_local = true;


        // property________________________________________________________________
        public bool SyncAtStart { get => m_syncAtStart; set => m_syncAtStart = value; }
        public bool SyncAtUpdate { get => m_syncAtUpdate; set => m_syncAtUpdate = value; }
        public List<Transform> Points { get => m_points; set => m_points = value; }
        public LineRenderer Line { get => m_line; set => m_line = value; }
        public bool Local { get => m_local; set => m_local = value; }


        // monoBehaviour___________________________________________________________
        private void Awake()
        {
            if (Line == null)
                Line = GetComponent<LineRenderer>();
        }
        private void Start()
        {
            if (SyncAtStart)
                Sync();
        }
        private void Update()
        {
            if (!IsActive) return;
            if (!SyncAtUpdate) return;

            Sync();
        }
        void OnDrawGizmos()
        {
            if (m_syncOnEditor)
                Sync();
        }


        // function________________________________________________________________
        public void Sync()
        {
            if (!IsActive) return;

            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i] == null) continue;
                if (Local)
                    Line.SetPosition(i, Points[i].localPosition);
                else
                    Line.SetPosition(i, Points[i].position);
            }
        }
    }
}