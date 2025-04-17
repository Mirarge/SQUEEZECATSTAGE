using Godot;
using System;

public partial class Cannon2Tower : Tower
{
    public override int HP { get; set; } = 300;
    public int fireCooldown = 10;
    public int baseCooldown = 10;
    public override int sightDistance { get; set; } = 6;
    public async override void Fire()
    {
        if (fireCooldown > 0) fireCooldown--;
        else
        {
            manager.gameManager.projectileManager.SpawnProjectile("CannonProjectile", Position, new Vector2(1, 0), this.ZIndex + 1, true);
            await ToSignal(GetTree().CreateTimer(0.25f), "timeout");
            manager.gameManager.projectileManager.SpawnProjectile("CannonProjectile", Position, new Vector2(1, 0), this.ZIndex + 1, true);
            fireCooldown = baseCooldown;
        }
    }
}
