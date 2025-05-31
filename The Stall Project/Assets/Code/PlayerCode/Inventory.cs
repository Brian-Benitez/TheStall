using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Players List")]
    public List<GameObject> PlayerInventory;

    public void ScollUpInventory()
    {
        if(PlayerInventory.Count == 0)
            return;
        else
        {
            Rigidbody rb = PlayerInventory[0].GetComponent<Rigidbody>();
            rb.useGravity = false;
            PlayerInventory[0].gameObject.SetActive(true);

        }
            
    }

    public void ClearPlayerlist() => PlayerInventory.Clear();
}
