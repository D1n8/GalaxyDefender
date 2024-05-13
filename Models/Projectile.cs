using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GalaxyDefender.Models
{
	public class Projectile
	{
		private readonly Texture2D texture;
		public Vector2 position;
		private readonly float speed;
		private Vector2 direction;
		public Rectangle CollisionRectangle => new(position.ToPoint(), texture.Bounds.Size);

		public Projectile(Texture2D Texture, Vector2 Position, Vector2 Direction, float Speed)
		{
			texture = Texture;
			position = Position;
			speed = Speed;
			direction = Direction;
		}



		public void Update()
		{
			position += direction * speed * Globals.Time;
		}

		public void Draw()
		{
			Globals.SpriteBatch.Draw(texture, position, Color.White);
		}
	}
}
