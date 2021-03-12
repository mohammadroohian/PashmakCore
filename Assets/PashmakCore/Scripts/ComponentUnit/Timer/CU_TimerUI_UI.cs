using UnityEngine;
using UnityEngine.UI;

namespace Pashmak.Core.CU._UnityEngine
{
    public class CU_TimerUI_UI : CU_TimerUI
    {
        // variable________________________________________________________________
        [SerializeField] private Text m_digitsText = null;


        // property________________________________________________________________
        protected Text DigitsText { get => m_digitsText; set => m_digitsText = value; }


        // monoBehaviour___________________________________________________________
        void Start()
        {
            if (DigitsText == null)
                DigitsText = GetComponent<Text>();
        }


        // override________________________________________________________________
        public override void SetUIText(string value)
        {
            DigitsText.text = value;
        }
    }
}