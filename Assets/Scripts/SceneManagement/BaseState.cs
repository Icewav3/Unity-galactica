using UnityEngine;

namespace SceneManagement
{
    public abstract class BaseState : MonoBehaviour //parent state class
    {
        public abstract void Update();
    }
}