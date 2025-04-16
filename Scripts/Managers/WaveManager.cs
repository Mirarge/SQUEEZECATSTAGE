using Godot;
using SqueezecatStage.Scripts;
using System;
using System.Collections.Generic;

public partial class WaveManager : Node2D
{
	public GameManager gameManager;
	private List<Enemy> enemies = new List<Enemy>();
	private float enemyUpdateCooldown = 0f;  // How often to update enemies
	private double cooldownTimer = 1f;
	public override void _Process(double delta)
	{
		cooldownTimer += delta;

		if (cooldownTimer >= enemyUpdateCooldown)
		{
			foreach (Enemy enemy in enemies)
			{
				enemy.Update();
			}

			cooldownTimer = 0.0f;
		}
	}

	public void AddEnemy(string enemyName, int lane)
	{
		PackedScene enemyScene = getEnemyByName(enemyName);
		Enemy enemy = enemyScene.Instantiate<Enemy>();
		enemy.Position = new Vector2(gameManager.lanePosition.X+((gameManager.laneManager.laneLength+3)*64)+32 + (lane * 16), (lane*64)+32) + gameManager.lanePosition;
		enemy.ZIndex = (lane * 2)+1;
		enemy.Name = enemyName;
		enemy.manager = this;
		AddChild(enemy);
		enemies.Add(enemy);
	}
	public void DestroyEnemy(Enemy enemy)
	{
		enemies.Remove(enemy);
		enemy.QueueFree();
	}

	public void SpawnWave()
	{
		//for (int i = 0; i < 3; i++)
		//{
		//	AddEnemy("TestEnemy", i);
		//}
		for (int i = 0; i < 20; i++)
		{
			AddEnemy("TestEnemy", (int)(GD.Randf()*3));
		}
	}
	public PackedScene getEnemyByName(string towerName)
	{
		try
		{
			return DataStorage.Instance.enemyTypes[towerName];
		}
		catch (Exception e)
		{
			GD.Print("Issue getting enemy object: " + e.Message);
			return DataStorage.Instance.enemyTypes["TestEnemy"];
		}

	}

	
}
