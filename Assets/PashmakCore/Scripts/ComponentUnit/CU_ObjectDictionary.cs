using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Pashmak.Core
{
    public class CU_ObjectDictionary : MonoBehaviour
    {
        // variable________________________________________________________________
        [ReorderableList]
        [SerializeField] private List<ObjectNameId> m_objects = new List<ObjectNameId>();
        private Dictionary<string, ObjectNameId> m_objectsDict = new Dictionary<string, ObjectNameId>();
        public GameObject this[int i] => m_objects[i].Obj;
        public GameObject this[string nameId] => GetObject(nameId);


        // property________________________________________________________________
        public Dictionary<string, ObjectNameId> Objects { get => m_objectsDict; private set => m_objectsDict = value; }


        // monoBehaviour___________________________________________________________
        private void Awake()
        {
            FillDict();
        }


        // function________________________________________________________________
        public bool ContainsKey(string objectKey)
        {
            return Objects.ContainsKey(objectKey);
        }
        public ObjectNameId GetObjectInfo(string objectKey)
        {
#if UNITY_EDITOR
            FillDict();
#endif
            return Objects[objectKey.ToLower().Trim()];
        }
        public GameObject GetObject(string objectKey)
        {
            return GetObjectInfo(objectKey).Obj;
        }
        private void FillDict()
        {
            m_objectsDict = new Dictionary<string, ObjectNameId>();
            foreach (var item in m_objects)
            {
                Objects.Add(item.NameId.ToLower().Trim(), item);
            }
        }
    }
}