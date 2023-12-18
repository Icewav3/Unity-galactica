using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Helper;

namespace Client.GameEditor
{
    public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler //Deprecated - see EditorBlockButton.cs
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
            Helper.Util.CheckForNull(Camera.main,"Main Camera");
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Snap the position to the nearest grid cell
            Vector2Int gridPosition =
                new Vector2Int(Mathf.RoundToInt(mousePosition.x), Mathf.RoundToInt(mousePosition.y));

            //Find prefab
            Debug.Log("gameobject name: " + gameObject.name);
            var blockPrefab = PrefabFinder(gameObject);
            if (blockPrefab == null)
            {
                Debug.LogError("blockPrefab is null");
                return;
            }

            var image = blockPrefab.GetComponent<Image>();
            if (image == null)
            {
                Debug.LogError("Image component is missing in blockPrefab");
                return;
            }

            BlockCreator draggedBlockCreator = blockPrefab.GetComponent<BlockCreator>();
            if (draggedBlockCreator == null)
            {
                Debug.LogError("BlockCreator component is missing in blockPrefab");
                return;
            }

            BlockCreator[] existingBlockCreators = FindObjectsOfType<BlockCreator>();
            if (existingBlockCreators == null || existingBlockCreators.Length == 0)
            {
                Debug.LogError("No BlockCreator objects found in the scene");
                return;
            }

            foreach (BlockCreator existingBlockCreator in existingBlockCreators)
            {
                foreach (Vector2 existingAttachPoint in existingBlockCreator.attachPoints)
                {
                    foreach (Vector2 draggedAttachPoint in draggedBlockCreator.attachPoints)
                    {
                        // Calculate the distance between the attach points
                        float distance = Vector2.Distance(existingAttachPoint, draggedAttachPoint);

                        // If the distance is within a certain threshold
                        if (distance <= 0.5f) // Change this value to your needs
                        {
                            // Instantiate the block prefab at the position of the existing object's attach point
                            Instantiate(blockPrefab, new Vector3(
                                    existingAttachPoint.x, 
                                    existingAttachPoint.y), 
                                    Quaternion.identity);
                            return;
                        }
                    }
                }
            }

            // If no attach point is within the threshold, instantiate the block prefab at the grid position
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