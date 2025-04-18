using Godot;
using System;

public partial class Cannon2Tower : Tower
{

    public override void _Ready()
    {
        base.HP = 300;
        base.fireCooldown = 10;
        base.baseCooldown = 10;
        base.sightDistance = 6;
        base._Ready();
    }
    public async override void Fire()
    {
        if (fireCooldown > 0) fireCooldown--;
        else
        {
            manager.gameManager.projectileManager.SpawnProjectile("CannonProjectile", Position, new Vector2(2, 0), this.ZIndex + 1, true);
            fireCooldown = baseCooldown; //Set cooldown before waiting so we don't have another trigger of the method.
            await ToSignal(GetTree().CreateTimer(0.25f), "timeout");
            manager.gameManager.projectileManager.SpawnProjectile("CannonProjectile", Position, new Vector2(2, 0), this.ZIndex + 1, true);
            
        }
    }
}
