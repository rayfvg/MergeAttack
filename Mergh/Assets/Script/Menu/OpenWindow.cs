using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenWindow : MonoBehaviour
{
   public void OpenWindowInGame(GameObject gameObject)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseWindowInGame(GameObject gameObject)
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
