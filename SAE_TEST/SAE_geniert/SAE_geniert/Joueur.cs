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
    public class Joueur
    {
        public Joueur()
        {

        }

        public Vector2 _positionPerso;
        public int _vitessePerso;
        public AnimatedSprite _perso;
        public int _sensPerso;

        

        //-----> Autres
        private KeyboardState _keyboardState;

        public void playerInitialize(Vector2 positionPerso, int vitessePerso, Game1 _game)
        {
            _vitessePerso = vitessePerso;
            _positionPerso = positionPerso;
            SpriteSheet spriteSheet = _game.Content.Load<SpriteSheet>("BryaAnimations.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);
        }
        public void DeplacementsPerso(float deltaSeconds, TiledMap _tiledMap, TiledMapTileLayer _mapLayer, Game1 game)
        {
            
            float walkSpeed = deltaSeconds * _vitessePerso; // Vitesse de déplacement du sprite

            // TODO: Add your update logic here

            /*╔════════════════════════════════《✧》════════════════════════════════╗*/
            //-----------------Déplacements-------------------------------------------------------------------
            KeyboardState keyboardState = Keyboard.GetState();
            _keyboardState = Keyboard.GetState();
            //-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^

            //-=-=-=-=-=-=-=-=-=-DROITE-=-=-=-=-=-=-=-=-=-\\

            //-----------------Déplacements-------------------------------------------------------------------
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 0.8);
                //-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^



                if (!IsCollision(tx, ty, _mapLayer))
                    _positionPerso.X += walkSpeed;
                _perso.Play("walkEast");


            }
            //-=-=-=-=-=-=-=-=-=-GAUCHE-=-=-=-=-=-=-=-=-=-\\

            //-----------------Déplacements-------------------------------------------------------------------
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 0.5);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 0.5);
                //-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^

                if (!IsCollision(tx, ty, _mapLayer))
                    _positionPerso.X -= walkSpeed;
                _perso.Play("walkWest");
            }
            //-=-=-=-=-=-=-=-=-=-HAUT-=-=-=-=-=-=-=-=-=-\\

            //-----------------Déplacements-------------------------------------------------------------------
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
                //-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-^-
                if (!IsCollision(tx, ty, _mapLayer))
                    _positionPerso.Y -= walkSpeed;

            }
            //-=-=-=-=-=-=-=-=-=-BAS-=-=-=-=-=-=-=-=-=-\\
            
            /*━━━━━━━━━━━━━━━━━━━━━━━━━━°⌜Déplacements⌟°━━━━━━━━━━━━━━━━━━━━━━━━━━┓*/
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
                /*━━━━━━━━━━━━━━━━━━━━━━°⌜Fin Déplacements⌟°━━━━━━━━━━━━━━━━━━━━━━━┛*/

                /*╭┈┈┈┈┈┈┈┈┈┈┈┈┈Collision┈┈┈┈┈┈┈┈┈┈┈┈╮*/
                if (!IsCollision(tx, ty, _mapLayer))
                    _positionPerso.Y += walkSpeed;
                /*╰┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈╯*/
            }
            /*════════════════════════════════《✧》════════════════════════════════╝*/

                //============ INTERACTIONS

                //debug map (collision vers le bas)
                int a = _mapLayer.GetTile((ushort)(_positionPerso.X / _tiledMap.TileWidth -1), (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 1)).GlobalIdentifier;
            Console.WriteLine(a);

            //debug autres collisions (collision vers le bas)
            int b = _mapLayer.GetTile((ushort)(_positionPerso.X / _tiledMap.TileWidth -1), (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 1)).GlobalIdentifier;
            Console.WriteLine(b);

            if (a == 1405 && _keyboardState.IsKeyDown(Keys.Up)){
                //Console.WriteLine("ON A REUSSI SUUUUUUUUUUUUU");
                game.LoadGrotte(); 
            }
            //============


            if (a == 402 && _keyboardState.IsKeyDown(Keys.E))
            {
                Console.WriteLine("Map2"); ////////////////////////////////////////////////////////////////////////////////////////
                game.LoadMap2();
            }

            if(a == 196 && _keyboardState.IsKeyDown(Keys.E))
            {
                Console.WriteLine("Grotte2");
                game.LoadGrotte2(); 
            }
            if (a == 8277 || a == 6928)
            {
                Console.WriteLine("LA FINNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN");

                if(_keyboardState.IsKeyDown(Keys.E))
                {
                    game.LoadFin();
                    if (_keyboardState.IsKeyDown(Keys.Enter))
                    {
                        Console.WriteLine("oui"); 
                    }
                }
                

            }
                

            if (a == 5658)
                Console.WriteLine("LE CHATTTTTTTTTTTTTTTTTTTTTTTTTT MIAOUUUUUUU");

            if ( _keyboardState.IsKeyDown(Keys.R))
            {
                _positionPerso = new Vector2(30, 60);
            }

            

        }
        private bool IsCollision(ushort x, ushort y, TiledMapTileLayer mapLayer)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }
        

        }
    }

