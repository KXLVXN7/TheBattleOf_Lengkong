using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
/*    public PauseResume pauseResumeScript;*/
    public void ChangeScenee(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        /*    pauseResumeScript.Resume();*/
        Destroy(gameObject);
    }
    public void QuitApp()
    {
        Application.Quit();
        Destroy(gameObject); // Hapus salinan ganda GameManager
    }
/*    public void DestroyAll()
    {
        DestroyObject(gameObject);
    }*/
}
