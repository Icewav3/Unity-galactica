using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class Util
    {
        private void DisableChildrenWithAttachPoint(Transform parent)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);
                if (child.name == "AttachPoint")
                {
                    child.gameObject.SetActive(false);
                }
                else
                {
                    DisableChildrenWithAttachPoint(child);
                }
            }
        }
    }
}

