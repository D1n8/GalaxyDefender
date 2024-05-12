using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GalaxyDefender.Models
{
	public class Enemy
	{
		public Vector2 position = new(Globals.WindowSize.X / 2, Globals.WindowSize.Y - 100);
		private readonly Texture2D texture;
		private readonly float speed = 250f;
		private Vector2 direction = Vector2.Zero;
		private Vector2 destination;
		private float frameTime = 0.1f;
		private int currentFrame;
		private int increment = 1;
		private Rectangle sourceRectangle;
		private readonly Point frameSize;
		public Rectangle CollisionRectangle => new(position.ToPoint(), frameSize);
		public Enemy(Texture2D Texture)
		{
			texture = Texture;
			frameSize = new(texture.Width / 3, texture.Height);
		}

		public void Update()
		{
			UpdateAnimation();
			UpdateRectangle();
			UpdatePosition();
		}

		private void UpdatePosition()
		{
			position += direction * speed * Globals.Time;

			if (Vector2.Distance(position, destination) < 5f)
			{
				SetRandomDestination();
			}
		}

		public void SetRandomDestination()
		{
			Random r = new();
			var x = r.Next(Globals.WindowSize.X - 48);
			var y = r.Next(Globals.WindowSize.Y - 48);
			destination = new(x, y);

			direction = destination - position;
			if (direction != Vector2.Zero) direction.Normalize();
		}

		private void UpdateRectangle()
		{
			Point location = new(currentFrame * frameSize.X, 0);
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

		public void Draw()
		{
			Globals.SpriteBatch.Draw(texture, position, sourceRectangle, Color.White);
		}
	}
}
