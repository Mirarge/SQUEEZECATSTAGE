using Godot;
using System;

public partial class CannonProjectile : Projectile
{
    public CannonProjectile()
    {
        base.damage = 40;
        base.HP = 2;
    }
}
