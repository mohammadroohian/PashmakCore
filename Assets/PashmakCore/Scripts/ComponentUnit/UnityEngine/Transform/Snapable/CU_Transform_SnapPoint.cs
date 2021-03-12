using System.Collections.Generic;
using Pashmak.Core.Common;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

namespace Pashmak.Core.CU._UnityEngine._Transform
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CU_Transform_SnapPoint : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private GameObject m_baseGameObject = null;
        [BoxGroup("KeyCode")]
        [SerializeField] private bool m_multiKeyCode = false;
        [BoxGroup("KeyCode")]
        [HideIf("m_multiKeyCode")]
        [SerializeField] private string m_keyCode = "";
        [BoxGroup("KeyCode")]
        [ShowIf("m_multiKeyCode")]
        [ReorderableList]
        [SerializeField] private List<string> m_keyCodes = new List<string>() { "key1", "key2" };
        [BoxGroup("KeyCode")]
        [SerializeField] private bool m_caseSensitiveKeyCode = false;
        [SerializeField] private Transform m_pivot = null;
        [SerializeField] private UnityEvent m_onSetSnappedObj = new UnityEvent();
        [SerializeField] private UnityEvent m_onDropSnappedObj = new UnityEvent();
        private CU_Transform_SnapObj m_snappedObject;


        // property________________________________________________________________
        public GameObject BaseGameObject { get => m_baseGameObject; set => m_baseGameObject = value; }
        public bool MultiKeyCode { get => m_multiKeyCode; set => m_multiKeyCode = value; }
        public string KeyCode { get => m_keyCode; private set => m_keyCode = value; }
        public List<string> KeyCodes { get => m_keyCodes; set => m_keyCodes = value; }
        public bool CaseSensitiveKeyCode { get => m_caseSensitiveKeyCode; set => m_caseSensitiveKeyCode = value; }
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
        public UnityEvent OnSetSnappedObj { get => m_onSetSnappedObj; private set => m_onSetSnappedObj = value; }
        public UnityEvent OnDropSnappedObj { get => m_onDropSnappedObj; private set => m_onDropSnappedObj = value; }
        public bool IsFull { get { return SnappedObject == null ? false : true; } }
        public CU_Transform_SnapObj SnappedObject { get => m_snappedObject; set => m_snappedObject = value; }


        // monoBehaviour___________________________________________________________
        protected void Awake()
        {
            if (!BaseGameObject)
                BaseGameObject = gameObject;

            CleanKeyCodes();
        }


        // function________________________________________________________________
        public bool ContainsKey(string key)
        {
            if (MultiKeyCode)
            {
                return KeyCodes.Contains(CaseSensitiveKeyCode ? key.Trim() : key.Trim().ToLower());
            }
            else
            {
                return string.Equals(CaseSensitiveKeyCode ? key.Trim() : key.Trim().ToLower(),
                                    CaseSensitiveKeyCode ? KeyCode.Trim() : KeyCode.Trim().ToLower());
            }
        }
        public void CleanKeyCodes()
        {
            for (int i = 0; i < KeyCodes.Count; i++)
            {
                KeyCodes[i] = KeyCodes[i].Trim();
                if (!CaseSensitiveKeyCode)
                    KeyCodes[i] = KeyCodes[i].ToLower();
            }
        }
        public void SetObj(CU_Transform_SnapObj obj)
        {
            // give snapable to snap point
            SnappedObject = obj;

            // run event
            OnSetSnappedObj.Invoke();
        }
        public void DropObj()
        {
            if (!IsFull) return;

            SnappedObject = null;
            OnDropSnappedObj.Invoke();
        }
        public void UnsnapSnappedObj()
        {
            if (IsFull)
                SnappedObject.Unsnap();
        }
    }
}