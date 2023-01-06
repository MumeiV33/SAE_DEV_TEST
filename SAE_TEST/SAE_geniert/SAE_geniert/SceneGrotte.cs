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
    public class SceneGrotte : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public SceneGrotte(Game1 game) : base(game) { }

        private GraphicsDeviceManager _graphics;
        private TiledMap _tiledMap;
        private TiledMap _tiledMapTest;
        private TiledMap _tiledMapTestFin;
        private TiledMap _tiledMapRendererTestFin;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer mapLayer;
        private TiledMapTileLayer mapLayerTest;

        private Vector2 _positionPerso;
        private AnimatedSprite _perso;
        private int _sensPerso;
        private int _vitessePerso;



        private Vector2 _positionTortue;
        private AnimatedSprite _Tortue;
        private int _sensTortue;
        private int _vitesseTortue;

        public override void Initialize()
        {
            base.Initialize();

            Console.WriteLine("grotte");

        }

        public override void LoadContent()
        {
            base.LoadContent();

            _tiledMap = Content.Load<TiledMap>("MapGrotte");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

            // TODO: use this.Content to load your game content here
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("BryaAnimations.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);

            SpriteSheet spriteSheetTortue = Content.Load<SpriteSheet>("Torute.sf", new JsonContentLoader());
            _Tortue = new AnimatedSprite(spriteSheetTortue);

            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Calque de Tuiles 2");
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime)
        {

        }

    }
}
