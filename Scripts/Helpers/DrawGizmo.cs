using UnityEngine;

public class DrawGizmo : MonoBehaviour
{
    public string GizmoName;
    public Color color;



    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, GizmoName +".png", true, color);


    }
}
