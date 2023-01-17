using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

/// <summary>
/// Custom editor for EntityProperties class
/// </summary>
/// <remarks>
/// As of Unity 2021.3, it is not possible to have the UIElements property drawers to be part of inspectors.
/// It is necessary to create a new inspector to use a Custom property drawer based in UIElements.
/// </remarks>
[CustomEditor(typeof(EntityProperties))]
public class EntityPropertiesEditor : Editor
{
    public VisualTreeAsset inspectorXML;

    public override VisualElement CreateInspectorGUI()
    {
        // Create a new VisualElement to be the root of our inspector UI
        VisualElement myInspector = new VisualElement();

        inspectorXML.CloneTree(myInspector);

        // Add a simple label
        myInspector.Add(new Label("This is a custom inspector - from C#"));

        VisualElement inspectorFoldout = myInspector.Q("default_inspector");
        InspectorElement.FillDefaultInspector(inspectorFoldout, serializedObject, this);

        // Return the finished inspector UI
        return myInspector;
    }
    
}