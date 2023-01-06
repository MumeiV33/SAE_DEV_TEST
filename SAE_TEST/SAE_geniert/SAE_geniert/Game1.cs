using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using System;
using MonoGame.Extended.Content;

namespace SAE_geniert
{
    public class Game1 : Game
    {
        /*=-=-=-=-=-=-=-=-=-=-CHAMPS-=-=-=-=-=-=-=-=-=-*/
        //-----> Map
        private GraphicsDeviceManager _graphics;
        private TiledMap _tiledMap;
        private TiledMap _tiledMapTest;
        private TiledMap _tiledMapTestFin;
        private TiledMap _tiledMapRendererTestFin;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer mapLayer;
        private TiledMapTileLayer mapLayerTest;
        //-----> Perso
        private Vector2 _positionPerso;
        private AnimatedSprite _perso;
        private int _sensPerso;
        private int _vitessePerso;

        //-----> Tortue   /*]=-• COPY CODE TORTUE*/
        private Vector2 _positionTortue;
        private AnimatedSprite _Tortue;
        private int _sensTortue;
        private int _vitesseTortue;


        //-----> Autres
        private KeyboardState _keyboardState;
        private SpriteBatch _spriteBatch;


        /*=-=-=-=-=-=-=-PUBLIC_CONSTANT-=-=-=-=-=-=-*/
        public const int LARGEUR_FENETRE = 496;
        public const int HAUTEUR_FENETRE = 496;
        

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
            _positionPerso = new Vector2(300, 340);
            _vitessePerso = 100;
            _positionTortue = new Vector2(300, 300);   /*]=-• COPY CODE TORTUE*/
            _vitesseTortue = 100;   /*]=-• COPY CODE TORTUE*/

            _sensTortue = 1;   /*]=-• COPY CODE TORTUE*/

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap = Content.Load<TiledMap>("Map_Generale_SilverWorld");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            
            // TODO: use this.Content to load your game content here
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("BryaAnimations.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);

            SpriteSheet spriteSheetTortue = Content.Load<SpriteSheet>("Torute.sf", new JsonContentLoader());
            _Tortue = new AnimatedSprite(spriteSheetTortue);

            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("COLISIONS");
            mapLayerTest = _tiledMap.GetLayer<TiledMapTileLayer>("Calque de Tuiles 2");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _tiledMapRenderer.Update(gameTime);
            // TODO: Add your update logic here

            //-----------------Déplacements-------------------------------------------------------------------
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
            float walkSpeed = deltaSeconds * _vitessePerso; // Vitesse de déplacement du sprite
            KeyboardState keyboardState = Keyboard.GetState();
            _perso.Update(deltaSeconds); // time écoulé

            _keyboardState = Keyboard.GetState();
            //-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^

            //-=-=-=-=-=-=-=-=-=-DROITE-=-=-=-=-=-=-=-=-=-\\

            //-----------------Déplacements-------------------------------------------------------------------
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 0.8);
            //-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^



                if (!IsCollision(tx, ty)) 
                    _positionPerso.X += walkSpeed;
                _perso.Play("walkEast");


            }
            //-=-=-=-=-=-=-=-=-=-GAUCHE-=-=-=-=-=-=-=-=-=-\\

            //-----------------Déplacements-------------------------------------------------------------------
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 0.5);
            //-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^

                if (!IsCollision(tx, ty))
                    _positionPerso.X -= walkSpeed;
                _perso.Play("walkWest");
            }
            //-=-=-=-=-=-=-=-=-=-HAUT-=-=-=-=-=-=-=-=-=-\\

            //-----------------Déplacements-------------------------------------------------------------------
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
            
                if (_keyboardState.IsKeyDown(Keys.Left))
                    _perso.Play("walkWest");
                else if (_keyboardState.IsKeyDown(Keys.Right))
                    _perso.Play("walkEast");
                else
                    _perso.Play("walkNorth");
            //-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^



                if (!IsCollision(tx, ty))
                    _positionPerso.Y -= walkSpeed;

            }
            //-=-=-=-=-=-=-=-=-=-BAS-=-=-=-=-=-=-=-=-=-\\

            //-----------------Déplacements-------------------------------------------------------------------

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 1);

                if (_keyboardState.IsKeyDown(Keys.Left))
                    _perso.Play("walkWest");
                else if (_keyboardState.IsKeyDown(Keys.Right))
                    _perso.Play("walkEast");
                else
                    _perso.Play("walkSouth");
            //-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^
                if (!IsCollision(tx, ty))
                    _positionPerso.Y += walkSpeed;
            }
            
            /*]=-• COPY CODE TORTUE*/
            //--------------Déplacements-Torute-----------------------------------------------------------

            float deltaSecondsTortue = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
                float walkSpeedTortue = deltaSecondsTortue * _vitesseTortue; // Vitesse de déplacement du sprite
                _Tortue.Update(deltaSecondsTortue); // time écoulé
            
            
            //-->> Gauche
            if (_sensTortue == 1)
            {

                ushort txTortue = (ushort)(_positionTortue.X / _tiledMap.TileWidth - 0.5);
                ushort tyTortue = (ushort)(_positionTortue.Y / _tiledMap.TileHeight);


                if (!IsCollision(txTortue, tyTortue))
                {
                    _positionTortue.X -= walkSpeedTortue;
                    _Tortue.Play("walkWest");
                }
                else
                {
                    _sensTortue = 0;
                }


            }
            //-->> Droite
            if (_sensTortue == 0)
            {

                ushort txTortue = (ushort)(_positionTortue.X / _tiledMap.TileWidth + 0.5);
                ushort tyTortue = (ushort)(_positionTortue.Y / _tiledMap.TileHeight);


                if (!IsCollision(txTortue, tyTortue))
                {
                    _positionTortue.X += walkSpeedTortue;
                    _Tortue.Play("walkEast");
                }
                else
                {
                    _sensTortue = 1;
                }


            }
            //ushort txI = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
            //ushort tyI = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
            //if (IsInteraction(tyI,txI))
            //    _positionPerso.Y += walkSpeed;

            //-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^
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
        
        //private bool IsInteraction(ushort x, ushort y)
        //{
        //    // définition de tile qui peut être null (?)
        //    TiledMapTile? tile;
        //    if (mapLayerTest.TryGetTile(x, y, out tile) == false)
        //        if (tile.)
        //        return false;
        //    if (!tile.Value.IsBlank)
        //        return true;

        //    return false;
        //}

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);
            _tiledMapRenderer.Draw();
            
            _spriteBatch.Begin();
            
            _spriteBatch.Draw(_perso, _positionPerso);
            _spriteBatch.Draw(_Tortue, _positionTortue);   /*]=-• COPY CODE TORTUE*/
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}