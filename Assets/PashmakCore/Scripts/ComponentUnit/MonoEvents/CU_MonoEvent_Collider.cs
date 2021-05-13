using NaughtyAttributes;
using Pashmak.Core.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Pashmak.Core.CU.MonoEvent
{
    public class CU_MonoEvent_Collider : CU_Component
    {
        // enum____________________________________________________________________
        public enum EventDetection
        {
            AutoDetect = 0,
            Collision = 1,
            Trigger = 2
        }
        public enum ColliderState
        {
            Enter = 0,
            Stay = 1,
            Exit = 2
        }
        public enum ColliderDemention
        {
            _2D = 0,
            _3D = 1
        }


        // variable________________________________________________________________
        [SerializeField] EventDetection m_detection = EventDetection.AutoDetect;
        private Collider m_tmpEventTypeGetFromComponent;
        private Collider2D m_tmpEventTypeGetFromComponent2D;
        [SerializeField] ColliderState m_state = ColliderState.Enter;
        [SerializeField] ColliderDemention m_demention = ColliderDemention._2D;
        [Tag]
        [SerializeField] private string m_objectTag = "Untagged";//Tag of game object that collision with this.if be empty means every tag can work.
        [SerializeField] UnityEvent m_onDetect = new UnityEvent();
        [SerializeField] private MessageToGameObject m_sendMessageOptions = new MessageToGameObject();


        // property________________________________________________________________
        public EventDetection Detection { get => m_detection; set => m_detection = value; }
        public ColliderState State { get => m_state; set => m_state = value; }
        public SendMessageType MessageType { get => SendMessageOptions.MessageType; private set => SendMessageOptions.MessageType = value; }
        public string SendMessageFunction { get => SendMessageOptions.FunctionName; private set => SendMessageOptions.FunctionName = value; }
        public ColliderDemention Demention { get => m_demention; set => m_demention = value; }
        public string ObjectTag { get => m_objectTag; set => m_objectTag = value; }
        public UnityEvent OnDetect { get => m_onDetect; private set => m_onDetect = value; }
        public MessageToGameObject SendMessageOptions { get => m_sendMessageOptions; private set => m_sendMessageOptions = value; }
        public bool IsTrigger
        {
            get
            {
                switch (Detection)
                {
                    case EventDetection.AutoDetect:
                        if (Demention == ColliderDemention._3D)
                            return m_tmpEventTypeGetFromComponent.isTrigger;
                        else
                            return m_tmpEventTypeGetFromComponent2D.isTrigger;
                    case EventDetection.Collision:
                        return false;
                    case EventDetection.Trigger:
                        return true;
                    default:
                        return false;
                }
            }
        }



        // monoBehaviour___________________________________________________________
        private void Awake()
        {
            if (Detection == EventDetection.AutoDetect)
            {
                if (Demention == ColliderDemention._3D)
                    m_tmpEventTypeGetFromComponent = GetComponentInChildren<Collider>();
                else
                    m_tmpEventTypeGetFromComponent2D = GetComponentInChildren<Collider2D>();
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (
                m_isActive &&
                CheckTag(collider.gameObject) &&
                IsTrigger &&
                State == ColliderState.Enter &&
                Demention == ColliderDemention._2D)
            {
                SendMessageOptions.SendMessageToObject(collider.gameObject);
                OnDetect.Invoke();
            }
        }
        private void OnTriggerStay2D(Collider2D collider)
        {
            if (
                m_isActive &&
                CheckTag(collider.gameObject) &&
                IsTrigger &&
                State == ColliderState.Stay &&
                Demention == ColliderDemention._2D)
            {
                SendMessageOptions.SendMessageToObject(collider.gameObject);
                OnDetect.Invoke();
            }
        }
        private void OnTriggerExit2D(Collider2D collider)
        {
            if (
                m_isActive &&
                CheckTag(collider.gameObject) &&
                IsTrigger &&
                State == ColliderState.Exit &&
                Demention == ColliderDemention._2D)
            {
                SendMessageOptions.SendMessageToObject(collider.gameObject);
                OnDetect.Invoke();
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (
                m_isActive &&
                CheckTag(collider.gameObject) &&
                IsTrigger &&
                State == ColliderState.Enter &&
                Demention == ColliderDemention._3D)
            {
                SendMessageOptions.SendMessageToObject(collider.gameObject);
                OnDetect.Invoke();
            }
        }
        private void OnTriggerStay(Collider collider)
        {
            if (
                m_isActive &&
                CheckTag(collider.gameObject) &&
                IsTrigger &&
                State == ColliderState.Stay &&
                Demention == ColliderDemention._3D)
            {
                SendMessageOptions.SendMessageToObject(collider.gameObject);
                OnDetect.Invoke();
            }
        }
        private void OnTriggerExit(Collider collider)
        {
            if (
                m_isActive &&
                CheckTag(collider.gameObject) &&
                IsTrigger &&
                State == ColliderState.Exit &&
                Demention == ColliderDemention._3D)
            {
                SendMessageOptions.SendMessageToObject(collider.gameObject);
                OnDetect.Invoke();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (
                m_isActive &&
                CheckTag(collision.gameObject) &&
                !IsTrigger &&
                State == ColliderState.Enter &&
                Demention == ColliderDemention._2D)
            {
                SendMessageOptions.SendMessageToObject(collision.gameObject);
                OnDetect.Invoke();
            }
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (
                m_isActive &&
                CheckTag(collision.gameObject) &&
                !IsTrigger &&
                State == ColliderState.Stay &&
                Demention == ColliderDemention._2D)
            {
                SendMessageOptions.SendMessageToObject(collision.gameObject);
                OnDetect.Invoke();
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (
                m_isActive &&
                CheckTag(collision.gameObject) &&
                !IsTrigger &&
                State == ColliderState.Exit &&
                Demention == ColliderDemention._2D)
            {
                SendMessageOptions.SendMessageToObject(collision.gameObject);
                OnDetect.Invoke();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (
                m_isActive &&
                CheckTag(collision.gameObject) &&
                !IsTrigger &&
                State == ColliderState.Enter &&
                Demention == ColliderDemention._3D)
            {
                SendMessageOptions.SendMessageToObject(collision.gameObject);
                OnDetect.Invoke();
            }
        }
        private void OnCollisionStay(Collision collision)
        {
            if (
                m_isActive &&
                CheckTag(collision.gameObject) &&
                !IsTrigger &&
                State == ColliderState.Stay &&
                Demention == ColliderDemention._3D)
            {
                SendMessageOptions.SendMessageToObject(collision.gameObject);
                OnDetect.Invoke();
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (
                m_isActive &&
                CheckTag(collision.gameObject) &&
                !IsTrigger &&
                State == ColliderState.Exit &&
                Demention == ColliderDemention._3D)
            {
                SendMessageOptions.SendMessageToObject(collision.gameObject);
                OnDetect.Invoke();
            }
        }


        // function________________________________________________________________
        public void SetIsActive(bool value) => IsActive = value;
        private bool CheckTag(GameObject obj)
        {
            if (ObjectTag == string.Empty)
                return true;
            else
                return obj.CompareTag(ObjectTag);
        }
    }
}