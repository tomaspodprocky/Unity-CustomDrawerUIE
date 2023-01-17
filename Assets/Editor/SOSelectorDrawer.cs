using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;

/// <summary>
/// Basic drawer to select an asset from a dropdown list. It provides the reference to the asset
/// in the same way as if the asset would be drag-dropped onto the inspector element.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SOSelectorDrawer<T>: PropertyDrawer where T: ScriptableObject 
{
    // this does not seem to currently work as in custom editors deriving from Editor
    // Therefore the reference to the XML is done manually in CreatePropertyGUI()
    public VisualTreeAsset drawerXML; 

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Create property container element.
        var container = new VisualElement();

        if (drawerXML == null)
        {
            // this is an alternative to referencing the XML file with a path. 
            // GUID does not change with the asset rename within Unity
            string assetPath = AssetDatabase.GUIDToAssetPath("4d898aaecf365974ab0d18c5e7a89007");
            drawerXML = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(assetPath);
        }
        drawerXML.CloneTree(container);

        // Get the dropdownfield
        var dropDown = container.Q<DropdownField>(name:"entityDropDown");
        dropDown.label = $"Select {typeof(T)}";
        if (dropDown == null)
        {
            container.Add(new HelpBox("Missing a DropDownField in the XML file", HelpBoxMessageType.Error));
        }

        string[] guidList = AssetDatabase.FindAssets($"t:{typeof(T)}");

        List<string> dropDownValues = new List<string>();
        List<T> assetList = new List<T>();

        foreach(string guid in guidList)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            T asset = AssetDatabase.LoadAssetAtPath<T>(path);
            dropDownValues.Add(asset.name);
            assetList.Add(asset);
        }
        dropDown.choices = dropDownValues;

        if (property.objectReferenceValue !=null)
        {
            string currentEntityName = assetList.Find((x) => x.Equals(property.objectReferenceValue)).name;
            dropDown.value = currentEntityName;
        }

        dropDown.RegisterValueChangedCallback((evt) => SetEntityClass(evt, property, assetList));

        return container;
    }

    private void SetEntityClass(ChangeEvent<string> evt , SerializedProperty property, 
                                                        List<T> assetList)
    {
        property.objectReferenceValue = assetList.Find((x) => x.name.Equals(evt.newValue));
        property.serializedObject.ApplyModifiedProperties();
        
    }

}

