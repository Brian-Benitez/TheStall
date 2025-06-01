using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Players List")]
    public List<GameObject> PlayerInventory;
    public int InventoryIndex = 0;

    public Inventory InventoryRef;

    public Transform VisualFakeObjectTransform;
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            PlayerInventory[InventoryIndex].gameObject.SetActive(false);
            InventoryIndex = 0;
        }
        if(Input.GetKeyUp(KeyCode.Alpha2))
        {
            PlayerInventory[InventoryIndex].gameObject.SetActive(false);
            InventoryIndex = 1;
        }
        if( Input.GetKeyUp(KeyCode.Alpha3))
        {
            PlayerInventory[InventoryIndex].gameObject.SetActive(false);
            InventoryIndex = 2;
        }
        if(Input.GetKeyUp(KeyCode.Alpha4))
        {

        }

        ChangeItemVisually();
    }

    public void ChangeItemVisually()
    {
        
        if (PlayerInventory.Count == 0)
            return;

        if (InventoryIndex >= PlayerInventory.Count)
            InventoryIndex--;
        else
        {
            PlayerInventory[InventoryIndex].gameObject.SetActive(true);
            PlayerInventory[InventoryIndex].gameObject.transform.position = VisualFakeObjectTransform.position;
        }

        
    }

    public void ClearPlayerlist() => PlayerInventory.Clear();
}
