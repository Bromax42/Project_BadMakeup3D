using UnityEngine;

public class ColEyebrows : MonoBehaviour
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
        rb.AddForce(Vector3.back * 0.2f, ForceMode.VelocityChange);


        if (other.CompareTag("Eyebrows") && canHit)
        {                                                                                   
            canHit = false;
            gameManager.Event_HitScore(true);

            // Trigger girl reaction animation.
      //      other.GetComponentInParent<GirlAnimations>().GirlReact();

        }
    }
    void OnCollisionEnter(Collision col)
    {
        gameManager.crosshair.transform.position = new Vector3(-10, -10, -10);
        rb.useGravity = true;
        GetComponent<CosmeticsController>().canMove = false;
        rb.AddForce(Vector3.back * 0.2f, ForceMode.VelocityChange);


    }

}
