using GalaxyDefender.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GalaxyDefender.Managers
{
	public static class ExplosionManager
	{
		public static readonly List<Explosion> Explosions = new();
		private static Texture2D texture;

		public static void Restart()
		{
			Explosions.Clear();
		}

		public static void AddExplosion(Vector2 position)
		{
			texture ??= Globals.Content.Load<Texture2D>("explosion");
			Explosion explosion = new(texture, position);
			Explosions.Add(explosion);
		}

		public static void UpdateExplosions()
		{
			Explosions.ForEach(e => e.Update());
			Explosions.RemoveAll(e => e.currentFrame > 7);
		}

		public static void DrawExplosions()
		{
			Explosions.ForEach(e => e.Draw());
		}
	}
}
