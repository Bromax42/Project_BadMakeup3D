using UnityEngine;

public class ColLips : MonoBehaviour
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



        if (other.CompareTag("Lips") && canHit)
        {
            canHit = false;
            gameManager.Event_HitScore(true);

            // Trigger girl reaction animation.
        //    other.GetComponentInParent<GirlAnimations>().GirlReact();



        }
    }
    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject.GetComponentInChildren<CastRay>());
        gameManager.crosshair.transform.position = new Vector3(-10, -10, -10);
        rb.useGravity = true;
        GetComponent<CosmeticsController>().canMove = false;

    }

}
