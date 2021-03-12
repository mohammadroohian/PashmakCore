using Pashmak.Core.Condition;
using Pashmak.Core.IO;
using UnityEngine;
using UnityEngine.Events;

namespace Pashmak.Core.CU
{
    public class CU_Condition_Variable : CU_Component, ICondition
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_checkAtStart = false;
        [SerializeField] private string m_key = "";// The key specify ane save from another.
        [SerializeField] private VariableType m_type = new VariableType();
        [SerializeField] private ConditionHolder m_condition = null;
        [SerializeField] private VariableSaveType m_saveType = VariableSaveType.Binary;
        [SerializeField] private UnityEvent m_onTrue = new UnityEvent();
        [SerializeField] private UnityEvent m_onFalse = new UnityEvent();


        // property________________________________________________________________
        public bool CheckAtStart { get => m_checkAtStart; private set => m_checkAtStart = value; }
        public string Key { get => m_key; set => m_key = value; }
        public VariableType VType { get => m_type; set => m_type = value; }
        public ConditionHolder Condition { get => m_condition; set => m_condition = value; }
        public VariableSaveType SaveType { get => m_saveType; set => m_saveType = value; }
        public UnityEvent OnTrue { get => m_onTrue; private set => m_onTrue = value; }
        public UnityEvent OnFalse { get => m_onFalse; private set => m_onFalse = value; }


        // implement_______________________________________________________________
        bool ICondition.CheckCondition()
        {
            bool result = false;
            switch (m_type)
            {
                case VariableType.Int:
                    #region Int
                    switch (m_condition.m_int.m_conditionElement)
                    {
                        case Int_ConditionElement.Equals:
                            if (GetIntValue() == m_condition.m_int.m_value) result = true;
                            break;
                        case Int_ConditionElement.Grater:
                            if (GetIntValue() > m_condition.m_int.m_value) result = true;
                            break;
                        case Int_ConditionElement.Less:
                            if (GetIntValue() < m_condition.m_int.m_value) result = true;
                            break;
                        case Int_ConditionElement.NotEqual:
                            if (GetIntValue() != m_condition.m_int.m_value) result = true;
                            break;
                        case Int_ConditionElement.GraterOrEquals:
                            if (GetIntValue() >= m_condition.m_int.m_value) result = true;
                            break;
                        case Int_ConditionElement.LessOrEquals:
                            if (GetIntValue() <= m_condition.m_int.m_value) result = true;
                            break;
                    }
                    break;
                #endregion
                case VariableType.Float:
                    #region Float
                    switch (m_condition.m_float.m_conditionElement)
                    {
                        case Float_ConditionElement.Grater:
                            if (GetFloatValue() > m_condition.m_float.m_value) result = true;
                            break;
                        case Float_ConditionElement.Less:
                            if (GetFloatValue() < m_condition.m_float.m_value) result = true;
                            break;
                    }
                    break;
                #endregion
                case VariableType.String:
                    #region String
                    switch (m_condition.m_string.m_conditionElement)
                    {
                        case String_ConditionElement.Equals:
                            if (GetStringValue().Equals(m_condition.m_string.m_value)) result = true;
                            break;
                        case String_ConditionElement.Contains:
                            if (GetStringValue().Contains(m_condition.m_string.m_value)) result = true;
                            break;
                        case String_ConditionElement.EndsWith:
                            if (GetStringValue().EndsWith(m_condition.m_string.m_value)) result = true;
                            break;
                        case String_ConditionElement.StartsWith:
                            if (GetStringValue().StartsWith(m_condition.m_string.m_value)) result = true;
                            break;
                    }
                    break;
                    #endregion
            }

            if (result)
                OnTrue.Invoke();
            else
                OnFalse.Invoke();

            return result;
        }
        string ICondition.GetDetails()
        {
            switch (VType)
            {
                case VariableType.Int:
                    #region Int
                    switch (m_condition.m_int.m_conditionElement)
                    {
                        case Int_ConditionElement.Equals:
                            return string.Format(" {0} == {1}:", Key, m_condition.m_int.m_value);

                        case Int_ConditionElement.Grater:
                            return string.Format(" {0} > {1}:", Key, m_condition.m_int.m_value);

                        case Int_ConditionElement.Less:
                            return string.Format(" {0} < {1}:", Key, m_condition.m_int.m_value);

                        case Int_ConditionElement.NotEqual:
                            return string.Format(" {0} != {1}:", Key, m_condition.m_int.m_value);

                        case Int_ConditionElement.GraterOrEquals:
                            return string.Format(" {0} >= {1}:", Key, m_condition.m_int.m_value);

                        case Int_ConditionElement.LessOrEquals:
                            return string.Format(" {0} <= {1}:", Key, m_condition.m_int.m_value);

                    }
                    break;
                #endregion
                case VariableType.Float:
                    #region Float
                    switch (m_condition.m_float.m_conditionElement)
                    {
                        case Float_ConditionElement.Grater:
                            return string.Format(" {0} > {1}:", Key, m_condition.m_float.m_value);

                        case Float_ConditionElement.Less:
                            return string.Format(" {0} < {1}:", Key, m_condition.m_float.m_value);

                    }
                    break;
                #endregion
                case VariableType.String:
                    #region String
                    switch (m_condition.m_string.m_conditionElement)
                    {
                        case String_ConditionElement.Equals:
                            return string.Format(" {0}.Equals({1}):", Key, m_condition.m_string.m_value);

                        case String_ConditionElement.Contains:
                            return string.Format(" {0}.Contains({1}):", Key, m_condition.m_string.m_value);

                        case String_ConditionElement.EndsWith:
                            return string.Format(" {0}.EndsWith({1}):", Key, m_condition.m_string.m_value);

                        case String_ConditionElement.StartsWith:
                            return string.Format(" {0}.StartsWith({1}):", Key, m_condition.m_string.m_value);

                    }
                    break;
                    #endregion
            }
            return null;
        }


        // override________________________________________________________________


        // monoBehaviour___________________________________________________________
        private void Start()
        {
            if (CheckAtStart)
                Check();
        }


        // function________________________________________________________________  
        private object GetValue() => SaveLoad.Load(Key, m_type, SaveType);// Load value form m_key.
        private int GetIntValue() => (int)SaveLoad.Load(Key, m_type, SaveType);
        private float GetFloatValue() => (float)SaveLoad.Load(Key, m_type, SaveType);
        private string GetStringValue() => (string)SaveLoad.Load(Key, m_type, SaveType);


        // function________________________________________________________________  
        public void Check()
        {
            if (!IsActive) return;

            ((ICondition)this).CheckCondition();
        }
    }
}