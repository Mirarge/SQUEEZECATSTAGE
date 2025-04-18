using Godot;
using System;

public partial class InstProjectile : Projectile
{

    public InstProjectile()
    {
        base.damage = int.MaxValue;
    }
}
