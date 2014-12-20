using UnityEditor;
using UnityEngine;

/// <summary>
/// Class UIPageViewEditor.
/// </summary>
[CustomEditor(typeof(UIPageView))]
public class UIPageViewEditor : UIScrollViewEditor
{
    /// <summary>
    /// Called when [inspector GUI].
    /// </summary>
    public override void OnInspectorGUI()
    {
        NGUIEditorTools.DrawProperty("Page View Item Width", serializedObject, "itemWidth");
        NGUIEditorTools.DrawProperty("Page View Item Height", serializedObject, "itemHeight");

        serializedObject.ApplyModifiedProperties();

        base.OnInspectorGUI();
    }
}