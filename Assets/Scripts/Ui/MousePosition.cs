using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;
using Unity.Mathematics;
using UnityEngine;

namespace Ui
{
    public class MousePosition
    {
        public GameObject Canvas { get; set; }
        public Camera MainCamera { get; set; }

        public Vector3 Aiming()
        {
            Vector3 mousePos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            return mousePos;
        }
        public Vector3 Editor()
        {
            Vector3 mousePos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            return mousePos;
        }
    }  
}

