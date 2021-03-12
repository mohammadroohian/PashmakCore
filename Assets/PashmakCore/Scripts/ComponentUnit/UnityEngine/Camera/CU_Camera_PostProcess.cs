using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Rendering.PostProcessing;

namespace Pashmak.Core.CU._UnityEngine
{
    public class CU_Camera_PostProcess : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_setAtStart = false;
        [SerializeField] private bool m_useMainCamera = true;
        [SerializeField] [HideIf("m_useMainCamera")] private Camera m_baseCamera = null;
        [SerializeField] private PostProcessProfile m_profile = null;


        // property________________________________________________________________
        public bool SetAtStart { get => m_setAtStart; private set => m_setAtStart = value; }
        public bool UseMainCamera { get => m_useMainCamera; set => m_useMainCamera = value; }
        public Camera BaseCamera { get => m_baseCamera; set => m_baseCamera = value; }
        public PostProcessProfile Profile { get => m_profile; set => m_profile = value; }


        // monoBehaviour___________________________________________________________
        private void Start()
        {
            if (SetAtStart)
                SetProfile();
        }


        // function________________________________________________________________
        public void SetProfile()
        {
            if (!IsActive) return;

            if (UseMainCamera)
                Camera.main.GetComponent<PostProcessVolume>().profile = Profile;
            else
                BaseCamera.GetComponent<PostProcessVolume>().profile = Profile;
        }
    }
}