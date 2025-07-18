using UnityEngine;
using DG.Tweening;

public class BallCO : MonoBehaviour
{
  private Rigidbody rb;

    [Header("Atış Ayarları")]
    public float forceAmount = 500f;
    private bool hasShot = false;

    [Header("Tween Ayarları")]
    public Ease startEase = Ease.OutBack;
    public Ease resetEase = Ease.OutBack;
    public Ease hitEaseOut = Ease.OutQuad;
    public Ease hitEaseBack = Ease.OutBack;

       void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // Başlangıç pozisyonunu DOTween ile ayarla
        // Ease tipleri: Linear, InSine, OutSine, InOutSine, InQuad, OutQuad, InOutQuad, InCubic, OutCubic, InOutCubic, InQuart, OutQuart, InOutQuart, InQuint, OutQuint, InOutQuint, InExpo, OutExpo, InOutExpo, InCirc, OutCirc, InOutCirc, InElastic, OutElastic, InOutElastic, InBack, OutBack, InOutBack, InBounce, OutBounce, InOutBounce, Flash, InFlash, OutFlash, InOutFlash, INTERNAL_Custom, INTERNAL_Custom
        transform.DOMove(new Vector3(0, 0.21f, -3.67f), 0.3f)
            .SetEase(Ease.OutBack);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !hasShot)
        {
            rb.AddForce(Vector3.forward * forceAmount);
            hasShot = true;
        }
    }


private void OnCollisionEnter(Collision collision)
{
    if (!hasShot) return;

    string tag = collision.gameObject.tag;

    if (tag == "Target")
    {
        // DOTween ile hedefe vurulduğunda scale animasyonu
        // Ease tipleri: Linear, InSine, OutSine, InOutSine, InQuad, OutQuad, InOutQuad, InCubic, OutCubic, InOutCubic, InQuart, OutQuart, InOutQuart, InQuint, OutQuint, InOutQuint, InExpo, OutExpo, InOutExpo, InCirc, OutCirc, InOutCirc, InElastic, OutElastic, InOutElastic, InBack, OutBack, InOutBack, InBounce, OutBounce, InOutBounce, Flash, InFlash, OutFlash, InOutFlash, INTERNAL_Custom, INTERNAL_Custom
        collision.transform.DOScale(Vector3.one * 0.6f, 0.1f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                collision.transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.OutBack);
            });

        GameManager.instance.OnSuccessfulHit();
    }
    else if (tag == "Ground")
    {
        GameManager.instance.OnMiss();
    }

    hasShot = false; // ikinci çarpışmayı engeller
}

    public void ResetBall()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        // DOTween ile yumuşak bir şekilde başlangıç pozisyonuna git
        transform.DOMove(new Vector3(0, 0.21f, -3.67f), 0.5f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => {
                hasShot = false;
            });
    }
}
