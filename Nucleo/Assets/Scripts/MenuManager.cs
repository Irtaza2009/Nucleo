using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayButton()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonPress();

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void ControlsButton()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonPress();

        UnityEngine.SceneManagement.SceneManager.LoadScene("Controls");
    }

    public void ControlsReturnButton()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonPress();

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
