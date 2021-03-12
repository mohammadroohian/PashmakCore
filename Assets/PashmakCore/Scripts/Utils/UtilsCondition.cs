namespace Pashmak.Core.Condition
{
    // enum____________________________________________________________________
    public enum Value_conditionElement { Equal = 0, NotEqual = 1 }
    public enum Int_ConditionElement // Condition statement in int type.
    {
        Equals = 0,
        Grater = 1,
        Less = 2,
        NotEqual = 3,
        GraterOrEquals = 4,
        LessOrEquals = 5
    }
    public enum Float_ConditionElement // Condition statement in float type.
    {
        Grater = 0,
        Less = 1
    }
    public enum String_ConditionElement // Condition statement in string type.
    {
        Equals = 0,
        Contains = 1,
        EndsWith = 2,
        StartsWith = 3
    }


    // class___________________________________________________________________
    [System.Serializable]
    public class ConditionHolder_NoValue
    {
        public Int_ConditionElement m_int;
        public Float_ConditionElement m_float;
        public String_ConditionElement m_string;
    }

    [System.Serializable]
    public class ConditionHolder
    {
        public Condition_Int m_int;
        public Condition_Float m_float;
        public Condition_String m_string;
    }
    [System.Serializable]
    public class Condition_Int
    {
        public int m_value;
        public Int_ConditionElement m_conditionElement;
    }
    [System.Serializable]
    public class Condition_Float
    {
        public float m_value;
        public Float_ConditionElement m_conditionElement;
    }
    [System.Serializable]
    public class Condition_String
    {
        public string m_value;
        public String_ConditionElement m_conditionElement;
    }
}