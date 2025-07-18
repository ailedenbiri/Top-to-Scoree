using UnityEngine;

public class BallCO : MonoBehaviour
{
   private Rigidbody rb;
    public float forceAmount = 500f;
    private bool hasShot = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

    if (collision.gameObject.CompareTag("Target"))
    {
        GameManager.instance.OnSuccessfulHit();
    }
    else if (collision.gameObject.CompareTag("Ground"))
    {
        GameManager.instance.OnMiss();
    }

    hasShot = false; // çift tetiklemeyi engeller
}

    public void ResetBall()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = new Vector3(0, 0.5f, 0);
        hasShot = false;
    }
}
