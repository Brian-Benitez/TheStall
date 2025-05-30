using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("Values")]
    public float SensX;
    public float SensY;

    [Header("Transforms")]
    public Transform PlayerBody;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * SensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * SensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        //rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        //PlayerBody.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        //NOTE: ^This works BUT, it rotates the WHOLE player... so maybe just rotate something else or figure it out..
        //PlayerBody.Rotate(Vector3.up * mouseX);
    }
}
