using QuickUnity.FX;
using System.Collections;
using UnityEditor;
using UnityEngine;

/// <summary>
/// The FX namespace.
/// </summary>
namespace QuickUnityEditor.FX
{
    /// <summary>
    /// The Inspector view of SimpleOcean.
    /// </summary>
    [CustomEditor(typeof(SimpleOcean))]
    public class SimpleOceanInspector : Editor
    {
        /// <summary>
        /// Gets the ocean object.
        /// </summary>
        /// <value>The ocean.</value>
        private SimpleOcean ocean
        {
            get { return target as SimpleOcean; }
        }

        /// <summary>
        /// The texture settings foldout.
        /// </summary>
        private bool textureSettingsExpand = EditorPrefs.GetBool("textureSettingsExpand");

        /// <summary>
        /// The color settings foldout.
        /// </summary>
        private bool colorSettingsExpand = EditorPrefs.GetBool("colorSettingsExpand");

        /// <summary>
        /// The size settings foldout.
        /// </summary>
        private bool sizeSettingsExpand = EditorPrefs.GetBool("sizeSettingsExpand");

        /// <summary>
        /// The wave settings foldout.
        /// </summary>
        private bool waveSettingsExpand = EditorPrefs.GetBool("waveSettingsExpand");

        /// <summary>
        /// The other settings foldout.
        /// </summary>
        private bool otherSettingsExpand = EditorPrefs.GetBool("otherSettingsExpand");

