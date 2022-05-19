using UnityEngine;

public class ThrowableCollisions : MonoBehaviour
{
    public string TargetOnFace;
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

        // Reset Hit Info in Game Manager.
   //     gameManager.didScore = false;

    }


    void OnTriggerEnter(Collider other)
    {
        // Fix throwing simulation.
        throwableActions.canMove = false;
        rb.useGravity = true;
        rb.AddForce(Vector3.forward * 0.25f, ForceMode.VelocityChange);

        if (other.CompareTag(TargetOnFace.ToString()) && canShoot)
        {
            canShoot = false;
            gameManager.Event_HitScore(true);

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        throwableActions.canMove = false;
        rb.useGravity = true;
        rb.AddForce(Vector3.forward * 0.25f, ForceMode.VelocityChange);

    }
}
