using GalaxyDefender;
using System;

namespace GalaxyDefender;

public static class Program
{
	private static void Main()
	{
		using var game = new Game1();
		game.Run();
	}
}