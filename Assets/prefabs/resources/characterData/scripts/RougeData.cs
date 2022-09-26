using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

//Adds this to the create menu in navigation
[CreateAssetMenuAttribute(fileName = "New Rouge Data", menuName = "Character Data/Rouge")]
public class RougeData : CharacterData
{
    public RougeWpnType wpnType;
    public RougeStrategyType strType;
}
