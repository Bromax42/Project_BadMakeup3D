using System.Collections;
using UnityEngine;

public class BeautyblenderController : MonoBehaviour
{
    public Transform FX_Foundation;
    public GameObject foundation;
    
    public void StopFoundation()
    {
        FX_Foundation.SetParent(transform, true);
        StartCoroutine(DisableFoundation());
    }
    IEnumerator DisableFoundation()
    {
        yield return new WaitForSeconds(1.0f);
        foundation.SetActive(false);
    }

    public void Continue()
    {
        // Save cosmetic rotation and destroy animator component.
        GetComponent<Animator>().applyRootMotion = true;
        Destroy(GetComponent<Animator>());

        // Enable Indicator.
        transform.GetChild(0).gameObject.SetActive(true);
        // Enable cosmetic throwing.
        GetComponent<CosmeticsController>().enabled = true;
    }
}
