using Godot;
using System;
using System.Collections.Generic;

public partial class TowerManager : Node2D
{
    public List<Tower> towers = new List<Tower>();
    private Dictionary<string, PackedScene> towerTypes = new Dictionary<string, PackedScene>();
    public GameManager gameManager;

    public override void _Ready()
    {
        LoadTowers();
    }
    public void PlaceTower(Tile tile, string towerName)
    {
        if (tile.isThereATowerOnMe()) return;
        PackedScene towerScene = getTowerByName(towerName);
        Tower tower = towerScene.Instantiate<Tower>();
        tower.Position = tile.Position;
        tower.ZIndex = tile.ZIndex+1;
        AddChild(tower);

        towers.Add(tower);
        tile.placeTower(tower);
    }

    public void FireAllTowers()
    {
        foreach (var tower in towers)
        {
            tower.Fire();
        }

    }

    public PackedScene getTowerByName(string towerName)
    {
        try
        {
            return towerTypes[towerName];
        }
        catch(Exception e)
        {
            GD.Print("Issue getting tower object: " + e.Message);
            return towerTypes["TestTower"];
        }
       
    }

    private void LoadTowers()
    {
        towerTypes.Add("TestTower", GD.Load<PackedScene>("res://ObjectScenes/Towers/Tower.tscn"));
        towerTypes.Add("CannonTower", GD.Load<PackedScene>("res://ObjectScenes/Towers/CannonTower.tscn"));
        towerTypes.Add("BarricadeTower", GD.Load<PackedScene>("res://ObjectScenes/Towers/BarricadeTower.tscn"));
    }
}
