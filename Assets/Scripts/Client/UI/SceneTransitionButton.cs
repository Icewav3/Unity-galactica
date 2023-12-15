using SceneManagement;
using UnityEngine;

namespace Client.UI
{
    public interface IButtonFunctionality
    {
        void OnButtonClick();
    }
    public class SceneTransitionButton : MonoBehaviour, IButtonFunctionality
    {
        public string sceneName;
        public void OnButtonClick()
        {
            StateMachine.TransitionTo(sceneName);
            print("Pressed button for scene: " + sceneName);
        }
    }
}