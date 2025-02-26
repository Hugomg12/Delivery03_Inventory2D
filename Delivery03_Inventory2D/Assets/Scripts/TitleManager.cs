using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
             Quit();
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
    public static void Quit()
    {
        Debug.Log("Saliendo del juego...");
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
}

    public void StartGame()
    {
        // Carga la escena de Gameplay
        SceneManager.LoadScene("Gameplay");
    }
    
}