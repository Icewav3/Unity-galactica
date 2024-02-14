using UnityEngine;

namespace Mechanics {
    public class AttachPointManager : MonoBehaviour {
        [SerializeField] private bool topColliderActive = true;
        [SerializeField] private bool bottomColliderActive = true;
        [SerializeField] private bool leftColliderActive = true;
        [SerializeField] private bool rightColliderActive = true;

        private GameObject _attachPointColliderPrefab;
        private Vector2 _squareSize;
        private GameObject[] _colliders = new GameObject[4]; // 0 - Top, 1 - Right, 2 - Bottom, 3 - Left

        private void Awake() {
            if (_attachPointColliderPrefab == null)
                _attachPointColliderPrefab = Resources.Load<GameObject>("Prefabs/AttachPointCollider");

            _squareSize = GetComponent<SpriteRenderer>().bounds.size;
            CreateColliders();
        }

        private void CreateColliders() {
            if (topColliderActive)
                _colliders[0] = CreateCollider(new Vector2(0, _squareSize.y));
            if (rightColliderActive)
                _colliders[1] = CreateCollider(new Vector2(_squareSize.x, 0));
            if (bottomColliderActive)
                _colliders[2] = CreateCollider(new Vector2(0, -_squareSize.y));
            if (leftColliderActive)
                _colliders[3] = CreateCollider(new Vector2(-_squareSize.x, 0));
        }

        private GameObject CreateCollider(Vector2 offset) {
            var instance = Instantiate(_attachPointColliderPrefab, transform, false);
            instance.transform.localPosition = offset;
            return instance;
        }

        public void ToggleColliders(bool state)
        {
            foreach (GameObject crnt_collider in _colliders)
            {
                if (crnt_collider)
                    crnt_collider.SetActive(state);
            }
        }
    }
}