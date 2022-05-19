using System.Collections;
using UnityEngine;

public class CosmeticsController : MonoBehaviour
{
    public GameManager gameManager;
    public string targetCollider;
    [HideInInspector] Vector2 targetColliderInput;

 //   private bool canShoot;

    [HideInInspector]
    public bool canMove;

    public GameObject throwableCosmetic;
    public GameObject raycaster;
    public CastRay castRay;
    Vector3 targetPosition;


    void Start()
    {
        // Connect GameManager and related components.
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Instantiate(gameManager.effect_Go, gameManager.userInterface.transform, false);
   //     canShoot = true;
        canMove = false;

        // Set current rounds Decal Hue.
        gameManager.DecalData();

        // Get targeted collider to position the Raycaster.
        GirlColliderPositions colliderInfo = gameManager.currentGirlTransform.GetComponent<GirlColliderPositions>();
        switch (targetCollider)
        {
            case "Eyebrows":
                targetColliderInput = new Vector2 (colliderInfo.eyebrowCollider.position.x, colliderInfo.eyebrowCollider.position.y);
                break;
            case "Eyes":
                targetColliderInput = new Vector2(colliderInfo.eyeCollider.position.x, colliderInfo.eyeCollider.position.y);
                break;
            case "Cheeks":
                targetColliderInput = new Vector2(colliderInfo.cheekCollider.position.x, colliderInfo.cheekCollider.position.y);
                break;
            case "Lips":
                targetColliderInput = new Vector2(colliderInfo.lipsCollider.position.x, colliderInfo.lipsCollider.position.y);
                break;
            default:
                targetColliderInput = transform.position;
                break;
        }


        Vector3 raycasterPosition = new Vector3(targetColliderInput.x, targetColliderInput.y, transform.position.z);
        // Spawn and set RayCasting object for current cosmetic.
        raycaster = Instantiate(gameManager.Raycaster, raycasterPosition, Quaternion.Euler(0, 180, 0));
        castRay = raycaster.GetComponent<CastRay>();
        castRay.throwableCosmetic = throwableCosmetic;
        castRay.initialCosmetic = gameObject;

    }

    // Simulate cosmetic throw:
    void FixedUpdate()
    {
        // Simulate throwing:
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 2.5f * Time.fixedDeltaTime);
        }

    }


    public void ThrowCosmetics(Vector3 targetPositionInput)
    {
        // Set Destination.
        targetPosition = targetPositionInput;

        // Allow movement.
        canMove = true;                                                                            // print("Firing cosmetic!");

        // Self-Destroy in case player misses.
        Destroy(gameObject, 4.0f);

        // Reset Hit Info in Game Manager.
        gameManager.didScore = false;

        // Check for missed shot.
        StartCoroutine(CheckForMissedShot());
    }
    IEnumerator CheckForMissedShot()
    {
        yield return new WaitForSeconds(1.55f);

        if (!gameManager.didScore)
        {
            gameManager.Event_HitScore(false);
        }

        // Continue with Throwables.
        castRay.ContinueShooting();
    }


}
// RECICLE BIN //


/*
private ParticleSystem VFX;
private ParticleSystem.EmissionModule emissionModule;
private ParticleSystem.MainModule mainModule;
*/

//  public float throwForce = 1f;

// Vector3 touchTranslation;




// [Header("Exeptional Cosmetics are marked here:")]
// Exeptional cosmetic:
//  [Tooltip("Is this the eyebrow brush cosmetic?")]
//  public bool isEyebrowBrush;

// public bool isBeautyBlender;








//Rigidbody rb = GetComponent<Rigidbody>();

/*
// Eyebrow brush and blender are facing wrong directions.
// Check if we're throwing them.
if (isEyebrowBrush)
{ rb.AddForce(-transform.right * throwForce, ForceMode.VelocityChange); }
else if (isBeautyBlender)
{ rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange); }
else
{ rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange); }

rb.useGravity = true;

// Get rid of Hit indicator.
transform.GetChild(0).GetComponent<HitIndicatorActions>().DetachAndDestroy();
*/
