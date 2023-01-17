using UnityEngine;

[CreateAssetMenu(fileName = "NewItemClass", menuName = "Custom/New Item Class", order = 0)]
public class ItemClass : ScriptableObject
{
        [SerializeField] float itemProperty;
}
