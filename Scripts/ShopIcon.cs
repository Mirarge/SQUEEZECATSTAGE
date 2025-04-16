using Godot;
using System;
using System.Xml;

public partial class ShopIcon : Control
{
    private string name;
    private int price;
    private string imageName;

    private Label nameLabel;
    private TextureRect towerImage;
    private Label priceLabel;

    public override void _Ready()
    {
        nameLabel = GetNode<Label>("RootControl/NameOverlay/NameLabel");
        priceLabel = GetNode<Label>("RootControl/PriceOverlay/PriceLabel");
        towerImage = GetNode<TextureRect>("RootControl/TowerImage");
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
}
