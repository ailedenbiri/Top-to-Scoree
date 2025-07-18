using UnityEngine;
using DG.Tweening;

public class TargetCO : MonoBehaviour
{
    public float moveDistance = 3f;
    public float moveDuration = 2f;

    void Start()
    {
        Vector3 rightTarget = transform.position + Vector3.right * moveDistance;
        Vector3 leftTarget = transform.position - Vector3.right * moveDistance;

        // Sonsuz sağ-sol hareket
        transform.DOMove(rightTarget, moveDuration)
                 .SetLoops(-1, LoopType.Yoyo)
                 .SetEase(Ease.InOutSine);
    }
}
