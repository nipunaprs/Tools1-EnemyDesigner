using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

//Adds this to the create menu in navigation
[CreateAssetMenuAttribute(fileName = "New Warrior Data", menuName = "Character Data/Warrior")]
public class WarriorData : CharacterData
{
    public WarriorClassType classType;
    public WarriorWpnType wpnType;
}
