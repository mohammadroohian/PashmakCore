using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }


        // implement_______________________________________________________________
        void IDefaultExcute.DefaultExecute() => Quit();
    }
}