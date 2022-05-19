using System.Collections;
using UnityEngine;

public class EyeshadowController : MonoBehaviour
{
    public GameObject[] brushes;
    int currentBrush;
    public float offsetY;

    void Start()
    {
        currentBrush = Random.Range(0, (brushes.Length)-1);
        StartCoroutine(WaitBeforeSpawn());
    }
    IEnumerator WaitBeforeSpawn()
    {
        yield return new WaitForSeconds(0.8f);

        // Spawn a random brush
        Instantiate(brushes[currentBrush], transform.position + new Vector3(0, offsetY, 0), new Quaternion(0, 0, 0, 0));

    }
}
