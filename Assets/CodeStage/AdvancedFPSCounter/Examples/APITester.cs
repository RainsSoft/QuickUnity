using System;
using CodeStage.AdvancedFPSCounter.CountersData;
using CodeStage.AdvancedFPSCounter.Labels;
using UnityEngine;

namespace CodeStage.AdvancedFPSCounter
{
	public class APITester : MonoBehaviour
	{
		private int selectedTab = 0;
		private readonly string[] tabs = {"Common", "FPS Counter", "Memory Counter", "Device info"};

		private void OnGUI()
		{
			GUILayout.BeginArea(new Rect(40,110,Screen.width-80,Screen.height - 140));

			GUIStyle centeredStyle = new GUIStyle(GUI.skin.label);
			centeredStyle.alignment = TextAnchor.UpperCenter;
			centeredStyle.richText = true;

			GUIStyle richStyle = new GUIStyle(GUI.skin.label);
			richStyle.richText = true;

			GUILayout.Label("<b>Public API usage examples</b>", centeredStyle);

			selectedTab = GUILayout.Toolbar(selectedTab, tabs);

			if (selectedTab == 0)
			{
				GUILayout.Space(10);

				GUILayout.Label("Operation Mode");
				int operationMode = (int)AFPSCounter.Instance.OperationMode;
				operationMode = GUILayout.Toolbar(operationMode, new[]
				{
					AFPSCounterOperationMode.Disabled.ToString(),
					AFPSCounterOperationMode.Background.ToString(),
					AFPSCounterOperationMode.Normal.ToString()
				});

				AFPSCounter.Instance.OperationMode = (AFPSCounterOperationMode)operationMode;

				GUILayout.Label("Hot Key");
				int hotKeyIndex;

				if (AFPSCounter.Instance.hotKey == KeyCode.BackQuote)
				{
					hotKeyIndex = 1;
				}
				else
				{
					hotKeyIndex = (int)AFPSCounter.Instance.hotKey;
				}

				hotKeyIndex = GUILayout.Toolbar(hotKeyIndex, new[] { "None (disabled)", "BackQoute (`)"});
				if (hotKeyIndex == 1)
				{
					AFPSCounter.Instance.hotKey = KeyCode.BackQuote;
				}
				else
				{
					AFPSCounter.Instance.hotKey = KeyCode.None;
				}

				AFPSCounter.Instance.keepAlive = GUILayout.Toggle(AFPSCounter.Instance.keepAlive, "Keep Alive");

				GUILayout.BeginHorizontal();
				AFPSCounter.Instance.ForceFrameRate = GUILayout.Toggle(AFPSCounter.Instance.ForceFrameRate, "Force FPS", GUILayout.Width(100));
				AFPSCounter.Instance.ForcedFrameRate = (int)SliderLabel(AFPSCounter.Instance.ForcedFrameRate, -1, 100);
				GUILayout.EndHorizontal();

				
				float offsetX = AFPSCounter.Instance.AnchorsOffset.x;
				float offsetY = AFPSCounter.Instance.AnchorsOffset.y;

				GUILayout.BeginHorizontal();
				GUILayout.Label("Pixel offset X", GUILayout.Width(100));
				offsetX = (int)SliderLabel(offsetX, 0, 100);
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				GUILayout.Label("Pixel offset Y", GUILayout.Width(100));
				offsetY = (int)SliderLabel(offsetY, 0, 100);
				GUILayout.EndHorizontal();

				AFPSCounter.Instance.AnchorsOffset = new Vector2(offsetX, offsetY);

				GUILayout.BeginHorizontal();
				GUILayout.Label("Font Size", GUILayout.Width(100));
				AFPSCounter.Instance.FontSize = (int)SliderLabel(AFPSCounter.Instance.FontSize, 0, 100);
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				GUILayout.Label("Line spacing", GUILayout.Width(100));
				AFPSCounter.Instance.LineSpacing = SliderLabel(AFPSCounter.Instance.LineSpacing, 0f, 10f);
				GUILayout.EndHorizontal();
			}
			else if (selectedTab == 1)
			{
				GUILayout.Space(10);
				AFPSCounter.Instance.fpsCounter.Enabled = GUILayout.Toggle(AFPSCounter.Instance.fpsCounter.Enabled, "Enabled");
				GUILayout.Space(10);
				AFPSCounter.Instance.fpsCounter.Anchor = (LabelAnchor)GUILayout.Toolbar((int)AFPSCounter.Instance.fpsCounter.Anchor, new[] { "UpperLeft", "UpperRight", "LowerLeft", "LowerRight" });
				GUILayout.Space(10);

				GUILayout.BeginHorizontal();
				GUILayout.Label("Update Interval", GUILayout.Width(100));
				AFPSCounter.Instance.fpsCounter.UpdateInterval = SliderLabel(AFPSCounter.Instance.fpsCounter.UpdateInterval, 0.1f, 10f);
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				AFPSCounter.Instance.fpsCounter.ShowAverage = GUILayout.Toggle(AFPSCounter.Instance.fpsCounter.ShowAverage, "Average FPS");

				if (AFPSCounter.Instance.fpsCounter.ShowAverage)
				{
					GUILayout.Label("Samples", GUILayout.Width(60));
					AFPSCounter.Instance.fpsCounter.AverageFromSamples = (int)SliderLabel(AFPSCounter.Instance.fpsCounter.AverageFromSamples, 0, 100);
					GUILayout.Space(10);
					AFPSCounter.Instance.fpsCounter.resetAverageOnNewScene = GUILayout.Toggle(AFPSCounter.Instance.fpsCounter.resetAverageOnNewScene, "Reset Average On New Scene Load", GUILayout.ExpandWidth(false));
					if (GUILayout.Button("Reset now!", GUILayout.ExpandWidth(false)))
					{
						AFPSCounter.Instance.fpsCounter.ResetAverage();
					}
				}
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				AFPSCounter.Instance.fpsCounter.ShowMinMax = GUILayout.Toggle(AFPSCounter.Instance.fpsCounter.ShowMinMax, "MinMax FPS");

				if (AFPSCounter.Instance.fpsCounter.ShowMinMax)
				{
					AFPSCounter.Instance.fpsCounter.resetMinMaxOnNewScene = GUILayout.Toggle(AFPSCounter.Instance.fpsCounter.resetMinMaxOnNewScene, "Reset MinMax On New Scene Load", GUILayout.ExpandWidth(false));
					if (GUILayout.Button("Reset now!", GUILayout.ExpandWidth(false)))
					{
						AFPSCounter.Instance.fpsCounter.ResetMinMax();
					}
				}
				GUILayout.EndHorizontal();
			}
			else if (selectedTab == 2)
			{
				GUILayout.Space(10);
				AFPSCounter.Instance.memoryCounter.Enabled = GUILayout.Toggle(AFPSCounter.Instance.memoryCounter.Enabled, "Enabled");
				GUILayout.Space(10);
				AFPSCounter.Instance.memoryCounter.Anchor = (LabelAnchor)GUILayout.Toolbar((int)AFPSCounter.Instance.memoryCounter.Anchor, new[] { "UpperLeft", "UpperRight", "LowerLeft", "LowerRight" });
				GUILayout.Space(10);
				GUILayout.BeginHorizontal();
				GUILayout.Label("Update Interval", GUILayout.Width(100));
				AFPSCounter.Instance.memoryCounter.UpdateInterval = SliderLabel(AFPSCounter.Instance.memoryCounter.UpdateInterval, 0.1f, 10f);
				GUILayout.EndHorizontal();
				
				AFPSCounter.Instance.memoryCounter.PreciseValues = GUILayout.Toggle(AFPSCounter.Instance.memoryCounter.PreciseValues, "Precise (uses more system resources)");
				AFPSCounter.Instance.memoryCounter.TotalReserved = GUILayout.Toggle(AFPSCounter.Instance.memoryCounter.TotalReserved, "Show total reserved memory size");
				AFPSCounter.Instance.memoryCounter.Allocated = GUILayout.Toggle(AFPSCounter.Instance.memoryCounter.Allocated, "Show allocated memory size");
				AFPSCounter.Instance.memoryCounter.MonoUsage = GUILayout.Toggle(AFPSCounter.Instance.memoryCounter.MonoUsage, "Show mono memory usage");
			}
			else if (selectedTab == 3)
			{
				GUILayout.Space(10);
				AFPSCounter.Instance.deviceInfoCounter.Enabled = GUILayout.Toggle(AFPSCounter.Instance.deviceInfoCounter.Enabled, "Enabled");
				GUILayout.Space(10);
				AFPSCounter.Instance.deviceInfoCounter.Anchor = (LabelAnchor)GUILayout.Toolbar((int)AFPSCounter.Instance.deviceInfoCounter.Anchor, new[] { "UpperLeft", "UpperRight", "LowerLeft", "LowerRight" });
				GUILayout.Space(10);
				AFPSCounter.Instance.deviceInfoCounter.CpuModel = GUILayout.Toggle(AFPSCounter.Instance.deviceInfoCounter.CpuModel, "Show CPU model and maximum threads count");
				AFPSCounter.Instance.deviceInfoCounter.GpuModel = GUILayout.Toggle(AFPSCounter.Instance.deviceInfoCounter.GpuModel, "Show GPU model and total VRAM count");
				AFPSCounter.Instance.deviceInfoCounter.RamSize = GUILayout.Toggle(AFPSCounter.Instance.deviceInfoCounter.RamSize, "Show total RAM size");
				AFPSCounter.Instance.deviceInfoCounter.ScreenData = GUILayout.Toggle(AFPSCounter.Instance.deviceInfoCounter.ScreenData, "Show resolution, window size and DPI (if possible)");
			}

			int averageFps = AFPSCounter.Instance.fpsCounter.lastAverageValue;

			GUILayout.Label("<b>Raw counters values</b> (read using API)", richStyle);

			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical(GUILayout.ExpandWidth(true));
			
			GUILayout.Label("  FPS: " + AFPSCounter.Instance.fpsCounter.lastValue +
							"  AVG: " + averageFps + 
							"\n  MIN: " + AFPSCounter.Instance.fpsCounter.lastMinimumValue +
							"  MAX: " + AFPSCounter.Instance.fpsCounter.lastMaximumValue);

			if (AFPSCounter.Instance.memoryCounter.PreciseValues)
			{
				GUILayout.Label("  Memory (Total, Allocated, Mono):\n  " +
								AFPSCounter.Instance.memoryCounter.lastTotalValue / (float)MemoryCounterData.MEMORY_DIVIDER + ", " +
								AFPSCounter.Instance.memoryCounter.lastAllocatedValue / (float)MemoryCounterData.MEMORY_DIVIDER + ", " +
								AFPSCounter.Instance.memoryCounter.lastMonoValue / (float)MemoryCounterData.MEMORY_DIVIDER);
			}
			else
			{
				GUILayout.Label("  Memory (Total, Allocated, Mono):\n  " +
								AFPSCounter.Instance.memoryCounter.lastTotalValue + ", " +
								AFPSCounter.Instance.memoryCounter.lastAllocatedValue + ", " +
								AFPSCounter.Instance.memoryCounter.lastMonoValue);
			}
			GUILayout.EndVertical();
			GUILayout.Label(AFPSCounter.Instance.deviceInfoCounter.lastValue);
			GUILayout.EndHorizontal();

			GUILayout.EndArea();
		}

		private float SliderLabel(float sliderValue, float sliderMinValue, float sliderMaxValue)
		{
			GUILayout.BeginHorizontal();
			sliderValue = GUILayout.HorizontalSlider(sliderValue, sliderMinValue, sliderMaxValue);
			GUILayout.Space(10);
			GUILayout.Label(String.Format("{0:F2}", sliderValue), GUILayout.ExpandWidth(false));
			GUILayout.EndHorizontal();

			return sliderValue;
		}
	}
}
