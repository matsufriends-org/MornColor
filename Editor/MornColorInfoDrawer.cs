#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace MornColor
{
    [CustomPropertyDrawer(typeof(MornColorInfo))]
    public class MornColorInfoDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 色で表示
            var colorInfo = property.objectReferenceValue as MornColorInfo;
            if (colorInfo == null)
            {
                EditorGUI.PropertyField(position, property, label, true);
                return;
            }

            // 左半分にPropertyField 、右半分に色
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                var rect = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
                var width = rect.width;
                rect.width = width * 0.5f;
                EditorGUI.PropertyField(rect, property, GUIContent.none);
                rect.x += rect.width;
                rect.width = width * 0.5f;
                EditorGUI.DrawRect(rect, colorInfo.Color);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }
    }
}
#endif