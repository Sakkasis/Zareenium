using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class NormEnemyBehavior : MonoBehaviour
{

    [Header("Generic Stats")]
    public float health = 100f;
    public float maxHealth = 100f;
    public float mana = 100f;
    public float maxMana = 100f;
    public bool doesAIUseMagic = false;

    [Header("Patrol Logic")]
    public bool doesAIPatrol = true;
    public int patrolRouteInt = 1;
    public int patrolPointInt = 0;
    public float patrolWaitFloat = 5f;
    public List<GameObject> patrolPointObjectsList;

    bool playerDetected = false;
    const float maxRayDistance = 20f;

    GameObject beanObject;
    NavMeshAgent cubean;
    Animator weaponAnimator;

    AILogic internalLogic = new AILogic();

    IDataClasses DataClasses = new DataClasses();

    private void Awake()
    {

        RayGrid grid = new RayGrid();
        grid.CreateGrid();

    }

    private void Start()
    {

        cubean = gameObject.GetComponent<NavMeshAgent>();

        if (patrolPointObjectsList.Count == 0)
        {

            patrolPointObjectsList = internalLogic.FindPatrolRoute(patrolRouteInt);

        }

        PatrolSetNextDestinationVoid();
        StartCoroutine(PatrolReachedDestinationIE(false));
        Raycast();

    }

    private void Update()
    {

        if (beanObject != null)
        {

            AttackAnimation();

        }
        else
        {

            beanObject = GameObject.FindGameObjectWithTag("bean");

        }

    }

    IEnumerator PatrolReachedDestinationIE(bool stoppedByDoor)
    {

        if (stoppedByDoor == false)
        {

            if (internalLogic.IsTargetInRange(patrolPointObjectsList[patrolPointInt].transform.position, gameObject))
            {

                yield return new WaitForSecondsRealtime(patrolWaitFloat);
                patrolPointInt += 1;
                PatrolSetNextDestinationVoid();
                yield return new WaitForSecondsRealtime(1f);
                StartCoroutine(ResetPatrolLoopIE(false));

            }
            else
            {

                yield return new WaitForSecondsRealtime(0.4f);
                StartCoroutine(ResetPatrolLoopIE(false));

            }

        }
        else
        {

            PatrolSetNextDestinationVoid();
            StartCoroutine(ResetPatrolLoopIE(false));

        }

    }

    IEnumerator ResetPatrolLoopIE(bool stoppedByDoor)
    {

        if (stoppedByDoor == false)
        {

            yield return new WaitForSecondsRealtime(0.4f);

            if (internalLogic.IsTargetInRange(patrolPointObjectsList[patrolPointInt].transform.position, gameObject))
            {

                StartCoroutine(PatrolReachedDestinationIE(false));

            }
            else
            {

                StartCoroutine(ResetPatrolLoopIE(false));

            }

        }
        else
        {

            StartCoroutine(PatrolReachedDestinationIE(true));

        }

    }

    void PatrolSetNextDestinationVoid()
    {

        if (patrolPointInt >= patrolPointObjectsList.Count)
        {

            patrolPointInt = 0;

        }

        cubean.SetDestination(patrolPointObjectsList[patrolPointInt].transform.position);

    }

    public void StoppedByDoorVoid(float waitTime)
    {

        StopAllCoroutines();
        Raycast();
        StartCoroutine(StoppedByDoorIE(waitTime));

    }

    IEnumerator StoppedByDoorIE(float waitTime)
    {

        cubean.isStopped = true;
        yield return new WaitForSecondsRealtime(waitTime);
        cubean.isStopped = false;
        StartCoroutine(ResetPatrolLoopIE(true));

    }

    void Raycast()
    {

        if (RaycastHitsList().Count != 0 && playerDetected == false)
        {

            playerDetected = true;
            PlayerDetectedVoid(true);

        }
        else
        {

            if (playerDetected == true)
            {

                playerDetected = false;
                StopAllCoroutines();
                StartCoroutine(FindPlayer());

            }

        }

        StartCoroutine(ResetRaycast());

    }

    IEnumerator FindPlayer()
    {

        PlayerDetectedVoid(false);
        yield return new WaitForSecondsRealtime(5f);
        PatrolSetNextDestinationVoid();
        StartCoroutine(PatrolReachedDestinationIE(false));

    }

    IEnumerator ResetRaycast()
    {

        yield return new WaitForSecondsRealtime(0.1f);
        Raycast();

    }

    void PlayerDetectedVoid(bool reset)
    {

        if (reset == true)
        {

            StopAllCoroutines();

        }

        cubean.isStopped = false;
        StartCoroutine(PlayerDetectedIE());

    }

    IEnumerator PlayerDetectedIE()
    {

        if (beanObject == null)
        {

            beanObject = GameObject.FindGameObjectWithTag("bean");

        }

        cubean.SetDestination(internalLogic.PlayerTargetingVector(gameObject, beanObject));
        yield return new WaitForSecondsRealtime(0.2f);
        Raycast();

    }

    List<RaycastHit> RaycastHitsList()
    {

        GridData data = DataClasses.GridDataClass();
        List<RaycastHit> proxy = new List<RaycastHit>();
        LayerMask target = LayerMask.GetMask("Ignore Raycast", "Player", "Ground");

        for (int iV = 0; iV < data.gridY.Count; iV++)
        {

            for (int iH = 0; iH < data.gridX.Count; iH++)
            {

                if (Physics.Raycast(OriginVector(iV, iH), DirectionVector(0f, data.gridX[iV], data.gridX[iH], data.gridY[iV], -20f) * maxRayDistance, out RaycastHit hit, maxRayDistance, target))
                {

                    if (hit.collider.tag == "bean")
                    {

                        proxy.Add(hit);
                        Debug.DrawRay(OriginVector(iV, iH), DirectionVector(0f, data.gridX[iV], data.gridX[iH], data.gridY[iV], -20f) * maxRayDistance, Color.green, 0.1f);

                    }
                    else
                    {

                        Debug.DrawRay(OriginVector(iV, iH), DirectionVector(0f, data.gridX[iV], data.gridX[iH], data.gridY[iV], -20f) * maxRayDistance, Color.red, 0.1f);

                    }

                }
                else
                {

                    Debug.DrawRay(OriginVector(iV, iH), DirectionVector(0f, data.gridX[iV], data.gridX[iH], data.gridY[iV], -20f) * maxRayDistance, Color.red, 0.1f);

                }

            }

        }

        return proxy;

    }

    Vector3 OriginVector(int iV, int iH)
    {

        GridData data = DataClasses.GridDataClass();
        Vector3 proxy = gameObject.transform.localPosition + gameObject.transform.forward * 0.6f;

        proxy.x += data.gridX[iH] * 0.5f;
        proxy.y += data.gridY[iV] * 0.5f;
        proxy.z += 0f;

        return proxy;

    }

    Vector3 DirectionVector(float xOffset, float yOffsetI, float yOffsetII, float zOffset, float multiplier)
    {

        Quaternion rotation = Quaternion.Euler(xOffset * multiplier, (yOffsetI * multiplier) + (yOffsetII * 40f), zOffset * 2f);
        Vector3 direction = rotation * gameObject.transform.forward;
        return direction;

    }

    void AttackAnimation()
    {

        float distance = Vector3.Distance(beanObject.transform.localPosition, gameObject.transform.localPosition);

        if (weaponAnimator == null)
        {

            weaponAnimator = gameObject.GetComponentInChildren<Animator>();

        }

        if (distance <= 2f)
        {

            if (Physics.Raycast(gameObject.transform.localPosition, gameObject.transform.forward * 1.4f, out RaycastHit hit, 1.4f, LayerMask.GetMask("Player")))
            {

                weaponAnimator.SetBool("attackAnimBool", true);

            }
            else
            {

                weaponAnimator.SetBool("attackAnimBool", false);

            }

            Debug.DrawRay(gameObject.transform.localPosition, gameObject.transform.forward * 1.4f, Color.yellow, 0f);

        }
        else
        {

            weaponAnimator.SetBool("attackAnimBool", false);

        }

    }

    public void DamageReceived(float damageAmount)
    {

        health -= damageAmount;

        if (health <= 0f)
        {

            Destroy(gameObject);

        }

    }

    public void SetConfigParameters(Vector3 position, Quaternion rotation)
    {

        gameObject.transform.SetPositionAndRotation(position, rotation);

    }

}

