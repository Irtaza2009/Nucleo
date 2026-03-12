using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public List<UpgradeData> allUpgrades;

    public UpgradeCard[] cards;

    public GameObject upgradePanel;

    public PlayerCore player;
    public RadiationShooter shooter;

    [Header("Card Sprites")]
    public Sprite commonCardSprite;
    public Sprite rareCardSprite;
    public Sprite legendaryCardSprite;

    [Header("Pity System")]
    public float rarePityPerWave = 5f;
    public float legendaryPityPerWave = 2f;

    private float rarePityBonus = 0f;
    private float legendaryPityBonus = 0f;

    public void ShowUpgrades()
    {
        if (upgradePanel == null || cards == null || cards.Length == 0 || allUpgrades == null || allUpgrades.Count == 0)
            return;

        // Increase pity before generating this wave's cards
        rarePityBonus += rarePityPerWave;
        legendaryPityBonus += legendaryPityPerWave;

        upgradePanel.SetActive(true);
        Time.timeScale = 0f;

        List<UpgradeData> choices = new List<UpgradeData>();
        int maxChoices = Mathf.Min(cards.Length, allUpgrades.Count);

        while (choices.Count < maxChoices)
        {
            UpgradeData random = GetWeightedRandomUpgrade();

            if (!choices.Contains(random))
                choices.Add(random);
        }

        // Reset pity counters if rare/legendary appeared as options
        bool rareAppeared = choices.Exists(c => c.rarity == Rarity.Rare);
        bool legendaryAppeared = choices.Exists(c => c.rarity == Rarity.Legendary);
        if (rareAppeared) rarePityBonus = 0f;
        if (legendaryAppeared) legendaryPityBonus = 0f;

        for (int i = 0; i < cards.Length; i++)
        {
            if (i < choices.Count)
            {
                cards[i].gameObject.SetActive(true);
                cards[i].Setup(choices[i], this);
            }
            else
            {
                cards[i].gameObject.SetActive(false);
            }
        }
    }

    UpgradeData GetWeightedRandomUpgrade()
    {
        // Filter upgrades by rarity
        List<UpgradeData> commonUpgrades = new List<UpgradeData>();
        List<UpgradeData> rareUpgrades = new List<UpgradeData>();
        List<UpgradeData> legendaryUpgrades = new List<UpgradeData>();

        foreach (UpgradeData upgrade in allUpgrades)
        {
            switch (upgrade.rarity)
            {
                case Rarity.Common:
                    commonUpgrades.Add(upgrade);
                    break;
                case Rarity.Rare:
                    rareUpgrades.Add(upgrade);
                    break;
                case Rarity.Legendary:
                    legendaryUpgrades.Add(upgrade);
                    break;
            }
        }

        // Generate random number for weighted selection
        float randomValue = Random.Range(0f, 100f);

        // Base: 70% common, 25% rare, 5% legendary — adjusted by pity (capped)
        float rareThreshold = Mathf.Min(25f + rarePityBonus, 60f);
        float legendaryThreshold = Mathf.Min(5f + legendaryPityBonus, 30f);
        float commonThreshold = Mathf.Max(100f - rareThreshold - legendaryThreshold, 10f);

        if (randomValue < commonThreshold && commonUpgrades.Count > 0)
        {
            return commonUpgrades[Random.Range(0, commonUpgrades.Count)];
        }
        else if (randomValue < commonThreshold + rareThreshold && rareUpgrades.Count > 0)
        {
            return rareUpgrades[Random.Range(0, rareUpgrades.Count)];
        }
        else if (legendaryUpgrades.Count > 0)
        {
            return legendaryUpgrades[Random.Range(0, legendaryUpgrades.Count)];
        }

        // Fallback if no upgrades in selected rarity
        if (commonUpgrades.Count > 0)
            return commonUpgrades[Random.Range(0, commonUpgrades.Count)];
        if (rareUpgrades.Count > 0)
            return rareUpgrades[Random.Range(0, rareUpgrades.Count)];
        
        return allUpgrades[Random.Range(0, allUpgrades.Count)];
    }

    public Sprite GetSpriteForRarity(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return commonCardSprite;
            case Rarity.Rare:
                return rareCardSprite;
            case Rarity.Legendary:
                return legendaryCardSprite;
            default:
                return commonCardSprite;
        }
    }

    public void ApplyUpgrade(UpgradeData upgrade)
    {
        switch (upgrade.upgradeType)
        {
            case UpgradeType.AlphaDamage:
                shooter.alphaDamageMultiplier += upgrade.value;
                break;

            case UpgradeType.BetaDamage:
                shooter.betaDamageMultiplier += upgrade.value;
                break;

            case UpgradeType.GammaDamage:
                shooter.gammaDamageMultiplier += upgrade.value;
                break;
            case UpgradeType.AlphaRange:
                shooter.alphaRangeMultiplier += upgrade.value;
                break;
            case UpgradeType.BetaRange:
                shooter.betaRangeMultiplier += upgrade.value;
                break;
            case UpgradeType.GammaRange:
                shooter.gammaRangeMultiplier += upgrade.value;
                break;
            case UpgradeType.EnergyMax:
                player.AddMaxEnergy(upgrade.value);
                break;
            case UpgradeType.EnergyRegen:
                player.energyRegen += upgrade.value;
                break;
            case UpgradeType.MaxHealth:
                player.AddMaxHealth(upgrade.value);
                break;
            case UpgradeType.Health:
                player.Heal(upgrade.value);
                break;
        }

        CloseUpgrades();
    }

    public void CloseUpgrades()
    {
        upgradePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
