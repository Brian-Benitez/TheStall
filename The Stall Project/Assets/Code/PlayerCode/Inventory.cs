using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Players List")]
    public List<GameObject> PlayerInventory;
    public int InventoryIndex = 0;

    public void ChangeItemVisually()
    {
        if(PlayerInventory.Count == 0)
            return;
        else
        {
            PlayerInventory[InventoryIndex].gameObject.SetActive(true);
        }
            
    }

    public void ClearPlayerlist() => PlayerInventory.Clear();
}
