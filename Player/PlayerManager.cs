using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpHeight = 2f;
    public float gravity = 9.81f;

    [Header("Statistics")]
    public float health = 100f;
    public float mana = 100f;
    public float maxHealth = 100f;
    public float maxMana = 100f;
    public float hpRegenAmount = 1f;
    public float manaRegenAmount = 2f;
    public float hpRegenCooldown = 0.4f;
    public float manaRegenCooldown = 0.4f;

    [SerializeField] Slider healthBar;
    [SerializeField] Slider manaBar;

    [Header("Attack")]
    public float damageAmount = 20f;
    public int critRate = 50;
    public float critDamage = 50f;
    public float attackCooldown = 0.5f;
    public float attackManaCost = 10f;

    [field: NonSerialized] public bool playerIsDead = false;

    bool hpRegen = true;
    bool manaRegen = true;
    bool attack = true;
    float verticalVelocity;
    Vector3 movementVector;

    Camera cam;
    CharacterController controller;

    PlayerLogic LogicScript = new PlayerLogic();
    IMathService MathService = new MathService();

    private void Update()
    {
        
        if (playerIsDead == false)
        {

            Controls();
            RotationCheck();
            PlayerAttack();

        }

    }

    private void LateUpdate()
    {

        VarCheck();

    }

    void Controls()
    {

        if (controller != null)
        {

            verticalVelocity += gravity * Time.deltaTime;
            movementVector = LogicScript.MovementVector(gameObject) * LogicScript.Speed();
            if (Input.GetButtonDown("Jump") && GroundCheck())
            {

                float proxy = jumpHeight * 2 * gravity;
                verticalVelocity = Mathf.Sqrt(MathService.MakeFloatPositive(proxy));
                
            }
            movementVector.y += verticalVelocity;
            controller.Move(movementVector * Time.deltaTime);

        }
        else
        {

            controller = gameObject.GetComponent<CharacterController>();

        }

    }

    bool GroundCheck()
    {

        if (Physics.Raycast(gameObject.transform.localPosition, gameObject.transform.up * -1.08f, 1.08f, LayerMask.GetMask("Ground")))
        {

            return true;

        }
        else
        {

            return false;

        }

    }

    void RotationCheck()
    {

        if (cam != null)
        {

            Quaternion camRotation = new Quaternion(0f, cam.transform.rotation.y, 0f, cam.transform.rotation.w);
            gameObject.transform.rotation = camRotation;

        }
        else
        {

            cam = Camera.main;

        }

    }

    public void PlayerTakesDamage(float damageAmount, float damagePercent)
    {

        if (damageAmount != 0f)
        {

            health -= MathService.MakeFloatPositive(damageAmount);

            if (LogicScript.IsPlayerDead(health))
            {

                health = 0f;
                PlayerDied();

            }

        }

        if (damagePercent != 0f)
        {

            float percent = damagePercent * 0.01f;
            health -= maxHealth * percent;

            if (LogicScript.IsPlayerDead(health))
            {

                health = 0f;
                PlayerDied();

            }

        }

        StopAllCoroutines();
        StartCoroutine(ResetHPRegen());

        if (attack == false)
        {

            StartCoroutine(ResetManaRegen());
            StartCoroutine(AttackCooldown(0f));

        }

    }

    public void PlayerReceivesHealing(float healingAmount, float healingPercent)
    {

        if (healingAmount != 0)
        {

            health += MathService.MakeFloatPositive(healingAmount);

        }

        if (healingPercent != 0)
        {

            health += MathService.MakeFloatPositive(maxHealth * (healingPercent * 0.01f));

        }

    }

    void PlayerDied()
    {

        PlayerCamScript camScript = cam.GetComponent<PlayerCamScript>();
        DeathScreen deathScreen = GameObject.FindGameObjectWithTag("UIManager").GetComponent<DeathScreen>();
        playerIsDead = true;
        camScript.disableCam = true;
        deathScreen.PlayerDiedVoid(2f);
        StopAllCoroutines();

    }

    void PlayerAttack()
    {

        if (Input.GetButtonDown("Fire1") && attack)
        {

            StopAllCoroutines();
            StartCoroutine(ResetManaRegen());
            StartCoroutine(AttackCooldown(attackCooldown));

            mana -= attackManaCost;

            if (hpRegen == false)
            {

                StartCoroutine(ResetHPRegen());

            }

            if (Physics.Raycast(cam.transform.localPosition, cam.transform.forward, out RaycastHit hit, 200f))
            {

                if (hit.collider.tag == "Enemy")
                {

                    NormEnemyBehavior enemyScript = hit.collider.GetComponent<NormEnemyBehavior>();
                    
                    if (LogicScript.CriticalCheck())
                    {

                        float critDamMultiplier = (critDamage + 100f) * 0.01f;
                        enemyScript.DamageReceived(damageAmount * critDamMultiplier);

                    }
                    else
                    {

                        enemyScript.DamageReceived(damageAmount);

                    }

                }

            }

            Debug.DrawRay(cam.transform.localPosition, cam.transform.forward * 200f, Color.blue, 2f);

        }

    }

    IEnumerator AttackCooldown(float waitTime)
    {

        attack = false;
        yield return new WaitForSecondsRealtime(waitTime);
        attack = true;

    }

    void VarCheck()
    {

        if (health >= maxHealth)
        {

            health = maxHealth;

        }
        if (mana >= maxMana)
        {

            mana = maxMana;

        }

        if (healthBar != null && manaBar != null)
        {

            healthBar.value = health;
            manaBar.value = mana;

            LogicScript.walkSpeed = walkSpeed;
            LogicScript.runSpeed = runSpeed;
            
            if (critRate <= 0)
            {

                LogicScript.critRate = 0;

            }
            else
            {

                LogicScript.critRate = critRate;

            }

        }
        else
        {

            healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
            manaBar = GameObject.FindGameObjectWithTag("ManaBar").GetComponent<Slider>();

        }

        if (hpRegen)
        {

            StartCoroutine(HpRegen());

        }

        if (manaRegen)
        {

            StartCoroutine(ManaRegen());

        }

    }

    IEnumerator HpRegen()
    {

        hpRegen = false;

        if (health < maxHealth)
        {

            health += hpRegenAmount;

        }
        else
        {

            health = maxHealth;

        }

        yield return new WaitForSecondsRealtime(hpRegenCooldown);
        hpRegen = true;

    }

    IEnumerator ResetHPRegen()
    {

        yield return new WaitForSecondsRealtime(2f);
        hpRegen = true;

    }

    IEnumerator ManaRegen()
    {

        manaRegen = false;

        if (mana < maxMana)
        {

            mana += manaRegenAmount;

        }
        else
        {

            mana = maxMana;

        }

        yield return new WaitForSecondsRealtime(hpRegenCooldown);
        manaRegen = true;

    }

    IEnumerator ResetManaRegen()
    {

        yield return new WaitForSecondsRealtime(2f);
        manaRegen = true;

    }

    public void SetConfigParameters(Vector3 position, Quaternion rotation)
    {

        gameObject.transform.SetPositionAndRotation(position, rotation);

    }

}

[Serializable]
public class PlayerLogic
{

    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public int critRate = 50;

    public Vector3 MovementVector(GameObject bean)
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 proxy = bean.transform.right.normalized * x + bean.transform.forward.normalized * z;
        return proxy;

    }

    public float Speed()
    {

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = isRunning ? runSpeed : walkSpeed;
        return speed;

    }

    public bool IsPlayerDead(float health)
    {

        if (health <= 0f)
        {

            return true;

        }
        else
        {

            return false;

        }

    }

    public bool CriticalCheck()
    {

        int random = UnityEngine.Random.Range(0, 101);

        if (random <= critRate)
        {

            return true;

        }
        else
        {

            return false;

        }

    }

}
