using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public static MarketManager Instance { get; private set; }
    private void Awake() => Instance = this;

    private const string dataFileName = "PlayerStats";
    public Data data;
    public UpgradeHandler[] upgradeHandler;
    public TextMeshProUGUI moneyText;

    public float saveTime;
    // Update is called once per frame
    void Update()
    {
        moneyText.text = $"{data.money:0}";

        saveTime += Time.deltaTime * (1 / Time.timeScale);
        if (saveTime >= 5)
        {
            SaveSystem.SaveData(data, dataFileName);
            saveTime = 0;
        }   
    }
    // Start is called before the first frame update
    void Start()
    {
        data = SaveSystem.SaveExists(dataFileName)
               ? SaveSystem.LoadData<Data>(dataFileName)
               : new Data();

        upgradeHandler[0].UpgradesNames = new string[] { "Sıradan Öğrenci", "Hızlı Öğrenci", "Tank Öğrenci" };
        upgradeHandler[0].UpgradesCostMultiplier = new float[,]
        {
            { 1.82f, 1.82f, 1.82f, 1.82f },
            {1.82f, 1.82f, 1.82f, 1.82f },
            {1.719f, 1.719f, 1.585f, 1.585f}
        };
        upgradeHandler[0].UpgradesPowerMultiplier = new float[,]
        {
            { 1.38f, 1.38f, 1.15f, 1.085f },
            { 1.38f, 1.38f, 1.15f, 1.06f },
            {1.38f, 1.38f, 1.15f, 1.06f}
        };
        upgradeHandler[0].UpgradesPower = new float[,] { 
            { 10 * Mathf.Pow( upgradeHandler[0].UpgradesCostMultiplier[0,0] ,data.upgradeLevels[0][0]), 100 * Mathf.Pow( upgradeHandler[0].UpgradesCostMultiplier[0,1] ,data.upgradeLevels[0][1]), 1 * Mathf.Pow(upgradeHandler[0].UpgradesCostMultiplier[0, 2], data.upgradeLevels[0][2]), 2f * Mathf.Pow(upgradeHandler[0].UpgradesCostMultiplier[0, 3], data.upgradeLevels[0][3]) },
            { 20, 50, 1.5f, 3f },
            { 50, 250, 0.5f, 1.5f } 
        };
        upgradeHandler[0].UpgradesCost = new float[,] { 
            { 250, 250, 1000, 1000 },
            { 500, 500, 2000, 2000 },
            { 1000, 1000, 5000, 5000 } 
        };
        
        CreateUpgrades(data.upgradeLevels, 0);
        
        void CreateUpgrades<T>(List<T> level, int index)
        {
            for (int i = 0; i < level.Count; i++)
            {
                Upgrades upgrade = Instantiate(upgradeHandler[index].UpgradePrefab, upgradeHandler[index].UpgradesPanel);
                upgrade.upgradeId = i;
                upgradeHandler[index].Upgrades.Add(upgrade);
            }
            upgradeHandler[index].UpgradesScroll.normalizedPosition = Vector3.zero;
        }

        UpdateUpgradeUI("Student");
    }
    public void UpdateUpgradeUI(string type, int upgradeId = -1)
    {
        switch (type)
        {
            case "Student":
                UpdateAllUI(upgradeHandler[0].Upgrades, upgradeHandler[0].UpgradesNames, 0);
                break;
            default:
                break;
        }


        void UpdateAllUI(List<Upgrades> upgrades, string[] upgradeNames, int index)
        {
            if (upgradeId == -1)
                for (int i = 0; i < upgradeHandler[index].Upgrades.Count; i++)
                    UpdateUI(i);
            else UpdateUI(upgradeId);

            void UpdateUI(int id)
            {
                upgrades[id].nameText.text = upgradeNames[id];
                upgrades[id].damageText.text = $"Hasar: {upgradeHandler[0].UpgradesPower[id, 0]:0} -> {upgradeHandler[0].UpgradesPower[id, 0] * upgradeHandler[0].UpgradesPowerMultiplier[id, 0]:0}";
                upgrades[id].healthText.text = $"Can: {upgradeHandler[0].UpgradesPower[id, 1]:0} -> {upgradeHandler[0].UpgradesPower[id, 1] * upgradeHandler[0].UpgradesPowerMultiplier[id, 1]:0}";
                upgrades[id].speedText.text = $"Hız: {upgradeHandler[0].UpgradesPower[id, 2]:0.0} -> {upgradeHandler[0].UpgradesPower[id, 2] * upgradeHandler[0].UpgradesPowerMultiplier[id, 2]:0.0}";
                upgrades[id].rangeText.text = $"Menzil: {upgradeHandler[0].UpgradesPower[id, 3]:0.0} -> {upgradeHandler[0].UpgradesPower[id, 3] * upgradeHandler[0].UpgradesPowerMultiplier[id, 3]:0.0}";
                upgrades[id].damageCostText.text = $"{UpgradeCost("Damage",id):0}";
                upgrades[id].healthCostText.text = $"{UpgradeCost("Health", id):0}";
                upgrades[id].speedCostText.text = $"{UpgradeCost("Speed", id):0}";
                upgrades[id].rangeCostText.text = $"{UpgradeCost("Range", id):0}";
            }

        }
    }
    public float UpgradeCost(string type, int upgradeId)
    {
        return type switch
        {
            "Damage" => Cost(0, 0, data.upgradeLevels[upgradeId]),
            "Health" => Cost(0, 1, data.upgradeLevels[upgradeId]),
            "Speed" => Cost(0, 2, data.upgradeLevels[upgradeId]),
            "Range" => Cost(0, 3, data.upgradeLevels[upgradeId]),
            _ => 0,
        };
        float Cost(int index,int statIndex, List<int> levels)
        {
            return upgradeHandler[index].UpgradesCost[upgradeId, statIndex]
                * Mathf.Pow( upgradeHandler[index].UpgradesCostMultiplier[upgradeId, statIndex] , levels[statIndex]);
        }
    }
    public void BuyUpgrade(string type, int upgradeId)
    {
        switch (type)
        {
            case "Damage":
                Buy(data.upgradeLevels[upgradeId], 0);
                break;
            case "Health":
                Buy(data.upgradeLevels[upgradeId], 1);
                break;
            case "Speed":
                Buy(data.upgradeLevels[upgradeId], 2);
                break;
            case "Range":
                Buy(data.upgradeLevels[upgradeId], 3);
                break;
            default:
                break;
        }
        void Buy(List<int> upgrades, int index)
        {
            if (data.money >= UpgradeCost(type, upgradeId))
            {
                data.money -= UpgradeCost(type, upgradeId);
                upgrades[index] += 1;

            }
            UpdateUpgradeUI("Student", upgradeId);
        }
    }
}
