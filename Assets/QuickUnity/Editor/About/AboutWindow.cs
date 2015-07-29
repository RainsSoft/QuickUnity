using System;
using UnityEditor;
using UnityEngine;

namespace QuickUnity.Editor.About
{
    /// <summary>
    /// AboutWindow show the information about author.
    /// </summary>
    public sealed class AboutWindow : EditorWindow
    {
        /// <summary>
        /// Editor GUI code.
        /// </summary>
        private void OnGUI()
        {
            GUILayout.Space(10);
            GUILayout.BeginVertical();
            EditorGUILayout.LabelField("QuickUnity framework");
            EditorGUILayout.LabelField("Author: Jerry Lee");
            EditorGUILayout.LabelField("E-mail: cosmos53076@163.com");
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("OK"))
            {
                Close();
            }

            GUILayout.Space(10);
        }
    }
}