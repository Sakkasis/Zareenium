using System;
using UnityEngine;

public class PlayerCamScript : MonoBehaviour
{

    [Header("General")]
    public float xMouseSensitivity = 6f;
    public float yMouseSensitivity = 6f;

    [field: NonSerialized] public bool disableCam = false;

    Vector2 currentRotation;
    GameObject beanObject;

    CamLogic logicScript = new CamLogic();

    void Update()
    {

        if (beanObject != null)
        {

            if (disableCam == false)
            {

                CameraFollow();
                CameraRotation();
                Cursor.lockState = CursorLockMode.Locked;

            }
            else
            {

                Cursor.lockState = CursorLockMode.None;

            }

        }
        else
        {

            beanObject = GameObject.FindGameObjectWithTag("bean");

        }

    }

    void CameraFollow()
    {

        Vector3 followPosition = new Vector3(beanObject.transform.position.x, beanObject.transform.position.y + 0.65f, beanObject.transform.position.z);
        gameObject.transform.position = followPosition;

    }

    void CameraRotation()
    {

        currentRotation = logicScript.CamRotation(currentRotation, xMouseSensitivity, yMouseSensitivity);
        gameObject.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0).normalized;

    }

    public void SetConfigParameters(Vector3 position, Quaternion rotation)
    {

        gameObject.transform.SetPositionAndRotation(position, rotation);

    }

}

[Serializable]
public class CamLogic
{

    const float maxYAngle = 80f;

    public Vector2 CamRotation(Vector2 currentRotation, float xMouseSensitivity, float yMouseSensitivity)
    {

        Vector2 proxy = currentRotation;
        proxy.x += Input.GetAxis("Mouse X") * xMouseSensitivity;
        proxy.y -= Input.GetAxis("Mouse Y") * yMouseSensitivity;
        proxy.x = Mathf.Repeat(proxy.x, 360);
        proxy.y = Mathf.Clamp(proxy.y, -maxYAngle, maxYAngle);

        return proxy;

    }

}
