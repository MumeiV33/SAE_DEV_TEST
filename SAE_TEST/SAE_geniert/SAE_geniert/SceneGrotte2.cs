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
    public class SceneGrotte2 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public SceneGrotte2(Game1 game) : base(game) { }

        //------> Map
        private GraphicsDeviceManager _graphics;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer _mapLayer;

        //-----> Perso
        public Joueur _player = new Joueur();
        private Vector2 _positionPerso;
        private AnimatedSprite _perso;
        private int _vitessePerso;


        //-----> Tortue
        public Enemis _Enemis = new Enemis();

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


            _Enemis._positionTortue = new Vector2(275, 360);
            _Enemis._vitesseTortue = 100;
            _Enemis._sensTortue = 1;

            Game.Window.Title = "Silver World";
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

           

            base.Initialize();
        }





        public override void LoadContent()
        {

            _tiledMap = Content.Load<TiledMap>("MapGrotte2");

            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);


            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("BryaAnimations.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);

            SpriteSheet spriteSheetTortue = Content.Load<SpriteSheet>("Torute.sf", new JsonContentLoader());
            _Tortue = new AnimatedSprite(spriteSheetTortue);


            /*SpriteSheet spriteSheetChainsaw = Content.Load<SpriteSheet>("chainsaw.sf", new JsonContentLoader());
            _chainsaw = new AnimatedSprite(spriteSheetChainsaw);*/

            _mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Colision");
            Game.mapLayer = _mapLayer;

            base.LoadContent();
        }






        public override void Update(GameTime gameTime)
        {
            _tiledMapRenderer.Update(gameTime);

            
            float deltaSecondsTortue = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
            float walkSpeedTortue = deltaSecondsTortue * _vitesseTortue; // Vitesse de déplacement du sprite
            _Enemis.DeplacementsTortue(deltaSecondsTortue, _tiledMap, _mapLayer, this);

            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime

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
                if (!IsCollision(tx, ty, _mapLayer))
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

                if (!IsCollision(tx, ty, _mapLayer))
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

                if (!IsCollision(tx, ty, _mapLayer))
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

                if (!IsCollision(tx, ty, _mapLayer))
                    _positionPerso.Y += walkSpeed;
            }






            //-->> Gauche
            if (_sensTortue == 1)
            {
                ushort txTortue = (ushort)(_positionTortue.X / _tiledMap.TileWidth - 0.5);
                ushort tyTortue = (ushort)(_positionTortue.Y / _tiledMap.TileHeight);
                if (!IsCollision(txTortue, tyTortue, _mapLayer))
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
                if (!IsCollision(txTortue, tyTortue, _mapLayer))
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


        private bool IsCollision(ushort x, ushort y, TiledMapTileLayer __mapLayer)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (__mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }



        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _tiledMapRenderer.Draw();

            Game.SpriteBatch.Begin();
            Game.SpriteBatch.Draw(_Tortue, _Enemis._positionTortue);
            Game.SpriteBatch.Draw(Game._player._perso, Game._player._positionPerso);
            Game.SpriteBatch.End();
        }

    }
}
