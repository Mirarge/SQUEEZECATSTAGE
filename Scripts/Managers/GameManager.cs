using Godot;
using System;

public partial class GameManager : Node2D
{
    public LaneManager laneManager;
    public TowerManager towerManager;
    public override void _Ready()
    {
        Timer timer = new Timer();
        AddChild(timer);
        timer.WaitTime = 0.25;
        timer.OneShot = false;
        timer.Start();
        timer.Connect("timeout", new Callable(this, nameof(TimerLoop)));

        towerManager = GetNode<TowerManager>("TowerManager");
        GD.Print("My tower manager: " + towerManager);
        towerManager.gameManager = this;
        laneManager = GetNode<LaneManager>("LaneManager");
        GD.Print("My lane manager: " + laneManager);
        laneManager.gameManager = this;

        laneManager.SpawnTiles();
    }

    private void TimerLoop()
    {
        towerManager.FireAllTowers();
    }
}
