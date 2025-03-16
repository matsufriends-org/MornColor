#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MornColor
{
    public sealed class MornColorWindow : EditorWindow
    {
        [MenuItem("Tools/" + nameof(MornColorWindow))]
        private static void Open()
        {
            var window = GetWindow<MornColorWindow>();
            window.titleContent = new GUIContent(nameof(MornColorWindow));
        }

        private Vector2 _scrollPosition;
        private readonly List<MornColorInfo> _colorInfos = new();
        private readonly List<MornGradientInfo> _gradationInfos = new();
        private readonly List<(string, MornColorInfo, MornGradientInfo)> _allList = new();

        private void OnEnable()
        {
            UpdateList();
        }

        private void UpdateList()
        {
            _colorInfos.Clear();
            _gradationInfos.Clear();
            _allList.Clear();
            foreach (var guid in AssetDatabase.FindAssets($"t:{nameof(MornColorInfo)}"))
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var colorInfo = AssetDatabase.LoadAssetAtPath<MornColorInfo>(path);
                _colorInfos.Add(colorInfo);
            }

            foreach (var guid in AssetDatabase.FindAssets($"t:{nameof(MornGradientInfo)}"))
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var gradationInfo = AssetDatabase.LoadAssetAtPath<MornGradientInfo>(path);
                _gradationInfos.Add(gradationInfo);
            }

            foreach (var colorInfo in _colorInfos)
            {
                _allList.Add((colorInfo.name, colorInfo, null));
            }

            foreach (var gradationInfo in _gradationInfos)
            {
                _allList.Add((gradationInfo.name, null, gradationInfo));
            }

            _allList.Sort((a, b) => string.Compare(a.Item1, b.Item1, StringComparison.Ordinal));
        }

        private void OnGUI()
        {
            using (var scrollView = new EditorGUILayout.ScrollViewScope(_scrollPosition))
            {
                if (GUILayout.Button("リスト更新", GUILayout.Height(50)))
                {
                    UpdateList();
                }

                // ここで色を編集しても反映されません。元アセットの色を変更してください。
                EditorGUILayout.HelpBox("ここで色を編集しても反映されません。元アセットの色を変更してください。", MessageType.Warning);
                foreach (var (fileName, colorInfo, gradationInfo) in _allList)
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            if (colorInfo != null)
                            {
                                using (new EditorGUI.DisabledScope(true))
                                {
                                    EditorGUILayout.ObjectField(colorInfo, typeof(MornColorInfo), false);
                                }

                                EditorGUILayout.ColorField(colorInfo.Color);
                            }
                            else if (gradationInfo != null)
                            {
                                using (new EditorGUI.DisabledScope(true))
                                {
                                    EditorGUILayout.ObjectField(gradationInfo, typeof(MornGradientInfo), false);
                                }

                                if (gradationInfo.Gradient != null)
                                {
                                    EditorGUILayout.GradientField(gradationInfo.Gradient);
                                }
                            }
                        }
                    }
                }

                _scrollPosition = scrollView.scrollPosition;
            }
        }
    }
}
#endif