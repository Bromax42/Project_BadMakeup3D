//using UnityEditor.Animations;
using UnityEngine;

public class MascaraController : MonoBehaviour
{

    // Called from animation.
    public void Continue()
    {

        // Save cosmetic rotation and destroy animator component.
        GetComponent<Animator>().applyRootMotion = true;
        Destroy(GetComponent<Animator>());

        // Enable Indicator.
        transform.GetChild(0).gameObject.SetActive(true);
        // Enable cosmetic throwing.
        GetComponent<CosmeticsController>().enabled = true;

        DetachCap();
    } 
    void DetachCap()
    {
        Transform cap = transform.GetChild(1);
        Destroy(cap.gameObject);
    }
}
