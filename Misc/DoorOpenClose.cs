using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class DoorOpenClose : MonoBehaviour
{

    

    [SerializeField] float doorOpenCloseTime;
    [SerializeField] float AIWaitTime;
    [SerializeField] bool isDoorOpenableByPlayer;
    [SerializeField] GameObject doorRightObject;
    [SerializeField] GameObject doorLeftObject;
    [SerializeField] bool moveDoorByX;
    [SerializeField] bool moveDoorByY;
    [SerializeField] bool moveDoorByZ;

    Vector3 doorRightOpenTargetVector;
    Vector3 doorLeftOpenTargetVector;
    Vector3 doorRightCloseTargetVector;
    Vector3 doorLeftCloseTargetVector;

    private void Start()
    {

        doorRightCloseTargetVector = doorRightObject.transform.localPosition;
        doorLeftCloseTargetVector = doorLeftObject.transform.localPosition;

        if (moveDoorByX == true)
        {

            Vector3 rightVector;
            rightVector = doorRightObject.transform.localPosition;
            rightVector.x -= 2f;

            Vector3 leftVector;
            leftVector = doorLeftObject.transform.localPosition;
            leftVector.x += 2f;

            doorRightOpenTargetVector = rightVector;
            doorLeftOpenTargetVector = leftVector;

        }
        else if (moveDoorByY == true)
        {

            Vector3 rightVector;
            rightVector = doorRightObject.transform.localPosition;
            rightVector.y -= 2f;

            Vector3 leftVector;
            leftVector = doorLeftObject.transform.localPosition;
            leftVector.y += 2f;

            doorRightOpenTargetVector = rightVector;
            doorLeftOpenTargetVector = leftVector;

        }
        else if (moveDoorByZ == true)
        {

            Vector3 rightVector;
            rightVector = doorRightObject.transform.localPosition;
            rightVector.z -= 2f;

            Vector3 leftVector;
            leftVector = doorLeftObject.transform.localPosition;
            leftVector.z += 2f;

            doorRightOpenTargetVector = rightVector;
            doorLeftOpenTargetVector = leftVector;

        }

    }

    public void OpenDoorVoid(Collider detectedCollider)
    {

        if (detectedCollider.tag == "bean" && isDoorOpenableByPlayer == true)
        {

            OpenCloseDoorVoid(true);

        }

        if (detectedCollider.tag == "Enemy")
        {

            NormEnemyBehavior enemyAIScript = detectedCollider.GetComponent<NormEnemyBehavior>();
            MakeAIWait(enemyAIScript);
            OpenCloseDoorVoid(true);

        }

    }

    void MakeAIWait(NormEnemyBehavior aiScript)
    {

        aiScript.StoppedByDoorVoid(AIWaitTime);

    }

    public void CloseDoorVoid(Collider detectedCollider)
    {

        if (detectedCollider.tag == "bean" && isDoorOpenableByPlayer == true)
        {

            OpenCloseDoorVoid(false);

        }

        if (detectedCollider.tag == "Enemy")
        {

            OpenCloseDoorVoid(false);

        }

    }

    private void OpenCloseDoorVoid(bool openDoor)
    {

        Tweener doorRightOpenTween = doorRightObject.transform.DOLocalMove(doorRightOpenTargetVector, doorOpenCloseTime)
                .SetUpdate(UpdateType.Fixed, false)
                .SetAutoKill(true);

        Tweener doorLeftOpenTween = doorLeftObject.transform.DOLocalMove(doorLeftOpenTargetVector, doorOpenCloseTime)
            .SetUpdate(UpdateType.Fixed, false)
            .SetAutoKill(true);

        Tweener doorRightCloseTween = doorRightObject.transform.DOLocalMove(doorRightCloseTargetVector, doorOpenCloseTime)
            .SetUpdate(UpdateType.Fixed, false)
            .SetAutoKill(true);

        Tweener doorLeftCloseTween = doorLeftObject.transform.DOLocalMove(doorLeftCloseTargetVector, doorOpenCloseTime)
            .SetUpdate(UpdateType.Fixed, false)
            .SetAutoKill(true);

        if ((doorRightOpenTween.IsPlaying() == true && doorLeftOpenTween.IsPlaying() == true) && openDoor == true)
        {

            doorRightOpenTween.ChangeEndValue(doorRightCloseTargetVector, 1f);
            doorLeftOpenTween.ChangeEndValue(doorLeftCloseTargetVector, 1f);

        }
        else if ((doorRightCloseTween.IsPlaying() == true && doorLeftCloseTween.IsPlaying() == true) && openDoor == false)
        {

            doorRightCloseTween.ChangeEndValue(doorRightOpenTargetVector, 1f);
            doorLeftCloseTween.ChangeEndValue(doorLeftOpenTargetVector, 1f);

        }
        else
        {

            if (openDoor == true)
            {

                doorRightOpenTween.Play();
                doorLeftOpenTween.Play();

            }
            else
            {

                doorRightCloseTween.Play();
                doorLeftCloseTween.Play();

            }

        }

    }

}
