using UnityEngine;

namespace Pashmak.Core.CU._UnityEngine
{
    public class CU_SingletonObject : CU_Component
    {
        // variable________________________________________________________________
        [SerializeField] private string m_instanceCode = "-1";


        // property________________________________________________________________
        public string InstanceCode { get => m_instanceCode; private set => m_instanceCode = value; }


        // monoBehaviour___________________________________________________________
        private void Awake()
        {
            CU_SingletonObject[] allUnits = Resources.FindObjectsOfTypeAll(typeof(CU_SingletonObject)) as CU_SingletonObject[];
            for (int i = 0; i < allUnits.Length; i++)
            {
                if (allUnits[i].InstanceCode.Equals(InstanceCode) && allUnits[i] != this && allUnits[i].isActiveAndEnabled)
                    Destroy(gameObject);
            }
        }
    }
}