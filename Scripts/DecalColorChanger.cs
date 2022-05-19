using UnityEngine;

public class DecalColorChanger : MonoBehaviour
{
    private Color color;
    float hue;
    public float saturation;
    public float value;

    public bool rdmHue; /* , rdmSaturation, rdmValue; */
    void Start()
    {
        hue = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().hueData;
        //  if (rdmHue) {hue = Random.Range(0f, 1f); } 


        color = Color.HSVToRGB(hue, saturation, value);
    //    color.a = 0.25f;
        GetComponent<Renderer>().material.SetColor("_TintColor", color);
    }

}

