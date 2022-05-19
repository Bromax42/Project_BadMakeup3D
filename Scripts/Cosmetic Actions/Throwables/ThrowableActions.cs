using System.Collections;
using UnityEngine;

public class ThrowableActions : MonoBehaviour
{
    public GameManager gameManager;

    [HideInInspector] public bool canMove;
    private Vector3 targetPosition;


    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        // Destroy self if player misses.
        Destroy(gameObject, 4.0f);
    }


    public void Throw(Vector3 destination)
    {
        // Set destination and enable shooting:
        targetPosition = destination;
        canMove = true;
    }

    void FixedUpdate() // Simulate Cosmetic Throw:
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 2.5f * Time.fixedDeltaTime);

            // Check for missed shot.
            StartCoroutine(CheckForMissedShot());
        }

    }

    IEnumerator CheckForMissedShot()
    {
        yield return new WaitForSeconds(1.75f);


        if (!gameManager.didScore)
        {
            // Continue gameloop with level failed.
            // Start fail animation.
            gameManager.Event_HitScore(false);
        }
    }



}
