using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class StateMachine : MonoBehaviour
    {
        public static readonly bool
            DebugMode = true; // Renamed 'Debug' to 'DebugMode' to avoid conflict with UnityEngine.Debug

        private BaseState _currentState;
        public static StateMachine Instance { get; private set; }
        public string CurrentScene { get; private set; } // Use a property instead of a public field

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject); // Don't destroy the StateMachine when loading a new scene
            }
        }

        private void Start()
        {
            TransitionTo("Game");
            Debug.Log("Booted scene: " + SceneManager.GetActiveScene().name);
        }

        private void Update()
        {
            UpdateState();
        }

        public static void TransitionTo(string sceneName)
        {
            Instance.CurrentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
            SceneManager.UnloadSceneAsync(Instance.CurrentScene);
            Instance.CurrentScene = SceneManager.GetActiveScene().name;
            UpdateState();
        }

        private static void UpdateState()
        {
            Instance._currentState?.Update(); // Null check before updating state
        }
    }
}