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
        private TiledMapTileLayer mapLayer;
        private TiledMapTileLayer mapLayerTest;


        private int LARGEUR_FENETRE = 800;
        private int HAUTEUR_FENETRE = 500;

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
        //private ScreenMenu _screenMenu;
        private SceneMapPrincipale _sceneMapPrincipale;
        private SceneGrotte _sceneGrotte;
        private ScreenMenu _screenMenu;
        private SpriteBatch _spriteBatch;


        public Joueur _player = new Joueur();
        private GameTime gameTime;



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
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);

            // écrant de base Menu 
            Etat = Etats.Menu;

            //  chargement les 2 écrans 
            //_screenMenu = new ScreenMenu(this);
            //_sceneMapPrincipale = new SceneMapPrincipale(this);


        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = LARGEUR_FENETRE;                                            // Theo doit ranger  
            _graphics.PreferredBackBufferHeight = HAUTEUR_FENETRE;                                            // Theo doit ranger 
            _graphics.ApplyChanges();
            _player.DeplacementsPerso(gameTime);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap = Content.Load<TiledMap>("Map_Generale_SilverWorld");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            //Joueur.LoadContentPerso();
            //-- charmenet du menu de base 
            //_screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black));

        }

        protected override void Update(GameTime gameTime)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
            float walkSpeed = deltaSeconds * _vitessePerso; // Vitesse de déplacement du sprite
            KeyboardState keyboardState = Keyboard.GetState();
            _perso.Update(deltaSeconds); // time écoulé



            //============ INTERACTIONS

            //debug map (collision vers le bas)
            int a = mapLayer.GetTile((ushort)(_positionPerso.X / _tiledMap.TileWidth), (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 1)).GlobalIdentifier;
            Console.WriteLine(a);

            //debug autres collisions (collision vers le bas)
            int b = mapLayer.GetTile((ushort)(_positionPerso.X / _tiledMap.TileWidth), (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 1)).GlobalIdentifier;
            Console.WriteLine(b);
            //============ 


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))                // Théo range
                Exit();                                                                                                                             // Théo range



            _tiledMapRenderer.Update(gameTime);

            

            base.Update(gameTime);

            
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
            

            //if (Keyboard.GetState().IsKeyDown(Keys.Back))
            //{
            //   
            //}

        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);
            _tiledMapRenderer.Draw();

            SpriteBatch.Begin();

            SpriteBatch.Draw(_perso, _positionPerso);
            SpriteBatch.End();

            base.Draw(gameTime);
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