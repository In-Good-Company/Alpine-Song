using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemID : MonoBehaviour
{
    public Item _item;

    public void SetActiveItem()
    {
        Inventory.instance.activeItem = _item;
        //Sprite _activeItemImage = Inventory.instance.activeItemImage.GetComponent<Sprite>();
        //_activeItemImage = _item.icon;
        Inventory.instance.SetActiveItemIcon(_item.icon);
        //Inventory.instance.activeItemIcon = _item.icon;
    }
}
