using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string gameSceneName = "SampleScene";

    public void StartNewGame()
    {
        Debug.Log("Iniciando Nueva Partida...");
        SceneManager.LoadScene(gameSceneName);
    }

    public void LoadGame()
    {
        Debug.Log("Función Cargar Partida (aún no implementada)");
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}