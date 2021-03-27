﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public GameObject inventoryPanel;

    public GameObject itemSlot;

    public GameObject itemSlotParent;

    public bool inventoryOpen;

    public Item activeItem;

    public GameObject activeItemImage;

    public Sprite inventoryDefaultSprite;

    public Image activeItemIcon;

    public GameObject itemToDelete;

    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        inventoryPanel.SetActive(false);

        if(instance != null)
        {
            Debug.LogWarning("More than one inventory found");
        }
        instance = this;
    }
    #endregion

    private void Start()
    {
        activeItemIcon = activeItemImage.GetComponent<Image>();
        activeItemIcon.sprite = inventoryDefaultSprite;
    }

    public void SetActiveItemIcon(Sprite _sprite)
    {
        //activeItemIcon = activeItemImage.GetComponent<Image>();
        activeItemIcon.sprite = _sprite;
    }

    public void InventoryToggle()
    {
        inventoryOpen = !inventoryOpen;
        inventoryPanel.SetActive(inventoryOpen);
    }


    public List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
        GameObject newSlot = Instantiate(itemSlot, itemSlotParent.transform);
        newSlot.name = item.name;
        Text textC = newSlot.GetComponentInChildren<Text>();
        textC.text = item.name;
        Debug.Log (item.name);
        Image itemSprite = newSlot.GetComponent<Image>();
        itemSprite.sprite = item.icon;
        ItemID ID = newSlot.GetComponent<ItemID>();
        ID._item = item;

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    public void RemoveItem(Item item)
    {
        activeItemIcon.sprite = inventoryDefaultSprite;
        Destroy(itemToDelete);
        items.Remove(item);
        //activeItem = null;

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
        Debug.Log(activeItem.name + " removed");
    }


    //private void Update()
    //{
    //    if (Input.GetButtonDown("InventoryToggle"))
    //    {
    //        if (inventoryOpen == false)
    //        {
    //            OpenInventory();
    //        }
    //        else if (inventoryOpen == true)
    //        {
    //            CloseInventory();
    //        }
    //    }
    //
    //    if (Input.GetButtonDown("Cancel"))
    //    {
    //        activeItem = null;
    //        activeItemIcon.sprite = inventoryDefaultSprite;
    //    }
    //}
}
