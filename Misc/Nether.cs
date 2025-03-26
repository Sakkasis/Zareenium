using System;
using UnityEngine;

public class Nether : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "bean")
        {

            PlayerManager playerScript = other.GetComponent<PlayerManager>();
            playerScript.health = 0f;

        }
        else
        {

            GameObject.Destroy(other);

        }

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "bean")
        {

            PlayerManager playerScript = other.GetComponent<PlayerManager>();
            playerScript.health = 0f;

        }
        else
        {

            GameObject.Destroy(other);

        }

    }

}
