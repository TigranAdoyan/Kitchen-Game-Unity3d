using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ResipeListSO : ScriptableObject
{
    [SerializeField] public List<ResipeSO> resipeSOList;
}
