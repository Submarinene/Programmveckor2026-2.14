using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Meny : MonoBehaviour
{
  public void startscen()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void StartCutscene1()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void StartCutscene2()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(2);
    }
    public void pussStartCutscene3()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void StartCutscene4()
    {
        SceneManager.LoadSceneAsync(4);
    }
    public void Cutscene1()
    {
        SceneManager.LoadSceneAsync(5);
    }
    public void pussel1()
    {
        SceneManager.LoadSceneAsync(6);
    }
    public void TestsceneEstella()
    {
        SceneManager.LoadSceneAsync(7);
    }
    public void pussel2p1()
    {
        SceneManager.LoadSceneAsync(8);
    }
    public void pussel2p2()
    {
        SceneManager.LoadSceneAsync(9);
    }
    public void cutscene2()
    {
        SceneManager.LoadSceneAsync(10);
    }
    public void cutscene3()
    {
        SceneManager.LoadSceneAsync(11);
    }
    public void finalcutscene()
    {
        SceneManager.LoadSceneAsync(14);
    }

    public void doExitGame()
    {
        Application.Quit();
    }

}
