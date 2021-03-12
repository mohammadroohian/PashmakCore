using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pashmak.Core.Common
{
    // enum____________________________________________________________________
    public enum Order { Ascending = 0, Descending = 1 }
    public enum SendMessageType
    {
        Normal = 0,
        Upwards = 1
    }
    public enum ParameterType
    {
        None = 0,
        IntValue = 1,
        FloatValue = 2,
        StringValue = 3,
        BoolValue = 4,
        GameObjectValue = 5
    }


    // class___________________________________________________________________
    public static class TransformUtils
    {
        public static void SnapP1ToP2(Transform p1, Transform p1_pivot, Transform p2_pivot, bool localRotation = false)
        {
            p1.position = p2_pivot.position;

            if (!p1_pivot.Equals(p1) && p1_pivot != null)
            {
                Vector3 deltaPosition = localRotation ?
                                        p1.localRotation * p1_pivot.localPosition :
                                        p1.rotation * p1_pivot.localPosition; // rotate it
                deltaPosition = new Vector3(deltaPosition.x * p1.localScale.x,
                                            deltaPosition.y * p1.localScale.y,
                                            deltaPosition.z * p1.localScale.z);
                p1.localPosition -= deltaPosition;
            }
        }
    }
    [System.Serializable]
    public class MessageToGameObject
    {
        // Variable________________________________________
        [SerializeField] private SendMessageType m_messageType = SendMessageType.Normal;
        [SerializeField] private string m_functionName;
        [SerializeField] private ParameterType m_functionParameterType = ParameterType.None;
        [SerializeField] private int m_intValue;
        [SerializeField] private float m_floatValue;
        [SerializeField] private string m_stringValue;
        [SerializeField] private bool m_boolValue;
        [SerializeField] private GameObject m_gameObjectValue;


        // property________________________________________
        public SendMessageType MessageType { get => m_messageType; set => m_messageType = value; }
        public string FunctionName { get => m_functionName; set => m_functionName = value; }
        public ParameterType FunctionParameterType { get => m_functionParameterType; set => m_functionParameterType = value; }
        public int IntValue { get => m_intValue; set => m_intValue = value; }
        public float FloatValue { get => m_floatValue; set => m_floatValue = value; }
        public string StringValue { get => m_stringValue; set => m_stringValue = value; }
        public bool BoolValue { get => m_boolValue; set => m_boolValue = value; }
        public GameObject GameObjectValue { get => m_gameObjectValue; set => m_gameObjectValue = value; }


        // Function________________________________________
        public void SendMessageToObject(GameObject gameObj)
        {
            switch (MessageType)
            {
                case SendMessageType.Normal:
                    switch (FunctionParameterType)
                    {
                        case ParameterType.None:
                            gameObj.SendMessage(FunctionName, UnityEngine.SendMessageOptions.DontRequireReceiver);
                            break;
                        case ParameterType.IntValue:
                            gameObj.SendMessage(FunctionName, IntValue, UnityEngine.SendMessageOptions.DontRequireReceiver);
                            break;
                        case ParameterType.FloatValue:
                            gameObj.SendMessage(FunctionName, FloatValue, UnityEngine.SendMessageOptions.DontRequireReceiver);
                            break;
                        case ParameterType.StringValue:
                            gameObj.SendMessage(FunctionName, StringValue, UnityEngine.SendMessageOptions.DontRequireReceiver);
                            break;
                        case ParameterType.BoolValue:
                            gameObj.SendMessage(FunctionName, BoolValue, UnityEngine.SendMessageOptions.DontRequireReceiver);
                            break;
                        case ParameterType.GameObjectValue:
                            gameObj.SendMessage(FunctionName, GameObjectValue, UnityEngine.SendMessageOptions.DontRequireReceiver);
                            break;
                    }
                    break;
                case SendMessageType.Upwards:
                    switch (FunctionParameterType)
                    {
                        case ParameterType.None:
                            gameObj.SendMessageUpwards(FunctionName, UnityEngine.SendMessageOptions.DontRequireReceiver);
                            break;
                        case ParameterType.IntValue:
                            gameObj.SendMessageUpwards(FunctionName, IntValue, UnityEngine.SendMessageOptions.DontRequireReceiver);
                            break;
                        case ParameterType.FloatValue:
                            gameObj.SendMessageUpwards(FunctionName, FloatValue, UnityEngine.SendMessageOptions.DontRequireReceiver);
                            break;
                        case ParameterType.StringValue:
                            gameObj.SendMessageUpwards(FunctionName, StringValue, UnityEngine.SendMessageOptions.DontRequireReceiver);
                            break;
                        case ParameterType.BoolValue:
                            gameObj.SendMessageUpwards(FunctionName, BoolValue, UnityEngine.SendMessageOptions.DontRequireReceiver);
                            break;
                        case ParameterType.GameObjectValue:
                            gameObj.SendMessageUpwards(FunctionName, GameObjectValue, UnityEngine.SendMessageOptions.DontRequireReceiver);
                            break;
                    }
                    break;
            }
        }
    }
}