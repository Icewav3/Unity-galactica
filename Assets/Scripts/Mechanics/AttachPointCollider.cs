using Client.UI;
using Content;
using UnityEngine;

namespace Mechanics
{
    public class AttachPointCollider : MonoBehaviour
    {
        public bool active = true;
        private BoxCollider2D _collider;
        private EditorStateController _editorStateController;
        private ShipContainer _shipContainer;

        private void Start()
        {
            _collider = GetComponent<BoxCollider2D>();
            _shipContainer = transform.parent.GetComponentInParent<ShipContainer>();
            _editorStateController = GameObject.Find("ShipEditor").GetComponent<EditorStateController>();
            _editorStateController.onEditorModeChanged.AddListener(OnEditorModeChanged);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && active)
            {
                if (_collider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    _collider.enabled = false;
                    ConnectBlock();
                }
            }
        }

        private void ConnectBlock()
        {
            if (!active)
            {
                // If the AttachPointCollider is not active, don't connect any blocks
                return;
            }

            if (!EditorBlockButton.CurrentInstantiatedPrefab
                    .TryGetComponent<
                        Block>(out Block blockToAdd)) //BUG if the currentinstanciatedprefab is destroyed via right click, this will throw an error
            {
                Debug.LogError("No block component in the instantiated prefab. Check the prefab setup.");
                return;
            }

            _shipContainer.AddBlock(blockToAdd, _collider.transform.position);
            Debug.Log("Connected block at position: " + _collider.transform.position);

            this.enabled = false;
        }

        private void OnEditorModeChanged(bool inEditorMode)
        {
            active = inEditorMode;
        }
    }
}