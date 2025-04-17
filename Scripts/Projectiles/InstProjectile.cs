using Godot;
using System;

public partial class InstProjectile : Projectile
{

    public override void _Ready()
    {
        base._Ready();
        base.damage = int.MaxValue;
    }
}
