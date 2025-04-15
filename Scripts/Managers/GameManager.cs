using Godot;
using System;

public partial class GameManager : Node2D
{
	public LaneManager laneManager;
	public TowerManager towerManager;
	public WaveManager waveManager;
	public ProjectileManager projectileManager;

	public Node2D boundary;
	public override void _Ready()
	{
		Timer timer = new Timer();
		AddChild(timer);
		timer.WaitTime = 0.25;
		timer.OneShot = false;
		timer.Start();
		timer.Connect("timeout", new Callable(this, nameof(TimerLoop)));

		boundary = GetNode<Node2D>("Boundary");

		towerManager = GetNode<TowerManager>("TowerManager");
		towerManager.gameManager = this;
		laneManager = GetNode<LaneManager>("LaneManager");
		laneManager.gameManager = this;
		waveManager = GetNode<WaveManager>("WaveManager");
		waveManager.gameManager = this;
		projectileManager = GetNode<ProjectileManager>("ProjectileManager");
		projectileManager.gameManager = this;

		laneManager.SpawnTiles();
		waveManager.SpawnWave();
	}

	private void TimerLoop()
	{
		towerManager.FireAllTowers();
	}
}
