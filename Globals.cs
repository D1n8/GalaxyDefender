using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GalaxyDefender
{
	public static class Globals
	{
		public static float Time { get; set; }
		public static SpriteBatch SpriteBatch { get; set; }
		public static Point WindowSize { get; set; }
		public static ContentManager Content { get; set; }
		public static GraphicsDevice GraphicsDevice { get; set; }
		public static Texture2D Texture { get; set; }
		public static void Update(GameTime gameTime)
		{
			Time = (float)gameTime.ElapsedGameTime.TotalSeconds;
		}
	}
}
