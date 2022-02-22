using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Inventory
{
    public class DisplayInventory : MonoBehaviour
    {
        public MouseItem mouseItem = new MouseItem();

        public GameObject inventoryPrefad;
        public InventoryObject inventory;
        public int X_Start;
        public int Y_Start;
        public int X_Space_Between_Item;
        public int Number_Of_Column;
        public int Y_Space_Between_Item;
        //Dictionary<InventorySlot, GameObject> itemDisplayed = new Dictionary<InventorySlot, GameObject>();
        Dictionary<GameObject, InventorySlot> itemDisplayed = new Dictionary<GameObject, InventorySlot>();

        void Start()
        {
            //Cursor.lockState = CursorLockMode.Locked;
            CreateSlot();
        }

        void Update()
        {
            //UpdateDisplay();
            UpdataSlot();
        }

        public void UpdataSlot()
        {
            foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemDisplayed)
            {              
                if (_slot.Value.ID >= 0)
                {
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                    _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
                }
                else
                {
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                    _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
            }
        }
        
        //public void UpdateDisplay()
        //{
        //    for (int i = 0; i < inventory.Container.Items.Count; i++)
        //    {
        //        InventorySlot slot = inventory.Container.Items[i];
        //        if (itemDisplayed.ContainsKey(slot))
        //        {
        //            itemDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
        //        }
        //        else
        //        {
        //            var obj = Instantiate(inventoryPrefad, Vector3.zero, Quaternion.identity, transform);
        //            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.Id].uiDisplay;
        //            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
        //            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container.Items[i].amount.ToString("n0");

        //            itemDisplayed.Add(inventory.Container.Items[i], obj);
        //        }
        //    }
        //}

        public void CreateSlot()
        {
            itemDisplayed = new Dictionary<GameObject, InventorySlot>();
            for (int i = 0; i < inventory.Container.Items.Length; i++)
            {
                var obj = Instantiate(inventoryPrefad, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

                AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
                AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
                AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
                AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
                AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

                itemDisplayed.Add(obj, inventory.Container.Items[i]);
            }
        }
        
        public void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            EventTrigger trigger = obj.GetComponent<EventTrigger>();
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            trigger.triggers.Add(eventTrigger);
        }

        public void OnEnter(GameObject obj)
        {
            mouseItem.hoverObj = obj;
            if (itemDisplayed.ContainsKey(obj))
                mouseItem.item = itemDisplayed[obj];
        }
        public void OnExit(GameObject obj)
        {
            
            mouseItem.hoverObj = null;
            mouseItem.item = null;
        }
        public void OnDragStart(GameObject obj)
        {
            var mouseObject = new GameObject();
            var rt = mouseObject.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(40, 40);
            mouseObject.transform.SetParent(transform.parent);

            if(itemDisplayed[obj].ID >=0)
            {
                var img = mouseObject.AddComponent<Image>();
                img.sprite = inventory.database.GetItem[itemDisplayed[obj].ID].uiDisplay;
                img.raycastTarget = false;
            }

            mouseItem.obj = mouseObject;
            //mouseItem.item = itemDisplayed[obj];
        }
        public void OnDragEnd(GameObject obj)
        {
            if(mouseItem.hoverObj)
            {
                inventory.MoveItem(itemDisplayed[obj], itemDisplayed[mouseItem.hoverObj]);
            }
            else{
                inventory.RemoveItem(itemDisplayed[obj].item);
            }
            Destroy(mouseItem.obj);
            mouseItem.item = null;
        }
        public void OnDrag(GameObject obj)
        {
            if(mouseItem.obj !=null)
            {
                mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
            }
        }

        public Vector3 GetPosition(int i) => new Vector3(X_Start + (X_Space_Between_Item * (i % Number_Of_Column)),
                                                        Y_Start + (-Y_Space_Between_Item * (i / Number_Of_Column)), 0f);
    }
    public class MouseItem
    {
        public GameObject obj;
        public InventorySlot item;
        public InventorySlot hoverItem;
        public GameObject hoverObj;
    }
}


