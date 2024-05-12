using GalaxyDefender.Managers;
using GalaxyDefender.Models;
using Microsoft.Xna.Framework.Graphics;
using static System.Formats.Asn1.AsnWriter;

namespace GalaxyDefender
{
	public class GameManager
	{
		private readonly PlayerShip ship;

		public GameManager()
		{
			ship = new(Globals.Content.Load<Texture2D>("ship"));
			EnemyManager.AddEnemy();
			EnemyManager.AddEnemy();
			EnemyManager.AddEnemy();
		}

		public void HandleCollisions()
		{
			foreach (var enemy in EnemyManager.Enemies.ToArray())
			{
				foreach (var projectile in ProjectileManager.Projectiles.ToArray())
				{
					if (enemy.CollisionRectangle.Intersects(projectile.CollisionRectangle))
					{
						EnemyManager.Enemies.Remove(enemy);
						ProjectileManager.Projectiles.Remove(projectile);
						break;
					};
				};
			};
		}

		public void Update()
		{
			ship.Update();
			EnemyManager.UpdateEnemies();
			ProjectileManager.UpdateProjectiles();
			HandleCollisions();
		}

		public void Draw()
		{
			Globals.SpriteBatch.Begin();

			EnemyManager.DrawEnemies();
			ProjectileManager.DrawProjectiles();
			ship.Draw();

			Globals.SpriteBatch.End();
		}
	}
}
