using System;
using System.Collections;
using UnityEngine;

public class GirlAnimations : MonoBehaviour
{
    public GameManager gameManager;
    private Animator animator;

    // Blend Shape parameters.
    public SkinnedMeshRenderer skinnedMeshRenderer;
    float lerpDuration = 1.5f;
    private bool canEmote0, canEmote1, canEmote2;

    // Make sure this array has 3 members.
    private int[] emotions = {0, 0, 0};
    private int emotionNumber;
    // 0, smile     1, frown     2, confused;

    private bool canAnimateLevelFinished;


    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        canEmote0 = false; canEmote1 = false; canEmote2 = false;
        canAnimateLevelFinished = true;
    }
    public void GirlReact()
    {
        // Generate reaction number (1-2-3).
        int eventNumber = UnityEngine.Random.Range(1, 4);

        animator.SetTrigger("HitFace" + eventNumber.ToString());
        //Debug.Log("HitFace " + eventNumber.ToString());
    }
    public void GirlWinAnimation()
    {
        animator.SetTrigger("Win");
        animator.SetBool("LevelPassed", true);
    }
    public void GirlLoseAnimation()
    {
        animator.SetTrigger("Lose");
    }

    IEnumerator LerpSmile()
    {
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration && canEmote0)
        {
            emotions[0] = Convert.ToInt32(Mathf.Lerp(0, 100, timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;

            // Apply Smile.
            skinnedMeshRenderer.SetBlendShapeWeight(0, emotions[0]);
            yield return null;

        }
        canEmote0 = false;

        // Return to Base emotion.
        yield return new WaitForSeconds(1.0f);

        while(timeElapsed > 0)
        {
            emotions[0] = Convert.ToInt32(Mathf.Lerp(0, 100, timeElapsed / lerpDuration));
            timeElapsed -= Time.deltaTime;

            // Revert Smile.
            skinnedMeshRenderer.SetBlendShapeWeight(0, emotions[0]);
            yield return null;
        }
    }
    IEnumerator LerpFrown()
    {
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration && canEmote1)
        {
            emotions[1] = Convert.ToInt32(Mathf.Lerp(0, 100, timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;

            // Apply Smile.
            skinnedMeshRenderer.SetBlendShapeWeight(1, emotions[1]);
            yield return null;
        }
        canEmote1 = false;

        // Return to Base emotion.
        yield return new WaitForSeconds(1.0f);

        while (timeElapsed > 0)
        {
            emotions[1] = Convert.ToInt32(Mathf.Lerp(0, 100, timeElapsed / lerpDuration));
            timeElapsed -= Time.deltaTime;

            // Revert Smile.
            skinnedMeshRenderer.SetBlendShapeWeight(1, emotions[1]);
            yield return null;
        }
        yield break;

    }
    IEnumerator LerpConfused()
    {
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration && canEmote2)
        {
            emotions[2] = Convert.ToInt32(Mathf.Lerp(0, 100, timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;

            // Apply Smile.
            skinnedMeshRenderer.SetBlendShapeWeight(2, emotions[2]);
            yield return null;
        }
        canEmote2 = false;

        // Return to Base emotion.
        yield return new WaitForSeconds(1.0f);

        while (timeElapsed > 0)
        {
            emotions[2] = Convert.ToInt32(Mathf.Lerp(0, 100, timeElapsed / lerpDuration));
            timeElapsed -= Time.deltaTime;

            // Revert Smile.
            skinnedMeshRenderer.SetBlendShapeWeight(2, emotions[2]);
            yield return null;

        }
        yield break;

    }
    
    // Called via "Hit Face" animations.
    public void ExpressFacially(int emotionInput)
    {
        emotionNumber = emotionInput;
        switch (emotionNumber)
        {
            case 0:
                canEmote0 = true;
                StartCoroutine(LerpSmile());
                break;

            case 1:
                canEmote1 = true;
                StartCoroutine(LerpFrown());
                break;

            case 2:
                canEmote2 = true;
                StartCoroutine(LerpConfused());
                break;
        }

    }


    public void SmileOrFrown()
    {
        gameManager.SaveProgress();

        if (gameManager.levelPassed)
        {
            gameManager.ElephantLevelPassed();
            StartCoroutine(SmileOnWin());
        }
        else
        {
            gameManager.ElephantLevelFailed();
            StartCoroutine(FrownOnLose());
        }
    }
    IEnumerator SmileOnWin()
    {
                                                                                                                                // print("checkpoint: IEnum - SmileOnWin()");
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
        {
            emotions[0] = Convert.ToInt32(Mathf.Lerp(0, 100, timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;
            skinnedMeshRenderer.SetBlendShapeWeight(0, emotions[0]);

            yield return null;
        }
    }
    IEnumerator FrownOnLose()
    {
                                                                                                                                //print("checkpoint: IEnum - FrownOnLose()");
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
        {
            emotions[1] = Convert.ToInt32(Mathf.Lerp(0, 100, timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;
            skinnedMeshRenderer.SetBlendShapeWeight(1, emotions[1]);

            yield return null;
        }
    }
    // Called via "Walk Drunk" animation.
    public void LevelCompletionReel()
    {
        // Play Level Completion Reel.
        if (canAnimateLevelFinished)
        {
            Instantiate(gameManager.LevelCompletionReel, gameManager.userInterface.transform, false);
            canAnimateLevelFinished = false;
        }
    }

}



/*
 ------------RECICLE BIN-------------
 


        // Get random emotion.
        //emotionNumber = UnityEngine.Random.Range(0, 3);


    
        // Apply emote change.
        if (canEmote0 && emotions[0] < 100)
        {
                emotions[0]++;
        }
        else if (canEmote0)
        { 
            canEmote0 = false;
            StartCoroutine(ResetEmotions());
        }
        //////////////
        if (canEmote1 && emotions[1] < 100)
        {
            emotions[1]++;
        }
        else if (canEmote1)
        {
            canEmote0 = false;
            StartCoroutine(ResetEmotions());

        }
        /////////////
        if (canEmote2 && emotions[2] < 100)
        {
            emotions[2]++;
        }
        else if (canEmote2)
        {
            canEmote0 = false;
            StartCoroutine(ResetEmotions());

        }

        // Update ShapeWeights.
        skinnedMeshRenderer.SetBlendShapeWeight(0, emotions[0]);
        skinnedMeshRenderer.SetBlendShapeWeight(1, emotions[1]);
        skinnedMeshRenderer.SetBlendShapeWeight(2, emotions[2]);

    }

    IEnumerator ResetEmotions()
    {
        yield return new WaitForSeconds(3);
        emotions[0] = 0;
        emotions[1] = 0;
        emotions[2] = 0;

    }
    public void ExpressFacially()
    {
        emotionNumber = Random.Range(0, 3);

        switch (emotionNumber)
        {
            case 0:
                canEmote0 = true;
                break;

            case 1:
                canEmote1 = true;
                break;

            case 2:
                canEmote2 = true;
                break;
        }




    }


















    // AFTER LERP BİN
           // private AgentMoveTo agentMoveTo;
           // agentMoveTo = GetComponent<AgentMoveTo>();




            float girlSpeed = Mathf.Clamp(agentMoveTo.agent.velocity.magnitude * 1.5f, 0f, 2.5f);
            animator.SetFloat("Speed", girlSpeed);


         // speed for movement and Idle.
    // need a trigger (like arriving at NavMesh destination) to sit.
    // need Level Complete Trigger to standUp.
     
     


            // Get a Girl's speed and max speed.
   //     float speed = agentMoveTo.gameObject.
   //     float maxSpeed = 2.5f; //agentMoveTo.agent.speed;
        //float normalizedSpeed = 

   //     float normalizedSpeed = speed / maxSpeed;
   //     animator.speed = normalizedSpeed;

   //     Debug.Log(normalizedSpeed);
     */
