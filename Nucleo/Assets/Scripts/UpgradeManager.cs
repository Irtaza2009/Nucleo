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

    public void ShowUpgrades()
    {
        if (upgradePanel == null || cards == null || cards.Length == 0 || allUpgrades == null || allUpgrades.Count == 0)
            return;

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

        // 70% chance for common
        if (randomValue < 70f && commonUpgrades.Count > 0)
        {
            return commonUpgrades[Random.Range(0, commonUpgrades.Count)];
        }
        // 25% chance for rare (70-95)
        else if (randomValue < 95f && rareUpgrades.Count > 0)
        {
            return rareUpgrades[Random.Range(0, rareUpgrades.Count)];
        }
        // 5% chance for legendary (95-100)
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
            case UpgradeType.CoreHealth:
                player.AddMaxHealth(upgrade.value);
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
