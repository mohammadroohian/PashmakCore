using UnityEngine;
using static UnityEngine.ScreenCapture;

namespace Pashmak.Core.CU._UnityEngine
{
    public class CU_CaptureScreenshot : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] bool m_captureAtStart = false;
        [SerializeField] bool m_captureAtUpdate = false;
        private static int m_capturedCount = 0;
        [SerializeField] string m_savePath = "captured_image";
        [SerializeField] int m_superSize = 1;
        [SerializeField] bool m_isSterio = false;
        [SerializeField] StereoScreenCaptureMode m_StereoMode = StereoScreenCaptureMode.BothEyes;


        // property________________________________________________________________
        public bool CaptureAtStart { get => m_captureAtStart; private set => m_captureAtStart = value; }
        public bool CaptureAtUpdate { get => m_captureAtUpdate; set => m_captureAtUpdate = value; }
        public static int CapturedCount { get => m_capturedCount; private set => m_capturedCount = value; }
        public string SavePath { get => m_savePath; set => m_savePath = value; }
        public int SuperSize { get => m_superSize; set => m_superSize = value; }
        public bool IsSterio { get => m_isSterio; set => m_isSterio = value; }
        public StereoScreenCaptureMode StereoMode { get => m_StereoMode; set => m_StereoMode = value; }


        // monoBehaviour___________________________________________________________
        private void Start()
        {
            if (CaptureAtStart)
                Capture();
        }
        private void Update()
        {
            if (CaptureAtUpdate)
                Capture();
        }


        // override________________________________________________________________
        public void Capture()
        {
            if (!IsActive) return;

            string tmpPath = string.Format("{0}_{1}.png", SavePath, CapturedCount);
            if (IsSterio)
                CaptureScreenshot(tmpPath, StereoMode);
            else if (SuperSize > 1)
                CaptureScreenshot(tmpPath, SuperSize);
            else
                CaptureScreenshot(tmpPath);
            CapturedCount++;
        }
    }
}