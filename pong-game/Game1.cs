using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Ping_Pong
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>  
	public class Game1 : Game
	{
		/// <summary>
		/// The width of the game screen.
		/// </summary>
		private const int ScreenWidth = 800; // 640;

		/// <summary>
		/// The height of the game screen.
		/// </summary>
		private const int ScreenHeight = 480;

		/// <summary>
		/// Manages the graphics device.
		/// </summary>
		private readonly GraphicsDeviceManager _graphics;

		/// <summary>
		/// Used for drawing sprites.
		/// </summary>
		private SpriteBatch _spriteBatch;

		/// <summary>
		/// Player 1's score.
		/// </summary>
		private int _score1;

		/// <summary>
		/// Player 2's score.
		/// </summary>
		private int _score2;

		/// <summary>
		/// Font used to display the score.
		/// </summary>
		private SpriteFont _scoreFont;

		/// <summary>
		/// Font used to display the player's name.
		/// </summary>
		private SpriteFont _nameFont;

		/// <summary>
		/// My name.
		/// </summary>
		private string _myName = "Juan Mireles";

		/// <summary>
		/// The game's ball object.
		/// </summary>
		private Ball _ball;

		/// <summary>
		/// Texture for the ball.
		/// </summary>
		private Texture2D _textureBall;

		/// <summary>
		/// Player 1's paddle object.
		/// </summary>
		private Paddle _paddle1;

		/// <summary>
		/// Player 2's paddle object.
		/// </summary>
		private Paddle _paddle2;

		/// <summary>
		/// Texture for the paddles.
		/// </summary>
		private Texture2D _texturePaddle;

		/// <summary>
		/// Texture for the background.
		/// </summary>
		private Texture2D _textureBackground;

		/// <summary>
		/// Sound effect for when the ball hits a paddle.
		/// </summary>
		private SoundEffect _paddleHitSound;

		/// <summary>
		/// Sound effect for when the ball hits a wall.
		/// </summary>
		private SoundEffect _wallHitSound;

		/// <summary>
		/// Background music for the game.
		/// </summary>
		private Song _backgroundMusic;

		/// <summary>
		/// Current keyboard state.
		/// </summary>
		private KeyboardState _currentKeyboardState;

		/// <summary>
		/// Previous keyboard state.
		/// </summary>
		private KeyboardState _previousKeyboardState;

		/// <summary>
		/// Initializes a new instance of the <see cref="Game1"/> class.
		/// </summary>
		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// use a fixed frame rate of 30 frames per second
			IsFixedTimeStep = true;
			TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 33);

			InitScreen();
			InitGameObjects();

			base.Initialize();
		}

		/// <summary>
		/// Initializes screen-related tasks.
		/// </summary>
		private void InitScreen()
		{
			// back buffer
			_graphics.PreferredBackBufferHeight = ScreenHeight;
			_graphics.PreferredBackBufferWidth = ScreenWidth;
			_graphics.PreferMultiSampling = false;
			_graphics.ApplyChanges();
		}

        /// <summary>
        /// Initializes game-related tasks.
        /// </summary>
		private void InitGameObjects()
		{
			// create an instance of our ball
			_ball = new Ball
			{
				// set the size of the ball
				Width = 15.0f,
				Height = 15.0f
			};

			// create 2 instances of our paddle
			_paddle1 = new Paddle();
			_paddle2 = new Paddle();

			// set the size of the paddles
			_paddle1.Width = 15.0f;
			_paddle1.Height = 100.0f;
			_paddle2.Width = 15.0f;
			_paddle2.Height = 100.0f;

			ResetGame();
		}

        /// <summary>
        /// Initializes the play state, called when the game is first run, and whenever a player scores 100 goals.
        /// </summary>
		private void ResetGame()
		{
			// reset scores
			_score1 = 0;
			_score2 = 0;

			// place the ball at the center of the screen
			_ball.X =
				(float)ScreenWidth / 2 - _ball.Width / 2;
			_ball.Y =
				(float)ScreenHeight / 2 - _ball.Height / 2;

			// Randomize the initial speed and direction of the ball
			var random = new Random();
			if (random.Next(2) == 0) // 50% chance for positive or negative
			{
				// Positive x direction: [4, 6]
				_ball.Dx = 4 + (float)(random.NextDouble() * 2);
			}
			else
			{
				// Negative x direction: [-6, -4]
				_ball.Dx = -6 + (float)(random.NextDouble() * 2);
			}

			if (random.Next(2) == 0)
			{
				// Positive y direction: [3, 5]
				_ball.Dy = 3 + (float)(random.NextDouble() * 2);
			}
			else
			{
				// Negative y direction: [-5, -3]
				_ball.Dy = -5 + (float)(random.NextDouble() * 2);
			}

			// place the paddles at either end of the screen
			_paddle1.X = 30;
			_paddle1.Y =
				(float)ScreenHeight / 2 - _paddle1.Height / 2;
			_paddle2.X =
				ScreenWidth - 30 - _paddle2.Width;
			_paddle2.Y =
				(float)ScreenHeight / 2 - _paddle1.Height / 2;
		}

        /// <summary>
        /// LoadContent will be called once per game and is the place to load all of your content.
        /// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			LoadGameGraphics();
			LoadGameAudio();
		}

        /// <summary>
        /// Loads game graphics from disk.
        /// </summary>
		private void LoadGameGraphics()
		{
			_textureBall =
				Content.Load<Texture2D>(@"media\ball");
			_ball.Visual = _textureBall;

			_texturePaddle =
				Content.Load<Texture2D>(@"media\paddle");
			_paddle1.Visual = _texturePaddle;
			_paddle2.Visual = _texturePaddle;

			_scoreFont =
				Content.Load<SpriteFont>(@"media\ScoreFont");
			_nameFont = Content.Load<SpriteFont>(@"media\NameFont");

			_textureBackground = Content.Load<Texture2D>(@"media\background");
		}

        /// <summary>
        /// Loads game audio from disk.
        /// </summary>
		private void LoadGameAudio()
		{
			_paddleHitSound = Content.Load<SoundEffect>(@"audio\paddle-hit");
			_wallHitSound = Content.Load<SoundEffect>(@"audio\wall-hit");
			_backgroundMusic = Content.Load<Song>(@"audio\background-music");

			MediaPlayer.IsRepeating = true;
			MediaPlayer.Volume = 0.5f;
			MediaPlayer.Play(_backgroundMusic);
		}

        /// <summary>
        /// Toggles the background music.
        /// </summary>
		private void ToggleMusic()
		{
			if (MediaPlayer.State == MediaState.Playing)
			{
				MediaPlayer.Pause();
			}
			else if (MediaPlayer.State == MediaState.Paused)
			{
				MediaPlayer.Resume();
			}
			else if (MediaPlayer.State == MediaState.Stopped)
			{
				MediaPlayer.Play(_backgroundMusic);
			}
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			_previousKeyboardState = _currentKeyboardState;
			_currentKeyboardState = Keyboard.GetState();

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
				_currentKeyboardState.IsKeyDown(Keys.Escape) || _currentKeyboardState.IsKeyDown(Keys.Q))
			{
				Exit();
			}

			if (_currentKeyboardState.IsKeyDown(Keys.P) && !_previousKeyboardState.IsKeyDown(Keys.P))
			{
				ToggleMusic();
			}

			if (_currentKeyboardState.IsKeyDown(Keys.R) && !_previousKeyboardState.IsKeyDown(Keys.R))
			{
				ResetGame();
			}

			UpdateBallLocation();
			UpdatePaddlesLocation();

			base.Update(gameTime);
		}

        /// <summary>
        /// Moves the ball based on its current Dx and Dy settings and checks for collisions.
        /// </summary>
		private void UpdateBallLocation()
		{
			// actually move the ball
			_ball.X += _ball.Dx;
			_ball.Y += _ball.Dy;

			// did ball touch top or bottom side?
			if (_ball.Y <= 0 ||
				_ball.Y >= ScreenHeight - _ball.Height)
			{
				// reverse vertical direction
				_ball.Dy *= -1;

				_wallHitSound.Play();
			}

			// did ball touch the left side?
			if (_ball.X <= 0)
			{
				// at higher speeds, the ball can leave the 
				// playing field, make sure that doesn't happen
				_ball.X = 0;

				// increment player 2's score
				_score2++;

				// reduce speed, reverse direction
				_ball.Dx = 5.0f;

				_wallHitSound.Play();
			}

			// did ball touch the right side?
			if (_ball.X >= ScreenWidth - _ball.Width)
			{
				// at higher speeds, the ball can leave the 
				// playing field, make sure that doesn't happen
				_ball.X = ScreenWidth - _ball.Width;

				// increment player 1's score
				_score1++;

				// reduce speed, reverse direction
				_ball.Dx = -5.0f;

				_wallHitSound.Play();
			}

			// Reset game if a player scores 100 goals
			if (_score1 >= 100 || _score2 >= 100)
			{
				ResetGame();
			}

			// did ball hit the paddle from the front?
			if (CollisionOccurred())
			{
				// reverse hoizontal direction
				_ball.Dx *= -1;

				// increase the speed a little.
				_ball.Dx *= 1.15f;

				_paddleHitSound.Play(0.3f, 0.0f, 0.0f);
			}
		}

        /// <summary>
        /// Checks for a collision between the ball and paddles.
        /// </summary>
        /// <returns>True if a collision occurred, false otherwise.</returns>
		private bool CollisionOccurred()
		{
			bool retval;

			// heading towards player one
			if (_ball.Dx < 0)
			{
				Rectangle b = _ball.Rect;
				Rectangle p = _paddle1.Rect;
				retval =
					b.Left < p.Right &&
					b.Right > p.Left &&
					b.Top < p.Bottom &&
					b.Bottom > p.Top;
			}
			// heading towards player two
			else // _ball.Dx > 0
			{
				Rectangle b = _ball.Rect;
				Rectangle p = _paddle2.Rect;
				retval =
					b.Left < p.Right &&
					b.Right > p.Left &&
					b.Top < p.Bottom &&
					b.Bottom > p.Top;
			}

			return retval;
		}

        /// <summary>
        /// The amount to move the paddle each frame.
        /// </summary>
		private const float PaddleStride = 10.0f;

        /// <summary>
        /// Moves the paddles based on player input.
        /// </summary>
		private void UpdatePaddlesLocation()
		{
			// define bounds for the paddles
			float minY = 0.0f;
			float maxY = ScreenHeight - _paddle1.Height;

			// get player input
			GamePadState pad1 =
				GamePad.GetState(PlayerIndex.One);
			GamePadState pad2 =
				GamePad.GetState(PlayerIndex.Two);
			KeyboardState keyb =
				Keyboard.GetState();

			// check the controller, PLAYER ONE
			bool PlayerUp =
				pad1.DPad.Up == ButtonState.Pressed;
			bool PlayerDown =
				pad1.DPad.Down == ButtonState.Pressed;

			// also check the keyboard, PLAYER ONE
			PlayerUp |= keyb.IsKeyDown(Keys.W);
			PlayerDown |= keyb.IsKeyDown(Keys.S);

			// move the paddle
			if (PlayerUp)
			{
				_paddle1.Y -= PaddleStride;
				if (_paddle1.Y < minY)
				{
					_paddle1.Y = minY;
				}
			}
			else if (PlayerDown)
			{
				_paddle1.Y += PaddleStride;
				if (_paddle1.Y > maxY)
				{
					_paddle1.Y = maxY;
				}
			}

			// check the controller, PLAYER TWO
			PlayerUp =
				pad2.DPad.Up == ButtonState.Pressed;
			PlayerDown =
				pad2.DPad.Down == ButtonState.Pressed;

			// also check the keyboard, PLAYER TWO
			PlayerUp |= keyb.IsKeyDown(Keys.Up);
			PlayerDown |= keyb.IsKeyDown(Keys.Down);

			// move the paddle
			if (PlayerUp)
			{
				_paddle2.Y -= PaddleStride;
				if (_paddle2.Y < minY)
				{
					_paddle2.Y = minY;
				}
			}
			else if (PlayerDown)
			{
				_paddle2.Y += PaddleStride;
				if (_paddle2.Y > maxY)
				{
					_paddle2.Y = maxY;
				}
			}
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			// our game-specific drawing logic
			Render();

			base.Draw(gameTime);
		}

        /// <summary>
        /// Draws the score at the specified location.
        /// </summary>
        /// <param name="x">The x-coordinate of the score.</param>
        /// <param name="y">The y-coordinate of the score.</param>
        /// <param name="score">The score to draw.</param>
		private void DrawScore(float x, float y, int score)
		{
			string scoreText = $"{score}";
			_spriteBatch.DrawString(_scoreFont, scoreText, new Vector2(x, y), Color.OrangeRed);
		}

        /// <summary>
        /// Draws the game objects.
        /// </summary>
		private void Render()
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// start rendering our game graphics
			_spriteBatch.Begin();

			_spriteBatch.Draw(_textureBackground, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);

			// draw the score first, so the ball can
			// move over it without being obscured
			DrawScore(ScreenWidth * 0.25f,
				20, _score1);
			DrawScore(ScreenWidth * 0.65f,
				20, _score2);

			Vector2 textSize = _nameFont.MeasureString(_myName);
			float x = (GraphicsDevice.Viewport.Width - textSize.X) / 2;
			Vector2 textPosition = new Vector2(x, 30);

			_spriteBatch.DrawString(_nameFont, _myName, textPosition, Color.OrangeRed);

			// render the game objects
			_spriteBatch.Draw((Texture2D)_ball.Visual,
				_ball.Rect, Color.Orange);
			_spriteBatch.Draw((Texture2D)_paddle1.Visual,
				_paddle1.Rect, Color.Orange);
			_spriteBatch.Draw((Texture2D)_paddle2.Visual,
				_paddle2.Rect, Color.Orange);

			// we're done drawing
			_spriteBatch.End();
		}

	}
}
