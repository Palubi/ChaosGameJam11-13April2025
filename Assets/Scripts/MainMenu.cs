using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject screen2;
    [SerializeField] private int sceneIndex;
    public void StarGame()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Options()
    {
        screen2.SetActive(true);
    }

    public void Back()
    {
        screen2.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
