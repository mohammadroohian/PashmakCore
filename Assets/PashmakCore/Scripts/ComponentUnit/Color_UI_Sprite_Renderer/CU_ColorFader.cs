using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Pashmak.Core.CU.CU_ColorFader_Exception;


namespace Pashmak.Core.CU
{
    public class CU_ColorFader : CU_Component
    {
        // enum____________________________________________________________________
        public enum Initialize
        {
            None = 0,
            AutoPlayOnStart = 1,
            AutoPlayOnEnable = 2
        }
        public enum RendererGraphicType
        {
            AutoDetect = 0,
            UI_TextImage = 1,
            SpriteRenderer = 2,
            UI_Outline = 3,
            Renderer = 4
        }


        // variable________________________________________________________________
        [SerializeField] private RendererGraphicType m_graphicType = RendererGraphicType.AutoDetect;
        [SerializeField] private Initialize m_initializeType = Initialize.AutoPlayOnEnable;
        [SerializeField] private int m_autoPlayOnStartIndex = 0;
        [SerializeField] private int m_autoPlayOnEnableIndex = 0;
        [SerializeField]
        private FadeContent[] m_fadeContents = {
                new FadeContent(new Color(0f,0f,0f,0f), new Color(1f,1f,1f,1f), new AnimationCurve(
                        new Keyframe(0f,0f),
                        new Keyframe(1f,1f)
                    )),
                new FadeContent(new Color(1f,1f,1f,1f), new Color(0f,0f,0f,0f), new AnimationCurve(
                        new Keyframe(0f,0f),
                        new Keyframe(1f,1f)
                    ))
              };
        [SerializeField] private bool m_actOnChildren = false;
        /// <summary>
        /// Should Components on inactive GameObjects be included in the found set?
        /// </summary>
        [SerializeField] private bool m_includeInactive = false;
        [SerializeField] private bool m_getGraphicItemsAtRunTime = false;
        [SerializeField] private bool m_getGraphicItemsAtUpdate = false;

        [SerializeField] private UnityEvent m_onActive = new UnityEvent();
        [SerializeField] private UnityEvent m_onDeActive = new UnityEvent();

        private FadeContent m_currentContent;
        private float m_tempTime;
        private float m_timer = 0;
        private List<object> m_graphicItems = new List<object>();
        private List<Color> m_graphicItemsColors = new List<Color>();


        // property________________________________________________________________
        public RendererGraphicType GraphicType { get => m_graphicType; set => m_graphicType = value; }
        public Initialize InitializeType { get => m_initializeType; set => m_initializeType = value; }
        public int AutoPlayOnStartIndex { get => m_autoPlayOnStartIndex; set => m_autoPlayOnStartIndex = value; }
        public int AutoPlayOnEnableIndex { get => m_autoPlayOnEnableIndex; set => m_autoPlayOnEnableIndex = value; }
        public FadeContent[] FadeContents { get => m_fadeContents; set => m_fadeContents = value; }
        public bool ActOnChildren { get => m_actOnChildren; set => m_actOnChildren = value; }
        public bool IncludeInactive { get => m_includeInactive; set => m_includeInactive = value; }
        public bool GetGraphicItemsAtRunTime { get => m_getGraphicItemsAtRunTime; set => m_getGraphicItemsAtRunTime = value; }
        public bool GetGraphicItemsAtUpdate { get => m_getGraphicItemsAtUpdate; set => m_getGraphicItemsAtUpdate = value; }
        public UnityEvent OnActive { get => m_onActive; private set => m_onActive = value; }
        public UnityEvent OnDeActive { get => m_onDeActive; private set => m_onDeActive = value; }


