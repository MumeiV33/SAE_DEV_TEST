 using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using System;
using System.Windows;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;


namespace SAE_geniert
{
    public class SceneGrotte : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public SceneGrotte(Game1 game) : base(game) { }

        //------> Map
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
        private int _vitessePerso;


        //-----> Tortue
        private Vector2 _positionTortue;
        private AnimatedSprite _Tortue;
        private int _sensTortue;
        private int _vitesseTortue;

        //-----> chainsaw
        private Vector2 _positionChainsaw;
        private AnimatedSprite _chainsaw;
        private int _sensChainsaw;
        private int _vitesseChainsaw;


        private readonly ScreenManager _screenManager;
        
        
        
        //-----> Autres
        private KeyboardState _keyboardState;



        public Joueur _player = new Joueur();
        private SpriteBatch _spriteBatch;
        /*=-=-=-=-=-=-=-PUBLIC_CONSTANT-=-=-=-=-=-=-*/
        public const int LARGEUR_FENETRE = 512;
        public const int HAUTEUR_FENETRE = 512;

        public SpriteBatch SpriteBatch
        {
            get
            {
                return this._spriteBatch;
            }

            set
            {
                this._spriteBatch = value;
            }
        }

        public override void Initialize()
        {

            Console.WriteLine("grotte");


            _positionTortue = new Vector2(300, 300);   
            _vitesseTortue = 100;   
            _sensTortue = 1;

            _positionPerso = new Vector2(30, 30);
            _graphics.ApplyChanges();
            Game.Window.Title = "Silver World";
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _player._positionPerso = new Vector2(30, 30);
            
            base.Initialize();
        }





        public override void LoadContent()
        {

            _tiledMap = Content.Load<TiledMap>("MapGrotte");
            
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);


            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("BryaAnimations.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);

            
            SpriteSheet spriteSheetChainsaw = Content.Load<SpriteSheet>("chainsaw.sf", new JsonContentLoader());
            _chainsaw = new AnimatedSprite(spriteSheetChainsaw);

            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Calque de Tuiles 2");
            
            base.LoadContent();
        }






        public override void Update(GameTime gameTime)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
            _tiledMapRenderer.Update(gameTime);

            //-----------------Déplacements--------
            
            float walkSpeed = deltaSeconds * _vitessePerso; // Vitesse de déplacement du sprite
            KeyboardState keyboardState = Keyboard.GetState();
            _perso.Update(deltaSeconds); // time écoulé
            _keyboardState = Keyboard.GetState();
            
            //-=-=-=-=-=-=-=-=-=-DROITE-=-=-=-=-=-=-=-=-=-\\

            //-----------------Déplacements---------
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 0.8);
                //-^-^-^-^-^-^-^-^--COLLISIONS--^-^-^-^-^-^-^-^
                if (!IsCollision(tx, ty))
                    _positionPerso.X += walkSpeed;
                _perso.Play("walkEast");
            }
            //-=-=-=-=-=-=-=-=-=-GAUCHE-=-=-=-=-=-=-=-=-=-\\

            //-----------------Déplacements-------------
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 0.5);
             //-^-^-^-^-^-^-^-^--COLLISIONS--^-^-^-^-^-^-^-^

                if (!IsCollision(tx, ty))
                    _positionPerso.X -= walkSpeed;
                _perso.Play("walkWest");
            }
            //-=-=-=-=-=-=-=-=-=-HAUT-=-=-=-=-=-=-=-=-=-\\

            //-----------------Déplacements----------
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
            //-^-^-^-^-^-^-^-^--COLLISIONS--^-^-^-^-^-^-^-^

                if (!IsCollision(tx, ty))
                    _positionPerso.Y -= walkSpeed;
            }


            //-=-=-=-=-=-=-=-=-=-BAS-=-=-=-=-=-=-=-=-=-\\

            //-----------------Déplacements--------

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
            //-^-^-^-^-^-^-^-^--COLLISIONS--^-^-^-^-^-^-^-^

                if (!IsCollision(tx, ty))
                    _positionPerso.Y += walkSpeed;
            }



            /*]=-• COPY CODE TORTUE*/
            //--------------Déplacements-Torute--------

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



        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _tiledMapRenderer.Draw();

            SpriteBatch.Begin();
            SpriteBatch.Draw(_player._perso, _positionPerso);
            SpriteBatch.End();
        }

    }
}
