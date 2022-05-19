using UnityEngine;

public class CameraTransitions : MonoBehaviour
{
    public GameManager gameManager;

    [Tooltip("This is the UI image that has the Fade-In/Out animations.")]
    public Animator fadeAnimator;

    // Camera focus target.
    public Transform target;




    void Start()
    {
        // Get the first targeted girl.
        FocusOnGirl();
    }

    // Focus on either standing cameraFocus or sitting cameraFocus.
    public void FocusOnGirl()
    {
        // Set target to CamFocus on a girl.
        target = gameManager.currentGirlTransform.transform.GetChild(0);

    }


    // Focus at target realtime.
    void LateUpdate()
    {
            transform.LookAt(target);
    }

    public void FadeIn()
    {
        fadeAnimator.SetTrigger("FadeIn");

        // Camera focus on standing pivot.
     //   target = gameManager.currentGirlTransform.transform.GetChild(0);

    }

    public void FadeOut(bool isSitting)
    {
        if (isSitting)
        {
            // Camera focus on sitting pivot.
            target = gameManager.currentGirlTransform.transform.GetChild(1);
        }
        else
        {
            // Camera focus on standing pivot.
            target = gameManager.currentGirlTransform.transform.GetChild(0);
        }
        fadeAnimator.SetTrigger("FadeOut");
    }

}

// Extra:

// target = null;

