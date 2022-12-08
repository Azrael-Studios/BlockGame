using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Pause()
    {
        Time.timeScale = 0.0000000000000f; // A lot of zeros!
        pauseMenu.SetActive(true);
    }

    public void Lose(GameObject deadMenu)
    {
        Time.timeScale = 0;
        deadMenu.SetActive(true);
    }

    public void Run()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}