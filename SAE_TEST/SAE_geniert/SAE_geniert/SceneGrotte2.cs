﻿using Microsoft.Xna.Framework;
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
        private AnimatedSprite _Chainsaw;
        private int _sensChainsaw;
        public int _vitesseChainsaw;


        private readonly ScreenManager _screenManager;



        //-----> Autres
        private KeyboardState _keyboardState;
        public float niveauGravite = 1;
        public float niveauSaut = 8;

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


            _positionTortue = _Enemis._positionTortue = new Vector2(275, 360);
            _vitesseTortue = _Enemis._vitesseTortue = 100;
            _sensTortue = _Enemis._sensTortue = 0;

            _positionChainsaw = _Enemis._positionChainsaw = new Vector2(103, 481);
            _vitesseChainsaw = _Enemis._vitesseChainsaw = 100;
            _sensChainsaw = _Enemis._sensChainsaw = 1;

            Game.Window.Title = "Silver World";
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            Game._player._positionPerso = new Vector2(30, 60);

            base.Initialize();
        }





        public override void LoadContent()
        {

            _tiledMap = Content.Load<TiledMap>("MapGrotte");

            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);


            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("BryaAnimations.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);

            SpriteSheet spriteSheetTortue = Content.Load<SpriteSheet>("Torute.sf", new JsonContentLoader());
            _Tortue = new AnimatedSprite(spriteSheetTortue);

            SpriteSheet spriteSheetChainsaw = Content.Load<SpriteSheet>("chainsaw.sf", new JsonContentLoader());
            _Chainsaw = new AnimatedSprite(spriteSheetChainsaw);

            _mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Colision");
            Game.mapLayer = _mapLayer;


            base.LoadContent();
        }






        public override void Update(GameTime gameTime)
        {

            _keyboardState = Keyboard.GetState();
            _tiledMapRenderer.Update(gameTime);


            float deltaSecondsTortue = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime                   Tortue
            float walkSpeedTortue = deltaSecondsTortue * _vitesseTortue; // Vitesse de déplacement du sprite        tortue      


            float deltaSecondsChainsaw = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime                 Chainsaw
            float walkSpeedChainsaw = deltaSecondsChainsaw * _vitesseChainsaw; // Vitesse de déplacement du sprite    Chainsaw


            _Enemis.DeplacementsTortue(deltaSecondsTortue, _tiledMap, _mapLayer, this);
            _Enemis.DeplacementsChainsaw(deltaSecondsChainsaw, _tiledMap, _mapLayer, this);

            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
            float walkSpeed = deltaSeconds * _player._vitessePerso; // Vitesse de déplacement du sprite

            KeyboardState keyboardState = Keyboard.GetState();
            //=================GRAVITY=================\\
            if (!keyboardState.IsKeyDown(Keys.Space))
            {
                ushort txGravity = (ushort)(Game._player._positionPerso.X / _tiledMap.TileWidth);
                ushort tyGravity = (ushort)(Game._player._positionPerso.Y / _tiledMap.TileHeight + 0.5);

                if (!IsCollision(txGravity, tyGravity, _mapLayer))
                { 
                    Game._player._positionPerso.Y += niveauGravite;
                    _perso.Play("idle");
                }
                if (keyboardState.IsKeyDown(Keys.Space) && IsCollision(txGravity, tyGravity, _mapLayer))
                    Game._player._positionPerso.Y -= niveauSaut;

                
            }



            _perso.Update(deltaSeconds); // time écoulé



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
            Game.SpriteBatch.Draw(_Chainsaw, _Enemis._positionChainsaw);
            Game.SpriteBatch.Draw(Game._player._perso, Game._player._positionPerso);
            Game.SpriteBatch.End();
        }

    }
}
