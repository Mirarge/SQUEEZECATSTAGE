using Godot;
using System;
using System.Collections.Generic;

public partial class ProjectileManager : Node2D
{
	public GameManager gameManager;
	public List<Projectile> projectilePool = new List<Projectile>();
	public List<Projectile> projectilesRequestingRemoval = new List<Projectile>();
	public List<Projectile> projectilesInUse = new List<Projectile>();
	private Dictionary<string, PackedScene> projectileTypes = new Dictionary<string, PackedScene>();

	private float projectileUpdateCooldown = 0f;  // How often to update enemies
	private double cooldownTimer = 1f;

	public override void _Ready()
	{
		LoadProjectiles();
	}
	public override void _Process(double delta)
	{
		cooldownTimer += delta;

		if (cooldownTimer >= projectileUpdateCooldown)
		{
			ProcessProjectilesRequestingRemoval();
			foreach (Projectile projectile in projectilesInUse)
			{
				projectile.Update();
			}

			cooldownTimer = 0.0f;
		}
	}
	private void ProcessProjectilesRequestingRemoval()
	{
		for (int i = 0; i < projectilesRequestingRemoval.Count; i++)
		{
			DestroyProjectile(projectilesRequestingRemoval[i]);
		}
		projectilesRequestingRemoval.Clear();
	}
	public void SpawnProjectile(string projectileName, Vector2 position, Vector2 direction, int zIndex, bool isTowerProjectile)
	{
		Projectile projectile = null;
		foreach (Projectile proj in projectilePool)
		{
			if(proj.name == projectileName)
			{//Use projectile from the pool
				projectile = proj;
				projectilePool.Remove(proj);
				break;
			}
		}
		if(projectile == null)
		{//spawn new projectile
			PackedScene projectileScene = getProjectileByName(projectileName);
			projectile = projectileScene.Instantiate<Projectile>();
			
		}
		projectile.Position = position;
		projectile.ZIndex = zIndex;
		projectile.manager = this;
		projectile.isTowerProjectile = isTowerProjectile;
		projectile.direction = direction;
		projectile.name = projectileName;
		projectile.ResetHP();
		projectilesInUse.Add(projectile);
		AddChild(projectile);
		projectile.UpdateCollisionMask(); //Since there is a chance the projectile does not yet exist, we wait with updating the collision mask until its presumably ready
	}
	public void RequestProjectileRemoval(Projectile projectile)
	{ //Make sure that projectiles can't immediately be removed, we have to wait for all the projectiles to first have gone through their updates. Basically CallDeferred?
		projectilesRequestingRemoval.Add(projectile);

	}
	private void DestroyProjectile(Projectile projectile)
	{
		projectilesInUse.Remove(projectile);
		projectilePool.Add(projectile);
		GD.Print("Projectiles in projectile pool: " + projectilePool.Count);
		if(projectilePool.Count > 100)
		{ //just in case, limit the pool to 100 projectiles
			Projectile removable = projectilePool[0];
			projectilePool.Remove(removable);
			removable.CallDeferred("queue_free");
		}
		CallDeferred("remove_child", projectile);
		projectile.Position = new Vector2(0,0);
	}

	public PackedScene getProjectileByName(string projectileName)
	{
		try
		{
			return projectileTypes[projectileName];
		}
		catch (Exception e)
		{
			GD.Print("Issue getting projectile object: " + e.Message);
			return projectileTypes["TestProjectile"];
		}

	}

	private void LoadProjectiles()
	{
		projectileTypes.Add("TestProjectile", GD.Load<PackedScene>("res://ObjectScenes/Projectiles/Projectile.tscn"));
	}
}
