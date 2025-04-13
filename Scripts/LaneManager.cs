using Godot;
using System;

public partial class LaneManager : Node2D
{
	PackedScene tileScene;

	public override void _Ready()
	{
		tileScene = (PackedScene)ResourceLoader.Load("res://ObjectScenes/Tile.tscn");
		SpawnTiles();
	}

	private void SpawnTiles()
	{
		for (int lane = 0; lane < 3; lane++)
		{
			for (int row = 0; row < 10; row++)
			{
				Tile tileInstance = tileScene.Instantiate<Tile>();
				tileInstance.Position = new Vector2((row * 64)+32, (lane*64)+32);
				tileInstance.setRowColumn(row, lane);
				AddChild(tileInstance);
			}
		}
	}
}
