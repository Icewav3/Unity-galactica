using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Helper;

namespace Client.GameEditor
{
    public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        private Canvas _canvas;
        private RectTransform _rectTransform;
        private Vector2 _startPosition;

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
            Util.CheckForNull(Camera.main, "Main Camera");
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int gridPosition = new Vector2Int(Mathf.RoundToInt(mousePosition.x),
                Mathf.RoundToInt(mousePosition.y));

            var blockPrefab = PrefabFinder(gameObject);
            if (blockPrefab == null)
            {
                Debug.LogError("blockPrefab is null");
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

            // Linq to select the first attach point within threshold
            var existingAttachPoint = existingBlockCreators
                .SelectMany(creator => creator.attachPoints)
                .FirstOrDefault(point =>
                    draggedBlockCreator.attachPoints
                        .Any(draggedPoint => Vector2.Distance(point, draggedPoint) <= 0.5f));

            // Use the resulted attach point if it exists, otherwise use the gridPosition
            Vector3 instantiatePosition = existingAttachPoint != null
                ? new Vector3(existingAttachPoint.x, existingAttachPoint.y, 0)
                : new Vector3(gridPosition.x, gridPosition.y, 0);

            // Instantiate the block prefab at the target position
            Instantiate(blockPrefab, instantiatePosition, Quaternion.identity);
        }

        private GameObject PrefabFinder(GameObject blockSprite)
        {
            string prefabName = blockSprite.name;
            string path = "Prefabs" + "/" + prefabName;
            GameObject prefab = Resources.Load<GameObject>(path);

            return prefab;
        }
    }
}