        // monoBehaviour___________________________________________________________
        void Awake()
        {
            // Check for graphic type Detection.
            if (GraphicType == RendererGraphicType.AutoDetect)
            {
                AutoDetectGraphicType();
            }

            // Get elements.
            FillGraphicItems();

            // Get all items colors for use item color as first color.
            for (int i = 0; i < m_graphicItems.Count; i++)
            {
                m_graphicItemsColors.Add(GetGraphicItemColor(m_graphicItems[i]));
            }


            // Initialize.
            switch (InitializeType)
            {
                case Initialize.AutoPlayOnStart:
                    IsActive = true;
                    break;
                case Initialize.AutoPlayOnEnable:
                    IsActive = true;
                    break;
            }

            // Check is empty or not.
            if (FadeContents.Length == 0)
            {
                IsActive = false;
                return;
            }

            // First Element.
            m_currentContent = FadeContents[0];
        }
        void OnEnable()
        {
            if (IsActive && InitializeType == Initialize.AutoPlayOnEnable)
            {
                StartFade(AutoPlayOnEnableIndex);
            }
        }
        void Start()
        {
            if (IsActive && InitializeType == Initialize.AutoPlayOnStart)
            {
                StartFade(AutoPlayOnStartIndex);
            }
        }
        void Update()
        {
            // Check Is Active.
            if (!IsActive) return;

            // Set timer.
            m_timer += Time.deltaTime;

            // Get graphic items at update.
            if (GetGraphicItemsAtUpdate)
            {
                FillGraphicItems();
            }

            // Get value from curve.
            float tmpCurveValue = m_currentContent.FadeCurve.Evaluate(Time.time - m_tempTime);

            Color tmpItemColor = new Color();

            // Effect on graphic elements.
            for (int i = 0; i < m_graphicItems.Count; i++)
            {
                // Get graphic item color.
                tmpItemColor = GetGraphicItemColor(m_graphicItems[i]);

                // Check using graphic item color as first color.
                if (m_currentContent.UseItemColorAsFirstColor)
                {
                    // Change the first color to graphic item color.
                    m_currentContent.FirstColor = m_graphicItemsColors[i];
                }

                // Check is just alpha or not.
                if (m_currentContent.JustAlpha)
                {
                    // Set graphic item color.
                    SetGraphicItemsColor(
                        m_graphicItems[i],
                        Color.Lerp(
                            new Color(
                                tmpItemColor.r,
                                tmpItemColor.g,
                                tmpItemColor.b,
                                m_currentContent.FirstColor.a),
                            new Color(
                                tmpItemColor.r,
                                tmpItemColor.g,
                                tmpItemColor.b,
                                m_currentContent.SecondColor.a),
                            tmpCurveValue));
                }
                else
                {
                    // Set graphic item color.
                    SetGraphicItemsColor(
                        m_graphicItems[i],
                        Color.Lerp(
                            m_currentContent.FirstColor,
                            m_currentContent.SecondColor,
                            tmpCurveValue));
                }
            }
        }


        // function________________________________________________________________
        public void SetActive(bool value)
        {
            IsActive = value;
            if (IsActive)
            {
                OnActive.Invoke();
            }
            else
            {
                OnDeActive.Invoke();
            }
        }

        public void StartFade()
        {
            StartFade(0);
        }
        public void StartFade(int index)
        {
            StartFade(FadeContents[index]);
        }
        public void StartFade(FadeContent content)
        {
            // Set current fade content.
            m_currentContent = content;

            // Get graphic items.
            if (GetGraphicItemsAtRunTime)
            {
                FillGraphicItems();
            }

            Color tmpItemColor = new Color();

            // Effect on graphic elements.
            for (int i = 0; i < m_graphicItems.Count; i++)
            {
                tmpItemColor = GetGraphicItemColor(m_graphicItems[i]);

                // Check using graphic item color as first color.
                if (m_currentContent.UseItemColorAsFirstColor)
                {
                    m_currentContent.FirstColor = tmpItemColor;
                }

                // Check is just alpha or not.
                if (m_currentContent.JustAlpha)
                {
                    // Set graphic item color.
                    SetGraphicItemsColor(
                        m_graphicItems[i],
                        new Color(
                            tmpItemColor.r,
                            tmpItemColor.g,
                            tmpItemColor.b,
                            m_currentContent.FirstColor.a));
                }
                else
                {
                    // Set graphic item color to first color.
                    SetGraphicItemsColor(m_graphicItems[i], m_currentContent.FirstColor);
                }
            }

            if (!IsActive)
                SetActive(true);
            m_tempTime = Time.time;
            m_timer = 0;
        }
        public void SetToFirstColor()
        {
            for (int i = 0; i < m_graphicItems.Count; i++)
            {
                SetGraphicItemsColor(m_graphicItems[i], m_graphicItemsColors[i]);
            }
        }

