using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class StateMachine : MonoBehaviour
    {
        
        public static readonly bool DebugMode = true;

        private BaseState _currentState;

        public EventSystem EventSystem { get; private set; }
        public AudioListener AudioListener { get; private set; }
        public static StateMachine Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                if (Instance.gameObject.scene.buildIndex != gameObject.scene.buildIndex)
                {
                    Destroy(this.gameObject);
                }
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
                InitializeEventSystem();
            }
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            InitializeEventSystem();
            InitializeAudioListener();
            _currentState.Start();
        }

        private void Start()
        {
            if (DebugMode)
            {
                Debug.Log("Started in scene: " + SceneManager.GetActiveScene().name);
            }
            // Remove the unnecessary calls here
            if (DebugMode)
            {
                Debug.Log("Booted to scene: " + SceneManager.GetActiveScene().name);
            }
        }

        private void Update()
        {
            _currentState.Update();
        }

        public static void TransitionTo(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        private void InitializeEventSystem()
        {
            EventSystem[] allEventSystems = FindObjectsOfType<EventSystem>();

            foreach (var es in allEventSystems)
            {
                if (es != EventSystem)
                {
                    Destroy(es.gameObject);
                }
            }

            if (EventSystem == null)
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
