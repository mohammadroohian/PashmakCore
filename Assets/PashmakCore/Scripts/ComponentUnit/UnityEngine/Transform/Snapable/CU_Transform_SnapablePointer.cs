using System.Collections.Generic;
using UnityEngine;

namespace Pashmak.Core.CU._UnityEngine._Transform
{
    [RequireComponent(typeof(Collider2D))]
    public class CU_Transform_SnapablePointer : MonoBehaviour
    {
        // variable________________________________________________________________
        [SerializeField] private CU_Transform_SnapPoint m_snapable;


        // property________________________________________________________________
        public CU_Transform_SnapPoint Snapable { get => m_snapable; set => m_snapable = value; }


        // monoBehaviour___________________________________________________________
        private void Awake()
        {
            if (Snapable == null)
                Debug.LogError("snapable is not setted.", gameObject);
        }
    }
}