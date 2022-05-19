using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ElephantSDK;

public class GameManager : MonoBehaviour
{
    [Header("UI Sprites")]
    public Sprite sEyebrowBrush;
    public Sprite sEyeshadowBrush;
    public Sprite sFoundation; // Beauty Blender!
    public Sprite sKabukiBrush;
    public Sprite sLipGloss;
    public Sprite sLiptstick;
    public Sprite sMascara;
    [Header("UI Targets")]
    public Sprite sFace;
    public Sprite sBrows;
    public Sprite sEyes;
    public Sprite sCheeks;
    public Sprite sLips;

    public GameObject cosmeticNamePrefab;
    private GameObject CosmeticNameHolder;


    [Header("UI Elements:")]
    public GameObject userInterface;
    public GameObject effect_Go;
    public GameObject StageCompletionReel;
    public GameObject LevelCompletionReel;
    public GameObject LevelFailedReel;
    public GameObject LevelText;

   
    [Header("Scene Helpers:")]
    public Transform[] waypoints;
    public Transform sitPosition;
    public Transform cameraPivot;
    public Transform cameraStart;
    public Transform mirror;
    public Transform girlSpawnPosition;
    public Light directionalLight;

    public GameObject Raycaster;
    public GameObject crosshair;

    // Game Loop elements.
    [HideInInspector] public bool levelPassed;
    [HideInInspector] public bool didScore;
    [HideInInspector] public bool canChangeDecalColor;

    // DEBUG CHANGE //
    public int hitScore;

    // Girls.
    public GameObject[] girls;

    [HideInInspector]
    public int currentGirl;
    public GameObject currentGirlTransform;


    // Cosmetics.
    public GameObject[] cosmetics;
    public Transform cosmeticSpawnPosition;
    private int roundsLeft;


    [Tooltip("This is the UI image that has the Fade-In/Out animations attached.")]
    public Animator fadeAnimator;

    // Decal Color
    public float hueData;


    //Make Singleton and Connect with other elements.
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        roundsLeft = 2;


        // Load saved game.
        if (PlayerPrefs.HasKey("currentLevel"))
        {
            currentGirl = PlayerPrefs.GetInt("currentLevel");
        }
        if (currentGirl > 9 ) { currentGirl = 0; }
        Elephant.LevelStarted((currentGirl+1));

        // Assign Level number to UI for the first time.
        LevelText.GetComponent<Text>().text = (currentGirl + 1).ToString();

