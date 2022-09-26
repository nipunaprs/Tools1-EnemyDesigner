using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

//Adds this to the create menu in navigation
[CreateAssetMenuAttribute(fileName="New Healer Data", menuName ="Character Data/Healer")]
public class HealerData : CharacterData
{
    public HealerStrategyType strType;
    public HealerWpnType wpnType;
}
