using Godot;
using System;

public partial class LandmineTower : Tower
{

    public override void _Ready()
    {
        base.HP = 999999;
        base.sightDistance = 0;
        base._Ready();
    }
    public async override void Fire()
    {
        base.TakeDamage(HP + 1);
        manager.gameManager.projectileManager.SpawnProjectile("InstProjectile", Position, new Vector2(0, 0), this.ZIndex + 1, true);
    }
}
