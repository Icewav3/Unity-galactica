using UnityEngine;

namespace SceneManagement
{
    public abstract class BaseState : MonoBehaviour, IState //parent state class
    {
        public abstract void StartState();

        public abstract void UpdateState();
    }
}