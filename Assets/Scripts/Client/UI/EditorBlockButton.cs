using UnityEngine;
using UnityEngine.UI;

namespace Client.UI
{
    public class EditorBlockButton : MonoBehaviour
    {
        private string _imageName;
        private GameObject _instantiatedPrefab;
        private Camera _mainCamera;

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
                _instantiatedPrefab.transform.position = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

                // Check for mouse clicks
                if (Input.GetMouseButton(0)) OnLeftClick(); // Left click
                else if (Input.GetMouseButton(1)) OnRightClick(); // Right click
            }
        }

        private void OnLeftClick()
        {
            // Call your method here
        }

        private void OnRightClick()
        {
            // Destroy the instantiated prefab
            Destroy(_instantiatedPrefab);
            _instantiatedPrefab = null;
        }
    }
}