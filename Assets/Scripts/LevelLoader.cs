using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GoMainMenu()
    {

    }

    public void RestartLevel()
    {
        StartCoroutine(WaitAndLoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadNextLevel()
    {
        StartCoroutine(WaitAndLoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator WaitAndLoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

}
