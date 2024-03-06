using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class EditorStateController : MonoBehaviour
{
    public UnityEvent<bool> onEditorModeChanged = new UnityEvent<bool>();
    public bool debugLogging = false;
    private bool _editor;
    private GameObject _editorCanvas;

    private void Start()
    {
        _editorCanvas = GameObject.Find("EditorCanvas");
        if (_editorCanvas == null)
        {
            Debug.LogWarning("EditorCanvas GameObject is missing in the scene or not assigned");
        }

        UpdateEditorMode(); //if this is not done after the gameobject.find will not work
    }

    public void ToggleEditorMode(InputAction.CallbackContext context)
    {
        _editor = !_editor;
        UpdateEditorMode();
    }

    private void UpdateEditorMode()
    {
        if (onEditorModeChanged != null)
        {
            onEditorModeChanged.Invoke(_editor); // Notify the listeners
        }

        _editorCanvas.SetActive(_editor);
        Time.timeScale = _editor ? 0 : 1;

        if (debugLogging)
        {
            Debug.Log($"Editor state: {_editor}");
            Debug.Log($"Sim speed: {Time.timeScale}");
        }
    }
}