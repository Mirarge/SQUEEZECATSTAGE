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
            }
        }
        private int coins = 0;

        [Signal]
        public delegate void KilledEnemiesChangedEventHandler(float newCoins);

        public int KilledEnemies
        {
            get => killedEnemies;
            set
            {
                killedEnemies = value;
                EmitSignal(SignalName.KilledEnemiesChanged, killedEnemies);
            }
        }
        private int killedEnemies = 0;


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
            projectileTypes.Add("CannonProjectile", GD.Load<PackedScene>("res://ObjectScenes/Projectiles/CannonProjectile.tscn"));
            projectileTypes.Add("InstProjectile", GD.Load<PackedScene>("res://ObjectScenes/Projectiles/InstProjectile.tscn"));
            projectileTypes.Add("RangedEnemyProjectile", GD.Load<PackedScene>("res://ObjectScenes/Projectiles/RangedEnemyProjectile.tscn"));
            projectileTypes.Add("BossShockwaveProjectile", GD.Load<PackedScene>("res://ObjectScenes/Projectiles/BossShockwaveProjectile.tscn"));
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
            //enemyTypes.Add("TestEnemy", GD.Load<PackedScene>("res://ObjectScenes/Enemies/Enemy.tscn"));
            enemyTypes.Add("DefaultEnemy", GD.Load<PackedScene>("res://ObjectScenes/Enemies/DefaultEnemy.tscn"));
            enemyTypes.Add("FastEnemy", GD.Load<PackedScene>("res://ObjectScenes/Enemies/FastEnemy.tscn"));
            enemyTypes.Add("RangedEnemy", GD.Load<PackedScene>("res://ObjectScenes/Enemies/RangedEnemy.tscn"));
            enemyTypes.Add("StrongerEnemy", GD.Load<PackedScene>("res://ObjectScenes/Enemies/StrongerEnemy.tscn"));
            enemyTypes.Add("CommanderBossEnemy", GD.Load<PackedScene>("res://ObjectScenes/Enemies/CommanderBossEnemy.tscn"));
        }
        private void LoadTowerShopDefinitions()
        {
            towerShopDefinitions.Add(new TowerShopDefinition("Cannon", "CannonTower", 100, "Ranged"));
            towerShopDefinitions.Add(new TowerShopDefinition("Cannon 2", "Cannon2Tower", 200, "Ranged", 10));
            towerShopDefinitions.Add(new TowerShopDefinition("Wall", "WallTower", 50, "Support", 5));
            towerShopDefinitions.Add(new TowerShopDefinition("Landmine", "LandmineTower", 200, "Melee", 20));
        }
    }
}
