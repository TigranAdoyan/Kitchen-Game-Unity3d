using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICounter 
{
    void SetActive(bool status);
    void Action(Player player);
    Transform GetFollowTransform();
}
