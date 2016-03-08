using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace muru3d
{
    public class Muru3d : Game
    {
        const float VIEW_ROTATION_SPEED = 0.1f;

        private Camera _camera;
        private GraphicsDeviceManager _graphicsDeviceManager;
        private Grid _grid;
        private Model _model;
        private SpriteBatch _spriteBatch;

        public Muru3d()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            _graphicsDeviceManager.PreferMultiSampling = true;
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _camera = new Camera();
            _grid = new Grid(_camera);
            _model = new Model(_graphicsDeviceManager.GraphicsDevice);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var keyboardState = Keyboard.GetState();
            foreach (var pressedKey in keyboardState.GetPressedKeys())
            {
                switch (pressedKey)
                {
                    case Keys.Up:
                        _camera.Tilt += VIEW_ROTATION_SPEED;
                        break;
                    case Keys.Down:
                        _camera.Tilt -= VIEW_ROTATION_SPEED;
                        break;
                    case Keys.Left:
                        _camera.Rotation -= VIEW_ROTATION_SPEED;
                        break;
                    case Keys.Right:
                        _camera.Rotation += VIEW_ROTATION_SPEED;
                        break;
                    case Keys.Add:
                    case Keys.OemPlus:
                        _camera.Zoom *= 1 - 0.1f;
                        break;
                    case Keys.Subtract:
                    case Keys.OemMinus:
                        _camera.Zoom *= 1 + 0.1f;
                        break;
                }
            }

            //var keys = string.Join(", ", keyboardState.GetPressedKeys().Select(x => x.ToString()));
            //System.Console.WriteLine(keys);

            _camera.Sanitize();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphicsDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            var graphicsDevice = _graphicsDeviceManager.GraphicsDevice;

            graphicsDevice.BlendState = BlendState.Opaque;
            graphicsDevice.DepthStencilState = DepthStencilState.Default;
            graphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
            graphicsDevice.RasterizerState = RasterizerState.CullNone;

            _model.Draw(_camera);

            _grid.Draw(_graphicsDeviceManager.GraphicsDevice);

            base.Draw(gameTime);
        }
    }
}
