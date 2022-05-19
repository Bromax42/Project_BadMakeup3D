using UnityEngine;

public class ColCheeks : MonoBehaviour
{
    public GameManager gameManager;
    private bool canHit;
    private Rigidbody rb;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        canHit = true;
        
    }

    void OnTriggerEnter(Collider other)
    {
        
        gameManager.crosshair.transform.position = new Vector3(-10, -10, -10);
        rb.useGravity = true;
        GetComponent<CosmeticsController>().canMove = false;



        if (other.CompareTag("Cheeks") && canHit)
        {
            canHit = false;
            gameManager.Event_HitScore(true);                                

            // Trigger girl reaction animation.
          // GirlAnimations girlAnimations = other.GetComponentInParent<GirlAnimations>();
          // girlAnimations.ExpressFacially();

        }
    }

    // Correct Cosmetic Throw simulation if target misses and hits Face Collider (Mesh Collider component on girl.)
    void OnCollisionEnter(Collision col)
    {
        gameManager.crosshair.transform.position = new Vector3(-10, -10, -10);
        rb.useGravity = true;
        GetComponent<CosmeticsController>().canMove = false;
    }

}
