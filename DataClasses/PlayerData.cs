using System;

[Serializable]
public class PlayerData
{

    public float walkSpeed;
    public float runSpeed;
    public float jumpHeight;
    public float gravity;
    public float health;
    public float mana;
    public float maxHealth;
    public float maxMana;
    public float hpRegenAmount;
    public float manaRegenAmount;
    public float hpRegenCooldown;
    public float manaRegenCooldown;
    public float damageAmount;
    public int critRate;
    public float critDamage;
    public float attackCooldown;
    public float attackManaCost;

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

}
