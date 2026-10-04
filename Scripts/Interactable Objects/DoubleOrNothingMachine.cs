using UnityEngine;
using System.Collections;

public class DoubleOrNothingMachine : MonoBehaviour, IInteractable
{
    [Header("Machine Variables")]
    [SerializeField] private int operatingCost = 100;

    [Header("Gambling")]
    [SerializeField] private int winChance = 47;
    [SerializeField] private float resultDelay = 3f;

    [Header("UI")]
    [SerializeField] private DoubleOrNothingUI doubleOrNothingUI;
    [SerializeField] private MachineFeedbackUI feedbackUI;
    [SerializeField] private GameObject dialogueBox;

    [Header("Particles")]
    [SerializeField] private ParticleSystem gambleParticles;

    private bool isGambling = false;
    private bool waitingForChoice = false;

    private int currentPayout = 0;

    public void interact()
    {
        if (isGambling)
        {
            Debug.Log("Machine is currently gambling!");
            return;
        }

        if (waitingForChoice)
        {
            OpenDecisionUI("YOU WON!");
            return;
        }

        StartCoroutine(StartNewGamble());
    }

    private IEnumerator StartNewGamble()
    {
        isGambling = true;

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

        currentPayout = operatingCost;

        Debug.Log("================================");
        Debug.Log("NEW GAMBLE");
        Debug.Log("Current Pot: " + currentPayout);

        StartParticles();
        yield return Flip();
        StopParticles();
        isGambling = false;
    }

    public void GambleAgain()
    {
        if (isGambling)
        {
            Debug.Log("Already gambling!");
            return;
        }

        if (!waitingForChoice)
        {
            Debug.Log("There is no active pot!");
            return;
        }

        AudioManager.Instance.PlaySFX(7);
        waitingForChoice = false;

        StartCoroutine(GambleCurrentPot());
    }

    private IEnumerator GambleCurrentPot()
    {
        isGambling = true;

        yield return Flip();

        isGambling = false;
    }

    private IEnumerator Flip()
    {
        int playerLuckBonus = 0;

        if (PlayerStats.Instance != null)
        {
            playerLuckBonus = PlayerStats.Instance.Luck;
        }

        int finalWinChance = winChance + playerLuckBonus;


        int roll = Random.Range(0, 100);

        yield return new WaitForSecondsRealtime(resultDelay);

        

        if (roll < finalWinChance)
        {
            WinCondition();
        }
        else
        {
            LoseCondition();
        }
    }

    private void WinCondition()
    {
        currentPayout *= 2;

        waitingForChoice = true;

        feedbackUI.ShowWin(currentPayout);
        AudioManager.Instance.PlaySFX(2);
        Debug.Log("YOU WIN!");
        Debug.Log("Current Pot: " + currentPayout);

        OpenDecisionUI("YOU WIN!");
    }

    private void LoseCondition()
    {
        feedbackUI.ShowLose();
        currentPayout = 0;
        waitingForChoice = false;

        doubleOrNothingUI.Close();
        AudioManager.Instance.PlaySFX(6);
        Debug.Log("Your entire pot is gone!");
    }

    private void OpenDecisionUI(string result)
    {
        AudioManager.Instance.PlaySFX(9);
        doubleOrNothingUI.Open(
            this,
            currentPayout,
            result
        );
    }

    public void CashOut()
    {
        if (!waitingForChoice)
        {
            AudioManager.Instance.PlaySFX(6);
            Debug.Log("There is nothing to cash out!");
            return;
        }

        if (currentPayout <= 0)
        {
            Debug.Log("No winnings available!");
            return;
        }
        AudioManager.Instance.PlaySFX(7);
        EconomyManager.Instance.AddCoins(currentPayout);

        Debug.Log("CASHED OUT!");
        Debug.Log("Received: " + currentPayout + " coins");

        currentPayout = 0;
        waitingForChoice = false;
    }
    private IEnumerator diaogueBoxRoutine()
    {
        dialogueBox.SetActive(true);
        yield return new WaitForSeconds(1);
        dialogueBox.SetActive(false);
    }
    private void StartParticles()
    {
        if (gambleParticles != null)
        {
            gambleParticles.Play();
        }
    }
    private void StopParticles()
    {
        if (gambleParticles != null)
        {
            gambleParticles.Stop();
        }
    }
}