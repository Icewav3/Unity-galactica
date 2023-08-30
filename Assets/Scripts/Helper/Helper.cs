using UnityEngine;

namespace Helper
{
    public static class Util
    {
        public static void DisableChildrenWithName(Transform parent, string name )
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);
                if (child.name == name)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }
}

