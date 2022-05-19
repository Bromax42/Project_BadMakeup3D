using UnityEngine;

public class StageCompletion : MonoBehaviour
{
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
        int randomEvent = UnityEngine.Random.Range(0, 7);

        animator.SetInteger("EventNumber", randomEvent);
    }

}
