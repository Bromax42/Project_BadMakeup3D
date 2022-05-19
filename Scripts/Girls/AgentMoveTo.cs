using System.Collections;
using UnityEngine;
using UnityEngine.AI;

    public class AgentMoveTo : MonoBehaviour
{
    public GameManager gameManager;

    public Transform[] waypoints;
    private int currentWaypoint;

    private Animator animator;

    public Transform sitPosition;
    private bool isComing;

    // Camera
    public Transform cameraPivot;
    public Transform cameraStart;

    private bool canSit;

    // This bool is sent to [CameraTransitions.cs] .
    [HideInInspector]
    public bool isSitting;

    [Tooltip("This is the mirror the girls will LookAt() whilst sitting and standing up.")]
    public Transform mirror;

    [HideInInspector]
    public NavMeshAgent agent;

    // MeshCollider: 
    public GameObject faceMesh;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        // Get Waypoints from GameManager.
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = gameManager.waypoints[i];
        }

        sitPosition = gameManager.sitPosition;
        cameraPivot = gameManager.cameraPivot;
        cameraStart = gameManager.cameraStart;

        mirror = gameManager.mirror;

        animator = GetComponent<Animator>();                                                               //  Debug.Log("Total Waypoints: " + waypoints.Length);
        agent = GetComponent<NavMeshAgent>();

        // initial waypoint movement
        currentWaypoint = 0;
        canSit = true;
        isSitting = false;
        isComing = true;
        agent.SetDestination(waypoints[0].position);

    }
    void Update()
    {
        // Check weather agent is coming or leaving.
        if (isComing) { AgentComing(); }
    }

    void AgentComing()
    {
        // Move the girl if the current path is finished and there are more waypoints:
        if (!agent.hasPath && currentWaypoint < (waypoints.Length) - 1)
        {
            currentWaypoint++;
            agent.SetDestination(waypoints[currentWaypoint].position);                                             // Debug.Log("Goint to Waypoint: " + (currentWaypoint + 1));

            if (currentWaypoint == (waypoints.Length) - 1)
            {
                Camera.main.GetComponent<CameraTransitions>().FadeIn();
            }

        }

        // Activate Idle animation when movement ends.
        if (canSit && currentWaypoint == (waypoints.Length) - 1 && agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            canSit = false;                                                                                      // Debug.Log("Finished last waypoint.");
            animator.SetFloat("Speed", 0);
            SitAnimation();
        }
    }

    // This function is triggered by the end of "Sit to Stand" animation.
    public void LeaveAfterStanding()
    {
        isComing = false;
        agent.SetDestination(gameManager.girlSpawnPosition.position);

        // Fade camera dark.
        Camera.main.GetComponent<CameraTransitions>().FadeOut(isSitting);

        // Destroy the girl on the way out.
       // Destroy(gameObject, 6);
        StartCoroutine(DestroyGirl());
    }

    void SitAnimation()
    {
        // Warp girl to sitting position.
        agent.Warp(sitPosition.position);

        // Sit the girl.
        animator.SetTrigger("Sit");
        // animator.applyRootMotion = true;

        // Turn the girl towards the mirror.()
        transform.LookAt(mirror);

        // Fix LookAt() error.
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        isSitting = true;
    }

    // This function is triggered by Sitting Animation Trigger to sit down, later on by Winning/Losing animations to stand up.
    public void FadeEvents()
    {
        if (isSitting)
        {
            // Change camera perspective to cosmetic throwing scene.
            Camera.main.transform.SetPositionAndRotation(cameraPivot.position, cameraPivot.rotation);
            Camera.main.GetComponent<CameraTransitions>().FadeOut(isSitting);

            // Prepare for standing up.
            isSitting = false;

            // Add mesh collider to girl for splash FX's.
            // MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
            //faceMesh.AddComponent<MeshCollider>();

            // Start stage.
            gameManager.DecideRounds();
        }
        else
        {
            // Change camera perspective to start.
            Camera.main.transform.SetPositionAndRotation(cameraStart.position, cameraPivot.rotation);
            Camera.main.GetComponent<CameraTransitions>().FadeIn();

        }

    }
    // This function is triggered by Winning or Losing animations.
    public void ReturnCamera()
    {
        Camera.main.GetComponent<CameraTransitions>().FadeIn();

    }

    IEnumerator DestroyGirl()
    {
        yield return new WaitForSeconds(4.5f);
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().ResetLevel();
        Destroy(gameObject);
    }

}



// Extra:
