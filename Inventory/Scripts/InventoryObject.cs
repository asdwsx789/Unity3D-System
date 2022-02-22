using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;
//Formatters 格式化

namespace Inventory
{
    [CreateAssetMenu(fileName = "New Inventory", menuName ="Inventory System/Inventory")]
    public class InventoryObject : ScriptableObject
    {
        public string savePath;
        public ItemDataBaseObject database;
        public Inventory Container;
        

        private void OnEnable()
        {
#if UNITY_EDITOR
            database = (ItemDataBaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDataBaseObject));
#else
            database = Resources.Load<ItemDataBaseObject>("Database.asset");
            Debug.Log("2");
#endif
        }

        public void AddItem(Item _item, int _amount)
        {
            //if(_item.buffs.Length > 0)
            //{
            //    Container.Items.Add(new InventorySlot(_item.Id, _item, _amount));
            //    return;
            //}

            //for(int i = 0; i < Container.Items.Count; i++)
            //{
            //    if(Container.Items[i].item.Id == _item.Id)
            //    {
            //        Container.Items[i].Addamount(_amount);
            //        return;
            //    }
            //}
            //Container.Items.Add(new InventorySlot(_item.Id, _item, _amount));

            if(_item.buffs.Length > 0)
            {
                SetEmptySlot(_item, _amount);
                return;
            }

            for (int i = 0; i < Container.Items.Length; i++)
            {
                if(Container.Items[i].ID == _item.Id)
                {
                    Container.Items[i].Addamount(_amount);
                    return;
                }
            }

            SetEmptySlot(_item, _amount);
        }

        public InventorySlot SetEmptySlot(Item _item, int _amount)
        {
            for (int i = 0; i < Container.Items.Length; i++)
            {
                if(Container.Items[i].ID <= -1)
                {
                    Container.Items[i].UpdataSlot(_item.Id, _item, _amount);
                    return Container.Items[i];
                }
            }

            return null;
        }

        public void MoveItem(InventorySlot item1, InventorySlot item2)
        {
            InventorySlot temp = new InventorySlot(item2.ID, item2.item, item2.amount);
            item2.UpdataSlot(item1.ID, item1.item, item1.amount);
            item1.UpdataSlot(temp.ID, temp.item, temp.amount);
        }

        public void RemoveItem(Item _item)
        {
            for (int i = 0; i < Container.Items.Length; i++)
            {
                if(Container.Items[i].item == _item)
                {
                    Container.Items[i].UpdataSlot(-1, null, 0);
                }
            }
        }

        [ContextMenu("Save")]
        public void Save()
        {
            /*
            string saveData = JsonUtility.ToJson(this, true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
            bf.Serialize(file, saveData);
            file.Close();
            */

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, Container);
            stream.Close();
        }

        [ContextMenu("Load")]
        public void Load()
        { 
            if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
            {
                /*
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
                file.Close();
                */

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
                Inventory NewContainer = (Inventory)formatter.Deserialize(stream);
                for (int i = 0; i < Container.Items.Length; i++)
                {
                    Container.Items[i].UpdataSlot(NewContainer.Items[i].ID, NewContainer.Items[i].item, NewContainer.Items[i].amount);
                }
                stream.Close();
            }
        }

        [ContextMenu("Claer")]
        public void Clear()
        {
            Container = new Inventory();
        }

        /*
        public void OnAfterDeserialize()
        {
            for (int i = 0; i < Container.Items.Count; i++)
            {
                Container.Items[i].item = database.GetItem[Container.Items[i].ID];
            }
        }

        public void OnBeforeSerialize()
        {
            
        }
        */
    }

    [System.Serializable]
    public class Inventory
    {
        //public List<InventorySlot> Items = new List<InventorySlot>();
        public InventorySlot[] Items = new InventorySlot[24];
    }

    [System.Serializable]
    public class InventorySlot
    {
        public int ID;
        public Item item;
        public int amount;

        public InventorySlot()
        {
            ID = -1;
            item = null;
            amount = 0;
        }

        public InventorySlot(int _id ,Item _item, int _amount)
        {
            ID = _id;
            item = _item;
            amount = _amount;
        }

        public void UpdataSlot(int _id, Item _item, int _amount)
        {
            ID = _id;
            item = _item;
            amount = _amount;
        }

        public void Addamount(int value)
        {
            amount += value;
        }
    }
}

