using Godot;
using System;
using System.Collections.Generic;

namespace SqueezecatStage.Scripts
{
    public partial class DataStorage : Node
    {

        public Dictionary<string, PackedScene> projectileTypes = new Dictionary<string, PackedScene>();
        public Dictionary<string, PackedScene> towerTypes = new Dictionary<string, PackedScene>();
        public Dictionary<string, PackedScene> enemyTypes = new Dictionary<string, PackedScene>();
        
        public List<TowerShopDefinition> towerShopDefinitions = new List<TowerShopDefinition>();

        [Signal]
        public delegate void WaveChangedEventHandler(float newWave);

        public int Wave
        {
            get => wave;
            set
            {
                wave = value;
                EmitSignal(SignalName.WaveChanged, wave);
                GD.Print("Changing wave to " + wave);
            }
        }
        private int wave = 0;

        [Signal]
        public delegate void CoinsChangedEventHandler(float newCoins);

        public int Coins
        {
            get => coins;
            set
            {
                coins = value;
                EmitSignal(SignalName.CoinsChanged, coins);
                GD.Print("Changing coins to " + coins);
            }
        }
        private int coins = 0;



        public static DataStorage Instance { get; private set; }
        public override void _Ready()
        {
            Instance = this;

            LoadProjectiles();
            LoadTowers();
            LoadEnemies();
            LoadTowerShopDefinitions();
        }
        private void LoadProjectiles()
        {
            projectileTypes.Add("TestProjectile", GD.Load<PackedScene>("res://ObjectScenes/Projectiles/Projectile.tscn"));
        }
        private void LoadTowers()
        {
            towerTypes.Add("TestTower", GD.Load<PackedScene>("res://ObjectScenes/Towers/Tower.tscn")); //Debug Tower
            towerTypes.Add("CannonTower", GD.Load<PackedScene>("res://ObjectScenes/Towers/CannonTower.tscn")); //Peashooter
            towerTypes.Add("Cannon2Tower", GD.Load<PackedScene>("res://ObjectScenes/Towers/Cannon2Tower.tscn")); //Repeater
            towerTypes.Add("WallTower", GD.Load<PackedScene>("res://ObjectScenes/Towers/WallTower.tscn")); //Wallnut
            towerTypes.Add("LandmineTower", GD.Load<PackedScene>("res://ObjectScenes/Towers/LandmineTower.tscn")); //potato mine
            towerTypes.Add("BarricadeTower", GD.Load<PackedScene>("res://ObjectScenes/Towers/BarricadeTower.tscn")); //LawnMower
        }
        private void LoadEnemies()
        {
            enemyTypes.Add("TestEnemy", GD.Load<PackedScene>("res://ObjectScenes/Enemies/Enemy.tscn"));
        }
        private void LoadTowerShopDefinitions()
        {
            towerShopDefinitions.Add(new TowerShopDefinition("Cannon", "CannonTower", 100, "Ranged"));
            towerShopDefinitions.Add(new TowerShopDefinition("Cannon 2", "Cannon2Tower", 200, "Ranged"));
            towerShopDefinitions.Add(new TowerShopDefinition("Wall", "WallTower", 50, "Support"));
            towerShopDefinitions.Add(new TowerShopDefinition("Landmine", "LandmineTower", 200, "Melee", 2));
        }
    }
}
