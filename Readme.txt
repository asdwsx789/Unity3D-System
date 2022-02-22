//---------------------------------
ItemObject.cs
{
道具物件模板
        public GameObject prefab;
        public ItemType type;
        [TextArea(15, 20)]
        public string description;

description 描述
}

DefaultObject.cs
建立菜單生成默認的物件

FoodObject.cs
建立菜單生成食物物件

EquipmentObject.cs
建立菜單生成道具(設備)物件
//---------------------------------
InventoryObject.cs
{
    List<InventorySlot>XXX

    AddItem(_item, _amount)
    {
        //確認物品槽內是否有物品
        for.....
            if(XXX[i].item == _item)
                XXX[i].Addamount(_amount)
        XXX.Add(new Inventory(database.GetId[_item],_item,_amount))

    }

    庫存槽[System.Serializable]
    class Inventory
    {   
        //物品標號,物品,數量
        ID
        ItemObject Item
        amount

        //讀取 存放相關數據
        punlic Invntory(_id, _item, _amount)

        //添加庫存
        Public Addamout(vaule)
    }
}

DisplayInventory.cs
{

}
