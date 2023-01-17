using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityProperties : MonoBehaviour
{
    [SerializeField] EntityClass entityClass;
    [SerializeField] ItemClass itemClass;

    [SerializeField] string myString = "Default string from component";
    [SerializeField] float[] parameters;
    [SerializeField] LayerMask layers;
    [SerializeField] Color color;

}
