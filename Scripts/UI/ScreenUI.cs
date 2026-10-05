using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScreenUI : MonoBehaviour
{
    [SerializeField] private TMP_Text ScreenUICoinsText;

    private void Start()
    {
        if (EconomyManager.Instance == null)
        {
            Debug.LogError("EconomyManager not found!");
            return;
        }
        

        EconomyManager.Instance.onCoinsChanged += updateCoinsUI;
        

        updateCoinsUI();
    }

    private void OnDestroy()
    {
        if (EconomyManager.Instance == null)
        {
            
            return;
        }
        
        EconomyManager.Instance.onCoinsChanged -= updateCoinsUI;
        
        
    }

    private void updateCoinsUI()
    {
        ScreenUICoinsText.text = (EconomyManager.Instance.totalCoins).ToString();
         
    }

    public void MainMenu()
    {
        AudioManager.Instance.PlaySFX(5);
        SceneManager.LoadSceneAsync("MainMenu");
    }


}