        /// <summary>
        /// Called when [inspector GUI].
        /// </summary>
        public override void OnInspectorGUI()
        {
            EditorGUIUtility.LookLikeControls(80.0f, 40.0f);

            EditorGUILayout.Separator();
            Rect rect = EditorGUILayout.BeginVertical();
            EditorGUI.DropShadowLabel(rect, "Simple Ocean");
            GUILayout.Space(16);
            EditorGUILayout.EndVertical();

            // Texture Settings.
            textureSettingsExpand = EditorGUILayout.Foldout(textureSettingsExpand, "Texture Settings");
            EditorPrefs.SetBool("textureSettingsExpand", textureSettingsExpand);

            if (textureSettingsExpand)
            {
                EditorGUILayout.BeginVertical();

                // Normal map width and height.
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Normal Map Width");
                GUILayout.Space(-20);
                ocean.NormalMapWidth = EditorGUILayout.IntField(ocean.NormalMapWidth, GUILayout.Width(40));
                GUILayout.Space(10);
                EditorGUILayout.LabelField("Normal Map Height");
                GUILayout.Space(-20);
                ocean.NormalMapHeight = EditorGUILayout.IntField(ocean.NormalMapHeight, GUILayout.Width(40));
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Separator();

                // Render Texture width and height.
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Render Texture Width");
                GUILayout.Space(-20);
                ocean.RenderTextureWidth = EditorGUILayout.IntField(ocean.RenderTextureWidth, GUILayout.Width(40));
                GUILayout.Space(10);
                EditorGUILayout.LabelField("Render Texture Height");
                GUILayout.Space(-20);
                ocean.RenderTextureHeight = EditorGUILayout.IntField(ocean.RenderTextureHeight, GUILayout.Width(40));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical();
                EditorGUILayout.Separator();
            }

            // Color Settings.
            colorSettingsExpand = EditorGUILayout.Foldout(colorSettingsExpand, "Color Settings");
            EditorPrefs.SetBool("colorSettingsExpand", colorSettingsExpand);

            if (colorSettingsExpand)
            {
                EditorGUILayout.BeginVertical();

                // Surface Color.
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Surface Color");
                GUILayout.Space(-180);
                ocean.SurfaceColor = EditorGUILayout.ColorField(ocean.SurfaceColor);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Separator();

                // Water Color.
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Water Color");
                GUILayout.Space(-180);
                ocean.WaterColor = EditorGUILayout.ColorField(ocean.WaterColor);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Separator();

                EditorGUILayout.EndVertical();
            }

            // Size Settings.
            sizeSettingsExpand = EditorGUILayout.Foldout(sizeSettingsExpand, "Size Settings");
            EditorPrefs.SetBool("sizeSettingsExpand", sizeSettingsExpand);

            if (sizeSettingsExpand)
            {
                EditorGUILayout.BeginVertical();

                // Tile polygon width.
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Tile Polygon Width");
                GUILayout.Space(-20);
                ocean.TilePolygonWidth = EditorGUILayout.IntField(ocean.TilePolygonWidth, GUILayout.Width(40));
                GUILayout.Space(10);
                EditorGUILayout.LabelField("Tile Polygon Height");
                GUILayout.Space(-20);
                ocean.TilePolygonHeight = EditorGUILayout.IntField(ocean.TilePolygonHeight, GUILayout.Width(40));
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Separator();

                // Ocean Tile Size.
                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField("Ocean Tile Size");
                ocean.OceanTileSize = EditorGUILayout.Vector3Field("", ocean.OceanTileSize);
                EditorGUILayout.EndVertical();

                EditorGUILayout.EndVertical();
                EditorGUILayout.Separator();
            }

            // Wave Settings.
            waveSettingsExpand = EditorGUILayout.Foldout(waveSettingsExpand, "Wave Settings");
            EditorPrefs.SetBool("waveSettingsExpand", waveSettingsExpand);

            if (waveSettingsExpand)
            {
                EditorGUILayout.BeginVertical();

                // Force Storm.
                ocean.ForceStorm = EditorGUILayout.ToggleLeft(" Force Storm", ocean.ForceStorm);
                EditorGUILayout.Separator();

                // Choopy Scale.
                EditorGUILayout.LabelField("Choopy Scale");
                ocean.ChoppyScale = EditorGUILayout.Slider(ocean.ChoppyScale, 0.1f, 10.0f);

                // Wave Scale Setting.
                EditorGUILayout.LabelField("Wave Scale");
                ocean.WaveScale = EditorGUILayout.Slider(ocean.WaveScale, 0.1f, 10.0f);
                EditorGUILayout.Separator();

                // Wave Speed Setting.
                EditorGUILayout.LabelField("Wave Speed");
                ocean.WaveSpeed = EditorGUILayout.Slider(ocean.WaveSpeed, 0.1f, 4.0f);
                EditorGUILayout.Separator();

                EditorGUILayout.EndVertical();
            }

            // Other Settings.
            otherSettingsExpand = EditorGUILayout.Foldout(otherSettingsExpand, "Other Settings");
            EditorPrefs.SetBool("otherSettingsExpand", otherSettingsExpand);

            if (otherSettingsExpand)
            {
                EditorGUILayout.BeginVertical();

                // Visible Simulation
                ocean.VisibleSimulation = EditorGUILayout.ToggleLeft(" Visible Simulation", ocean.VisibleSimulation);
                EditorGUILayout.Separator();

                // Reflection Enabled.
                ocean.ReflectionEnabled = EditorGUILayout.ToggleLeft(" Reflection Enabled", ocean.ReflectionEnabled);
                EditorGUILayout.Separator();

                // Ocean Material.
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Ocean Material");
                GUILayout.Space(-180);
                ocean.OceanMaterial = (Material)EditorGUILayout.ObjectField(ocean.OceanMaterial, typeof(Material), true);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Separator();

                // Render Layers.

                // Tiles Count.
                EditorGUILayout.LabelField("Tiles Count");
                ocean.TilesCount = (int)EditorGUILayout.Slider(ocean.TilesCount, 1, 10);
                EditorGUILayout.Separator();

                // Light Direction.
                EditorGUILayout.LabelField("Light Direction");
                ocean.LightDirection = EditorGUILayout.Vector3Field("", ocean.LightDirection);
                EditorGUILayout.Separator();

                EditorGUILayout.EndVertical();
            }
        }
    }
}