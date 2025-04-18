using Godot;
using System;

public partial class BossShockwaveProjectile : Projectile
{
    public BossShockwaveProjectile()
    {
        base.damage = 40;
        base.HP = base.baseHP  = 2;
    }
}
