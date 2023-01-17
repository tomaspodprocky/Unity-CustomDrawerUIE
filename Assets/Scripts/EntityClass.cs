using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEntityClass", menuName = "Custom/New Entity Class", order = 0)]
public class EntityClass : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] string id;
    [SerializeField] float entityProperty;

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    { 
        if (id==string.Empty)
        {
            id = System.Guid.NewGuid().ToString();
        }
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    { }

}

