using GalaxyDefender.Managers;
using GalaxyDefender.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System.Threading.Tasks.Sources;
using static System.Formats.Asn1.AsnWriter;

namespace GalaxyDefender
{
	public class GameManager
	{
		private readonly PlayerShip ship;
		private readonly SpriteFont font;
		private static int score;

		public GameManager()
		{
			font = Globals.Content.Load<SpriteFont>("font");
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
			score = 0;
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
						score++;
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

			Globals.SpriteBatch.DrawString(font, score.ToString(), Vector2.Zero, Color.White);

			Globals.SpriteBatch.End();
		}
	}
}
