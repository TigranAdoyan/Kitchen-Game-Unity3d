using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ResipeSO : ScriptableObject
{
    public List<KitchenObjectSO> kitchenObjectsSOList;

    public string resipeName;
}
