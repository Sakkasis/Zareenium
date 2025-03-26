using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyData
{

    public List<float> health = new List<float>();
    public List<float> mana = new List<float>();
    public List<float> maxHealth = new List<float>();
    public List<float> maxMana = new List<float>();
    public List<bool> doesAIUseMagic = new List<bool>();
    public List<bool> doesAIPatrol = new List<bool>();
    public List<int> patrolRouteInt = new List<int>();
    public List<int> patrolPointInt = new List<int>();
    public List<float> patrolWait = new List<float>();

    public List<float> enemyPositionX = new List<float>();
    public List<float> enemyPositionY = new List<float>();
    public List<float> enemyPositionZ = new List<float>();

    public List<float> enemyRotationX = new List<float>();
    public List<float> enemyRotationY = new List<float>();
    public List<float> enemyRotationZ = new List<float>();

}
