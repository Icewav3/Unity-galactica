using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameEditor
{
    public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        private Vector2 startPosition; // The starting position of the sprite
        private Canvas canvas; // The canvas the sprite is on
        private RectTransform rectTransform; // The rect transform of the sprite

        private void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
            rectTransform = transform as RectTransform;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // Get the position of the mouse in world space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Snap the position to the nearest grid cell
            Vector2Int gridPosition = new Vector2Int(Mathf.RoundToInt(mousePosition.x), Mathf.RoundToInt(mousePosition.y));

            //Find prefab
            var blockPrefab = PrefabFinder(gameObject);
            var image = blockPrefab.GetComponent<Image>();
            Debug.Log("prefab name: " + blockPrefab.name);

            // Instantiate the block prefab at the grid position
            Instantiate(blockPrefab, new Vector3(gridPosition.x, gridPosition.y), Quaternion.identity);
        }

        private GameObject PrefabFinder(GameObject blockSprite)
        {
            string prefabName = blockSprite.name;
            string path = "Prefabs" + "/" + prefabName;
            GameObject prefab = Resources.Load<GameObject>(path);
            
            //load corresponding sprite
            /*Sprite sprite = Resources.Load<Sprite>("BlockSprites/" + prefabName);
            print("prefab "+prefabName);
            prefab.GetComponent<SpriteRenderer>().sprite = sprite;*/
            
            return prefab;
        }
    }
}