        // Spawn first girl and assign its transform.
        currentGirlTransform = Instantiate(girls[currentGirl], girlSpawnPosition.position, girlSpawnPosition.rotation);
        
    }

    public void DecideRounds()
    {
        // Assign number of rounds according to girl/stage number.
        if (currentGirl < 2) { roundsLeft = 2; }
        else if (currentGirl > 1 && currentGirl < 6) { roundsLeft = 3; }
        else if (currentGirl > 5 && currentGirl < 9) { roundsLeft = 4; }
        else { roundsLeft = 5; }

        // Start next stage.
        StartCoroutine(SpawnCosmetics());
    }

    IEnumerator SpawnCosmetics()
    {
        yield return new WaitForSeconds(1f);

        // Reset hits.
        hitScore = 0;

        int cosmeticNumber;
        cosmeticNumber = Random.Range(0, 9);                                                     //Debug.Log("Rounds left before next level: " + roundsLeft);

        // Continue stage if there are any rounds left.
        // Spawn cosmetics here.
        if (roundsLeft > 0)
        {
            Instantiate(cosmetics[cosmeticNumber], cosmeticSpawnPosition.position + new Vector3(0, -0.07f, -0.2f), cosmeticSpawnPosition.rotation);
            roundsLeft--;                                                                        Debug.Log("Rounds left before next level: " + roundsLeft);

            // reset level status.
            levelPassed = false;
        }
        // Finish stage when rounds are over.
        else
        {
            // Level Passed.
            levelPassed = true;

            // Trigger Win animation.
            currentGirlTransform.GetComponent<GirlAnimations>().GirlWinAnimation();                                             Debug.Log("Rounds finished!");
        }
    }

    public void Event_HitScore(bool correctHit)
    {
        if (correctHit) { hitScore++; didScore = true; }
       // else { hitScore--;}
    }

    public void Event_CheckScore()
    {
        Animator girlAnimator = currentGirlTransform.GetComponent<Animator>();

        if (hitScore >= 5)
        {   // Smile.                                                                                                      
            currentGirlTransform.GetComponent<GirlAnimations>().ExpressFacially(0);                                           print("Stage passed. " + hitScore + "/8");

            // Play Stage Completion reel.
            Instantiate(StageCompletionReel, userInterface.transform, false);

            // Continue with next cosmetic or win stage:
            StartCoroutine(WaitBeforeNextCosmetic());

        }
        else
        {
            levelPassed = false;
            currentGirlTransform.GetComponent<GirlAnimations>().ExpressFacially(1); // Frown.

            // Lose Stage.
            levelPassed = false;
            girlAnimator.SetTrigger("Lose");
            girlAnimator.SetBool("LevelPassed", false);                                                                       print("Stage failed. " + hitScore + "/8");
            //      girlAnimator.SetTrigger("Lose");

        }


    }


    IEnumerator WaitBeforeNextCosmetic()
    {
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(SpawnCosmetics());
    }

    public void Event_CosmeticMissed()
    {
        // Level Failed.
        // Trigger Lose Animation.
        currentGirlTransform.GetComponent<GirlAnimations>().GirlLoseAnimation();
        currentGirlTransform.GetComponent<Animator>().SetBool("LevelPassed", false);

        // Play Fail Animation Reel.
        Instantiate(LevelFailedReel, userInterface.transform, false);


    }
    public void ResetLevel() // ...for the next round.
    {
        if (currentGirl >= 9) { currentGirl = 0; }
        else { currentGirl++; }

        // reset Hit Score.
        hitScore = 0;

        // Update UI - CurrentLevel text.
        LevelText.GetComponent<Text>().text = (currentGirl+1).ToString();

        currentGirlTransform = Instantiate(girls[currentGirl], girlSpawnPosition.position, girlSpawnPosition.rotation);

        Elephant.LevelStarted((currentGirl + 1));

        Camera.main.GetComponent<CameraTransitions>().FocusOnGirl();

    }





    // Function called during cosmetic spawn.
    // It updates current brush and hit target info on UI.
    public void UI_CosmeticNameHolder(int targetID, int cosmeticID)
    {
        CosmeticNameHolder = Instantiate(cosmeticNamePrefab, userInterface.transform, false);
        Image inputTarget = CosmeticNameHolder.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        Image inputCosmeticName = CosmeticNameHolder.transform.GetChild(0).GetChild(1).GetComponent<Image>();

        switch (targetID)
        {
            case 0: // Face
                inputTarget.sprite = sFace;
                inputCosmeticName.sprite = sFoundation; // Beauty Blender!
                break;
            case 1: // Brows
                inputTarget.sprite = sBrows;
                inputCosmeticName.sprite = sEyebrowBrush;
                break;
            case 2: // Eyes
                inputTarget.sprite = sEyes;
                if (cosmeticID == 0) { inputCosmeticName.sprite = sEyeshadowBrush; }
                else { inputCosmeticName.sprite = sMascara; }
                break;
            case 3: // Cheeks
                inputTarget.sprite = sCheeks;
                inputCosmeticName.sprite = sKabukiBrush; // Named "Foundation Base" as prefab.
                break;
            case 4: // Lips
                inputTarget.sprite = sLips;
                if (cosmeticID == 0) { inputCosmeticName.sprite = sLipGloss; }
                else { inputCosmeticName.sprite = sLiptstick; }
                break;


            default:
                break;
        }
    }

    public void DestroyUI_CosmeticNameHolder()
    {
        Destroy(CosmeticNameHolder);
    }
    
    public void SaveProgress()
    {
       PlayerPrefs.SetInt("currentLevel", (currentGirl+1));
        print("Progress saved. Current level :" + (currentGirl + 1));
    }

    public void ElephantLevelPassed()
    {
        Elephant.LevelCompleted((currentGirl + 1));
    }
    public void ElephantLevelFailed()
    {
        Elephant.LevelFailed((currentGirl + 1));
    }



    // DECAL COLOR CHANGE:
    public void DecalData()
    {
        hueData = Random.Range(0f, 1f);
    }


    // DEBUGGING
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            PlayerPrefs.DeleteAll();
            print("Reseted PlayerPrefs saves.");
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Time.timeScale += 0.5f;
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus) && Time.timeScale > 0.5f)
        {
            Time.timeScale -= 0.5f;
        }
    }

}
