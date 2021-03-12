using System.Text;
using UnityEngine;

namespace Pashmak.Core.CU
{
    public abstract class CU_Component : MonoBehaviour, IDetails
    {
        // variable________________________________________________________________
        [SerializeField] protected bool m_isActive = true;


        // property________________________________________________________________
        public bool IsActive { get => m_isActive; set => m_isActive = value; }


        // implement_______________________________________________________________
        public virtual string GetDetails(string gameObjectName, string componentName, string methodName, string methodParams)
        {
            StringBuilder strB = new StringBuilder();
            if (!string.IsNullOrEmpty(gameObjectName))
                strB.Append(string.Format(" [ {0} ] .", gameObjectName));
            if (!string.IsNullOrEmpty(componentName))
                strB.Append(string.Format(" {0} .", componentName));
            if (!string.IsNullOrEmpty(methodName))
                strB.Append(string.Format(" {0}(", methodName));
            if (!string.IsNullOrEmpty(methodParams))
                strB.Append(string.Format("{0}", methodParams));
            if (!string.IsNullOrEmpty(methodName))
                strB.Append(")");
            return strB.ToString();
        }
    }
}