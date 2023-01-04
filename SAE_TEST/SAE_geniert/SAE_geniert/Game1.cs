using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using System;

namespace SAE_geniert
{
    public class Game1 : Game
    {
        /*=-=-=-=-=-=-=-=-=-=-CHAMPS-=-=-=-=-=-=-=-=-=-*/
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private Vector2 _positionPerso;
        private AnimatedSprite _perso;
        private KeyboardState _keyboardState;
        private int _sensPerso;
        private int _vitessePerso;
        private TiledMapTileLayer mapLayer;

        /*=-=-=-=-=-=-=-PUBLIC_CONSTANT-=-=-=-=-=-=-*/
        public const int LARGEUR_FENETRE = 800;
        public const int HAUTEUR_FENETRE = 640;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = LARGEUR_FENETRE;
            _graphics.PreferredBackBufferHeight = HAUTEUR_FENETRE;
            _graphics.ApplyChanges();
            // TODO: Add your initialization logic here
            Window.Title = "Silver World";
            GraphicsDevice.BlendState = BlendState.AlphaBlend;



            _positionPerso = new Vector2(20, 340);
            _vitessePerso = 100;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap = Content.Load<TiledMap>("mapGenerale");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            // TODO: use this.Content to load your game content here

            var spriteSheet = Content.Load<SpriteSheet>("Viki_M.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstacles");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _tiledMapRenderer.Update(gameTime);
            // TODO: Add your update logic here


            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
            float walkSpeed = deltaSeconds * _vitessePerso; // Vitesse de déplacement du sprite
            KeyboardState keyboardState = Keyboard.GetState();
            String animation = "idle";
            _perso.Update(deltaSeconds); // time écoulé

            _keyboardState = Keyboard.GetState();

            //-=-=-=-=-=-=-=-=-=-DROITE-=-=-=-=-=-=-=-=-=-\\
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 1);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 1);
                animation = "walkEast";

                if (!IsCollision(tx, ty))
                    _positionPerso.X += walkSpeed;
                _perso.Play("walkEast");

            }
            //-=-=-=-=-=-=-=-=-=-GAUCHE-=-=-=-=-=-=-=-=-=-\\

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 1);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 1);
                animation = "walkWest";

                if (!IsCollision(tx, ty))
                    _positionPerso.X -= walkSpeed;
                _perso.Play("walkWest");
            }
            //-=-=-=-=-=-=-=-=-=-HAUT-=-=-=-=-=-=-=-=-=-\\

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                animation = "walkNorth";

                if (_keyboardState.IsKeyDown(Keys.Left))
                    _perso.Play("walkWest");
                if (_keyboardState.IsKeyDown(Keys.Right))
                    _perso.Play("walkEast");
                else
                    _perso.Play("walkNorth");


                if (!IsCollision(tx, ty))
                    _positionPerso.Y -= walkSpeed;
            }
            //-=-=-=-=-=-=-=-=-=-BAS-=-=-=-=-=-=-=-=-=-\\

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 1.5);
                animation = "walkSouth";

                if (_keyboardState.IsKeyDown(Keys.Left))
                    _perso.Play("walkWest");
                if (_keyboardState.IsKeyDown(Keys.Right))
                    _perso.Play("walkEast");
                else
                    _perso.Play("walkSouth");

                if (!IsCollision(tx, ty))
                    _positionPerso.Y += walkSpeed;
            }


            base.Update(gameTime);
        }
        private bool IsCollision(ushort x, ushort y)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);
            _tiledMapRenderer.Draw();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}