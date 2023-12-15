using SceneManagement;
using UnityEngine;

namespace Client.UI
{
    public class SceneTransitionButton : MonoBehaviour, IButtonFunctionality
    {
        public void OnButtonClick()
        {
            StateMachine.TransitionTo("MainMenu");
        }
    }
}