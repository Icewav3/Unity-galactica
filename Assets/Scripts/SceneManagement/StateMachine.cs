using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class StateMachine : MonoBehaviour
    {
        //topic decoupling or robust code?
        //This state machine/service managers purpose is to allow my game to be more robust. for example occasionaly the event system would vanish or we would have 2 instances, beacuse of this I implmented a check upon scene instanciation to see if the event manager exists and is the correct one. this makes my project more decoupled and less reliant on other parts
        //topic game states
        //due to needing seperate logic in different game states and their correpsonding scenes, I have different scene scripts, and due to the way I am working on making on making things more decoupled it would be logical to not have a random gameobject control the scene logic and instead the statemachine to act as a service manager to run it. problem is this involves coupling the scripts and relying on a literal inspector connection between the 2 scripts. This has caused pain
        public static readonly bool DebugMode = true;
        private String _currentState;
        public EventSystem EventSystem { get; private set; }
        public AudioListener AudioListener { get; private set; }
        public static StateMachine Instance { get; private set; }

        private void Awake()
        {
            //singleton pattern
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

        private void Start()
        {
            if (_currentState == null)
            {
                _currentState = SceneManager.GetActiveScene().name;
            }

            if (DebugMode)
            {
                Debug.Log("Started in scene: " + SceneManager.GetActiveScene().name);
            }
        }

        public static void TransitionTo(string sceneName) //somehow ended up w/a duplicate here
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            StateMachine.Instance.InitializeEventSystem(); //Access this specific insatnce of the state machine to make it work with static
        }

        private void InitializeEventSystem() // this is an example of me trying to make my code more robust - checking and instancing event system if it doesnt exist could prevent game breaking bugs if something happened during scene initalization
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