using Content;
using UnityEngine;
using UnityEngine.UI;

namespace Client.UI
{
    public class EditorBlockButton : MonoBehaviour
    {
        public static GameObject CurrentInstantiatedPrefab;
        public GameObject InstantiatedPrefab;
        private EditorStateController _editorStateController;
        private string _imageName;
        private Camera _mainCamera;
        private ShipContainer shipContainer;

        private void Start()
        {
            _editorStateController = FindObjectOfType<EditorStateController>();
            shipContainer = GameObject.Find("ShipContainer").GetComponent<ShipContainer>();
            if (_editorStateController)
            {
                _editorStateController.onEditorModeChanged.AddListener(OnEditorModeChanged);
            }

            Image imageComponent = GetComponent<Image>();
            if (imageComponent != null && imageComponent.sprite != null)
            {
                _imageName = imageComponent.sprite.name;
                Debug.Log("Image name: " + _imageName);
            }
            else
            {
                Debug.LogError("Image component or sprite is missing");
            }

            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (InstantiatedPrefab)
            {
                // Move the prefab with the cursor
                Vector3 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0; // Ensure the prefab stays in the 2D plane
                InstantiatedPrefab.transform.position = mousePos;

                // Check for mouse clicks
                if (Input.GetMouseButtonDown(1)) OnRightClick();
                if (Input.GetMouseButtonDown(0)) OnleftClick();
            }
        }

        private void OnleftClick()
        {
            if (CurrentInstantiatedPrefab && shipContainer)
            {
                shipContainer.AddBlock(CurrentInstantiatedPrefab,
                    _mainCamera.ScreenToWorldPoint(Input.mousePosition));
            }
        }
        public void OnButtonClick()
        {
            Debug.Log("Pressed button for block: " + _imageName);
            if (CurrentInstantiatedPrefab)
            {
                Destroy(CurrentInstantiatedPrefab);
                CurrentInstantiatedPrefab = null;
                Debug.Log("Destroyed the previous prefab");
            }

            // Instantiate the prefab
            GameObject prefab = Resources.Load<GameObject>("Prefabs/" + _imageName);
            if (prefab != null)
            {
                InstantiatedPrefab = Instantiate(prefab);
                CurrentInstantiatedPrefab = InstantiatedPrefab;
            }
            else
            {
                Debug.LogError("Prefab not found");
            }
        }

        private void OnRightClick()
        {
            // Destroy the instantiated prefab
            if (InstantiatedPrefab)
            {
                Destroy(InstantiatedPrefab);
                InstantiatedPrefab = null;
            }
        }

        private void OnEditorModeChanged(bool isEditorMode)
        {
            if (!isEditorMode && InstantiatedPrefab)
            {
                Destroy(InstantiatedPrefab);
                InstantiatedPrefab = null;
            }
        }
    }
}