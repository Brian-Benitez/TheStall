using interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [Header("Floats")]
    public float MaxRayCastDistance = 5f;

    private void Update()
    {
        //player input
        Ray ray = new Ray(transform.position, transform.forward);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.DrawLine(transform.position, transform.forward);
            Debug.Log("hit hit " + transform.name);
            if (Physics.Raycast(ray, out RaycastHit hit, MaxRayCastDistance))
            {
                if (hit.transform.TryGetComponent<Interact>(out Interact interactable))
                {
                    interactable.PickUp();
                }
            }
        }
    }
}
