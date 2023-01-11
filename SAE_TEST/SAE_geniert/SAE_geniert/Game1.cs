using Microsoft.Xna.Framework;
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
        public TiledMapTileLayer mapLayer;
        private TiledMapTileLayer mapLayerTest;
        private Texture2D _textBoutons;

        private int LARGEUR_FENETRE = 512;
        private int HAUTEUR_FENETRE = 512;

        //-----> Perso
        private Vector2 _positionPerso;

        private AnimatedSprite _perso;
        private int _sensPerso;
        private int _vitessePerso;

        //------------------------------------------------------------------> Changement de scene 

        private readonly ScreenManager _screenManager;
        // on définit les différents états possibles du jeu ( à compléter) 
        public enum Etats { Menu, Controls, Play, Quit  };

        // on définit un champ pour stocker l'état en cours du jeu
        private Etats etat;


        private ScreenMenu _screenMenu;
        private SceneMapPrincipale _sceneMapPrincipale;
        private SceneGrotte _sceneGrotte; 
        private SpriteBatch _spriteBatch;
        private SceneExplication _sceneFin; 

        
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
            Etat = Etats.Menu;        //pour le menu de base 
            //Etat = Etats.Play;

            //  chargement les 2 écrans 
            //LoadMap(); 
            //_sceneMapPrincipale = new SceneMapPrincipale(this);
            //_sceneGrotte = new SceneGrotte(this);
            _screenMenu = new ScreenMenu(this);
            


        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = LARGEUR_FENETRE;                                             
            _graphics.PreferredBackBufferHeight = HAUTEUR_FENETRE;                                            
            _graphics.ApplyChanges();
            _positionPerso = new Vector2(265, 210);
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            
           
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            LoadMenu(); 
             _tiledMap = Content.Load<TiledMap>("Map_Generale_SilverWorld");
             _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
             mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("COLISIONS");
            _textBoutons = Content.Load<Texture2D>("Menu_Avec_Nom_STart");


            _player.playerInitialize(_positionPerso, 100, this);
            
            _player.DeplacementsPerso(deltaSeconds, _tiledMap, mapLayer, this);



        }

        protected override void Update(GameTime gameTime)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
            float walkSpeed = deltaSeconds * _vitessePerso; // Vitesse de déplacement du sprite
            KeyboardState keyboardState = Keyboard.GetState();
            _player.DeplacementsPerso(deltaSeconds, _tiledMap, mapLayer, this);

            _player._perso.Update(deltaSeconds); // time écoulé


            if (keyboardState.IsKeyDown(Keys.NumPad2))
                LoadGrotte();

            if (keyboardState.IsKeyDown(Keys.NumPad1)) //|| keyboardState.IsKeyDown(Keys.Space))
                LoadMap();

            if (keyboardState.IsKeyDown(Keys.NumPad3))
                LoadExplication();

            if (keyboardState.IsKeyDown(Keys.NumPad4))
                LoadGrotte2();

            if (keyboardState.IsKeyDown(Keys.NumPad5))
                LoadMap2();

            if (keyboardState.IsKeyDown(Keys.T))
                LoadMenu();

            if (keyboardState.IsKeyDown(Keys.P))
                LoadExplication();

            if (etat == Etats.Menu && keyboardState.IsKeyDown(Keys.Enter))
                LoadExplication();

            if (keyboardState.IsKeyDown(Keys.Back))
                LoadMenu(); 

            








            // Théo range


            /*if (etat == Etats.Menu)
             {
                 _screenManager.LoadScreen(new SceneGrotte(this), new FadeTransition(GraphicsDevice, Color.Black));
             }*/



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
                    LoadMap();
                     //_screenManager.LoadScreen(_sceneMapPrincipale, new FadeTransition(GraphicsDevice, Color.Black));
                 
                 /*else if (this.Etat == Etats.Menu)
                    LoadMenu();
                     //_screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black));*/


             }


             if (Keyboard.GetState().IsKeyDown(Keys.Back))
             {
                if (this.Etat == Etats.Menu)
                    LoadMenu(); 
                     //_screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black));
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


        public void LoadMenu()
        {
            _screenManager.LoadScreen(new ScreenMenu(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        

        public void LoadGrotte()
        {
            _screenManager.LoadScreen(new SceneGrotte(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadGrotte2()
        {
            _screenManager.LoadScreen(new SceneGrotte2(this), new FadeTransition(GraphicsDevice, Color.Black));
        }

        public void LoadMap()
        {
            _screenManager.LoadScreen(new SceneMapPrincipale(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadExplication()
        {
            _screenManager.LoadScreen(new SceneExplication(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadMap2()
        {
            _screenManager.LoadScreen(new SceneMapPrincipale2(this), new FadeTransition(GraphicsDevice, Color.Black));
        }

        




        // JEU SUR LE GITHUB // 

    }
}