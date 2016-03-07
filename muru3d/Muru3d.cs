using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace muru3d
{
    public class Muru3d : Game
    {
        const float VIEW_ROTATION_SPEED = 0.1f;


        private BasicEffect _basicEffect;
        private Camera _camera;
        private GraphicsDeviceManager _graphicsDeviceManager;
        private Grid _grid;
        private Model _model;
        private SpriteBatch _spriteBatch;

        public Muru3d()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);   
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
            _basicEffect = new BasicEffect(_graphicsDeviceManager.GraphicsDevice);
            _grid = new Grid(_camera);
            _model = new Model();
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

            var keyBoardState = Keyboard.GetState();
            foreach (var pressedKey in keyBoardState.GetPressedKeys())
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
                }
                _camera.Sanitize();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphicsDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);


            var graphicsDevice = _graphicsDeviceManager.GraphicsDevice;

            // These three lines are required if you use SpriteBatch, to reset the states that it sets
            graphicsDevice.BlendState = BlendState.Opaque;
            graphicsDevice.DepthStencilState = DepthStencilState.Default;
            graphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
            // graphicsDevice.RasterizerState = RasterizerState.CullNone;

            _basicEffect.World = Matrix.Identity;

            _basicEffect.View = _camera.ViewMatrix();
            _basicEffect.Projection = _camera.ProjectionMatrix(graphicsDevice.Viewport.AspectRatio);

            _basicEffect.DiffuseColor = Color.White.ToVector3();
            _basicEffect.EnableDefaultLighting();

            _model.Draw(_graphicsDeviceManager.GraphicsDevice, _basicEffect);

            _grid.Draw(_graphicsDeviceManager.GraphicsDevice);

            base.Draw(gameTime);
        }
    }
}
