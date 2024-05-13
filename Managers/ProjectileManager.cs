using GalaxyDefender.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GalaxyDefender.Managers
{
	public static class ProjectileManager
	{
		public static readonly List<Projectile> Projectiles = new();
		private static Texture2D _projectile;
		public static readonly List<Projectile> EnemyProjectiles = new();
		private static Texture2D _enemyProjectile;

		public static void Restart()
		{
			Projectiles.Clear();
			EnemyProjectiles.Clear();
		}

		public static void AddProjectile(Vector2 position)
		{
			_projectile ??= Globals.Content.Load<Texture2D>("projectile1");
			Projectile projectile = new(_projectile, position, new(0, -1), 700);
			Projectiles.Add(projectile);
		}

		public static void AddEnemyProjectile(Vector2 position)
		{
			_enemyProjectile ??= Globals.Content.Load<Texture2D>("projectile2");
			Projectile projectile = new(_enemyProjectile, position, new(0, 1), 400);
			EnemyProjectiles.Add(projectile);
		}

		public static void UpdateProjectiles()
		{
			Projectiles.ForEach(p => p.Update());
			Projectiles.RemoveAll(p => p.position.Y < -20);
			EnemyProjectiles.ForEach(p => p.Update());
			EnemyProjectiles.RemoveAll(p => p.position.Y > Globals.WindowSize.Y);
		}

		public static void DrawProjectiles()
		{
			Projectiles.ForEach(p => p.Draw());
			EnemyProjectiles.ForEach(p => p.Draw());
		}
	}
}
