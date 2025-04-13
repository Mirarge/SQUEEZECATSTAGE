using Godot;
using System;

public partial class CannonTower : Tower
{
	public int HP = 1;
	public int fireCooldown = 10;
	public int baseCooldown = 10;
	public override int sightDistance { get; set; } = 5;
	public override void Fire()
	{
		if (fireCooldown > 0) fireCooldown--;
		else
		{
			GD.Print("I AM FIRING");
			fireCooldown = baseCooldown;
		}
	}
}
