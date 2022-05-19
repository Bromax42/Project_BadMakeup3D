using System;
using UnityEngine;
using UnityEngine.Rendering;

public class CosmeticFXTrigger : MonoBehaviour
{
    public bool InfiniteDecal;
    public Light DirLight;
    public bool isVR = true;
    public GameObject BloodAttach;
   // public GameObject[] BloodFX;

    private void Start()
    {
        DirLight = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().directionalLight;
    }
    Transform GetNearestObject(Transform hit, Vector3 hitPos)
    {
        var closestPos = 100f;
        Transform closestBone = null;
        var childs = hit.GetComponentsInChildren<Transform>();

        foreach (var child in childs)
        {
            var dist = Vector3.Distance(child.position, hitPos);
            if (dist < closestPos)
            {
                closestPos = dist;
                closestBone = child;
            }
        }

        var distRoot = Vector3.Distance(hit.position, hitPos);
        if (distRoot < closestPos)
        {
            closestPos = distRoot;
            closestBone = hit;
        }
        return closestBone;
    }

    public Vector3 direction;
    int effectIdx;
    /*
    void Update()
    {
        //if (isVR)
        //{

        //    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        //    {
        //        RaycastHit hit;
        //        if (Physics.Raycast(Dir.position, Dir.forward, out hit))
        //        {
        //            // var randRotation = new Vector3(0, Random.value * 360f, 0);
        //            // var dir = CalculateAngle(Vector3.forward, hit.normal);
        //            float angle = Mathf.Atan2(hit.normal.x, hit.normal.z) * Mathf.Rad2Deg + 180;

        //            var effectIdx = Random.Range(0, BloodFX.Length);
        //            var instance = Instantiate(BloodFX[effectIdx], hit.point, Quaternion.Euler(0, angle + 90, 0));
        //            var settings = instance.GetComponent<BFX_BloodSettings>();
        //            settings.DecalLiveTimeInfinite = InfiniteDecal;
        //            settings.LightIntencityMultiplier = DirLight.intensity;

        //            if (!InfiniteDecal) Destroy(instance, 20);

        //        }

        //    }
        //}
        //  else
        {
            if (Input.GetMouseButtonDown(0))
            {
              //  Ray ray2 = new Ray(transform.position, transform.forward);
              //  var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                   var ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    // var randRotation = new Vector3(0, Random.value * 360f, 0);
                    // var dir = CalculateAngle(Vector3.forward, hit.normal);
                    float angle = Mathf.Atan2(hit.normal.x, hit.normal.z) * Mathf.Rad2Deg + 180;

                    //var effectIdx = Random.Range(0, BloodFX.Length);
                    if (effectIdx == BloodFX.Length) effectIdx = 0;

                    var instance = Instantiate(BloodFX[effectIdx], hit.point, Quaternion.Euler(0, angle + 90, 0));
                    effectIdx++;

                    var settings = instance.GetComponent<BFX_BloodSettings>();
                    //settings.FreezeDecalDisappearance = InfiniteDecal;
                    settings.LightIntensityMultiplier = DirLight.intensity;

                    var nearestBone = GetNearestObject(hit.transform.root, hit.point);
                    if (nearestBone != null)
                    {
                        var attachBloodInstance = Instantiate(BloodAttach);
                        var bloodT = attachBloodInstance.transform;
                        bloodT.position = hit.point;
                        bloodT.localRotation = Quaternion.identity;
                        bloodT.localScale = Vector3.one * Random.Range(0.75f, 1.2f);
                        bloodT.LookAt(hit.point + hit.normal, direction);
                        bloodT.Rotate(90, 0, 0);
                        bloodT.transform.parent = nearestBone;
                        //Destroy(attachBloodInstance, 20);
                    }

                    // if (!InfiniteDecal) Destroy(instance, 20);

                }

            }
        }

    }
    */


    private bool canHit = true;


    private void OnCollisionEnter(Collision col)
    {
        if (canHit)
        {
            canHit = false;                                                                               //  print("Collided: " + col.collider.name);




            RaycastHit hit;                                                                                 // print("Ray created.");
            Ray ray = new Ray(transform.position, transform.forward);


            if (Physics.Raycast(ray, out hit))
            {                                                                                               //print("Ray hit: " + hit.transform.name);


                if (hit.transform.gameObject.layer == 8)
                {
                                                                                                            //print("Hit layer: " + hit.transform.gameObject.layer);
                    // var randRotation = new Vector3(0, Random.value * 360f, 0);
                     var dir = CalculateAngle(Vector3.forward, hit.normal);
                    direction = hit.normal;


                    float angle = Mathf.Atan2(hit.normal.x, hit.normal.z) * Mathf.Rad2Deg + 180;

                    //var effectIdx = Random.Range(0, BloodFX.Length);
          //          if (effectIdx == BloodFX.Length) effectIdx = 0;

          //          var instance = Instantiate(BloodFX[effectIdx], hit.point, Quaternion.Euler(0, angle + 90, 0));
                    effectIdx++;

          //          var settings = instance.GetComponent<BFX_BloodSettings>();
                    //settings.FreezeDecalDisappearance = InfiniteDecal;
          //          settings.LightIntensityMultiplier = DirLight.intensity;

                    var nearestBone = GetNearestObject(hit.transform.root, hit.point);
                    if (nearestBone != null)
                    {
                        var attachBloodInstance = Instantiate(BloodAttach);

                        var bloodT = attachBloodInstance.transform;
                        bloodT.position = hit.point;
                        bloodT.localRotation = Quaternion.identity;
                   //     bloodT.localScale = Vector3.one; //* UnityEngine.Random.Range(0.75f, 1.2f);
                       // bloodT.LookAt(hit.point + hit.normal, direction);
                        bloodT.Rotate(90, 0, 0);
                        bloodT.transform.parent = nearestBone;
                        //Destroy(attachBloodInstance, 20);
                        
                    }
                }
                // if (!InfiniteDecal) Destroy(instance, 20);

            }
        }

    }


    public float CalculateAngle(Vector3 from, Vector3 to)
    {

        return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;

    }

}
