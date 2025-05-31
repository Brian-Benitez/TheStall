using interfaces;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [Header("Floats")]
    public GameObject player;
    public Transform holdPos;
    //if you copy from below this point, you are legally required to like the video
    public float ThrowForce = 500f; //force at which the object is thrown at
    public float PickUpRange = 5f; //how far the player can pickup the object from
    private float _rotationSensitivity = 1f; //how fast/slow the object is rotated in relation to mouse movement
    private GameObject _heldObj; //object which we pick up
    private Rigidbody _heldObjRb; //rigidbody of object we pick up
    private bool _canDrop = true;
    private bool _hasObjectInHand = false;//this is needed so we don't throw/drop object when rotating the object
                                  //private int LayerNumber; //layer index
                                  //Reference to script which includes mouse movement of player (looking around)
                                  //we want to disable the player looking around when rotating the object
                                  //example below 
                                  //MouseLookScript mouseLookScript;

    public Inventory InventoryRef;
    void Start()
    {
       // LayerNumber = LayerMask.NameToLayer("holdLayer"); //if your holdLayer is named differently make sure to change this ""

        //mouseLookScript = player.GetComponent<MouseLookScript>();
    }
    void Update()
    {
        if (_hasObjectInHand)
            Debug.Log("Objects in your hands!");
        else
        {
            //player input
            Ray ray = new Ray(transform.position, transform.forward);
            if (Input.GetMouseButtonDown(0))
            {
                Debug.DrawLine(transform.position, transform.forward);
                Debug.Log("hit hit " + transform.name);
                if (Physics.Raycast(ray, out RaycastHit hit, PickUpRange))
                {
                    if (hit.transform.TryGetComponent<Interact>(out Interact interactable))
                    {
                        interactable.PickUp();
                    }
                }
            }
        }
        

        if (Input.GetKeyUp(KeyCode.E)) //change E to whichever key you want to press to pick up
        {
            Debug.Log("hello");
            if (_heldObj == null) //if currently not holding anything
            {
                if (InventoryRef.PlayerInventory == null)
                    Debug.Log("Player does not have anything");
                //make sure pickup tag is attached 
                else if (InventoryRef.PlayerInventory[0].transform.gameObject.tag == "canPickUp")
                {
                    _hasObjectInHand = true;
                    InventoryRef.PlayerInventory[0].SetActive(true);
                    //pass in object hit into the PickUpObject function
                    PickUpObject(InventoryRef.PlayerInventory[0].transform.gameObject);
                    Debug.Log("pick up obecjt");
                }

            }
            else
            {
                if (_canDrop == true)
                {
                    StopClipping(); //prevents object from clipping through walls
                    DropObject();
                }
            }
        }
        if (_heldObj != null) //if player is holding object
        {
            MoveObject(); //keep object position at holdPos
            if (Input.GetKeyDown(KeyCode.Mouse0) && _canDrop == true) //Mous0 (leftclick) is used to throw, change this if you want another button to be used)
            {
                _hasObjectInHand = false;
                StopClipping();
                ThrowObject();
            }

        }
    }
    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
        {
            _heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
            _heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
            _heldObjRb.isKinematic = true;
            _heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition
            //heldObj.layer = LayerNumber; //change the object layer to the holdLayer
            //make sure object doesnt collide with player, it can cause weird bugs
            Physics.IgnoreCollision(_heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }
    void DropObject()
    {
        //re-enable collision with player
        Physics.IgnoreCollision(_heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        _heldObj.layer = 0; //object assigned back to default layer
        _heldObjRb.isKinematic = false;
        _heldObj.transform.parent = null; //unparent object
        _heldObj = null; //undefine game object
    }
    void MoveObject()
    {
        //keep object position the same as the holdPosition position
        _heldObj.transform.position = holdPos.transform.position;
    }
   
    void ThrowObject()
    {
        //same as drop function, but add force to object before undefining it
        Physics.IgnoreCollision(_heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        _heldObj.layer = 0;
        _heldObjRb.isKinematic = false;
        _heldObj.transform.parent = null;
        _heldObjRb.AddForce(transform.forward * ThrowForce);
        _heldObj = null;
    }
    void StopClipping() //function only called when dropping/throwing
    {
        var clipRange = Vector3.Distance(_heldObj.transform.position, transform.position); //distance from holdPos to the camera
        //have to use RaycastAll as object blocks raycast in center screen
        //RaycastAll returns array of all colliders hit within the cliprange
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
        if (hits.Length > 1)
        {
            //change object position to camera position 
            _heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); //offset slightly downward to stop object dropping above player 
            //if your player is small, change the -0.5f to a smaller number (in magnitude) ie: -0.1f
        }
    }
}
