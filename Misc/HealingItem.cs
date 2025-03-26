using UnityEngine;
using DG.Tweening;

public class HealingItem : MonoBehaviour
{

    public float healingAmount = 20f;
    public bool percentHealing = false;

    Tweener moveYTween;
    Tweener rotateTween;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "bean")
        {

            PlayerManager playerScript = other.GetComponent<PlayerManager>();
            
            if (percentHealing)
            {

                playerScript.PlayerReceivesHealing(0f, 20f);
                DOTween.Kill(moveYTween);
                DOTween.Kill(rotateTween);
                Destroy(gameObject);

            }
            else
            {

                playerScript.PlayerReceivesHealing(20f, 0f);
                DOTween.Kill(moveYTween);
                DOTween.Kill(rotateTween);
                Destroy(gameObject);

            }

        }

    }

    private void Start()
    {

        moveYTween = gameObject.transform.DOMoveY(gameObject.transform.position.y + 0.4f, 2f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        Vector3 rotateVector = new Vector3();
        rotateVector.y += 360;
        rotateTween = gameObject.transform.DORotate(rotateVector, 4f, RotateMode.FastBeyond360)
            .SetEase(Ease.Flash)
            .SetLoops(-1, LoopType.Incremental);

        moveYTween.Play();
        rotateTween.Play();

    }

}
