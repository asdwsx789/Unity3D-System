//---------------------------------
ItemObject.cs
{
�D�㪫��ҪO
        public GameObject prefab;
        public ItemType type;
        [TextArea(15, 20)]
        public string description;

description �y�z
}

DefaultObject.cs
�إߵ��ͦ��q�{������

FoodObject.cs
�إߵ��ͦ���������

EquipmentObject.cs
�إߵ��ͦ��D��(�]��)����
//---------------------------------
InventoryObject.cs
{
    List<InventorySlot>XXX

    AddItem(_item, _amount)
    {
        //�T�{���~�Ѥ��O�_�����~
        for.....
            if(XXX[i].item == _item)
                XXX[i].Addamount(_amount)
        XXX.Add(new Inventory(database.GetId[_item],_item,_amount))

    }

    �w�s��[System.Serializable]
    class Inventory
    {   
        //���~�и�,���~,�ƶq
        ID
        ItemObject Item
        amount

        //Ū�� �s������ƾ�
        punlic Invntory(_id, _item, _amount)

        //�K�[�w�s
        Public Addamout(vaule)
    }
}

DisplayInventory.cs
{

}
