using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GalaxyDefender
{
	public class Game1 : Game
	{
		private readonly GraphicsDeviceManager graphics;
		private GameManager gameManager;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			Globals.WindowSize = new(800, 600);
			graphics.PreferredBackBufferWidth = Globals.WindowSize.X;
			graphics.PreferredBackBufferHeight = Globals.WindowSize.Y;
			graphics.ApplyChanges();

			Globals.Content = Content;
			base.Initialize();
		}

		protected override void LoadContent()
		{
			Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
			Globals.GraphicsDevice = GraphicsDevice;

			gameManager = new();
		}

		protected override void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			Globals.Update(gameTime);
			InputManager.Update();
			gameManager.Update();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			gameManager.Draw();

			base.Draw(gameTime);
		}
	}
}
