using UnityEngine;
using UnityEngine.UI;

namespace GameEditor
{
    public class GenerateScrollItems : MonoBehaviour
    {
        public GameObject draggablePrefab; // prefab for the draggable sprites
        public RectTransform content; // the content object in the scrollbar

        public Sprite[] sprites; // the sprites to use

        void Start()
        {
            // Load the sprites from the BlockSprites folder
            sprites = Resources.LoadAll<Sprite>("BlockSprites");

            // Instantiate a draggable sprite for each sprite in the folder
            foreach (var sprite in sprites)
            {
                GameObject draggable = Instantiate(draggablePrefab, content);
                draggable.GetComponent<Image>().sprite = sprite;
            }
        }
    }
}