﻿using GalaxyDefender.Managers;
using GalaxyDefender.Models;
using Microsoft.Xna.Framework.Graphics;

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

		public void Update()
		{
			ship.Update();
			EnemyManager.UpdateEnemies();
			ProjectileManager.UpdateProjectiles();
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
