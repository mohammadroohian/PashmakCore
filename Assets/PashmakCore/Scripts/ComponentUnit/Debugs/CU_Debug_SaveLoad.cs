using Pashmak.Core.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Pashmak.Core.CU.IO
{
    public class CU_Debug_SaveLoad : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_showAtStart = true;
        [SerializeField] private bool m_showAtUpdate = true;
        [SerializeField] private string m_key = "";// The key specify ane save from another.
        [SerializeField] private VariableType m_type = VariableType.Int;
        [SerializeField] private VariableSaveType m_saveType = VariableSaveType.Binary;
        [SerializeField] private Text m_textUi;



        // property________________________________________________________________
        public bool ShowAtStart { get => m_showAtStart; private set => m_showAtStart = value; }
        public bool ShowAtUpdate { get => m_showAtUpdate; set => m_showAtUpdate = value; }
        public string Key { get => m_key; set => m_key = value; }
        public VariableType Type { get => m_type; set => m_type = value; }
        public VariableSaveType SaveType { get => m_saveType; set => m_saveType = value; }
        public Text TextUi { get => m_textUi; set => m_textUi = value; }


        // monoBehaviour___________________________________________________________
        private void Awake()
        {
            if (TextUi == null)
                TextUi = GetComponent<Text>();
        }
        void Start()
        {
            if (!IsActive) return;
            if (ShowAtStart)
                ShowSavedData();
        }
        void Update()
        {
            if (!IsActive) return;
            if (!ShowAtUpdate) return;

            ShowSavedData();
        }


        // function________________________________________________________________
        public void ShowSavedData()
        {
            if (!IsActive) return;

            switch (Type)
            {
                case VariableType.Int:
                    TextUi.text = Key + ": " + SaveLoad.LoadInt(Key, SaveType).ToString();
                    break;
                case VariableType.Float:
                    TextUi.text = Key + ": " + SaveLoad.LoadFloat(Key, SaveType).ToString();
                    break;
                case VariableType.String:
                    TextUi.text = Key + ": " + SaveLoad.LoadString(Key, SaveType);
                    break;
            }
        }
    }
}