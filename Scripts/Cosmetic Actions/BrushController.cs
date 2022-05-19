using UnityEngine;

public class BrushController : MonoBehaviour
{
    /*
    [Header("Change brush material.")]
    [Header("Activate FX_Hit Indicator.")]
    [Header("Enable brush throwing.")]
    [Header("Continue game loop.")]
    */


    public GameObject fx_indicator;

    private void Start()
    {
        // >Change material of the mesh randomly.
    }

    public void Continue()
    {
        fx_indicator.SetActive(true);
        GetComponent<CosmeticsController>().enabled = true;
    }
}
