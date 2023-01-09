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
    public class SceneMapPrincipale : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public SceneMapPrincipale(Game1 game) : base(game) { }

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
        private int _sensPerso;
        private int _vitessePerso;

        //-----> Autres
        private KeyboardState _keyboardState;
        private SpriteBatch _spriteBatch;


        /*=-=-=-=-=-=-=-PUBLIC_CONSTANT-=-=-=-=-=-=-*/
        public const int LARGEUR_FENETRE = 496;
        public const int HAUTEUR_FENETRE = 496;



        public override void Initialize()
        {
            base.Initialize();

            
            Console.WriteLine("map");
            _positionPerso = new Vector2(300, 340);
            _vitessePerso = 100;


        }

        public override void LoadContent()
        {

            _tiledMap = Content.Load<TiledMap>("Map_Generale_SilverWorld");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("COLISIONS");

            // TODO: use this.Content to load your game content here
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("BryaAnimations.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);

           
            


            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            //debug map (collision vers le bas)
            int a = mapLayer.GetTile((ushort)(_positionPerso.X / _tiledMap.TileWidth), (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 1)).GlobalIdentifier;
            Console.WriteLine(a);


            //debug autres collisions (collision vers le bas)
            int b = mapLayer.GetTile((ushort)(_positionPerso.X / _tiledMap.TileWidth), (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 1)).GlobalIdentifier;
            Console.WriteLine(b);
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
            
            Game.SpriteBatch.Begin();

        }



    }


}

