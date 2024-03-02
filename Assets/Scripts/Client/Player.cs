using UnityEngine;

namespace Client
{
    public class Player : MonoBehaviour //client todo make this into a prefab, gamestate works in TestingScene
    {
        public string shipName;
        public GameObject shipContainer;
        public Canvas canvas;

        // Unity's Start or Awake methods can be used to initialize the references
        private void Awake()
        {
            shipContainer = GameObject.Find("ShipContainer");
            shipName = shipContainer.name;
            canvas = GameObject.Find("EditorCanvas").GetComponent<Canvas>();
        }
    }
}