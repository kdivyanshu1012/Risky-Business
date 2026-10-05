using UnityEngine;
using UnityEngine.SceneManagement;
public class CreditsUIManager : MonoBehaviour
{
    [SerializeField] private string link1;
    [SerializeField] private string link2;
    public void MainMenu()
    {
        AudioManager.Instance.PlaySFX(4);
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void OpenLink1(string url)
    {
        url = link1;
        AudioManager.Instance.PlaySFX(4);
        Application.OpenURL(url);
    }
    public void OpenLink2(string url)
    {
        url = link2;
        AudioManager.Instance.PlaySFX(4);
        Application.OpenURL(url);
    }

}
