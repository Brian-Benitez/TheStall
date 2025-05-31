using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using interfaces;

public class ToiletRoll : MonoBehaviour, Interact
{
    public Inventory InventoryRef;
    public void PickUp()
    {
        InventoryRef.PlayerInventory.Add(transform.gameObject);
        Debug.Log("Addede to inventory " + transform.name);
        transform.gameObject.SetActive(false);
    }
}
