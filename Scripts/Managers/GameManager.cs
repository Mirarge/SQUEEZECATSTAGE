using Godot;
using SqueezecatStage.Scripts;
using System;

public partial class GameManager : Node2D
{
	public LaneManager laneManager;
	public TowerManager towerManager;
	public WaveManager waveManager;
	public ProjectileManager projectileManager;
	public UiManager uiManager;


	public TowerShopDefinition selectedTower;


	public Node2D boundary;
	public Vector2 lanePosition;
	public override void _Ready()
	{
		Timer timer = new Timer();
		AddChild(timer);
		timer.WaitTime = 0.25;
		timer.OneShot = false;
		timer.Start();
		timer.Connect("timeout", new Callable(this, nameof(TimerLoop)));
		
		boundary = GetNode<Node2D>("Boundary");
		lanePosition = GetNode<Node2D>("LanePosition").Position;
        
		towerManager = GetNode<TowerManager>("TowerManager");
		towerManager.gameManager = this;
		laneManager = GetNode<LaneManager>("LaneManager");
		laneManager.gameManager = this;
		waveManager = GetNode<WaveManager>("WaveManager");
		waveManager.gameManager = this;
        uiManager = GetParent().GetNode<UiManager>("UIManager");
        uiManager.gameManager = this;
        projectileManager = GetNode<ProjectileManager>("ProjectileManager");
		projectileManager.gameManager = this;

		//new Vector2(204.43f, 162.495f);


		laneManager.SpawnTiles();
		waveManager.SpawnWave();
		ModifyCoins(100);


    }
	public void NextWave()
	{
		DataStorage.Instance.Wave += 1;
	}
    public void ModifyCoins(int modifyAmount)
    {
        DataStorage.Instance.Coins += modifyAmount;
    }
    private void TimerLoop()
	{
		towerManager.FireAllTowers();
	}

	public void SelectTower(TowerShopDefinition tower)
	{
		selectedTower = tower;
	}
}
