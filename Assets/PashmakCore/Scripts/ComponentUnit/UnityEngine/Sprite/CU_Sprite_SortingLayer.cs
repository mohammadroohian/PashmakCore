using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Pashmak.Core.CU._UnityEngine._SpriteRenderer
{
    public class CU_Sprite_SortingLayer : MonoBehaviour
    {
        [SerializeField] private bool m_overrideLayerName = true;
        [SerializeField] [ShowIf("m_overrideLayerName")] private string m_sortingLayerName = "Default";
        [SerializeField] private bool m_overrideOrder = true;
        [SerializeField] [ShowIf("m_overrideOrder")] private int m_sortingOrder = 1;
        [SerializeField] private bool m_actOnChildren = false;
        [SerializeField] private bool m_getRenderersOnRuntime = false;
        private List<SpriteRenderer> m_renderers = new List<SpriteRenderer>();


        // property________________________________________________________________
        public bool OverrideLayerName { get => m_overrideLayerName; set => m_overrideLayerName = value; }
        public string SortingLayerName { get => m_sortingLayerName; set => m_sortingLayerName = value; }
        public bool OverrideOrder { get => m_overrideOrder; set => m_overrideOrder = value; }
        public int SortingOrder { get => m_sortingOrder; set => m_sortingOrder = value; }
        public bool ActOnChildren
        {
            get => m_actOnChildren;
            set
            {
                if (m_actOnChildren != value)
                {
                    m_actOnChildren = value;
                    GetRendererComponets();
                }
            }
        }
        public bool GetRenderersOnRuntime { get => m_getRenderersOnRuntime; set => m_getRenderersOnRuntime = value; }
        public List<SpriteRenderer> Renderers { get => m_renderers; set => m_renderers = value; }


        // monoBehaviour___________________________________________________________
        void Awake()
        {
            GetRendererComponets();
        }
        private void GetRendererComponets()
        {
            if (ActOnChildren)
                Renderers.AddRange(GetComponentsInChildren<SpriteRenderer>());
            else
                Renderers.Add(GetComponent<SpriteRenderer>());
        }
        public void SetSortingConfig()
        {
            if (GetRenderersOnRuntime)
                GetRendererComponets();
            for (int i = 0; i < Renderers.Count; i++)
            {
                Renderers[i].sortingLayerName = OverrideLayerName ? SortingLayerName : Renderers[i].sortingLayerName;
                Renderers[i].sortingOrder = OverrideOrder ? SortingOrder : Renderers[i].sortingOrder;
            }
        }
        public void PlusPlusSortingOrder()
        {
            for (int i = 0; i < Renderers.Count; i++)
            {
                Renderers[i].sortingOrder = Renderers[i].sortingOrder + 1;
            }
        }
    }
}