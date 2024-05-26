using GalaxyDefender.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace GalaxyDefender.Managers
{
	public static class EnemyManager
	{
		public static readonly List<Enemy> Enemies = new();
		private const float baseSpawnTime = 1.5f;
		private const float decrementSpawnTme = 0.02f;
		private const float minSpawnTime = 0.5f;
		private static float spawnCooldown = baseSpawnTime;

		public static void Restart()
		{
			Enemies.Clear();
			spawnCooldown = baseSpawnTime;
		}

		public static void AddEnemy()
		{
			Random r = new();
			var s = r.Next(2) == 0 ? "1" : "2";
			Enemy enemy = new(Globals.Content.Load<Texture2D>($"enemy{s}"))
			{
				position = GeneratePosition()
			};
			enemy.SetRandomDestination();
			Enemies.Add(enemy);
		}

		private static Vector2 GeneratePosition()
		{
			Random r = new();
			return new(r.Next(Globals.WindowSize.X), -100);
		}

		public static void UpdateEnemies()
		{
			Enemies.ForEach(e => e.Update());

			spawnCooldown -= Globals.Time;
			if (spawnCooldown < 0)
			{
				spawnCooldown += CalculateSpawnTime();
				AddEnemy();
			}
		}

		private static float CalculateSpawnTime()
		{
			int score = GameManager.GetScore();
			return Math.Max(baseSpawnTime - (score * decrementSpawnTme), minSpawnTime);
		}

		public static void DrawEnemies()
		{
			Enemies.ForEach(e => e.Draw());
		}
	}
}
