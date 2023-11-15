using Client;
using Client.GameEditor;
using Content;
using Content.Blocks;
using Content.Blocks.MovementBlocks;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;



namespace Editor
{
    [CustomEditor(typeof(BlockCreator))]
    //this editor script allows for easier management for each attach point
    //TODO implement auto generation for some attach points
    
    public class BlockCreatorEditor : UnityEditor.Editor
    {
        private BlockCreator blockCreator;

        private void OnEnable()
        {
            blockCreator = (BlockCreator)target;
        }

        public override void OnInspectorGUI()
        {
            // Draw the default inspector for the script
            DrawDefaultInspector();

            // Add a button to add a new point to the array
            if (GUILayout.Button("Add Attach Point"))
            {
                Undo.RecordObject(blockCreator, "Add Attach Point");
                ArrayUtility.Add(ref blockCreator.attachPoints, Vector2.zero);
                
            }
        }

        private void OnSceneGUI()
        {
            Handles.color = Color.red;

            // Loop through the attachPoints array
            for (int i = 0; i < blockCreator.attachPoints.Length; i++)
            {
                Vector2 newPosition =
                    Handles.PositionHandle(blockCreator.transform.TransformPoint(blockCreator.attachPoints[i]),
                        Quaternion.identity);

                // Check if the point has moved
                if (newPosition != blockCreator.attachPoints[i])
                {
                    Undo.RecordObject(blockCreator, "Move Attach Point");
                    blockCreator.attachPoints[i] = blockCreator.transform.InverseTransformPoint(newPosition);
                }

                // Draw a sphere handle at the attach point's position
                EditorGUI.BeginChangeCheck();
                float handleSize =
                    HandleUtility.GetHandleSize(blockCreator.transform.TransformPoint(blockCreator.attachPoints[i])) *
                    0.1f;
                var fmh_59_90_638297678441340406 = Quaternion.identity; Vector2 newPointPosition = Handles.FreeMoveHandle(
                    blockCreator.transform.TransformPoint(blockCreator.attachPoints[i]),
                    handleSize, Vector2.one, Handles.SphereHandleCap);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(blockCreator, "Move Sphere Handle");
                    blockCreator.attachPoints[i] = blockCreator.transform.InverseTransformPoint(newPointPosition);
                }
            }
        }
    }
}
