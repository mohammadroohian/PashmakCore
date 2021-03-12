using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pashmak.Core.CU._UnityEngine._Transform
{
    public class CU_Transform_Shake : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_shakeAtStart = false;
        [SerializeField] private int m_shakeAtStartIndex = 0;
        [SerializeField] protected GameObject m_baseGameObject = null;
        private Transform m_objectHolder;
        [SerializeField] private List<ObjectShakeInfo> m_objectShakes = new List<ObjectShakeInfo>();
        private int m_shakeIndex;

        private bool m_isShaking = false;
        private bool m_backToInitialPosition = false;
        private bool m_backToInitialRotation = false;

        private float m_randomNextStep;

        private float m_xRandomPosition;
        private float m_yRandomPosition;
        private float m_zRandomPosition;

        private float m_xRandomRotation;
        private float m_yRandomRotation;
        private float m_zRandomRotation;

        private int m_randomCounter;

        private Quaternion m_initialRotation;
        private Vector3 m_initialPosition;

        private float m_power;


        // property________________________________________________________________
        public bool ShakeAtStart { get => m_shakeAtStart; private set => m_shakeAtStart = value; }
        public int IndexShakeAtStart { get => m_shakeAtStartIndex; private set => m_shakeAtStartIndex = value; }
        public GameObject BaseGameObject { get => m_baseGameObject; set => m_baseGameObject = value; }
        public List<ObjectShakeInfo> ObjectShakes { get => m_objectShakes; private set => m_objectShakes = value; }
        public int ShakeIndex { get => m_shakeIndex; private set => m_shakeIndex = value; }
        public bool IsShaking { get => m_isShaking; private set => m_isShaking = value; }


        // monoBehaviour___________________________________________________________
        void Awake()
        {
            if (!BaseGameObject)
                BaseGameObject = gameObject;

            m_initialRotation = BaseGameObject.transform.localRotation;
            m_initialPosition = new Vector3(0, 0, 0);

            m_randomCounter = ObjectShakes[ShakeIndex].RandomCount;
            m_power = 0.5f;
        }
        void Start()
        {
            if (ShakeAtStart)
                StartShake(IndexShakeAtStart);
        }
        void Update()
        {
            if (!m_isActive) return;

            //Rotate objectHolder back to initial rotation.
            if (m_backToInitialRotation)
            {
                if (Quaternion.Angle(BaseGameObject.transform.localRotation, m_initialRotation) < 0.01f)
                {
                    BaseGameObject.transform.localRotation = m_initialRotation;

                    if (m_randomCounter < 1)
                    {
                        this.IsShaking = false;
                        this.m_randomCounter = ObjectShakes[ShakeIndex].RandomCount;
                        m_backToInitialRotation = false;
                    }
                }
                else
                {
                    BaseGameObject.transform.localRotation = Quaternion.Lerp(BaseGameObject.transform.localRotation, m_initialRotation, Time.deltaTime * 2);
                }
            }

            //position objectHolder back to initial position.
            if (m_backToInitialPosition)
            {
                if (Vector3.Distance(BaseGameObject.transform.localPosition, m_initialPosition) < 0.01f)
                {
                    BaseGameObject.transform.localPosition = m_initialPosition;
                    m_backToInitialPosition = false;
                }
                else
                {
                    BaseGameObject.transform.localPosition = Vector3.Lerp(BaseGameObject.transform.localPosition, m_initialPosition, Time.deltaTime * 2);
                }
            }

            //print(randomCounter + "---" + xPositionRandomValue);

            if (IsShaking && m_randomCounter > 0)
            {
                BaseGameObject.transform.Rotate(new Vector3(m_xRandomRotation, m_yRandomRotation, m_zRandomRotation) * ObjectShakes[ShakeIndex].FadeSpeed);
                BaseGameObject.transform.Translate(new Vector3(m_xRandomPosition, m_yRandomPosition, m_zRandomPosition) * ObjectShakes[ShakeIndex].FadeSpeed);

                if (m_randomNextStep < Time.time)
                {
                    m_randomNextStep = Time.time + ObjectShakes[ShakeIndex].RandomRate;

                    //Position
                    m_xRandomPosition = Random.Range
                        (
                        ObjectShakes[ShakeIndex].RandomPosition.XRandomPosition.from * m_power,
                        ObjectShakes[ShakeIndex].RandomPosition.XRandomPosition.to * m_power
                        );
                    m_yRandomPosition = Random.Range
                        (
                        ObjectShakes[ShakeIndex].RandomPosition.YRandomPosition.from * m_power,
                        ObjectShakes[ShakeIndex].RandomPosition.YRandomPosition.to * m_power
                        );
                    m_zRandomPosition = Random.Range
                        (
                        ObjectShakes[ShakeIndex].RandomPosition.ZRandomPosition.from * m_power,
                        ObjectShakes[ShakeIndex].RandomPosition.ZRandomPosition.to * m_power
                        );

                    //Rotation.
                    m_xRandomRotation = Random.Range
                        (
                        ObjectShakes[ShakeIndex].RandomRotation.XRandomRotation.from * m_power,
                        ObjectShakes[ShakeIndex].RandomRotation.XRandomRotation.to * m_power
                        );
                    m_yRandomRotation = Random.Range
                        (
                        ObjectShakes[ShakeIndex].RandomRotation.YRandomRotation.from * m_power,
                        ObjectShakes[ShakeIndex].RandomRotation.YRandomRotation.to * m_power
                        );
                    m_zRandomRotation = Random.Range
                        (
                        ObjectShakes[ShakeIndex].RandomRotation.ZRandomRotation.from * m_power,
                        ObjectShakes[ShakeIndex].RandomRotation.ZRandomRotation.to * m_power
                        );

                    m_randomCounter--;
                    if (m_randomCounter == 0)
                    {
                        m_backToInitialRotation = m_backToInitialPosition = true;
                    }
                }
            }
        }


        // function________________________________________________________________
        public void StartShake(int shakeCode)
        {
            if (!m_isActive) return;
            float power = 1;
            int index = GetShake(shakeCode);
            if (index > -1)
            {
                this.IsShaking = true;
                this.ShakeIndex = index;
                this.m_randomCounter = ObjectShakes[ShakeIndex].RandomCount;
                this.m_power = power;
            }
        }
        public void StartShake() => StartShake(0);
        private int GetShake(int shakeCode)
        {
            for (int i = 0; i < ObjectShakes.Count; i++)
            {
                if (ObjectShakes[i].ShakeCode == shakeCode)
                {
                    return i;
                }
            }
            return -1;
        }


        // class___________________________________________________________________
        [System.Serializable]
        public class ObjectShakeInfo
        {
            // variable________________________________________________________________
            [SerializeField]
            private int shakeCode = 0;
            [SerializeField]
            private PositionInfo randomPosition;
            [SerializeField]
            private RotationInfo randomRotation;
            [SerializeField]
            private int randomCount = 20;
            [SerializeField]
            private float fadeSpeed = 0.5f;//Speed of fade a rotation to other rotation.
            [SerializeField]
            private float randomRate = 0.01f;//Speed of creation of random nubers.


            // property________________________________________________________________
            public int ShakeCode { get => this.shakeCode; }
            public PositionInfo RandomPosition { get => randomPosition; private set => randomPosition = value; }
            public RotationInfo RandomRotation { get => randomRotation; private set => randomRotation = value; }
            public int RandomCount { get => this.randomCount; }
            public float FadeSpeed { get => this.fadeSpeed; }
            public float RandomRate { get => this.randomRate; }
        }

        [System.Serializable]
        public class PositionInfo
        {
            // variable________________________________________________________________
            [SerializeField] private Range xRandomPosition = new Range(-0.1f, 0.1f);
            [SerializeField] private Range yRandomPosition = new Range(-0.1f, 0.1f);
            [SerializeField] private Range zRandomPosition = new Range(-0.1f, 0.1f);


            // property________________________________________________________________
            public Range XRandomPosition { get => this.xRandomPosition; }
            public Range YRandomPosition { get => this.yRandomPosition; }
            public Range ZRandomPosition { get => this.zRandomPosition; }
        }
        [System.Serializable]
        public class RotationInfo
        {
            // variable________________________________________________________________
            [SerializeField]
            private Range xRandomRotation = new Range(-0.1f, 0.1f);
            [SerializeField]
            private Range yRandomRotation = new Range(-0.1f, 0.1f);
            [SerializeField]
            private Range zRandomRotation = new Range(-0.1f, 0.1f);


            // property________________________________________________________________
            public Range XRandomRotation { get => this.xRandomRotation; }
            public Range YRandomRotation { get => this.yRandomRotation; }
            public Range ZRandomRotation { get => this.zRandomRotation; }
        }

        [System.Serializable]
        public class Range
        {
            // variable________________________________________________________________
            public float from;
            public float to;
            public Range(float from, float to)
            {
                this.from = from;
                this.to = to;
            }
        }
        [System.Serializable]
        public class Range_Int
        {
            // variable________________________________________________________________
            public int from;
            public int to;
            public Range_Int(int from, int to)
            {
                this.from = from;
                this.to = to;
            }
        }
    }
}