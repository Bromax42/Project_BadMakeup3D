using UnityEngine;
using UnityEngine.UI;

public class UIFunctions : MonoBehaviour
{
    public GameObject pausePanel;



    public void PausePanel()
    {
        if (pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1; 
        }
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }


}
