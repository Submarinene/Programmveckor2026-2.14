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
    public void pussel1()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void pussel2()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void pussel3()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void TeaterScen1()
    {
        SceneManager.LoadSceneAsync(4);
    }
    public void TeaterScen2()
    {
        SceneManager.LoadSceneAsync(5);
    }

   public void doExitGame()
    {
        Application.Quit();
    }

}
