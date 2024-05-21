using GalaxyDefender.Managers;
using GalaxyDefender.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalaxyDefender
{
	public class GameManager
	{
		private readonly PlayerShip ship;
		private readonly SpriteFont font;
		private static int score;
		private static int highScore;
		private readonly Texture2D backgroundTexture;

		public GameManager()
		{
			font = Globals.Content.Load<SpriteFont>("font");
			ship = new(Globals.Content.Load<Texture2D>("ship"));
			backgroundTexture = Globals.Content.Load<Texture2D>("background");
			EnemyManager.AddEnemy();
		}

		public static void Restart(PlayerShip playerShip)
		{
			if (score > highScore)
			{
				highScore = score;
			}
			playerShip.Restart();
			EnemyManager.Restart();
			ProjectileManager.Restart();
			ExplosionManager.Restart();
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
						ExplosionManager.AddExplosion(enemy.position);
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
			ExplosionManager.UpdateExplosions();
		}

		public void Draw()
		{
			Globals.SpriteBatch.Begin();

			Globals.SpriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, Globals.GraphicsDevice.Viewport.Width, Globals.GraphicsDevice.Viewport.Height), Color.White);

			EnemyManager.DrawEnemies();
			ProjectileManager.DrawProjectiles();
			ExplosionManager.DrawExplosions();
			ship.Draw();

			Globals.SpriteBatch.DrawString(font, "Score: " + score.ToString(), new Vector2(10, 10), Color.White);
			Globals.SpriteBatch.DrawString(font, "High Score: " + highScore, 
				new Vector2(Globals.GraphicsDevice.Viewport.Width - font.MeasureString("High Score: " + highScore).X - 10, 10), Color.White);

			Globals.SpriteBatch.End();
		}
	}
}
