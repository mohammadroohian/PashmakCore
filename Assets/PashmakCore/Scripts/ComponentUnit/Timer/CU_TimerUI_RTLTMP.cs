// using UnityEngine;
// using RTLTMPro;

// namespace Pashmak.Core.CU._UnityEngine
// {
//     public class CU_TimerUI_RTLTMP : CU_TimerUI
//     {
//         // variable________________________________________________________________
//         private RTLTextMeshPro m_digitsText = null;


//         // property________________________________________________________________
//         [SerializeField] public RTLTextMeshPro DigitsText { get => m_digitsText; set => m_digitsText = value; }


//         // monoBehaviour___________________________________________________________
//         void Start()
//         {
//             if (DigitsText == null)
//                 DigitsText = GetComponent<RTLTextMeshPro>();
//         }


//         // override________________________________________________________________
//         public override void SetUIText(string value)
//         {
//             DigitsText.text = value;
//         }
//     }
// }