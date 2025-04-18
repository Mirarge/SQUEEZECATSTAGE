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

	private int waveBudget = 10;
	private int waveTime = 30;


	public Node2D Rboundary;
    public Node2D Lboundary;
    public Vector2 lanePosition;
	public override void _Ready()
	{
		Timer timer = new Timer();
		AddChild(timer);
		timer.WaitTime = 0.25;
		timer.OneShot = false;
		timer.Start();
		timer.Connect("timeout", new Callable(this, nameof(TimerLoop)));
		
		Rboundary = GetNode<Node2D>("RightBoundary");
        Lboundary = GetNode<Node2D>("LeftBoundary");
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
		waveManager.SpawnWave(waveBudget, waveTime);
		ModifyCoins(300);


    }

    public void RequestNextWave()
    {
        if(waveManager.IsWaveCurrentlyHappening()) { return; } 
		else
		{
			waveManager.SpawnWave(waveBudget, waveTime);
			waveBudget = (int)(waveBudget * 1.2f);
        }

    }

	public void Lose()
	{
		GD.Print("You have LOST");
	}

    public void IncreaseWaveCount()
	{
		DataStorage.Instance.Wave += 1;
	}
    public void ModifyCoins(int modifyAmount)
    {
        DataStorage.Instance.Coins += modifyAmount;
    }
    public void ModifyKilledEnemies(int modifyAmount = 1)
    {
        DataStorage.Instance.KilledEnemies += modifyAmount;
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
