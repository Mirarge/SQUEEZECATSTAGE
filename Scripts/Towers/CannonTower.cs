using Godot;
using System;

public partial class CannonTower : Tower
{
	public override int HP { get; set; } = 10;
	public int fireCooldown = 10;
	public int baseCooldown = 10;
	public override int sightDistance { get; set; } = 5;
	public override void Fire()
	{
		if (fireCooldown > 0) fireCooldown--;
		else
		{
			GD.Print("Creating projectile");
			manager.gameManager.projectileManager.SpawnProjectile("TestProjectile", Position, new Vector2(1, 0), this.ZIndex + 1, true);
			fireCooldown = baseCooldown;
		}
	}
}
