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
    public class Enemis
    {
        public Enemis() { }
        //-----> Tortue
        public Vector2 _positionTortue;
        public AnimatedSprite _Tortue;
        public int _sensTortue;
        public int _vitesseTortue;
        public string animation = "";

        //-----> chainsaw
        public Vector2 _positionChainsaw;
        public AnimatedSprite _Chainsaw;
        public int _sensChainsaw;
        public int _vitesseChainsaw;

        public void TortueInitialize(Vector2 positionTortue, int vitesseTortue, Game1 _game)
        {
            _vitesseTortue = vitesseTortue;
            _positionTortue = positionTortue;
            SpriteSheet spriteSheetTortue = _game.Content.Load<SpriteSheet>("Torute.sf", new JsonContentLoader());
            _Tortue = new AnimatedSprite(spriteSheetTortue);
            
        }


        public void ChainsawInitialze(Vector2 positionChainsaw, int vitesseChainsaw, Game1 _game)
        {
            _vitesseChainsaw = vitesseChainsaw;
            _positionChainsaw = positionChainsaw;
            SpriteSheet spriteSheetChainsaw = _game.Content.Load<SpriteSheet>("chainsaw.sf", new JsonContentLoader());
            _Chainsaw = new AnimatedSprite(spriteSheetChainsaw);
        }


        public void DeplacementsTortue(float deltaSeconds, TiledMap _tiledMap, TiledMapTileLayer _mapLayer, GameScreen _game)
        {
            float walkSpeedTortue = deltaSeconds * _vitesseTortue; // Vitesse de déplacement du sprite
            


            //-->> Gauche
            if (_sensTortue == 1)
            {
                ushort txTortue = (ushort)(_positionTortue.X / _tiledMap.TileWidth - 0.5);
                ushort tyTortue = (ushort)(_positionTortue.Y / _tiledMap.TileHeight);
                animation = "walkWest";
                
                if (!IsCollisionEnnemies(txTortue, tyTortue, _mapLayer))
                {
                    _positionTortue.X -= walkSpeedTortue;
                    //_Tortue.Play(animation);
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
                animation = "walkEast";
                
                if (!IsCollisionEnnemies(txTortue, tyTortue, _mapLayer))
                {
                    _positionTortue.X += walkSpeedTortue;
                    //_Tortue.Play(animation);
                }
                else
                {
                    _sensTortue = 1;
                }
            }
        }
        public void DeplacementsChainsaw(float deltaSeconds, TiledMap _tiledMap, TiledMapTileLayer _mapLayer, GameScreen _game)
        {
            float walkSpeedChainsaw = deltaSeconds * _vitesseChainsaw; // Vitesse de déplacement du sprite



            ////-->> Gauche
            //if (_sensChainsaw == 1)
            //{
            //    ushort txChainsaw = (ushort)(_positionTortue.X / _tiledMap.TileWidth - 0.5);
            //    ushort tyChainsaw = (ushort)(_positionTortue.Y / _tiledMap.TileHeight);
            //    animation = "walkNorth";

            //    if (!IsCollisionEnnemies(txChainsaw, tyChainsaw, _mapLayer))
            //    {
            //        _positionChainsaw.Y -= walkSpeedChainsaw;
            //        _Chainsaw.Play(animation);
            //    }
            //    else
            //    {
            //        _sensChainsaw = 0;
            //    }

            //}
            ////-->> Droite
            //if (_sensChainsaw == 0)
            //{
            //    ushort txChainsaw = (ushort)(_positionTortue.X / _tiledMap.TileWidth + 0.5);
            //    ushort tyChainsaw = (ushort)(_positionTortue.Y / _tiledMap.TileHeight);
            //    animation = "walkSouth";

            //    if (!IsCollisionEnnemies(txChainsaw, tyChainsaw, _mapLayer))
            //    {
            //        _positionChainsaw.Y += walkSpeedChainsaw;
            //        _Chainsaw.Play(animation);
            //    }
            //    else
            //    {
            //        _sensChainsaw = 1;
            //    }
            //}
        }
        private bool IsCollisionEnnemies(ushort x, ushort y, TiledMapTileLayer _mapLayer)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (_mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }
    }
}
