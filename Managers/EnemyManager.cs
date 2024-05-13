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
		private const float spawnTime = 0.6f;
		private static float spawnCooldown = spawnTime;

		public static void Restart()
		{
			Enemies.Clear();
			spawnCooldown = spawnTime;
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
				spawnCooldown += spawnTime;
				AddEnemy();
			}
		}

		public static void DrawEnemies()
		{
			Enemies.ForEach(e => e.Draw());
		}
	}
}
