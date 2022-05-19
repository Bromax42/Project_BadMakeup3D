using UnityEngine;

public class ColEnter : MonoBehaviour
{
    public GameManager gameManager;

    public string targetOnFace;
    private bool canHit;
    private Rigidbody rb;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        canHit = true;

    }
    // PRIMARY COLLISION:
    void OnCollisionEnter(Collision col)
    {
        gameManager.crosshair.transform.position = new Vector3(-10, -10, -10);
        GetComponent<CosmeticsController>().canMove = false;
        rb.useGravity = true;

        if (col.collider.CompareTag(targetOnFace) && canHit)
        {
            canHit = false;
            gameManager.Event_HitScore(true);

        }
    }

    /*
    // SECONDARY COLLISIONS:
    void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject.GetComponentInChildren<CastRay>());
        gameManager.crosshair.transform.position = new Vector3(-10, -10, -10);
        rb.useGravity = true;
        GetComponent<CosmeticsController>().canMove = false;

    }
    */

}
