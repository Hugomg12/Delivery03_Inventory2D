using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameEnd : MonoBehaviour
{
    void Update(){

        if (Input.GetKeyDown(KeyCode.Escape))
        {
             TitleManager.Quit();
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}