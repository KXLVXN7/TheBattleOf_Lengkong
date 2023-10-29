using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseResume : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject Paures;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameIsPaused)
            {
                Pause();
            }
        }
    }

    private void Start()
    {
        GameIsPaused = true;
        Debug.Log("Kondisi Pause");
    }
    public void Resume()
    {
        Debug.Log("Lanjut");
        Time.timeScale = 1f;
        GameIsPaused = false;
        Paures.SetActive(false);

    }

    public void Pause()
    {
        Debug.Log("Sedang Melakukan Pause !");
        Time.timeScale = 0f;
        GameIsPaused = true;
        Paures.SetActive(true);


    }

    public void OnPlayButtonClicked()
    {
        Resume();
    }





}