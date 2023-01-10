﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using System;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

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


        private int LARGEUR_FENETRE = 496;
        private int HAUTEUR_FENETRE = 496;

        //-----> Perso
        //private Joueur _positionPerso;
        //private Joueur _perso;
        //private Joueur _sensPerso;
        //private Joueur _vitessePerso;
        private Vector2 _positionPerso;
        private AnimatedSprite _perso;
        private int _sensPerso;
        private int _vitessePerso;

        //------------------------------------------------------------------> Changement de scene 

        private readonly ScreenManager _screenManager;
        // on définit les différents états possibles du jeu ( à compléter) 
        public enum Etats { Menu, Controls, Play, Quit };

        // on définit un champ pour stocker l'état en cours du jeu
        private Etats etat;

        // on définit  2 écrans ( à compléter )
        
        private SceneMapPrincipale _sceneMapPrincipale;
        private SceneGrotte _sceneGrotte;
        private ScreenMenu _screenMenu;
        private SpriteBatch _spriteBatch;

        
        public Joueur _player = new Joueur();
        

        private GameTime gameTime;
        public float deltaSeconds = 1;

        // Theo doit ranger 

        public Etats Etat
        {
            get
            {
                return this.etat;
            }

            set
            {
                this.etat = value;
            }
        }



        // Theo doit ranger 

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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.IsFullScreen = false;

            Window.Title = "test";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);

            // écrant de base Menu 
            //Etat = Etats.Menu;        pour le menu de base 
            Etat = Etats.Play; 

            //  chargement les 2 écrans 
            
            _sceneMapPrincipale = new SceneMapPrincipale(this);
            _screenMenu = new ScreenMenu(this);


        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = LARGEUR_FENETRE;                                             
            _graphics.PreferredBackBufferHeight = HAUTEUR_FENETRE;                                            
            _graphics.ApplyChanges();
            _positionPerso = new Vector2(300, 340);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //-- charmenet du menu de base 
           // _screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black));
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap = Content.Load<TiledMap>("MapGrotte");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);


            //_screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black));
            _player.playerInitialize(_positionPerso, 100, this);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Colision");
            _player.DeplacementsPerso(deltaSeconds, _tiledMap, mapLayer);

        }

        protected override void Update(GameTime gameTime)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
            float walkSpeed = deltaSeconds * _vitessePerso; // Vitesse de déplacement du sprite
            KeyboardState keyboardState = Keyboard.GetState();
            _player.DeplacementsPerso(deltaSeconds, _tiledMap, mapLayer);

            _player._perso.Update(deltaSeconds); // time écoulé



            //============ INTERACTIONS
            
            //debug map (collision vers le bas)
           /* int a = mapLayer.GetTile((ushort)(_positionPerso.X / _tiledMap.TileWidth), (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 1)).GlobalIdentifier;
            Console.WriteLine(a);

            //debug autres collisions (collision vers le bas)
            int b = mapLayer.GetTile((ushort)(_positionPerso.X / _tiledMap.TileWidth), (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 1)).GlobalIdentifier;
            Console.WriteLine(b);*/
            //============ 


                                                                                                                                        // Théo range



            

            
            //clic souris                                                                                                                           //THEO RANGEEEEEE
            //-*-*-**-*-*-*-*-**-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // On teste le clic de souris et l'état pour savoir quelle action faire 
            MouseState _mouseState = Mouse.GetState();
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                // Attention, l'état a été mis à jour directement par l'écran en question
                if (this.Etat == Etats.Quit)
                    Exit();

                else if (this.Etat == Etats.Play)
                    _screenManager.LoadScreen(_sceneMapPrincipale, new FadeTransition(GraphicsDevice, Color.Black));
                else if (this.Etat == Etats.Menu)
                    _screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black));


            }


            if (Keyboard.GetState().IsKeyDown(Keys.Back))
            {
                if (this.Etat == Etats.Menu)
                    _screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black));
            }

            _tiledMapRenderer.Update(gameTime);

            //Console.WriteLine(_player._positionPerso);
            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);

            _tiledMapRenderer.Draw();

            SpriteBatch.Begin();
            SpriteBatch.Draw(_player._perso, _player._positionPerso);
            SpriteBatch.End();



            base.Draw(gameTime);
        }


        private void LoadMenu()
        {
            _screenManager.LoadScreen(new ScreenMenu(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        
        private void LoadGrotte()
        {
            _screenManager.LoadScreen(new SceneGrotte(this), new FadeTransition(GraphicsDevice, Color.Black));
        }

        private void LoadMap()
        {
            _screenManager.LoadScreen(new SceneMapPrincipale(this), new FadeTransition(GraphicsDevice, Color.Black));
        }

    }
}