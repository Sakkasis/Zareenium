using System;
using System.Collections;
using UnityEngine;

public class PlayerDamagerWeapon : MonoBehaviour
{

    GameObject bean;
    PlayerManager playerStatManager;

    [SerializeField] float damageAmount;
    [SerializeField] float damageCooldownT;

    bool damageCooldownB = false;

    private void Update()
    {

        bean = GameObject.FindGameObjectWithTag("bean");
        playerStatManager = bean.GetComponent<PlayerManager>();

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
        playerStatManager.PlayerTakesDamage(damageAmount, 0f);
        yield return new WaitForSecondsRealtime(damageCooldownT);
        damageCooldownB = false;

    }

}
