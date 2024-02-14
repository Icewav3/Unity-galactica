using UnityEngine;

namespace Mechanics {
    public class AttachPointCollider : MonoBehaviour {
        private BoxCollider2D _collider;
        private bool _isActive = false; // New field to track activation state

        private void Start()
        {
            _collider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
                return;

            if (!_isActive && _collider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition))) // Check if attach point is not already active
            {
                _isActive = true;
                _collider.enabled = false;
                ConnectBlock();
            }
        }

        private void ConnectBlock()
        {
            Debug.Log("Connected block at position: " + _collider.transform.position);
            this.enabled = false; // disable the script
        }
    }
}
