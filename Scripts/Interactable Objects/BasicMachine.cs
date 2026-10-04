using System.Collections;
using UnityEngine;

public class BasicMachine : MonoBehaviour, IInteractable
{
    [Header("Machine")]
    [SerializeField] private int operatingCost = 20;
    [SerializeField] private int winAmount = 40;
    [SerializeField] private int winChance = 52;
    [SerializeField] private float resultDelay = 2f;

    [Header("Feedback")]
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private MachineFeedbackUI feedbackUI;
    [SerializeField] private ParticleSystem particleEffect;

    private bool isGambling = false;
    

    public void interact()
    {
        Debug.Log("Interacting with Basic Machine");

        if (isGambling)
        {
            Debug.Log("Already gambling!");
            return;
        }

        StartCoroutine(gamble());
    }

    private IEnumerator gamble()
    {
        isGambling = true;

        if (PlayerStats.Instance == null)
        {
            Debug.LogError("PlayerStats not found!");

            isGambling = false;
            yield break;
        }

        if (EconomyManager.Instance == null)
        {
            Debug.LogError("EconomyManager not found!");
            
            isGambling = false;
            yield break;
        }

        
        if (!EconomyManager.Instance.SpendCoins(operatingCost))
        {
            int moneyNeeded = operatingCost - EconomyManager.Instance.totalCoins;
            feedbackUI.ShowNotEnoughMoney(moneyNeeded);
            isGambling = false;
            StartCoroutine(diaogueBoxRoutine());
            yield break;
        }

       
        yield return new WaitForSeconds(0.5f);

        int luck = PlayerStats.Instance.Luck;
        int finalWinChance = winChance + luck;

        int roll = Random.Range(0, 100);

        Debug.Log("Flipping...");
        Debug.Log("Win Chance: " + finalWinChance + "%");
        Debug.Log("Roll: " + roll);
        particleEffect.Play();
        yield return new WaitForSeconds(resultDelay);
        particleEffect.Stop();

        if (roll < finalWinChance)
        {
            winCondition();
        }
        else
        {
            loseCondition();
        }

        isGambling = false;
    }

    private IEnumerator diaogueBoxRoutine()
    {
        dialogueBox.SetActive(true);
        yield return new WaitForSeconds(1);
        dialogueBox.SetActive(false);
    }
    private void winCondition()
    {
        int payout = winAmount;
        AudioManager.Instance.PlaySFX(7);
        EconomyManager.Instance.AddCoins(payout);
        feedbackUI.ShowWin(payout);
        
    }

    private void loseCondition()
    {
        AudioManager.Instance.PlaySFX(6);
        feedbackUI.ShowLose();
    }
}