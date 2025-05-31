using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> PlayerInventory;


    public void ClearPlayerlist() => PlayerInventory.Clear();
}
