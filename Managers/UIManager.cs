using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    CanvasManager canvasScript;
    PlayerCamScript camScript;

    private void Start()
    {

        canvasScript = gameObject.GetComponent<CanvasManager>();

    }

    private void Update()
    {

        if (camScript == null)
        {

            camScript = Camera.main.GetComponent<PlayerCamScript>();

        }

        if (Input.GetButtonDown("Settings"))
        {

            canvasScript.Settings();

        }
        
        if (Input.GetButtonDown("Inventory"))
        {

            canvasScript.Inventory();

        }

    }

}
