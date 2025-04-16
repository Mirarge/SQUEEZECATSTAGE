using Godot;
using SqueezecatStage.Scripts;
using System;
using System.Collections.Generic;

public partial class TowerManager : Node2D
{
    public List<Tower> towers = new List<Tower>();
    
    public GameManager gameManager;

    public void PlaceTower(Tile tile, string towerName)
    {
        if (tile.isThereATowerOnMe()) return;
        PackedScene towerScene = getTowerByName(towerName);
        Tower tower = towerScene.Instantiate<Tower>();
        tower.Position = tile.Position;
        tower.ZIndex = tile.ZIndex+1;
        tower.manager = this;
        tower.myTile = tile;
        AddChild(tower);

        towers.Add(tower);
        tile.placeTower(tower);
    }
    public void DestroyTower(Tower tower)
    {
        towers.Remove(tower);
        tower.myTile.removeTower();
        tower.QueueFree();
    }

    public void FireAllTowers()
    {
        foreach (var tower in towers)
        {
            if (tower.DoISeeAnEnemy())
            {
                tower.Fire();
            }
        }

    }

    public PackedScene getTowerByName(string towerName)
    {
        try
        {
            return DataStorage.Instance.towerTypes[towerName];
        }
        catch(Exception e)
        {
            GD.Print("Issue getting tower object: " + e.Message);
            return DataStorage.Instance.towerTypes["TestTower"];
        }
       
    }

    
}
