using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject reloadBtn;
    public void Return()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        ToggleWin(false);
        ToggleLose(false);
    }

    public void Reload()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToggleWin(bool toggle)
    {
        if (toggle)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        winPanel.SetActive(toggle);
    }

    public void ToggleLose(bool toggle)
    {
        if (toggle) 
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        losePanel.SetActive(toggle);
    }
}
