using Pashmak.Core.Common;
using UnityEngine;
using NaughtyAttributes;

namespace Pashmak.Core.CU._UnityEngine._GameObject
{
    public class CU_GameObject_Instantiate : CU_Component, IDefaultExcute
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_instantiateAtStart = false;
        /// <summary>
        /// An existing object that you want make a copy of.
        /// </summary>
        [SerializeField] [ShowAssetPreview(96, 96)] private GameObject m_sampleGameObject = null;
        /// <summary>
        /// Position for the new object.
        /// </summary>
        [SerializeField] private Transform m_position = null;
        /// <summary>
        /// Orientation of the new object.
        /// </summary>
        [SerializeField] private Transform m_rotation = null;
        /// <summary>
        /// Parent that will be assigned to the new object.
        /// </summary>
        [SerializeField] private Transform m_parent = null;
        [SerializeField] private MessageToGameObject m_sendMessageOptions = new MessageToGameObject();


        // property________________________________________________________________
        public bool InstantiateAtStart { get => m_instantiateAtStart; private set => m_instantiateAtStart = value; }
        public GameObject SampleGameObject { get => m_sampleGameObject; set => m_sampleGameObject = value; }
        public Transform Position { get => m_position; set => m_position = value; }
        public Transform Rotation { get => m_rotation; set => m_rotation = value; }
        public Transform Parent { get => m_parent; set => m_parent = value; }
        public MessageToGameObject SendMessageOptions { get => m_sendMessageOptions; private set => m_sendMessageOptions = value; }


        // implement_______________________________________________________________
        void IDefaultExcute.DefaultExecute() => InstantiateGameObject();


        // override________________________________________________________________
        public override string GetDetails(string gameObjectName, string componentName, string methodName, string methodParams)
        {
            methodName = "Instantiate";
            if (SampleGameObject == null)
                methodParams = "null";
            else
                methodParams = SampleGameObject.name;
            componentName = "";
            return base.GetDetails(gameObjectName, componentName, methodName, methodParams);
        }


        // monoBehaviour___________________________________________________________
        private void Start()
        {
            if (InstantiateAtStart)
                InstantiateGameObject();
        }


        // function________________________________________________________________
        /// <summary>
        /// Clones the object original with defult values.
        /// </summary>
        public void InstantiateGameObject()
        {
            if (!IsActive) return;

            // Check there is any object to instantiate or not.
            if (!SampleGameObject)
            {
                Debug.LogError("no original object assigned!");
                return;
            }

            // Set Default position and rotation.
            Vector3 tmpPosition = SampleGameObject.transform.position;
            Quaternion tmpRotation = SampleGameObject.transform.rotation;

            // Override position if is passible.
            if (Position)
                tmpPosition = Position.position;

            // Override rotation if is possible.
            if (Rotation)
                tmpRotation = Rotation.rotation;

            GameObject tmpObj = null;

            // Check for parent // Parent that will be assigned to the new object.
            if (Parent)
                tmpObj = Instantiate(SampleGameObject, tmpPosition, tmpRotation, Parent);
            else
                tmpObj = Instantiate(SampleGameObject, tmpPosition, tmpRotation);

            // call function throw the Instantiated object
            if (tmpObj != null && !string.IsNullOrWhiteSpace(SendMessageOptions.FunctionName))
                SendMessageOptions.SendMessageToObject(tmpObj);
        }
    }
}