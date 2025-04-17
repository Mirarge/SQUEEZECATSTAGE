using Godot;
using SqueezecatStage.Scripts;
using System;
using System.Collections.Generic;
using static Godot.OpenXRInterface;

public partial class WaveManager : Node2D
{
	public GameManager gameManager;
    public List<Enemy> enemyPool = new List<Enemy>();
    private List<Enemy> enemiesInUse = new List<Enemy>();
	private float enemyUpdateCooldown = 0f;  // How often to update enemies
	private double cooldownTimer = 1f;
	public override void _Process(double delta)
	{
		cooldownTimer += delta;

		if (cooldownTimer >= enemyUpdateCooldown)
		{
			foreach (Enemy enemy in enemiesInUse)
			{
				enemy.Update();
			}

			cooldownTimer = 0.0f;
		}
	}

	public void AddEnemy(string enemyName, int lane)
	{
        Enemy enemy = null;
        foreach (Enemy en in enemyPool)
        {
            if (en.name == enemyName)
            {//Use projectile from the pool
                enemy = en;
                enemyPool.Remove(en);
                break;
            }
        }
        if (enemy == null)
        {//spawn new projectile
            PackedScene enemyScene = getEnemyByName(enemyName);
            enemy = enemyScene.Instantiate<Enemy>();

        }

		enemy.Position = new Vector2(gameManager.lanePosition.X+((gameManager.laneManager.laneLength+3)*64)+32 + (lane * 16), (lane*64)+32) + gameManager.lanePosition;
		enemy.ZIndex = (lane * 2)+1;
		enemy.name = enemyName;
		enemy.manager = this;
		AddChild(enemy);
        enemy.sprite.Position -= new Vector2(0, GD.Randf() * 15); //Offset the sprite for some variation
        enemiesInUse.Add(enemy);
	}
	public void DestroyEnemy(Enemy enemy)
	{
		enemiesInUse.Remove(enemy);
		enemy.QueueFree();
	}

	public async void SpawnWave(int pointBudget, int waveTimeInSeconds)
	{
		gameManager.IncreaseWaveCount();

        Dictionary<string, PackedScene> enemyTypes = DataStorage.Instance.enemyTypes;
        Random rand = new Random();

        int budgetRemaining = pointBudget;
        List<string> spawnPlan = new List<string>();
        List<KeyValuePair<string, int>> affordableEnemiesList = new List<KeyValuePair<string, int>>();

        foreach (KeyValuePair<string, PackedScene> kvp in enemyTypes)
        {
            affordableEnemiesList.Add(new KeyValuePair<string, int>(kvp.Key, (int)kvp.Value.Instantiate().Get("budgetCost")));
        } //Add all the enemies to the affordable list
		

        while (budgetRemaining > 0)
        {
            // Filter enemies that can be afforded
            if (affordableEnemiesList.Count == 0)
            {
                GD.Print("Couldn't spend full budget, stopping early.");
                break;
            }

            // Pick one at random
            KeyValuePair<string, int> chosen = affordableEnemiesList[rand.Next(affordableEnemiesList.Count)];
			if(chosen.Value > budgetRemaining)
			{//Picked an enemy that is too expensive and cannot be picked again this wave
				affordableEnemiesList.Remove(chosen);
			}
			else
			{
                spawnPlan.Add(chosen.Key);
                budgetRemaining -= chosen.Value;
            }
        }

        float delayBetweenSpawns = (float)waveTimeInSeconds / spawnPlan.Count;

        foreach (string enemyName in spawnPlan)
        {
            int lane = rand.Next(0, 3);
            AddEnemy(enemyName, lane);
            await ToSignal(GetTree().CreateTimer(delayBetweenSpawns), "timeout"); //Spreads all the enemies out over the given wave time
        }

    }
    public PackedScene getEnemyByName(string enemyName)
	{
		try
		{
			return DataStorage.Instance.enemyTypes[enemyName];
		}
		catch (Exception e)
		{
			GD.Print("Issue getting enemy object: " + e.Message);
			return DataStorage.Instance.enemyTypes["TestEnemy"];
		}

	}

	
}
