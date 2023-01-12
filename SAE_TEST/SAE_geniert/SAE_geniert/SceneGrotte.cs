using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using System;
using System.Windows;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Audio;

namespace SAE_geniert
{
    public class SceneGrotte : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public SceneGrotte(Game1 game) : base(game) { }

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
        //--> RECTANGLE DE SES MORTS
        public Vector2 persoPos;

        //-----> Tortue


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

        //ˏˋ°•*⁀➷ GRAVITY
        float velocity;
        float gravity = 4f;

        public float incrementFin = 0.8f;
        public float incrementDebut = 2f;
        public bool isJumping;

        public Vector2 hitBoxPlayer;
        public float persoJumpPosition;

        //Collisions entre sprites
        public Vector2 persoRectVelo;
        public Vector2 tortueRectVelo;
        public Vector2 chainsawRectVelo;

        //-----> Autres
        private KeyboardState _keyboardState;
        float deltaSeconds = 1;
        float niveauGravite = 4;
        public float niveauSaut = 2f;
        public Enemis _Enemis = new Enemis();

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

            _positionChainsaw = _Enemis._positionChainsaw = new Vector2(199, 481);
            _vitesseChainsaw = _Enemis._vitesseChainsaw = 100;
            _sensChainsaw = _Enemis._sensChainsaw = 1;

            Game.Window.Title = "Silver World";
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            Game._player._positionPerso = new Vector2(30, 60);

            persoRectVelo = Game._player._positionPerso;

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

            //Rectangle persoRect = new Rectangle((int)_positionPerso.X);

            float deltaSecondsTortue = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime                   Tortue
            float walkSpeedTortue = deltaSecondsTortue * _vitesseTortue; // Vitesse de déplacement du sprite        tortue      


            float deltaSecondsChainsaw = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime                 Chainsaw
            float walkSpeedChainsaw = deltaSecondsChainsaw * _vitesseChainsaw; // Vitesse de déplacement du sprite    Chainsaw


            

            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
            float walkSpeed = deltaSeconds * _player._vitessePerso; // Vitesse de déplacement du sprite

            KeyboardState keyboardState = Keyboard.GetState();

            float walkspeedRect = deltaSeconds * _vitessePerso;
            Rectangle persoRect = new Rectangle((int)Game._player._positionPerso.X, (int)Game._player._positionPerso.Y, 16, 32);
            persoRectVelo += Game._player._positionPerso;
            walkspeedRect = deltaSeconds * _vitesseTortue;
            Rectangle tortueRect = new Rectangle((int)_Enemis._positionTortue.X, (int)_Enemis._positionTortue.Y, 16, 16);
            tortueRectVelo += _Enemis._positionTortue;
            Rectangle chainsawRect = new Rectangle((int)_Enemis._positionChainsaw.X, (int)_Enemis._positionChainsaw.Y, 32, 16);
            chainsawRectVelo += _Enemis._positionChainsaw;

            

            _Enemis.DeplacementsTortue(deltaSecondsTortue, _tiledMap, _mapLayer, this);
            _Enemis.DeplacementsChainsaw(deltaSecondsChainsaw, _tiledMap, _mapLayer, this);

            IsIntersect(persoRect, tortueRect, chainsawRect);
            //=================GRAVITY=================\\

            if (keyboardState.IsKeyDown(Keys.Space) || !keyboardState.IsKeyDown(Keys.Space))
            {
                ushort txGravity = (ushort)(Game._player._positionPerso.X / _tiledMap.TileWidth);
                ushort tyGravity = (ushort)(Game._player._positionPerso.Y / _tiledMap.TileHeight + 1);

                if (!IsCollision(txGravity, tyGravity, _mapLayer))
                {
                    ushort txEatCol = (ushort)(Game._player._positionPerso.X / _tiledMap.TileWidth);
                    ushort tyEatCol = (ushort)(Game._player._positionPerso.Y / _tiledMap.TileHeight);
                    
                    ushort txColHaut = (ushort)(Game._player._positionPerso.X / _tiledMap.TileWidth);
                    ushort tyColHaut = (ushort)(Game._player._positionPerso.Y / _tiledMap.TileHeight - 1);

                    if (IsCollision(txColHaut, tyColHaut, _mapLayer))
                    {
                        isJumping = false;
                        Game._player._positionPerso.Y += (float)gravity * 2 * gravity;
                    }
                    if (IsCollision(txEatCol, tyEatCol, _mapLayer))
                    {
                        isJumping = false;
                        Game._player._positionPerso.Y += (float)gravity;
                    }
                    else
                    {
                        isJumping = false;
                        Game._player._positionPerso.Y += (float)gravity;
                        _perso.Play("idle");
                        UpdateGravity(gameTime, isJumping);
                    }
                }
                if (keyboardState.IsKeyDown(Keys.Space) && IsCollision(txGravity, tyGravity, _mapLayer))
                {
                    isJumping = true;
                    for (float incrementI = incrementFin; incrementI < 10; incrementI++)
                        Game._player._positionPerso.Y -= (float)niveauSaut * incrementDebut;
                    //UpdateGravity(gameTime, isJumping);
                }

            }
            _perso.Update(deltaSeconds);

        }
        public void UpdateGravity(GameTime gameTime, bool isJumping)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
            velocity += gravity * deltaSeconds;
            _player._positionPerso.Y += velocity * deltaSeconds;
            // Check if player is currently jumping
            if (isJumping == true)
            {
                if (persoJumpPosition < 10)
                {
                    Game._player._positionPerso.Y += gravity;
                    persoJumpPosition += gravity;
                }
                else if (persoJumpPosition >= 10)
                {
                    isJumping = false;
                    Game._player._positionPerso.Y -= gravity;
                    persoJumpPosition -= gravity;
                }
            }
            else if (isJumping == false)
            {
                if (persoJumpPosition > 0)
                {
                    persoJumpPosition -= (int)gravity;
                    Game._player._positionPerso.Y -= (float)gravity;
                }
            }
        }
        //-=-=-=-=-=-=-=-FIN GROS BORDEL GRAVITY-=-=-=-=-=-=-//



            _perso.Update(deltaSeconds); // time écoulé
            
            
        }
        private bool IsCollision(ushort x, ushort y, TiledMapTileLayer __mapLayer)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (__mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
            {
                //active la gravite
                return true;
            }
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
