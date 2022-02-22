using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Items/Food")]
public class FoodObject : ItemObject
{
     //restoreHHealth回復健康
     public int restoreHealthValue;
    public void Awake()
    {
        type = ItemType.Food;
     }
}