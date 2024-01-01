using Client.GameEditor;
using Content;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(BlockCreator))]
    // This editor script allows for easier management for each attach point
    public class BlockCreatorEditor : UnityEditor.Editor
    {
        private BlockCreator _blockCreator;
        private Block _blockScript;

        private void OnEnable()
        {
            _blockCreator = (BlockCreator)target;
            _blockScript = _blockCreator.GetComponent<Block>();
        }

        private void OnSceneGUI()
        {
            Handles.color = Color.red;

            for (var i = 0; i < _blockCreator.attachPoints.Count; i++)
            {
                Vector2 newCreatorPointPosition =
                    UpdateAttachPoint(_blockCreator.transform, _blockCreator.attachPoints[i]);
                if (newCreatorPointPosition == _blockCreator.attachPoints[i])
                    continue;

                Undo.RecordObject(_blockCreator, "Move Sphere Handle");
                _blockCreator.attachPoints[i] = newCreatorPointPosition;
                _blockScript.attachPoints[i] = newCreatorPointPosition;
            }
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); // Draw the default inspector for the script

            if (GUILayout.Button("Generate Attach Points"))
            {
                GenerateAttachPoints();
            }

            if (GUILayout.Button("Add Attach Point"))
            {
                Undo.RecordObject(_blockCreator, "Add Attach Point");
                _blockCreator.attachPoints.Add(Vector2.zero);
                _blockScript.attachPoints.Add(Vector2.zero);
            }
        }

        private static Vector2 UpdateAttachPoint(Transform transform, Vector2 attachPoint)
        {
            Vector2 transformedPointPosition = transform.TransformPoint(attachPoint);
            Vector2 newPointPosition = Handles.PositionHandle(
                transformedPointPosition,
                Quaternion.identity);
            return transform.InverseTransformPoint(newPointPosition);
        }

        private void GenerateAttachPoints()
        {
            SpriteRenderer spriteRenderer = _blockCreator.GetComponentInChildren<SpriteRenderer>();
            Sprite sprite = spriteRenderer.sprite;

            _blockCreator.attachPoints.Clear();

            float halfWidth = sprite.bounds.size.x / 2;
            float halfHeight = sprite.bounds.size.y / 2;

            Vector2[] points =
            {
                new Vector2(-halfWidth, 0), // Left
                new Vector2(halfWidth, 0), // Right
                new Vector2(0, -halfHeight), // Bottom 
                new Vector2(0, halfHeight) // Top
            };

            _blockCreator.attachPoints.AddRange(points);
            _blockScript.attachPoints = _blockCreator.attachPoints;
        }
    }
}