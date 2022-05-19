using UnityEngine;

public class CosmeticsID : MonoBehaviour
{
    public int targetID;
    public int cosmeticID;
    void Awake()
    {
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().UI_CosmeticNameHolder(targetID, cosmeticID);

    }

}

// 
