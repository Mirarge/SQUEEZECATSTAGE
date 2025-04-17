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

    private Label nameLabel;
    private TextureRect towerImage;
    private Label priceLabel;

    private Control clickArea;

    private Panel hoverBorder;

    public override void _Ready()
    {
        nameLabel = GetNode<Label>("RootControl/NameOverlay/NameLabel");
        priceLabel = GetNode<Label>("RootControl/PriceOverlay/PriceLabel");
        towerImage = GetNode<TextureRect>("RootControl/TowerImage");

        hoverBorder = GetNode<Panel>("HoverBorder");

        clickArea = GetNode<Control>("RootControl");
        clickArea.Connect("gui_input", new Callable(this, nameof(OnGuiInput)));
        clickArea.Connect("mouse_entered", new Callable(this, nameof(OnMouseEntered)));
        clickArea.Connect("mouse_exited", new Callable(this, nameof(OnMouseExited)));
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
    private void OnGuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
        {
            GD.Print("WOAGGG");
        }
    }
    private void OnMouseEntered()
    {
        hoverBorder.Visible = true;
    }

    private void OnMouseExited()
    {
        hoverBorder.Visible = false;
    }

}
