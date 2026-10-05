using System.Collections;
using UnityEngine;


public class Ship : MonoBehaviour, IInteractable
{
    [SerializeField] private int endShipCost;
    [SerializeField] private GameObject notEnoughCoinsIndicator;
    [SerializeField] private MachineFeedbackUI FeedbackUI;
    [SerializeField] private GameObject EndScreen;
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private CanvasGroup endScreenCanvasGroup;
    private bool hasShipSailed = false;


    public void interact()
    {
        if (hasShipSailed)
        {
            Debug.LogError("Ship has already sailed");
            return;
        }

        if (EconomyManager.Instance == null)
        {
            Debug.LogError("EconomyManager not found!");
            return;
        }
        if (endShipCost>EconomyManager.Instance.totalCoins)
        {
            int moreMoneyNeeded = endShipCost - EconomyManager.Instance.totalCoins;
            FeedbackUI.ShowNotEnoughMoney(moreMoneyNeeded);
            StartCoroutine(dialogBoxRoutine());
            return;
        }
        sailShip();
    }

    

    private void sailShip()
    {
        EconomyManager.Instance.SpendCoins(endShipCost);
        StartCoroutine(FadeInEndScreen());
    }

    private IEnumerator FadeInEndScreen()
    {
        EndScreen.SetActive(true);

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            float alpha = Mathf.Clamp01(elapsed / fadeDuration);

            endScreenCanvasGroup.alpha = alpha;

            yield return null;
        }

        endScreenCanvasGroup.alpha = 1f;
    }

    private IEnumerator dialogBoxRoutine()
    {
        notEnoughCoinsIndicator.SetActive(true);
        yield return new WaitForSeconds(1f);
        notEnoughCoinsIndicator.SetActive(false);
    }
}
