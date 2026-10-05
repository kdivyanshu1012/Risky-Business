using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    public void PlayButton()
    {
        AudioManager.Instance.PlaySFX(4);
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void CreditsButton()
    {
        AudioManager.Instance.PlaySFX(4);
        SceneManager.LoadSceneAsync("CreditsScene");
    }
    public void ExitButton()
    {
        AudioManager.Instance.PlaySFX(4);
        Application.Quit();
    }
}
