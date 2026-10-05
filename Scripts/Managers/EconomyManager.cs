using System;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance { get; private set; }

    public int totalCoins { get; private set; }


    public event Action onCoinsChanged;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void OnEnable()
    {
        Crate.onCrateBroken += tallyCoins;
    }

    private void OnDisable()
    {
        Crate.onCrateBroken -= tallyCoins;
    }

    private void tallyCoins()
    {
        AddCoins(PlayerStats.Instance.CrateReward);
    }
    public void AddCoins(int amount)
    {
        totalCoins += amount;
        Debug.Log("Coins Added" + totalCoins);
        onCoinsChanged?.Invoke();
    }

    public bool SpendCoins(int amount)
    {
        if (totalCoins < amount)
        {
            Debug.Log("Not enough money ! ");
            return false;
        }
        totalCoins -= amount;
        onCoinsChanged?.Invoke();
        return true;

    }
}