using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    [Header("Player Luck")]
    [SerializeField] int luck = 0;
    [SerializeField] int maxLuck = 5;
    [SerializeField] int luckMultiplier = 5;

    [Header("Luck Upgrade Cost")]
    [SerializeField] private int luckBaseCost = 25;
    [SerializeField] private int luckCostMultiplier = 2;

    [Header("Movement Speed")]
    [SerializeField] private int movementSpeedLevel = 0;
    [SerializeField] private int maxMovementSpeedLevel = 5;
    [SerializeField] private float baseMovementSpeed = 5f;
    [SerializeField] private float movementSpeedIncrease = 0.5f;

    [Header("Movement Speed Upgrade Cost")]
    [SerializeField] private int movementSpeedBaseCost = 20;
    [SerializeField] private int movementSpeedCostMultiplier = 2;

    [Header("Crate Cooldown")]
    [SerializeField] private int crateCooldownLevel = 0;
    [SerializeField] private int maxCrateCooldownLevel = 5;
    [SerializeField] private float baseCrateCooldown = 3f;
    [SerializeField] private float crateCooldownReduction = 0.5f;
    
    [Header("Crate Cooldown Upgrade Cost")]
    [SerializeField] private int crateCooldownBaseCost = 30;
    [SerializeField] private int crateCooldownCostMultiplier = 2;

    [Header("Crate Reward")]
    [SerializeField] private int crateRewardLevel = 0;
    [SerializeField] private int maxCrateRewardLevel = 5;
    [SerializeField] private int baseCrateReward = 5;
    [SerializeField] private int crateRewardIncrease = 2;

    [Header("Crate Reward Upgrade Cost")]
    [SerializeField] private int crateRewardBaseCost = 40;
    [SerializeField] private int crateRewardCostMultiplier = 2;

    [Header("UI Exposing variables")]
    public int LuckLevel => luck;
    public int MaxLuckLevel => maxLuck;

    public int MovementSpeedLevel => movementSpeedLevel;
    public int MaxMovementSpeedLevel => maxMovementSpeedLevel;

    public int CrateCooldownLevel => crateCooldownLevel;
    public int MaxCrateCooldownLevel => maxCrateCooldownLevel;

    public int CrateRewardLevel => crateRewardLevel;
    public int MaxCrateRewardLevel => maxCrateRewardLevel;

    [Header("actual scripts getters")]

    public int Luck => luck * luckMultiplier;
    public float MovementSpeed => baseMovementSpeed + (movementSpeedLevel * movementSpeedIncrease);

    public float CrateCooldown => Mathf.Max(0.5f, baseCrateCooldown - (crateCooldownLevel * crateCooldownReduction));

    public int CrateReward => baseCrateReward + (crateRewardLevel * crateRewardIncrease);

    public int LuckUpgradeCost => luckBaseCost * Mathf.RoundToInt(Mathf.Pow(luckCostMultiplier, luck));

    public int MovementSpeedUpgradeCost => movementSpeedBaseCost * Mathf.RoundToInt( Mathf.Pow(movementSpeedCostMultiplier, movementSpeedLevel));

    public int CrateCooldownUpgradeCost => crateCooldownBaseCost * Mathf.RoundToInt(Mathf.Pow(crateCooldownCostMultiplier, crateCooldownLevel));

    public int CrateRewardUpgradeCost => crateRewardBaseCost * Mathf.RoundToInt(Mathf.Pow(crateRewardCostMultiplier, crateRewardLevel));




    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void increasePlayerLuck()
    {
        if(luck >= maxLuck)
        {
            Debug.Log("Luck is already maxed!");
            return;
        }

        int cost = LuckUpgradeCost;

        if (!EconomyManager.Instance.SpendCoins(cost))
        {
            Debug.Log("Not enough coins for Luck upgrade!");
            return;
        }

        luck++;

        Debug.Log(
            "Luck Increased! " +
            "Level: " + luck +
            " | Bonus: " + Luck + "%" +
            " | Cost: " + cost
        );
    }
    public void increaseMovementSpeed()
    {
        if (movementSpeedLevel >= maxMovementSpeedLevel)
        {
            Debug.Log("Movement Speed is already maxed!");
            return;
        }

        int cost = MovementSpeedUpgradeCost;

        if (!EconomyManager.Instance.SpendCoins(cost))
        {
            Debug.Log("Not enough coins for Movement Speed upgrade!");
            return;
        }

        movementSpeedLevel++;

        Debug.Log(
            "Movement Speed Increased! " +
            "Level: " + movementSpeedLevel +
            " | Speed: " + MovementSpeed +
            " | Cost: " + cost
        );
    }
    public void decreaseCrateCooldown()
    {
        if (crateCooldownLevel >= maxCrateCooldownLevel)
        {
            Debug.Log("Crate Cooldown is already maxed!");
            return;
        }

        int cost = CrateCooldownUpgradeCost;

        if (!EconomyManager.Instance.SpendCoins(cost))
        {
            Debug.Log("Not enough coins for Crate Cooldown upgrade!");
            return;
        }

        crateCooldownLevel++;

        Debug.Log(
            "Crate Cooldown Improved! " +
            "Level: " + crateCooldownLevel +
            " | Cooldown: " + CrateCooldown +
            "s | Cost: " + cost
        );
    }
    public void increaseCrateReward()
    {
        if (crateRewardLevel >= maxCrateRewardLevel)
        {
            Debug.Log("Crate Reward is already maxed!");
            return;
        }

        int cost = CrateRewardUpgradeCost;

        if (!EconomyManager.Instance.SpendCoins(cost))
        {
            Debug.Log("Not enough coins for Crate Reward upgrade!");
            return;
        }

        crateRewardLevel++;

        Debug.Log(
            "Crate Reward Increased! " +
            "Level: " + crateRewardLevel +
            " | Reward: " + CrateReward +
            " | Cost: " + cost
        );
    }

}
