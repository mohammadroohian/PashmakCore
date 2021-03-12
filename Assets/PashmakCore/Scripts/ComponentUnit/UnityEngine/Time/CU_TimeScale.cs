using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pashmak.Core.CU._UnityEngine
{
    public class CU_TimeScale : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_setAtStart = false;
        [SerializeField] private float m_timeScale = 1;


        // property________________________________________________________________
        public float TimeScale { get => m_timeScale; set => m_timeScale = value; }


        // monoBehaviour___________________________________________________________
        void Start()
        {
            if (m_setAtStart)
                SetScale();
        }


        // function________________________________________________________________
        public void SetScale()
        {
            if (!IsActive) return;
            Time.timeScale = TimeScale;
        }
    }
}