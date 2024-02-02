using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingleTon<InventoryManager>
{
    public InventoryPanel inventoryPanel;
    [SerializeField] 
    private GameObject inventoryGroup;
    [SerializeField] 
    private ItemData[] initItems;
    [SerializeField] 
    private InventorySlot[] inventorySlots;
    [SerializeField] 
    private GameObject inventoryItemPrefab;
    [SerializeField] 
    private int maxStackedItem = 99;

    private int selectedSlot = -1;
    private int slotCounts = 24;
    private List<GameObject> itemInSlotList = new List<GameObject>();

    private void Start()
    {
        foreach (var item in initItems)
        {
            AddItem(item);
        }
    }

    public bool AddItem(ItemData item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (null != itemInSlot && itemInSlot.item == item &&
                itemInSlot.count < maxStackedItem && true == itemInSlot.item.isStackable)
            {
                itemInSlot.count++;
                itemInSlot.UpdateCount();
                return true;
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (null == itemInSlot)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    private void SpawnNewItem(ItemData item, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitItem(item);
        itemInSlotList.Add(newItem);
    }

    public void SellSelectedItem()
    {
        if (selectedSlot >= 0)
        {
            InventorySlot slot = inventorySlots[selectedSlot];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (null != itemInSlot)
            {
                if (true == itemInSlot.item.isStackable)
                {
                    itemInSlot.count--;

                    if (itemInSlot.count <= 0)
                    {
                        Destroy(itemInSlot.gameObject);
                        selectedSlot = -1;
                    }
                    else
                    {
                        itemInSlot.UpdateCount();
                    }
                }
                else
                {
                    Destroy(itemInSlot.gameObject);
                }

                ShopManager.Instance.coins += itemInSlot.item.itemCost;
                ShopManager.Instance.UpdateCoin();
                ShopManager.Instance.UpdatePurchaseButton();
            }
        }
    }

    public ItemData GetSelectedItem(bool isUsed)
    {
        if (selectedSlot >= 0)
        {
            InventorySlot slot = inventorySlots[selectedSlot];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (null != itemInSlot)
            {
                ItemData item = itemInSlot.item;

                if (isUsed)
                {
                    itemInSlot.count--;

                    if (itemInSlot.count <= 0)
                    {
                        Destroy(itemInSlot.gameObject);
                        selectedSlot = -1;
                    }
                    else
                    {
                        itemInSlot.UpdateCount();
                    }

                    return item;
                }
                return null;
            }
        }
        return null;
    }

    public bool HasEmptySlot()
    {
        return itemInSlotList.Count != slotCounts ? true : false;
    }

    public void ChangeSelectedSlot(int value)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].NotSelected();
        }

        inventorySlots[value].Selected();
        selectedSlot = value;
    }

    public void OnClickInventoryButton()
    {
        inventoryGroup.SetActive(!inventoryGroup.activeSelf);
        GameManager.Instance.isOpenInventory = inventoryGroup.activeSelf;
    }
}
