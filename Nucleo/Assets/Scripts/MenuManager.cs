using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
