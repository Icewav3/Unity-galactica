using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Client.GameEditor
{
    public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        private Canvas _canvas; // The canvas the sprite is on
        private RectTransform _rectTransform; // The rect transform of the sprite
        private Vector2 _startPosition; // The starting position of the sprite

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            _rectTransform = transform as RectTransform;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // Get the position of the mouse in world space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Snap the position to the nearest grid cell
            Vector2Int gridPosition =
                new Vector2Int(Mathf.RoundToInt(mousePosition.x), Mathf.RoundToInt(mousePosition.y));

            //Find prefab
            Debug.Log("gameobject name: " + gameObject.name);
            var blockPrefab = PrefabFinder(gameObject);
            var image = blockPrefab.GetComponent<Image>();
            Debug.Log("prefab name: " + image);

            // Instantiate the block prefab at the grid position
            Instantiate(blockPrefab, new Vector3(gridPosition.x, gridPosition.y), Quaternion.identity);
        }

        private GameObject PrefabFinder(GameObject blockSprite)
        {
            string prefabName = blockSprite.name;
            Debug.Log(blockSprite.name);
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