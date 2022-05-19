using UnityEngine;

public class CameraFadeEvents : MonoBehaviour
{
    private bool isSitting;

    // This function is called by sitting/Standing animations from Inspector > Animation Import Settings.
    public void FadeEvents()
    {
       isSitting = GetComponent<AgentMoveTo>().isSitting;


        if (isSitting)
        {
            Debug.Log(isSitting);
            // Focus camera to a girl's face.
            // Camera.main.transform.SetPositionAndRotation();
        }

    }
}
