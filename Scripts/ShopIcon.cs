using Godot;
using SqueezecatStage.Scripts;
using System;
using System.Data.Common;
using System.Xml;

public partial class ShopIcon : Control
{
    private string name;
    private int price;
    private string imageName;
    private int unlockWave = 0;
    private TowerShopDefinition towerDefinition;

    private Label nameLabel;
    private TextureRect towerImage;
    private Label priceLabel;

    private Label unlockWaveLabel;

    private Control waveUnlockBlock;
    private Panel blockedPanel;

    public bool Selected { get { return selected; } set { selected = value; hoverBorder.Visible = value; } }
    private bool selected = false;
    

    private Control clickArea;

    private Panel hoverBorder;

    public UiManager manager;

    public override void _Ready()
    {
        nameLabel = GetNode<Label>("RootControl/NameOverlay/NameLabel");
        priceLabel = GetNode<Label>("RootControl/PriceOverlay/PriceLabel");
        towerImage = GetNode<TextureRect>("RootControl/TowerImage");

        blockedPanel = GetNode<Panel>("BlockedPanel");
        unlockWaveLabel = GetNode<Label>("BlockedPanel/WaveUnlockBlock/BlockedPanel2/UnlocksAtWaveLabel");
        waveUnlockBlock = GetNode<Control>("BlockedPanel/WaveUnlockBlock");

        hoverBorder = GetNode<Panel>("HoverBorder");

        clickArea = GetNode<Control>("RootControl");
        clickArea.Connect("gui_input", new Callable(this, nameof(OnGuiInput)));
        clickArea.Connect("mouse_entered", new Callable(this, nameof(OnMouseEntered)));
        clickArea.Connect("mouse_exited", new Callable(this, nameof(OnMouseExited)));

        DataStorage.Instance.Connect("WaveChanged", new Callable(this, nameof(UpdateWave)));
        DataStorage.Instance.Connect("CoinsChanged", new Callable(this, nameof(UpdateCoins)));
    }
    public void SetNameLabel(string name)
    {
        this.name = name;
        nameLabel.Text = name;
    }
    public void SetPrice(int price)
    {
        this.price = price;
        priceLabel.Text = price.ToString();
    }
    public void SetImage(string imageName)
    {
        this.imageName = imageName;
        towerImage.Texture = GD.Load<Texture2D>($"res://Textures/Towers/{imageName}.png");
    }

    public void SetUnlockWave(int wave)
    {
        this.unlockWave = wave;
        unlockWaveLabel.Text = wave.ToString();
    }

    public void SetTowerDefinition(TowerShopDefinition definition)
    {
        this.towerDefinition = definition;
    }
    private void OnGuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
        {
            if (CheckIfTowerIsPurchasable())
            {
                manager.gameManager.SelectTower(this.towerDefinition);
                Selected = true;
                manager.RefreshShop();
            }
        }
    }
    private void OnMouseEntered()
    {
        hoverBorder.Visible = true;
    }

    private void OnMouseExited()
    {
        if(Selected == false)
        {
            hoverBorder.Visible = false;
        }
    }

    private void UpdateWave(int wave)
    {
        if(this.unlockWave <= wave)
        { //This wave this item has become available
            waveUnlockBlock.Visible = false;
        }
        else
        {
            waveUnlockBlock.Visible = true;
        }
        CheckIfTowerIsPurchasable();
    }
    private void UpdateCoins(int coins)
    {
        CheckIfTowerIsPurchasable();
    }
    public bool CheckIfTowerIsPurchasable()
    {
        if (manager.gameManager.selectedTower == this.towerDefinition)
        {
            Selected = true;
        }
        if (DataStorage.Instance.Coins >= this.price && DataStorage.Instance.Wave >= this.unlockWave)
        {
            blockedPanel.Visible = false;
            return true;
        }
        else
        {
            if (this.unlockWave <= DataStorage.Instance.Wave) { 
                waveUnlockBlock.Visible = false;
            }else{
                waveUnlockBlock.Visible = true;
            }
            blockedPanel.Visible = true;
            if (manager.gameManager.selectedTower == this.towerDefinition)
            {
                Selected = false;
                manager.gameManager.selectedTower = null;
            }
            return false;
        }
    }
}
