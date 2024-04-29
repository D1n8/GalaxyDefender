using Microsoft.Xna.Framework.Input;

namespace GalaxyDefender
{
	public static class InputManager
	{
		public static bool IsKeyDown(Keys key)
		{
			return Keyboard.GetState().IsKeyDown(key);
		}

		public static void Update()
		{
		}
	}
}
