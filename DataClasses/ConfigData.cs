using System;
using System.Collections.Generic;

[Serializable]
public class ConfigData
{

    public bool ConfigManager;

    // <Player data>
    public float pWalkSpeed;
    public float pRunSpeed;
    public float pJumpHeight;
    public float pGravity;
    public float pHealth;
    public float pMana;
    public float pMaxHealth;
    public float pMaxMana;
    public float pHpRegenAmount;
    public float pManaRegenAmount;
    public float pHpRegenCooldown;
    public float pManaRegenCooldown;
    public float pDamageAmount;
    public int pCritRate;
    public float pCritDamage;
    public float pAttackCooldown;
    public float pAttackManaCost;

    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;

    public float playerRotationX;
    public float playerRotationY;
    public float playerRotationZ;

    public float camPositionX;
    public float camPositionY;
    public float camPositionZ;

    public float camRotationX;
    public float camRotationY;
    public float camRotationZ;
    // </Player data>

    // <Enemy data>
    public List<float> eHealth = new List<float>();
    public List<float> eMaxHealth = new List<float>();
    public List<float> eMana = new List<float>();
    public List<float> eMaxMana = new List<float>();
    public List<bool> eDoesAIUseMagic = new List<bool>();
    public List<bool> eDoesAIPatrol = new List<bool>();
    public List<int> ePatrolRouteInt = new List<int>();
    public List<int> ePatrolPointInt = new List<int>();
    public List<float> ePatrolWait = new List<float>();

    public List<float> enemyPositionX = new List<float>();
    public List<float> enemyPositionY = new List<float>();
    public List<float> enemyPositionZ = new List<float>();

    public List<float> enemyRotationX = new List<float>();
    public List<float> enemyRotationY = new List<float>();
    public List<float> enemyRotationZ = new List<float>();
    // </Enemy data>

}