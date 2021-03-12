using Pashmak.Core.IO;
using UnityEngine;
using UnityEngine.UI;


namespace Pashmak.Core.CU._UnityEngine._Transform
{
    public class CU_LoadText : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_loadAtStart = false;
        [SerializeField] private string m_key = "";// The key specify ane save from another.
        [SerializeField] private VariableType m_varType = new VariableType();
        [SerializeField] private VariableSaveType m_saveType = VariableSaveType.Binary;
        [SerializeField] private Text m_UIText;



        // property________________________________________________________________
        public bool LoadAtStart { get => m_loadAtStart; private set => m_loadAtStart = value; }
        public string Key { get => m_key; set => m_key = value; }
        public VariableType VarType { get => m_varType; set => m_varType = value; }
        public VariableSaveType SaveType { get => m_saveType; set => m_saveType = value; }
        public Text UIText { get => m_UIText; set => m_UIText = value; }


        // monoBehaviour___________________________________________________________
        private void Awake()
        {
            if (UIText == null)
                UIText = GetComponent<Text>();
        }
        private void Start()
        {
            if (LoadAtStart)
                LoadText();
        }


        // function________________________________________________________________
        public void LoadText()
        {
            if (!IsActive) return;
            if (string.IsNullOrWhiteSpace(Key))
            {
                Debug.LogError("no key setted!", this);
                return;
            }
            var result = SaveLoad.Load(Key, VarType, SaveType);
            UIText.text = result.ToString();
        }
    }
}