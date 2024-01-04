using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class StateMachine : MonoBehaviour
    {
        public static readonly bool
            DebugMode = true;
        // Properties with private setters
        public EventSystem EventSystem{ get; private set; }
        public AudioListener AudioListener { get; private set; }
        //public string CurrentScene { get; private set; }
        public static StateMachine Instance { get; private set; }
        private BaseState _currentState;
        

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                if (Instance.gameObject.scene.buildIndex != gameObject.scene.buildIndex)
                {
                    //Destroy(this.gameObject);
                }
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject); // Don't destroy the StateMachine when loading a new scene
                
                InitializeEventSystem();
                Debug.Log(Instance.EventSystem);
                DontDestroyOnLoad(this.EventSystem);
                // InitializeAudioListener();
                // Debug.Log(Instance.AudioListener);
                // DontDestroyOnLoad(this.AudioListener);
            }
        }

        private void Start()
        {
            if(DebugMode){Debug.Log("Started in scene: " + SceneManager.GetActiveScene().name);}
            //TransitionTo("Game");
            if(DebugMode){Debug.Log("Booted to scene: " + SceneManager.GetActiveScene().name);}
        }

        private void Update()
        {
            UpdateState();
        }

        public static void TransitionTo(string sceneName)
        {
            //Instance.CurrentScene = SceneManager.GetActiveScene().name;
            //Debug.Log(Instance.CurrentScene);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            Instance.InitializeEventSystem();
            Instance.InitializeAudioListener();
            UpdateState();
        }

        private static void UpdateState()
        {
            Instance._currentState?.Update(); // Null check before updating state
        }
        private void InitializeEventSystem()
        {
            EventSystem[] allEventSystems = FindObjectsOfType<EventSystem>();

            foreach (var es in allEventSystems) // Destroy all EventSystems except ours
            {
                if (es != EventSystem)
                {
                    Destroy(es.gameObject);
                }
            }

            if (EventSystem == null) // If there is no EventSystem, create one
            {
                GameObject eventSystemObject = new GameObject("EventSystem");
                EventSystem = eventSystemObject.AddComponent<EventSystem>();
                eventSystemObject.AddComponent<StandaloneInputModule>();
                DontDestroyOnLoad(eventSystemObject);
            }
        }

        private void InitializeAudioListener()
        {
            AudioListener = FindObjectOfType<AudioListener>();

            if (AudioListener == null)
            {
                GameObject audioListenerObject = new GameObject("AudioListener");
                AudioListener = audioListenerObject.AddComponent<AudioListener>();
                DontDestroyOnLoad(audioListenerObject);
            }
        }
    }
}