using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GalaxyDefender.Models
{
	public class Explosion
	{
		private Rectangle sourceRectangle;
		private float frameTime = frameTimeSpeed;
		private const float frameTimeSpeed = 0.05f;
		public int currentFrame;
		private readonly Texture2D texture;
		private readonly Point frameSize;
		public Vector2 position;
		public Explosion(Texture2D Texture, Vector2 Position)
		{
			position = Position;
			texture = Texture;
			frameSize = new(texture.Width / 8, texture.Height);
		}

		private void UpdateAnimation()
		{
			frameTime -= Globals.Time;
			if (frameTime < 0)
			{
				frameTime += frameTimeSpeed;
				currentFrame++;
			}
		}

		private void UpdateRectangle()
		{
			Point location = new(currentFrame * frameSize.X, 0);
			sourceRectangle = new(location, frameSize);
		}

		public void Update()
		{
			UpdateAnimation();
			UpdateRectangle();
		}

		public void Draw()
		{
			Globals.SpriteBatch.Draw(texture, position, sourceRectangle, Color.White);
		}
	}
}