public class AILogic
{

    public List<GameObject> FindPatrolRoute(int patrolRouteIndex)
    {

        List<GameObject> patrolRouteObjects = new List<GameObject>();
        patrolRouteObjects.AddRange(GameObject.FindGameObjectsWithTag(PatrolRouteTag(patrolRouteIndex)));
        return SortPatrolPointList(patrolRouteObjects);

    }

    private string PatrolRouteTag(int patrolRouteIndex)
    {

        if (patrolRouteIndex == 1)
        {

            return "PatrolRoute-I";

        }
        else if (patrolRouteIndex == 2)
        {

            return "PatrolRoute-II";

        }
        else if (patrolRouteIndex == 3)
        {

            return "PatrolRoute-III";

        }
        else if (patrolRouteIndex == 4)
        {

            return "PatrolRoute-IV";

        }
        else if (patrolRouteIndex == 5)
        {

            return "PatrolRoute-V";

        }
        else if (patrolRouteIndex == 6)
        {

            return "PatrolRoute-VI";

        }
        else if (patrolRouteIndex == 7)
        {

            return "PatrolRoute-VII";

        }
        else if (patrolRouteIndex == 8)
        {

            return "PatrolRoute-VIII";

        }
        else if (patrolRouteIndex == 9)
        {

            return "PatrolRoute-IX";

        }
        else if (patrolRouteIndex == 10)
        {

            return "PatrolRoute-X";

        }
        else if (patrolRouteIndex == 11)
        {

            return "PatrolRoute-XI";

        }
        else if (patrolRouteIndex == 12)
        {

            return "PatrolRoute-XII";

        }
        else if (patrolRouteIndex == 13)
        {

            return "PatrolRoute-XIII";

        }
        else if (patrolRouteIndex == 14)
        {

            return "PatrolRoute-XIV";

        }
        else if (patrolRouteIndex == 15)
        {

            return "PatrolRoute-XV";

        }
        else if (patrolRouteIndex == 16)
        {

            return "PatrolRoute-XVI";

        }
        else if (patrolRouteIndex == 17)
        {

            return "PatrolRoute-XVII";

        }
        else if (patrolRouteIndex == 18)
        {

            return "PatrolRoute-XVIII";

        }
        else if (patrolRouteIndex == 19)
        {

            return "PatrolRoute-XIX";

        }
        else if (patrolRouteIndex == 20)
        {

            return "PatrolRoute-XX";

        }
        else
        {

            Debug.LogError("Index out of range! PatrolRouteTag in AILogic; NormEnemyBehaviour");
            return "PatrolRoute-I";

        }

    }

