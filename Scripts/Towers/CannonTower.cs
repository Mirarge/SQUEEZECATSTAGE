using Godot;
using System;

public partial class CannonTower : Tower
{
    public override void _Ready()
    {
        base.HP = 300;
        base.fireCooldown = 10;
        base.baseCooldown = 10;
        base.sightDistance = 8;
        base._Ready();
    }
    public async override void Fire()
	{
		if (fireCooldown > 0) fireCooldown--;
		else
		{
			manager.gameManager.projectileManager.SpawnProjectile("CannonProjectile", Position, new Vector2(2, 0), this.ZIndex + 1, true);
			fireCooldown = baseCooldown;
		}
	}
}
