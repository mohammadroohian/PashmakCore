using UnityEngine;

namespace Pashmak.Core
{
    [System.Serializable]
    public class ObjectNameId
    {
        // variable________________________________________________________________
        [SerializeField] private string m_nameId = "_nameId_";
        [SerializeField] private GameObject m_obj = null;


        // property________________________________________________________________
        public string NameId { get => m_nameId; private set => m_nameId = value; }
        public GameObject Obj { get => m_obj; set => m_obj = value; }
    }
}