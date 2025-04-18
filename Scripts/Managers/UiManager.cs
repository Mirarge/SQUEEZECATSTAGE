using Godot;
using SqueezecatStage.Scripts;
using System;
using System.Collections.Generic;

public partial class UiManager : Control
{
    public GameManager gameManager;

    private Label currentWaveLabel;
    private Label coinsLabel;
    private Label enemiesDefeatedLabel;
    private Button startNextWaveButton;

    private TabBar towerCategoriesTabbar;
    private GridContainer shopContainer;
    private List<ShopIcon> currentlyVisibleIcons = new List<ShopIcon>();
    private List<ShopIcon> unusedIconPool = new List<ShopIcon>();

    private PackedScene shopIconScene = GD.Load<PackedScene>("res://ObjectScenes/ShopIcon.tscn");

    private List<string> towerCategories = new List<string>();
    private Dictionary<string, List<TowerShopDefinition>> shopTabs = new Dictionary<string, List<TowerShopDefinition>>();
    

    public override void _Ready()
    {
        currentWaveLabel = GetNode<Label>("LeftSideInformationBox/CurrentWaveLabel");
        coinsLabel = GetNode<Label>("LeftSideInformationBox/CoinsLabel");
        enemiesDefeatedLabel = GetNode<Label>("LeftSideInformationBox/EnemiesDefeatedLabel");
        
        towerCategoriesTabbar = GetNode<TabBar>("TowerCategoriesTabBar");
        shopContainer = GetNode<GridContainer>("ScrollContainer/TowerContainer");

        startNextWaveButton = GetNode<Button>("NextWaveContainer/StartNextWaveButton");

        DataStorage.Instance.Connect("WaveChanged", new Callable(this, nameof(UpdateWave)));
        DataStorage.Instance.Connect("CoinsChanged", new Callable(this, nameof(UpdateCoins)));
        DataStorage.Instance.Connect("KilledEnemiesChanged", new Callable(this, nameof(UpdateKilledEnemies)));
        startNextWaveButton.Connect("pressed", new Callable(this, nameof(RequestNextWave)));

        LoadShopCategories();
        towerCategoriesTabbar.Connect("tab_changed", new Callable(this, nameof(UpdateShopListings)));
        UpdateShopListings(0);
    }
    private void LoadShopCategories()
    {
        List<TowerShopDefinition> definitions = DataStorage.Instance.towerShopDefinitions;
        foreach (TowerShopDefinition towerDefinition in definitions)
        {
            if (!towerCategories.Contains(towerDefinition.category))
            {
                towerCategories.Add(towerDefinition.category);
                towerCategoriesTabbar.AddTab(towerDefinition.category);
                shopTabs.Add(towerDefinition.category, new List<TowerShopDefinition>());
            }
            shopTabs[towerDefinition.category].Add(towerDefinition);
        }
    }

    public void RefreshShop()
    {
        UpdateShopListings(towerCategoriesTabbar.CurrentTab);
    }
    private void UpdateShopListings(int tabIndex)
    {
        string category = towerCategories[tabIndex];
        List<TowerShopDefinition> towersInCategory = shopTabs[category];
        for (int i = 0; i < currentlyVisibleIcons.Count; i++)
        {
            ShopIcon icon = currentlyVisibleIcons[i];
            unusedIconPool.Add(icon);
            shopContainer.RemoveChild(icon);
        }
        currentlyVisibleIcons.Clear();
        foreach (TowerShopDefinition tower in towersInCategory)
        {
            ShopIcon icon = null;
            if (unusedIconPool.Count > 0)
            {
                icon = unusedIconPool[0];
                unusedIconPool.Remove(icon);
            }
            else
            {
                icon = shopIconScene.Instantiate<ShopIcon>();
            }
            shopContainer.AddChild(icon);
            icon.SetNameLabel(tower.name);
            icon.SetPrice(tower.cost);
            icon.SetImage(tower.codeName);
            icon.SetUnlockWave(tower.unlockWave);
            icon.SetTowerDefinition(tower);
            icon.Selected = (gameManager.selectedTower == tower);
            icon.manager = this;

            icon.CheckIfTowerIsPurchasable();
            currentlyVisibleIcons.Add(icon);
        }
    }
    private void RequestNextWave()
    {
        gameManager.RequestNextWave();
    }

    private void UpdateWave(int wave)
    {
        currentWaveLabel.Text = "Wave " + wave;
        startNextWaveButton.Disabled = true;
    }
    private void UpdateCoins(int coins)
    {
        coinsLabel.Text = "Coins: " + coins;
    }
    private void UpdateKilledEnemies(int enemies)
    {
        enemiesDefeatedLabel.Text = "Total enemies defeated: " + enemies;
        if (gameManager.waveManager.IsWaveCurrentlyHappening()) 
        {
            startNextWaveButton.Disabled = true;
        }
        else
        {
            startNextWaveButton.Disabled = false;
        }
    }
}
