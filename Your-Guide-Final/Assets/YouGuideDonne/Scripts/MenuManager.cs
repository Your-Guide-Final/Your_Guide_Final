using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public string pauseButton;
    bool isActive = false;
    float lastTimeValue = 1;
    public bool menuPause = false;
    //public AudioSource audioSource;
    //public AudioClip valideSound;


    public void SetPauseOnOff()
    {
        if (isActive)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = lastTimeValue;
            isActive = false;
            //audioSource.PlayOneShot(valideSound);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            pauseMenu.SetActive(true);
            lastTimeValue = Time.timeScale;
            Time.timeScale = 0;
            isActive = true;
            //audioSource.PlayOneShot(valideSound);
            Cursor.lockState = CursorLockMode.None;
        }

    }

    public void LoadScene(string sceneName)
    {

        SceneManager.LoadScene(sceneName);

    }

    public void CloseApli()
    {

        Application.Quit();

    }

    public void Open_CloseMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeInHierarchy);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (menuPause)
        {
            pauseMenu.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(pauseButton) && menuPause)
        {
            SetPauseOnOff();
        }
        //Debug.Log(Time.timeScale);
    }
}
