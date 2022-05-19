using UnityEngine;

public class TriggerEnter : MonoBehaviour
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
    void OnTriggerEnter(Collider other)
    {
        gameManager.crosshair.transform.position = new Vector3(-10, -10, -10);
        rb.useGravity = true;
        GetComponent<CosmeticsController>().canMove = false;

        if (other.CompareTag(targetOnFace) && canHit)
        {
            canHit = false;
            gameManager.Event_HitScore(true);
        }
    }

    // SECONDARY COLLISION OR COLLISION FIX:
    void OnCollisionEnter(Collision col)
    {
        gameManager.crosshair.transform.position = new Vector3(-10, -10, -10);
        rb.useGravity = true;
        GetComponent<CosmeticsController>().canMove = false;
    }



}


