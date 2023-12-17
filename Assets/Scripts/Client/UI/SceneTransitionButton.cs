using SceneManagement;
using UnityEngine;

namespace Client.UI
{
    public class SceneTransitionButton : MonoBehaviour
    {
        public string sceneName;
        public void OnButtonClick()
        {
            StateMachine.TransitionTo(sceneName);
            print("Pressed button for scene: " + sceneName);
        }
    }
}