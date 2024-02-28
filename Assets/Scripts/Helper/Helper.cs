using UnityEngine;

namespace Helper
{
    public static class Util
    {
        public static void DisableChildrenWithName(Transform parent, string name)
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

        /// <summary>
        /// Finds or creates a GameObject with the specified name.
        /// </summary>
        /// <param name="name">The name of the GameObject to find or create.</param>
        /// <returns>The GameObject with the specified name. If the GameObject does not exist, a new GameObject with the specified name is returned.</returns>
        public static GameObject FindOrCreateGameObject(string name)
        {
            GameObject foundObject = GameObject.Find(name);
            if (foundObject == null)
            {
                foundObject = new GameObject(name);
            }
            
            return foundObject;
        }

        /// <summary>
        /// Finds or creates a Component of a specified type and name.
        /// </summary>
        /// <typeparam name="T">The type of the Component to find or create.</typeparam>
        /// <param name="name">The name of the Component to find or create.</param>
        /// <returns>The Component of the specified type and name. If the Component does not exist, a new Component with the specified type and name is returned.</returns>
        public static T FindOrCreateComponent<T>(string name) where T : Component
        {
            T foundComponent = GameObject.FindObjectOfType<T>();
            if (foundComponent == null)
            {
                GameObject newGameObject = new GameObject(name);
                foundComponent = newGameObject.AddComponent<T>();
            }

            return foundComponent;
        }
    }
}