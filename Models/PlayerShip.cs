﻿using GalaxyDefender.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GalaxyDefender.Models
{
	public class PlayerShip
	{
		public Vector2 position = new(Globals.WindowSize.X / 2, Globals.WindowSize.Y - 100);
		private readonly Texture2D texture;
		private readonly float speed = 500f;
		private const float shotTime = 0.25f;
		private float shotCooldown = shotTime;
		private Vector2 direction = Vector2.Zero;
		private float frameTime = 0.1f;
		private int currentFrame;
		private int increment = 1;
		private Rectangle sourceRectangle;
		private readonly Point frameSize;
		public Rectangle CollisionRectangle => new(position.ToPoint(), frameSize);

		public PlayerShip(Texture2D Texture)
		{
			texture = Texture;
			frameSize = new(texture.Width / 3, texture.Height / 3);
		}

		public void Restart()
		{
			shotCooldown = shotTime;
			position = new(Globals.WindowSize.X / 2, Globals.WindowSize.Y - 100);
		}

		public void Update()
		{
			UpdateAnimation();
			UpdateControls();
			UpdateRectangle();
			UpdatePosition();
			UpdatePlayerShipProjectiles();
		}

		private void UpdateControls()
		{
			direction = Vector2.Zero;

			if (InputManager.IsKeyDown(Keys.A)) direction.X = -1;
			if (InputManager.IsKeyDown(Keys.D)) direction.X = 1;
			if (InputManager.IsKeyDown(Keys.W)) direction.Y = -1;
			if (InputManager.IsKeyDown(Keys.S)) direction.Y = 1;

			if (direction != Vector2.Zero) direction.Normalize();
		}

		private void UpdatePosition()
		{
			position += direction * speed * Globals.Time;
			position = Vector2.Clamp(position, Vector2.Zero, new(Globals.WindowSize.X - frameSize.X, Globals.WindowSize.Y - frameSize.Y));
		}

		private void UpdateRectangle()
		{
			var row = 0;
			if (direction.X > 0) row = 1;
			if (direction.X < 0) row = 2;
			Point location = new(currentFrame * frameSize.X, row * frameSize.Y);
			sourceRectangle = new(location, frameSize);
		}
		
		private void UpdateAnimation()
		{
			frameTime -= Globals.Time;
			if (frameTime < 0)
			{
				frameTime += 0.1f;
				currentFrame += increment;

				if (currentFrame == 2) increment = -1;
				if (currentFrame == 0) increment = 1;
			}
		}

		private void UpdatePlayerShipProjectiles()
		{
			shotCooldown -= Globals.Time;
			if (shotCooldown < 0)
			{
				shotCooldown += shotTime;
				ProjectileManager.AddProjectile(new(position.X + 20, position.Y));
			}
		}

		public void Draw()
		{
			Globals.SpriteBatch.Draw(texture, position, sourceRectangle, Color.White);
		}
	}
}
