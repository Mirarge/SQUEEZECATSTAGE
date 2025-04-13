using Godot;
using System;
using System.Collections.Generic;

public partial class WaveManager : Node2D
{
	public GameManager gameManager;
	private List<Enemy> enemies = new List<Enemy>();
	private Dictionary<string, PackedScene> enemyTypes = new Dictionary<string, PackedScene>();
	private float enemyUpdateCooldown = 0f;  // How often to update enemies
	private double cooldownTimer = 0.0f;
	public override void _Ready()
	{
		LoadEnemies();
	}
	public override void _Process(double delta)
	{
		cooldownTimer += delta;

		if (cooldownTimer >= enemyUpdateCooldown)
		{
			// Loop through all enemies and update their behavior
			foreach (Enemy enemy in enemies)
			{
				enemy.MoveOnce();  // This could handle movement, attacks, etc.
			}

			cooldownTimer = 0.0f;  // Reset the cooldown
		}
	}

	public void AddEnemy(string enemyName, int lane)
	{
		PackedScene enemyScene = getEnemyByName(enemyName);
		Enemy enemy = enemyScene.Instantiate<Enemy>();
		enemy.Position = new Vector2(Position.X+((gameManager.laneManager.laneLength+3)*64)+32 + (lane * 16), (lane*64)-32);
		enemy.ZIndex = (lane * 2)+1;
		AddChild(enemy);
		enemies.Add(enemy);
	}

	public void SpawnWave()
	{
		for (int i = 0; i < 3; i++)
		{
			AddEnemy("TestEnemy", i);
		}
	}
	public PackedScene getEnemyByName(string towerName)
	{
		try
		{
			return enemyTypes[towerName];
		}
		catch (Exception e)
		{
			GD.Print("Issue getting enemy object: " + e.Message);
			return enemyTypes["TestEnemy"];
		}

	}

	private void LoadEnemies()
	{
		enemyTypes.Add("TestEnemy", GD.Load<PackedScene>("res://ObjectScenes/Enemies/Enemy.tscn"));
		//towerTypes.Add("CannonTower", GD.Load<PackedScene>("res://ObjectScenes/Towers/CannonTower.tscn"));
		//towerTypes.Add("BarricadeTower", GD.Load<PackedScene>("res://ObjectScenes/Towers/BarricadeTower.tscn"));
	}
}
