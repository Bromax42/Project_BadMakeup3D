using System.Collections;
using UnityEngine;

public class CastRay : MonoBehaviour
{

    public GameManager gameManager;
    public GameObject crosshair;
    Vector3 targetPosition;
    Vector3 shootDirection;
    Vector3 touchTranslation;
    private bool canShoot;
    private int shotsLeft;
    private bool isTouching;

    public LayerMask ignoredLayer;

    public CosmeticsController cosmeticController;
    public GameObject initialCosmetic;
    public GameObject throwableCosmetic;


    void Start()
    {        
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        shotsLeft = 7;
        shootDirection = new Vector3(0, 0, -1);
        canShoot = true;
        crosshair = gameManager.crosshair;

    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, shootDirection);

        // Touch Input:
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 touchSpeed = touch.deltaPosition / Time.deltaTime;
            Vector2 touchDirection = touchSpeed.normalized;
            touchTranslation = new Vector3(-touchDirection.x, touchDirection.y, 0.0f) * 0.13f;

        }
        // Stop movement when there is no touch.
        else { touchTranslation = Vector3.zero; }

        // Throw the cosmetics.
        if (Physics.Raycast(ray, out hit, 10.0f, ~ignoredLayer) /*&& hit.collider.isTrigger*/)
        {
            if (hit.collider.gameObject.layer == 8)
            {
                // Update Crosshair location and Target position.
                crosshair.transform.position = hit.point + new Vector3(0, 0, 0.05f);
                targetPosition = hit.point;

                if (Input.GetMouseButtonDown(0) && canShoot)
                {
                    canShoot = false;
                    gameManager.DestroyUI_CosmeticNameHolder();
                    initialCosmetic.GetComponent<CosmeticsController>().ThrowCosmetics(targetPosition);
                }
            }
            else { crosshair.transform.position = new Vector3(-10, -10, -10); }

        }


    }

    void FixedUpdate()
    {
        // Apply panning movement.
        transform.Translate(touchTranslation * Time.fixedDeltaTime, Space.World);                                            // print("Direction: " + touchDirection);

        // Limit Raycaster movement:

        if (gameManager.currentGirl == 1) // Girl 2
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.629f, -3.4875f), Mathf.Clamp(transform.position.y, 1.04f, 1.1765f ), transform.position.z);
        }
        else if (gameManager.currentGirl == 4) // Girl 5
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.629f, -3.4875f), Mathf.Clamp(transform.position.y, 1.04f, 1.1715f), transform.position.z);
        }
        else if (gameManager.currentGirl == 5) // Girl 6
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.629f, -3.4875f), Mathf.Clamp(transform.position.y, 1.04f, 1.1781f), transform.position.z);
        }
        else if (gameManager.currentGirl == 6) // Girl 7
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.629f, -3.4875f), Mathf.Clamp(transform.position.y, 1.04f, 1.1840f), transform.position.z);
        }
        else if (gameManager.currentGirl == 7) // Girl 8
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.629f, -3.4875f), Mathf.Clamp(transform.position.y, 1.04f, 1.1781f), transform.position.z);
        }
        else
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.629f, -3.4875f), Mathf.Clamp(transform.position.y, 1.04f, 1.1880f), transform.position.z);
        }



    }
    // Fire first Throwable and start Shooting Loop. 
    public void ContinueShooting()
    {
        StartCoroutine(ShootingLoop());
    }
    IEnumerator ShootingLoop()
    {
        while (shotsLeft > 0)
        {

            if (Input.touchCount > 0)
            {
                yield return new WaitForSeconds(0.65f);
                shotsLeft--;
                GameObject newThrowable = Instantiate(throwableCosmetic, gameManager.cosmeticSpawnPosition.position + new Vector3(0, -0.07f, -0.2f), gameManager.cosmeticSpawnPosition.rotation);
                newThrowable.GetComponent<ThrowableActions>().Throw(targetPosition);
            }

            yield return null;
        }


        // Clear Crosshair position:


        // Check for hit/miss ratio.
        yield return new WaitForSeconds(1.0f);
        gameManager.Event_CheckScore();
        crosshair.transform.position = new Vector3(-10, -10, -10);

        Destroy(gameObject);

    }

}
