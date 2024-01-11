using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemInfo
{
    /// <summary>
    /// 1. items 폴더에 저장된 프리펩 순서대로 작성할 것.
    /// 2. enum 유니티에 저장되는 이름으로 대소문자 잘 구분할 것!!!!
    /// </summary>
    public enum EItemName
    {
        A_Cap,
        A_Umbrella,
        A_Wing,
        P_Slime,
        T_Ironman,
        Len
    }

    public enum EItemTag
    {
        Clothes,
        Hands,
        Head,
        Bag,
        Pet,
        Food,
        Len
    }
} // end of class
