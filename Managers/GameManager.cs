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
			for (int i = 0; i < 3; i++)
			{
				EnemyManager.AddEnemy();
			}
		}

		public static void Restart(PlayerShip playerShip)
		{
			playerShip.Restart();
			EnemyManager.Restart();
			ProjectileManager.Restart();
		}

		public void HandleEnemyCollisions()
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

		public void HandlePlayerCollision()
		{
			foreach (var enemy in EnemyManager.Enemies)
			{
				if (enemy.CollisionRectangle.Intersects(ship.CollisionRectangle))
				{
					Restart(ship);
					break;
				};
			};

			foreach (var projectile in ProjectileManager.EnemyProjectiles)
			{
				if (projectile.CollisionRectangle.Intersects(ship.CollisionRectangle))
				{
					Restart(ship);
					break;
				};
			};
		}


		public void Update()
		{
			ship.Update();
			EnemyManager.UpdateEnemies();
			ProjectileManager.UpdateProjectiles();
			HandleEnemyCollisions();
			HandlePlayerCollision();
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
