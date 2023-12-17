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
        /// <summary>
        /// logs an error if the object is null in the console
        /// </summary>
        /// <param name="object to check for"></param>
        /// <param name="name of the object"></param>
        public static void CheckForNull(object obj, string name)
        {
            if (obj == null)
            {
                Debug.LogError($"The {name} is null.", null);
            }
        }
    }
}

