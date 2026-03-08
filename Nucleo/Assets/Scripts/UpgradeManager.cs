using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public List<UpgradeData> allUpgrades;

    public UpgradeCard[] cards;

    public GameObject upgradePanel;

    public PlayerCore player;
    public RadiationShooter shooter;

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
            UpgradeData random = allUpgrades[Random.Range(0, allUpgrades.Count)];

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
