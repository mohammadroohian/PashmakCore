using Pashmak.Core.IO;
using UnityEngine;


namespace Pashmak.Core.CU.IO
{
    public class CU_BoolVariable : CU_Component, IDefaultExcute
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_debugMode = false;// If be true value changes will be printed.
        [SerializeField] private bool m_loadAtStart = false;
        [SerializeField] private string m_key = "";// The key specify ane save from another.
        [SerializeField] private bool m_value = true;
        [SerializeField] private VariableSaveType m_saveType = VariableSaveType.Binary;


        // property________________________________________________________________
        public bool DebugMode { get => m_debugMode; set => m_debugMode = value; }
        public bool LoadAtStart { get => m_loadAtStart; private set => m_loadAtStart = value; }
        public string Key { get => m_key; set => m_key = value; }
        public bool Value { get => m_value; set => m_value = value; }
        public VariableSaveType SaveType { get => m_saveType; set => m_saveType = value; }


        // monoBehaviour___________________________________________________________
        private void Start()
        {
            if (LoadAtStart) Load();
            if (DebugMode)
                Debug.Log("Get '" + Key + "' at start : " + SaveLoad.LoadInt(Key, SaveType), this);
        }


        // override________________________________________________________________
        public override string GetDetails(string gameObjectName, string componentName, string methodName, string methodParams)
        {
            string parameterName = string.Format("\"{0}\"", Key);
            componentName = "";
            if (methodName.Contains("DefaultExecute") || methodName.Equals("Save"))
            {
                methodName = "Save";
                methodParams = string.Format("{0}, {1}", parameterName, Value);
            }
            else if (methodName.Equals("Load") || methodName.Contains("ToggleValue"))
                methodParams = parameterName;

            return base.GetDetails(gameObjectName, componentName, methodName, methodParams);
        }


        // implement_______________________________________________________________
        void IDefaultExcute.DefaultExecute() => Save();


        // function________________________________________________________________     
        public void SetValue(bool newValue)
        {
            if (!IsActive) return;
            Value = newValue;
        }
        public void Save()
        {
            if (!IsActive) return;
            SaveLoad.SaveBool(Key, Value, SaveType, DebugMode);
        }
        public void Load()
        {
            if (!IsActive) return;
            Value = SaveLoad.LoadBool(Key, SaveType);
        }
        public void ToggleValue()
        {
            if (!IsActive) return;
            Value = !Value;
        }
        public void ToggleValue_Save()
        {
            if (!IsActive) return;
            Value = !Value;
            Save();
        }
    }
}