        private void AutoDetectGraphicType()
        {
            // Check for graphic. // UIs
            Graphic[] graphicTmp = GetComponentsInChildren<Graphic>(IncludeInactive);
            if (graphicTmp.Length > 0)
            {
                GraphicType = RendererGraphicType.UI_TextImage;
                return;
            }

            // Check for sprites. // SpriteRenderer
            SpriteRenderer[] spriteRendererTmp = GetComponentsInChildren<SpriteRenderer>(IncludeInactive);
            if (spriteRendererTmp.Length > 0)
            {
                GraphicType = RendererGraphicType.SpriteRenderer;
                return;
            }
        }
        private void FillGraphicItems()
        {
            // Clear.
            m_graphicItems.Clear();

            // Add.
            switch (GraphicType)
            {
                case RendererGraphicType.UI_TextImage:
                    if (ActOnChildren)
                        m_graphicItems.AddRange(GetComponentsInChildren<Graphic>(IncludeInactive));
                    else
                        m_graphicItems.Add(GetComponent<Graphic>());
                    break;
                case RendererGraphicType.SpriteRenderer:
                    if (ActOnChildren)
                        m_graphicItems.AddRange(GetComponentsInChildren<SpriteRenderer>(IncludeInactive));
                    else
                        m_graphicItems.Add(GetComponent<SpriteRenderer>());
                    break;
                case RendererGraphicType.UI_Outline:
                    if (ActOnChildren)
                        m_graphicItems.AddRange(GetComponentsInChildren<Outline>(IncludeInactive));
                    else
                        m_graphicItems.Add(GetComponent<Outline>());
                    break;
                case RendererGraphicType.Renderer:
                    if (ActOnChildren)
                        m_graphicItems.AddRange(GetComponentsInChildren<Renderer>(IncludeInactive));
                    else
                        m_graphicItems.Add(GetComponent<Renderer>());
                    break;
            }

            // Remove Exceptions.
            CU_ColorFader_Exception[] tmpExcepts = GetComponentsInChildren<CU_ColorFader_Exception>(IncludeInactive);
            for (int i = 0; i < tmpExcepts.Length; i++)
            {
                if (tmpExcepts[i].Status == ColorFaderExceptionType.None)
                    continue;

                if (tmpExcepts[i].Status == ColorFaderExceptionType.ExceptedFromParent && tmpExcepts[i].gameObject == this.gameObject)
                    continue;

                switch (GraphicType)
                {
                    case RendererGraphicType.UI_TextImage:
                        m_graphicItems.Remove(tmpExcepts[i].GetComponent<Graphic>());
                        break;
                    case RendererGraphicType.SpriteRenderer:
                        m_graphicItems.Remove(tmpExcepts[i].GetComponent<SpriteRenderer>());
                        break;
                    case RendererGraphicType.UI_Outline:
                        m_graphicItems.Remove(tmpExcepts[i].GetComponent<Outline>());
                        break;
                    case RendererGraphicType.Renderer:
                        m_graphicItems.Remove(tmpExcepts[i].GetComponent<Renderer>());
                        break;
                }
            }
        }

        private void SetGraphicItemsColor(object graphicItem, Color value)
        {
            switch (GraphicType)
            {
                case RendererGraphicType.UI_TextImage:
                    ((Graphic)graphicItem).color = value;
                    break;
                case RendererGraphicType.SpriteRenderer:
                    ((SpriteRenderer)graphicItem).color = value;
                    break;
                case RendererGraphicType.UI_Outline:
                    ((Outline)graphicItem).effectColor = value;
                    break;
                case RendererGraphicType.Renderer:
                    ((Renderer)graphicItem).material.SetColor("_Color", value);
                    break;
            }
        }
        private Color GetGraphicItemColor(object graphicItem)
        {
            switch (GraphicType)
            {
                case RendererGraphicType.UI_TextImage:
                    return ((Graphic)graphicItem).color;
                case RendererGraphicType.SpriteRenderer:
                    return ((SpriteRenderer)graphicItem).color;
                case RendererGraphicType.UI_Outline:
                    return ((Outline)graphicItem).effectColor;
                case RendererGraphicType.Renderer:
                    return ((Renderer)graphicItem).material.GetColor("_Color");
                default:
                    return new Color();
            }
        }


        // class___________________________________________________________________
        [System.Serializable]
        public class FadeContent
        {
            // constructor_____________________________________________________________
            public FadeContent()
            {

            }
            public FadeContent(Color firstColor, Color secondColor, AnimationCurve fadeCurve)
            {
                this.FirstColor = firstColor;
                this.SecondColor = secondColor;
                this.FadeCurve = fadeCurve;
            }


            // variable________________________________________________________________
            [SerializeField] private bool m_justAlpha = false;
            [SerializeField] private bool m_useItemColorAsFirstColor = false;
            [SerializeField] private Color m_firstColor = new Color(1, 1, 1, 1);
            [SerializeField] private Color m_secondColor = new Color(0, 0, 0, 0);
            [SerializeField] private AnimationCurve m_fadeCurve = new AnimationCurve();


            // property________________________________________________________________
            public bool JustAlpha { get => m_justAlpha; set => m_justAlpha = value; }
            public Color FirstColor { get => m_firstColor; set => m_firstColor = value; }
            public Color SecondColor { get => m_secondColor; set => m_secondColor = value; }
            public AnimationCurve FadeCurve { get => m_fadeCurve; set => m_fadeCurve = value; }
            public bool UseItemColorAsFirstColor
            {
                get => m_useItemColorAsFirstColor;
                set => m_useItemColorAsFirstColor = value;
            }
        }
    }
}