using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Pashmak.Core.CU._UnityEngine._LineRenderer
{
    public class CU_UI_Slider : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private Slider m_baseSlider = null;
        [SerializeField] private UnityEvent m_onInteractableToOn = new UnityEvent();
        [SerializeField] private UnityEvent m_onInteractableToOff = new UnityEvent();


        // property________________________________________________________________
        public Slider BaseSlider { get => m_baseSlider; set => m_baseSlider = value; }
        public UnityEvent OnInteractableToOn { get => m_onInteractableToOn; set => m_onInteractableToOn = value; }
        public UnityEvent OnInteractableToOff { get => m_onInteractableToOff; set => m_onInteractableToOff = value; }


        // monoBehaviour___________________________________________________________
        private void Awake()
        {
            if (BaseSlider == null)
                BaseSlider = GetComponent<Slider>();
        }


        // function________________________________________________________________
        public void PlusPlus()
        {
            m_baseSlider.value += 1;
        }
        public void MinusMinus()
        {
            m_baseSlider.value -= 1;
        }
        public void Multiply(float value)
        {
            m_baseSlider.value *= value;
        }
        public void Division(float value)
        {
            m_baseSlider.value /= value;
        }
        public void Plus(float value)
        {
            m_baseSlider.value += value;
        }
        public void Minus(float value)
        {
            m_baseSlider.value -= value;
        }
        public void SetValue(float value)
        {
            m_baseSlider.value = value;
        }
        public void OnInteractable()
        {
            if (!m_baseSlider.interactable)
                OnInteractableToOn.Invoke();
            m_baseSlider.interactable = true;
        }
        public void OffInteractable()
        {
            if (m_baseSlider.interactable)
                OnInteractableToOff.Invoke();
            m_baseSlider.interactable = false;
        }
    }
}