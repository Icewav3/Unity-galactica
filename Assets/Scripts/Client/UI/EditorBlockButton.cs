using Client.GameEditor;
using Content;
using UnityEngine;
using UnityEngine.UI;

namespace Client.UI
{
    public class EditorBlockButton : MonoBehaviour
    {
        private string _imageName;
        private GameObject _instantiatedPrefab;
        private Camera _mainCamera;
        public ShipContainer ShipContainer { get; set; } // Reference to the ShipContainer

        private void Start()
        {
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

        public void OnButtonClick()
        {
            Debug.Log("Pressed button for block: " + _imageName);

            // Instantiate the prefab
            GameObject prefab = Resources.Load<GameObject>("Prefabs/" + _imageName);
            if (prefab != null)
            {
                _instantiatedPrefab = Instantiate(prefab);
            }
            else
            {
                Debug.LogError("Prefab not found");
            }
        }

        private void Update()
        {
            if (_instantiatedPrefab)
            {
                // Move the prefab with the cursor
                Vector3 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0; // Ensure the prefab stays in the 2D plane
                _instantiatedPrefab.transform.position = mousePos;

                // Check for mouse clicks
                if (Input.GetMouseButtonDown(0)) OnLeftClick(); // Left click
                else if (Input.GetMouseButtonDown(1)) OnRightClick(); // Right click
            }
        }

        private void OnLeftClick()
        {
            Debug.Log("OnLeftClick called");

            if (_instantiatedPrefab)
            {
                Debug.Log("Instantiated prefab exists");

                Block block = _instantiatedPrefab.GetComponent<Block>();
                if (block != null)
                {
                    Debug.Log("Block component found on prefab");

                    // Check if the block is close enough to an attach point
                    foreach (Vector2 attachPoint in block.attachPoints)
                    {
                        Debug.Log("Checking attach point: " + attachPoint);

                        if (Vector2.Distance(attachPoint, _instantiatedPrefab.transform.position) < 0.5f) // Change this value based on your needs
                        {
                            Debug.Log("Attach point is close enough");

                            // Add the block to the ship and remove the attach point
                            ShipContainer.AddBlock(block);
                            Debug.Log("Block added to ship");

                            // Assuming attachPoints is a List<Vector2>
                            block.attachPoints.Remove(attachPoint);
                            Debug.Log("Attach point removed from block");

                            _instantiatedPrefab = null; // Clear the reference to the prefab
                            Debug.Log("Prefab reference cleared");
                            break;
                        }
                    }
                }
                else
                {
                    Debug.Log("Block component not found on prefab");
                }
            }
            else
            {
                Debug.Log("Instantiated prefab does not exist");
            }
        }

        private void OnRightClick()
        {
            // Destroy the instantiated prefab
            if (_instantiatedPrefab)
            {
                Destroy(_instantiatedPrefab);
                _instantiatedPrefab = null;
            }
        }
    }
}