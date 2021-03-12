using UnityEngine;
using UnityEngine.Audio;


namespace Pashmak.Core.CU._UnityEngine._Audio._AudioMixer
{
    public class CU_AudioMixer_ExposedParameters : CU_Component, IDefaultExcute
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_setAtStart = false;
        [SerializeField] private AudioMixer m_mixer = null;
        [SerializeField] private string m_parameterName = "";// Exposed parameter name.
        [SerializeField] private float m_parameterValue = 0.0f;// Default value for this parameter when SetFloat function be called.


        // property________________________________________________________________
        public bool SetAtStart { get => m_setAtStart; private set => m_setAtStart = value; }
        public AudioMixer Mixer { get => m_mixer; set => m_mixer = value; }
        public string ParameterName { get => m_parameterName; set => m_parameterName = value; }
        public float ParameterValue { get => m_parameterValue; set => m_parameterValue = value; }


        // monoBehaviour___________________________________________________________
        private void Start()
        {
            if (SetAtStart)
                SetFloat();
        }


        // override________________________________________________________________
        public override string GetDetails(string gameObjectName, string componentName, string methodName, string methodParams)
        {
            if (methodName.Contains("ClearFloat"))
            {
                methodName = Mixer.name + ".ClearFloat";
                methodParams = string.Format("\"{0}\"", m_parameterName);
            }
            else
            {
                methodName = Mixer.name + ".SetFloat";
                methodParams = string.Format("\"{0}\", {1}", m_parameterName, ParameterValue);
            }

            componentName = "";
            return base.GetDetails(gameObjectName, componentName, methodName, methodParams);
        }


        // implement_______________________________________________________________
        void IDefaultExcute.DefaultExecute() => SetFloat();


        // function________________________________________________________________
        public void SetFloat()
        {
            if (!IsActive) return;
            SetFloat(ParameterName, ParameterValue);
        }
        public void ClearFloat()
        {
            if (!IsActive) return;
            ClearFloat(ParameterName);
        }

        private void SetFloat(string exParameterName, float newValue)
        {
            if (!IsActive) return;
            Mixer.SetFloat(exParameterName, newValue);
        }
        private void ClearFloat(string exParameterName)
        {
            if (!IsActive) return;
            Mixer.ClearFloat(exParameterName);
        }
    }
}