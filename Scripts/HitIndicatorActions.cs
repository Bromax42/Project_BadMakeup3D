using UnityEngine;

public class HitIndicatorActions : MonoBehaviour
{
    public void DetachAndDestroy()
    {
        transform.parent = null;
        Destroy(gameObject);
    }
}
