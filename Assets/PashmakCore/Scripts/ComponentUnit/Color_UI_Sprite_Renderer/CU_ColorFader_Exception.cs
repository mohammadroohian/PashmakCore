using UnityEngine;


namespace Pashmak.Core.CU
{
    public class CU_ColorFader_Exception : CU_Component
    {
        // enum____________________________________________________________________
        public enum ColorFaderExceptionType
        {
            ExceptedFromParent = 0,
            Excepted = 1,
            None = 2
        }


        // variable________________________________________________________________
        [SerializeField] private ColorFaderExceptionType m_status = ColorFaderExceptionType.ExceptedFromParent;


        // property________________________________________________________________
        public ColorFaderExceptionType Status { get => m_status; set => m_status = value; }
    }
}