using System;
using System.Collections;
using UnityEngine;

public class DoorDetector : MonoBehaviour
{

    DoorOpenClose doorOpenCloseScript;

    private void Start()
    {

        doorOpenCloseScript = gameObject.GetComponentInParent<DoorOpenClose>();

    }

    private void OnTriggerEnter(Collider other)
    {

        doorOpenCloseScript.OpenDoorVoid(other);

    }

    private void OnTriggerExit(Collider other)
    {

        doorOpenCloseScript.CloseDoorVoid(other);

    }

}
