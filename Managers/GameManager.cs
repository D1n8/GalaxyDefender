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
		}

		public void Update()
		{
			ship.Update();
		}

		public void Draw()
		{
			Globals.SpriteBatch.Begin();
			ship.Draw();
			Globals.SpriteBatch.End();
		}
	}
}
