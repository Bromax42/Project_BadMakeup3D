using UnityEngine;

public class ThrowableCollisionNonTrigger : MonoBehaviour
{
    public GameManager gameManager;
    public ThrowableActions throwableActions;

    // Gameloop elements:
    private bool canShoot;

    private Rigidbody rb;
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        throwableActions = gameObject.GetComponent<ThrowableActions>();

        rb = GetComponent<Rigidbody>();
        canShoot = true;

    }

    // PRIMARY COLISSION:
    void OnCollisionEnter(Collision collision)
    {
        // Fix throwing simulation.
        throwableActions.canMove = false;
        rb.useGravity = true;
        rb.AddForce(Vector3.forward * 0.3f, ForceMode.VelocityChange);

        if (collision.collider.CompareTag("Face") && canShoot)
        {
            canShoot = false;
            gameManager.Event_HitScore(true);

        }
    }

    /*
    // SECONDARY COLLISION:
    void OnTriggerEnter(Collider col)
    {
        gameManager.crosshair.transform.position = new Vector3(-10, -10, -10);
        GetComponent<CosmeticsController>().canMove = false;
        rb.useGravity = true;
        rb.isKinematic = false;
    }
    */

}
