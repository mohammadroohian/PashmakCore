using Pashmak.Core.Condition;
using Pashmak.Core.IO;
using UnityEngine;
using UnityEngine.Events;

namespace Pashmak.Core.CU
{
    public class CU_Condition_CompareTowVariable : CU_Component, ICondition
    {
        // variable________________________________________________________________
        [SerializeField] private bool m_checkAtStart = false;
        [SerializeField] private VariableType m_type = VariableType.Int;
        [SerializeField] private Variable m_vOne = new Variable(); // variable one
        [SerializeField] private Variable m_vTow = new Variable(); // variable tow
        [SerializeField] private ConditionHolder_NoValue m_condition = new ConditionHolder_NoValue();
        [SerializeField] private UnityEvent m_onTrue = new UnityEvent();
        [SerializeField] private UnityEvent m_onFalse = new UnityEvent();


        // property________________________________________________________________
        public bool CheckAtStart { get => m_checkAtStart; private set => m_checkAtStart = value; }
        public VariableType VType { get => m_type; set => m_type = value; }
        public Variable VOne { get => m_vOne; set => m_vOne = value; }
        public Variable VTow { get => m_vTow; set => m_vTow = value; }
        public ConditionHolder_NoValue Condition { get => m_condition; set => m_condition = value; }
        public UnityEvent OnTrue { get => m_onTrue; private set => m_onTrue = value; }
        public UnityEvent OnFalse { get => m_onFalse; private set => m_onFalse = value; }


        // implement_______________________________________________________________
        bool ICondition.CheckCondition()
        {
            bool result = false;

            switch (VType)
            {
                case VariableType.Int:
                    #region Int
                    switch (m_condition.m_int)
                    {
                        case Int_ConditionElement.Equals:
                            if (GetIntValue(VOne) == GetIntValue(VTow)) result = true;
                            break;
                        case Int_ConditionElement.Grater:
                            if (GetIntValue(VOne) > GetIntValue(VTow)) result = true;
                            break;
                        case Int_ConditionElement.Less:
                            if (GetIntValue(VOne) < GetIntValue(VTow)) result = true;
                            break;
                        case Int_ConditionElement.NotEqual:
                            if (GetIntValue(VOne) != GetIntValue(VTow)) result = true;
                            break;
                        case Int_ConditionElement.GraterOrEquals:
                            if (GetIntValue(VOne) >= GetIntValue(VTow)) result = true;
                            break;
                        case Int_ConditionElement.LessOrEquals:
                            if (GetIntValue(VOne) <= GetIntValue(VTow)) result = true;
                            break;
                    }
                    break;
                #endregion
                case VariableType.Float:
                    #region Float
                    switch (m_condition.m_float)
                    {
                        case Float_ConditionElement.Grater:
                            if (GetFloatValue(VOne) > GetFloatValue(VTow)) result = true;
                            break;
                        case Float_ConditionElement.Less:
                            if (GetFloatValue(VOne) < GetFloatValue(VTow)) result = true;
                            break;
                    }
                    break;
                #endregion
                case VariableType.String:
                    #region String
                    switch (m_condition.m_string)
                    {
                        case String_ConditionElement.Equals:
                            if (GetStringValue(VOne).Equals(GetStringValue(VTow))) result = true;
                            break;
                        case String_ConditionElement.Contains:
                            if (GetStringValue(VOne).Contains(GetStringValue(VTow))) result = true;
                            break;
                        case String_ConditionElement.EndsWith:
                            if (GetStringValue(VOne).EndsWith(GetStringValue(VTow))) result = true;
                            break;
                        case String_ConditionElement.StartsWith:
                            if (GetStringValue(VOne).StartsWith(GetStringValue(VTow))) result = true;
                            break;
                    }
                    break;
                    #endregion
            }

            if (result)
                OnTrue.Invoke();
            else
                OnFalse.Invoke();

            return false;
        }
        string ICondition.GetDetails()
        {
            switch (VType)
            {
                case VariableType.Int:
                    #region Int
                    switch (m_condition.m_int)
                    {
                        case Int_ConditionElement.Equals:
                            return string.Format(" {0} == {1}:", VOne.Key, VTow.Key);

                        case Int_ConditionElement.Grater:
                            return string.Format(" {0} > {1}:", VOne.Key, VTow.Key);

                        case Int_ConditionElement.Less:
                            return string.Format(" {0} < {1}:", VOne.Key, VTow.Key);

                        case Int_ConditionElement.NotEqual:
                            return string.Format(" {0} != {1}:", VOne.Key, VTow.Key);

                        case Int_ConditionElement.GraterOrEquals:
                            return string.Format(" {0} >= {1}:", VOne.Key, VTow.Key);

                        case Int_ConditionElement.LessOrEquals:
                            return string.Format(" {0} <= {1}:", VOne.Key, VTow.Key);

                    }
                    break;
                #endregion
                case VariableType.Float:
                    #region Float
                    switch (m_condition.m_float)
                    {
                        case Float_ConditionElement.Grater:
                            return string.Format(" {0} > {1}:", VOne.Key, VTow.Key);

                        case Float_ConditionElement.Less:
                            return string.Format(" {0} < {1}:", VOne.Key, VTow.Key);

                    }
                    break;
                #endregion
                case VariableType.String:
                    #region String
                    switch (m_condition.m_string)
                    {
                        case String_ConditionElement.Equals:
                            return string.Format(" {0}.Equals({1}):", VOne.Key, VTow.Key);

                        case String_ConditionElement.Contains:
                            return string.Format(" {0}.Contains({1}):", VOne.Key, VTow.Key);

                        case String_ConditionElement.EndsWith:
                            return string.Format(" {0}.EndsWith({1}):", VOne.Key, VTow.Key);

                        case String_ConditionElement.StartsWith:
                            return string.Format(" {0}.StartsWith({1}):", VOne.Key, VTow.Key);

                    }
                    break;
                    #endregion
            }
            return null;
        }


        // monoBehaviour___________________________________________________________
        private void Start()
        {
            if (!IsActive) return;
            if (CheckAtStart)
                Check();
        }


        // function________________________________________________________________  
        private object GetValue(Variable variable) => SaveLoad.Load(variable.Key, VType, variable.VSaveType); // Load value form m_key.
        private int GetIntValue(Variable variable) => (int)SaveLoad.Load(variable.Key, VType, variable.VSaveType);
        private float GetFloatValue(Variable variable) => (float)SaveLoad.Load(variable.Key, VType, variable.VSaveType);
        private string GetStringValue(Variable variable) => (string)SaveLoad.Load(variable.Key, VType, variable.VSaveType);
        public void Check()
        {
            if (!IsActive) return;

            ((ICondition)this).CheckCondition();
        }


        // class___________________________________________________________________
        [System.Serializable]
        public class Variable
        {
            // variable________________________________________________________________
            [SerializeField] private string m_key = "";// The key specify ane save from another.
            [SerializeField] private VariableSaveType m_saveType = VariableSaveType.Binary;


            // property________________________________________________________________
            public string Key { get => m_key; private set => m_key = value; }
            public VariableSaveType VSaveType { get => m_saveType; private set => m_saveType = value; }
        }
    }
}