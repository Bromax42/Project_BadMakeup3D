using UnityEngine;

public class ColFace : MonoBehaviour
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

    void OnCollisionEnter(Collision col)
    {

        gameManager.crosshair.transform.position = new Vector3(-10, -10, -10);
        rb.useGravity = true;
        GetComponent<CosmeticsController>().canMove = false;



        if (col.collider.CompareTag("Face") && canHit)
        {
            canHit = false;
            gameManager.Event_HitScore(true);

            // Trigger girl reaction animation.
     //       col.collider.GetComponentInParent<GirlAnimations>().GirlReact();


        }
    }

    // SECONDARY COLLISIONS:
    void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject.GetComponentInChildren<CastRay>());
        gameManager.crosshair.transform.position = new Vector3(-10, -10, -10);
        rb.useGravity = true;
        GetComponent<CosmeticsController>().canMove = false;


    }


}
