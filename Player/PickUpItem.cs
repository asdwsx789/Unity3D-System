using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

public class PickUpItem : MonoBehaviour
{
    public InventoryObject inventory;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            print("Save OK");
            inventory.Save();
        }
        
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            print("Load OK");
            inventory.Load();
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {/*
        var item = other.GetComponent<GroundItem>();
        if(item)
        {
            inventory.AddItem(new Item( item.item), 1);
            Destroy(other.gameObject);
        }*/
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[24];
    }
}
