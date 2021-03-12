using System.Collections.Generic;
using UnityEngine;

namespace Pashmak.Core.CU._UnityEngine._GameObject
{
    public class CU_GameObject_Destroy : CU_Component, IDefaultExcute
    {
        // enum____________________________________________________________________
        public enum AffectOnChildren { None = 0, FirstChild = 1, LastChild = 2, AllChildrenObjects = 3 }


        // variable________________________________________________________________
        [SerializeField] private bool m_destroyAtStart = false;
        /// <summary>
        /// The object to destroy.
        /// </summary>
        [SerializeField] private GameObject m_baseGameObject = null;
        /// <summary>
        /// The optional amount of time to delay before destroying the object.
        /// </summary>
        [SerializeField] private float m_delay = 0.0f;
        [SerializeField] private AffectOnChildren m_justChildren = AffectOnChildren.None;


        // property________________________________________________________________
        public bool DestroyAtStart { get => m_destroyAtStart; private set => m_destroyAtStart = value; }
        public GameObject BaseGameObject { get => m_baseGameObject; set => m_baseGameObject = value; }
        public float Delay { get => m_delay; set => m_delay = value; }
        public AffectOnChildren JustChildren { get => m_justChildren; set => m_justChildren = value; }


        // implement_______________________________________________________________
        void IDefaultExcute.DefaultExecute() => DestroyGameObject();


        // override________________________________________________________________
        public override string GetDetails(string gameObjectName, string componentName, string methodName, string methodParams)
        {
            methodName = "Destroy";
            if (BaseGameObject == null)
                methodParams = "null";
            else
                methodParams = BaseGameObject.name;
            componentName = "";
            return base.GetDetails(gameObjectName, componentName, methodName, methodParams);
        }


        // monoBehaviour___________________________________________________________
        private void Awake()
        {
            if (BaseGameObject == null)
                BaseGameObject = this.gameObject;
        }
        private void Start()
        {
            if (DestroyAtStart)
                DestroyGameObject();
        }


        // function________________________________________________________________
        /// <summary>
        /// Removes a gameobject, component or asset.
        /// </summary>
        public void DestroyGameObject()
        {
            if (!IsActive) return;
            // Check there is any object to destroy or not.
            if (!BaseGameObject)
            {
                Debug.LogError("Destroy() -- no object assigned!");
                return;
            }

            // Destroy
            List<GameObject> targetObjects = GetGameObject(BaseGameObject);
            if (m_delay < 0.0001)
            {
                for (int i = 0; i < targetObjects.Count; i++)
                {
                    Destroy(targetObjects[i]);
                }
            }
            else
            {
                for (int i = 0; i < targetObjects.Count; i++)
                {
                    Destroy(targetObjects[i], m_delay);
                }
            }
        }
        /// <summary>
        /// Destroys the object obj immediately.
        /// </summary>
        public void DestroyGameObjectImmediate()
        {
            if (!IsActive) return;
            // Check there is any object to destroy or not.
            if (!BaseGameObject)
            {
                Debug.LogError("DestroyImmediate() -- no object assigned!");
                return;
            }

            // Destroy immediately.
            List<GameObject> targetObjects = GetGameObject(BaseGameObject);
            for (int i = 0; i < targetObjects.Count; i++)
            {
                DestroyImmediate(targetObjects[i]);
            }
        }
        private List<GameObject> GetGameObject(GameObject obj)
        {
            List<GameObject> result = new List<GameObject>();
            switch (JustChildren)
            {
                case AffectOnChildren.None:
                    result.Add(obj);
                    break;
                case AffectOnChildren.FirstChild:
                    result.Add(obj.transform.GetChild(0).gameObject);
                    break;
                case AffectOnChildren.LastChild:
                    result.Add(obj.transform.GetChild(obj.transform.childCount - 1).gameObject);
                    break;
                case AffectOnChildren.AllChildrenObjects:
                    for (int i = 0; i < obj.transform.childCount; i++)
                    {
                        result.Add(obj.transform.GetChild(i).gameObject);
                    }
                    break;
            }
            return result;
        }
    }
}