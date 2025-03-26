using System;
using System.Collections;
using UnityEngine;

public class PlayerDamagerEnviornment : MonoBehaviour
{

    GameObject bean;
    PlayerManager playerStatManager;

    [SerializeField] float damagePercent;
    [SerializeField] float damageCooldownT;

    bool damageCooldownB = false;

    private void LateUpdate()
    {
        
        if (bean == null)
        {

            bean = GameObject.FindGameObjectWithTag("bean");
            playerStatManager = bean.GetComponent<PlayerManager>();

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "bean" && damageCooldownB == false)
        {

            StartCoroutine(DamagePlayer());

        }

    }

    IEnumerator DamagePlayer()
    {

        damageCooldownB = true;
        playerStatManager.PlayerTakesDamage(damagePercent, 0f);
        yield return new WaitForSecondsRealtime(damageCooldownT);
        damageCooldownB = false;

    }

}
