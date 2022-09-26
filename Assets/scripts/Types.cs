using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enums can exist outside of class
namespace Types
{
    public enum MageDmgType
    {
        FIRE,
        ICE
    }

    public enum MageWpnType
    {
        WAND
    }

    public enum WarriorClassType
    {
        DEFENDER,
        BERSERKER
    }

    public enum WarriorWpnType
    {
        ONE_HANDED_SWORD,
        TWO_HANDED_SWORD,
        DUAL_WIELDED_SWORDS
    }

    public enum RougeWpnType
    {
        DAGGERS,
        BOW
    }

    public enum RougeStrategyType
    {
        STEALTH,
        SPEED
    }

    public enum HealerWpnType
    {
        STAFF
    }

    public enum HealerStrategyType
    {
        HBOOST,
        DBOOST
        
    }


}