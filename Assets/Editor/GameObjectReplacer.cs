using UnityEngine;
using UnityEditor;

public class GameObjectReplacer : EditorWindow
{
    // Prefab to replace selected objects with
    private GameObject prefab;

    // Options for matching properties from the original object to the prefab
    private bool matchPosition = true;
    private bool matchRotation = true;
    private bool matchScale = true;

    // Adds the custom editor window to the Unity menu
    [MenuItem("Tools/GameObject Replacer")]
    static void OpenWindow()
    {
        // Open or create the window
        GameObjectReplacer window = (GameObjectReplacer)EditorWindow.GetWindow(typeof(GameObjectReplacer));
        window.Show();
    }

    // Draws the UI elements for the custom editor window
    private void OnGUI()
    {
        // Warn user if in Play mode (this tool works only in Edit mode)
        if (EditorApplication.isPlaying)
        {
            EditorGUILayout.HelpBox("GameObject Replacer only works in Edit mode!", MessageType.Warning);
            return;
        }

        // Input field for selecting the prefab
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);

        // Toggles for matching the transform properties
        matchPosition = EditorGUILayout.Toggle("Match Position", matchPosition);
        matchRotation = EditorGUILayout.Toggle("Match Rotation", matchRotation);
        matchScale = EditorGUILayout.Toggle("Match Scale", matchScale);

        // Button to execute the replacement operation
        if (GUILayout.Button("Replace"))
        {
            ReplaceSelectedObjects();
        }
    }

    // Replaces the selected game objects with the specified prefab
    private void ReplaceSelectedObjects()
    {
        if (prefab == null)
        {
            Debug.LogError("No prefab selected!");
            return;
        }

        GameObject[] selectedObjects = Selection.gameObjects;
        Undo.RecordObjects(selectedObjects, "Replace With Prefab");

        foreach (GameObject oldObject in selectedObjects)
        {
            // Store hierarchy info
            Transform parent = oldObject.transform.parent;
            int siblingIndex = oldObject.transform.GetSiblingIndex();
            string originalName = oldObject.name;
            string originalTag = oldObject.tag;
            int originalLayer = oldObject.layer;

            // Instantiate the new object
            GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            if (newObject == null)
            {
                Debug.LogError("Failed to instantiate prefab.");
                continue;
            }

            Undo.RegisterCreatedObjectUndo(newObject, "Replace With Prefab");

            // Match transform
            if (matchPosition)
                newObject.transform.position = oldObject.transform.position;

            if (matchRotation)
                newObject.transform.rotation = oldObject.transform.rotation;

            if (matchScale)
                newObject.transform.localScale = oldObject.transform.localScale;

            // Match hierarchy
            newObject.transform.SetParent(parent);
            newObject.transform.SetSiblingIndex(siblingIndex);

            // Preserve tag, layer, name
            newObject.tag = originalTag;
            newObject.layer = originalLayer;
            newObject.name = originalName;

            // Destroy old object
            Undo.DestroyObjectImmediate(oldObject);
        }
    }
}
