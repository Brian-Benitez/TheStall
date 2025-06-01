using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Inventory InventoryRef;
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            InventoryRef.InventoryIndex = 0;
            InventoryRef.ChangeItemVisually();
        }
        if(Input.GetKeyUp(KeyCode.Alpha2))
        {
            InventoryRef.InventoryIndex = 1;
            InventoryRef.ChangeItemVisually();
        }
        if( Input.GetKeyUp(KeyCode.Alpha3))
        {

        }
        if(Input.GetKeyUp(KeyCode.Alpha4))
        {

        }
           


    }
}
