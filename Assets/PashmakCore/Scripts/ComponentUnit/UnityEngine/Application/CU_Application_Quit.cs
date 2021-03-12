using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pashmak.Core.CU._UnityEngine._Application
{
    public class CU_Application_Quit : CU_Component, IDefaultExcute
    {
        // function________________________________________________________________
        public void Quit()
        {
            if (!IsActive) return;
            Application.Quit();
        }


        // implement_______________________________________________________________
        void IDefaultExcute.DefaultExecute() => Quit();
    }
}