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

		public static void AddProjectile(Vector2 position)
		{
			_projectile ??= Globals.Content.Load<Texture2D>("projectile1");
			Projectile projectile = new(_projectile, position, new(0, -1), 700);
			Projectiles.Add(projectile);
		}

		public static void UpdateProjectiles()
		{
			Projectiles.ForEach(p => p.Update());
			Projectiles.RemoveAll(p => p.position.Y < -20);
		}

		public static void DrawProjectiles()
		{
			Projectiles.ForEach(p => p.Draw());
		}
	}
}
