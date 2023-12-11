using UnityEngine;

namespace SceneManagement
{
    public class MainMenuState : BaseState
    {
        private Camera _camera;
        private GameObject _canvas;

        public override void Update()
        {
            print("MainMenuState");
        }
    }
}