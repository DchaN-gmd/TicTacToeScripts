using Game;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(Line)), CanEditMultipleObjects]
public class LineCustomGUI : Editor
{
    private SerializedProperty _showType;
    private SerializedProperty _image;
    private SerializedProperty _slider;
    private SerializedProperty _event;

    private void OnEnable()
    {
        _image = serializedObject.FindProperty("_image");
        _showType = serializedObject.FindProperty("_showType");
        _slider = serializedObject.FindProperty("_slider");
        _event = serializedObject.FindProperty("_showing");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.SetIsDifferentCacheDirty();
        EditorGUILayout.PropertyField(_showType);
        
        if (_showType.enumValueIndex == (int)ShowType.Image)
        {
            EditorGUILayout.PropertyField(_image);
        }
        else if(_showType.enumValueIndex == (int)ShowType.Slider)
        {
            EditorGUILayout.PropertyField(_slider);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Events");
        EditorGUILayout.PropertyField(_event);

        serializedObject.ApplyModifiedProperties();
    }
}
