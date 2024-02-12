using UnityEngine;

namespace Mechanics
{
    public class AttachPointCollider : MonoBehaviour
    {
        private BoxCollider2D _collider;

        private void Start()
        {
            _collider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
                return;

            if (_collider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
            {
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