    public List<GameObject> SortPatrolPointList(List<GameObject> patrolPointList)
    {

        patrolPointList = patrolPointList.OrderBy(go => ExtractNumberFromString(go.name)).ToList();
        return patrolPointList;

    }

    private int ExtractNumberFromString(string name)
    {

        string number = new string(name.Where(char.IsDigit).ToArray());
        return int.TryParse(number, out int result) ? result : int.MaxValue;

    }

    public bool IsTargetInRange(Vector3 target, GameObject cubean)
    {

        IMathService mathService = new MathService();

        bool min = false;
        bool max = false;

        Vector3 aiPosition;
        aiPosition.x = cubean.transform.position.x;
        aiPosition.y = cubean.transform.position.y;
        aiPosition.z = cubean.transform.position.z;

        aiPosition = mathService.MakeVectorPositive(aiPosition);

        Vector3 targetMinVector;
        targetMinVector.x = target.x;
        targetMinVector.y = cubean.transform.position.y;
        targetMinVector.z = target.z;

        targetMinVector = mathService.MakeVectorPositive(targetMinVector);

        targetMinVector.x -= 0.1f;
        targetMinVector.z -= 0.1f;

        Vector3 targetMaxVector;
        targetMaxVector.x = target.x;
        targetMaxVector.y = cubean.transform.position.y;
        targetMaxVector.z = target.z;

        targetMaxVector = mathService.MakeVectorPositive(targetMaxVector);

        targetMaxVector.x += 0.1f;
        targetMaxVector.z += 0.1f;

        if (aiPosition.x >= targetMinVector.x && aiPosition.z >= targetMinVector.z)
        {

            min = true;

        }
        else
        {

            min = false;

        }

        if (aiPosition.x <= targetMaxVector.x && aiPosition.z <= targetMaxVector.z)
        {

            max = true;

        }
        else
        {

            max = false;

        }

        if (min == true && max == true)
        {

            return true;

        }
        else
        {

            return false;

        }

    }

    public Vector3 PlayerTargetingVector(GameObject cubean, GameObject bean)
    {

        Vector3 beanVector = bean.transform.localPosition;
        Vector3 target = beanVector + cubean.transform.forward * -1.3f;
        return target;

    }

}
