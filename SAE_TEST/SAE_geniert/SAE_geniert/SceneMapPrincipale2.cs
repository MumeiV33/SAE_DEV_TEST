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
    public class SceneMapPrincipale2 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;

        public SceneMapPrincipale2(Game1 game) : base(game) { }

        // private new Game1 Game; 

        //------> Map
        public TiledMap _tiledMap;
        public TiledMapRenderer _tiledMapRenderer;
        public TiledMapTileLayer mapLayer;

        //-----> Perso
        private Vector2 _positionPerso;
        private AnimatedSprite _perso;


        //-----> Autres


        /*=-=-=-=-=-=-=-PUBLIC_CONSTANT-=-=-=-=-=-=-*/
        public const int LARGEUR_FENETRE = 496;
        public const int HAUTEUR_FENETRE = 496;


        public Joueur _player = new Joueur();
        private SpriteBatch _spriteBatch;
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



            Console.WriteLine("map");
            _positionPerso = new Vector2(300, 340);
            _player._positionPerso = _positionPerso;
            Game._player._positionPerso = new Vector2(458, 463);

            base.Initialize();
        }

        public override void LoadContent()
        {

            _positionPerso.X = 300;
            _positionPerso.Y = 340;

            _tiledMap = Content.Load<TiledMap>("Map_Generale_SilverWorld2");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("COLISIONS");

            // TODO: use this.Content to load your game content here
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("BryaAnimations.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);


            Game.mapLayer = mapLayer;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            /* //debug map (collision vers le bas)
             int a = mapLayer.GetTile((ushort)(_positionPerso.X / _tiledMap.TileWidth), (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 1)).GlobalIdentifier;
             Console.WriteLine(a);


             //debug autres collisions (collision vers le bas)
             int b = mapLayer.GetTile((ushort)(_positionPerso.X / _tiledMap.TileWidth), (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 1)).GlobalIdentifier;
             Console.WriteLine(b);

             if (a == 1064 && Keyboard.GetState().IsKeyDown(Keys.Up))
             {
                 Console.WriteLine("ON A REUSSI SUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU");
                 /*Game1.LoadGrotte();
                 GameScreen.LoadGrotte();
             }*/


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

            Game.SpriteBatch.Begin();
            Game.SpriteBatch.Draw(Game._player._perso, Game._player._positionPerso);
            Game.SpriteBatch.End();

            /*//debug autres collisions (collision vers le bas)
            int b = mapLayer.GetTile((ushort)(_positionPerso.X / _tiledMap.TileWidth), (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 1)).GlobalIdentifier;
            Console.WriteLine(b);
            
            if (a == 1064)
            {
                Console.WriteLine("oui");

            }
            //============ 
            return res; */
        }



    }
}
