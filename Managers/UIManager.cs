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

            if (canvasScript.IsCanvasActive(6))
            {



            }

        }
        
        if (Input.GetButtonDown("Inventory"))
        {

            canvasScript.Inventory();

        }

    }

    public void SaveBeforeExitVoid()
    {



    }

}
