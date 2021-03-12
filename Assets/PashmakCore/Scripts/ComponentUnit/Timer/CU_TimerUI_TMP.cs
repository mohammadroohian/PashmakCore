using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Pashmak.Core.CU._UnityEngine
{
    public class CU_TimerUI_TMP : CU_TimerUI
    {
        // variable________________________________________________________________
        [SerializeField] private TextMeshProUGUI m_digitsText = null;


        // property________________________________________________________________
        public TextMeshProUGUI DigitsText { get => m_digitsText; set => m_digitsText = value; }


        // monoBehaviour___________________________________________________________
        void Start()
        {
            if (DigitsText == null)
                DigitsText = GetComponent<TextMeshProUGUI>();
        }


        // override________________________________________________________________
        public override void SetUIText(string value)
        {
            DigitsText.text = value;
        }
    }
}