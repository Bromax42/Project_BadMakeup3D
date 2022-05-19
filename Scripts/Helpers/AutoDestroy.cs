using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float destroyTime;
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

}
