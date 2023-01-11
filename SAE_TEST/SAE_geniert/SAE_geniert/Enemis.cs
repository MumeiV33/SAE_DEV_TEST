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

        public void TortueInitialize(Vector2 positionTortue, int vitesseTortue, Game1 _game)
        {
            _vitesseTortue = vitesseTortue;
            _positionTortue = positionTortue;
            SpriteSheet spriteSheetTortue = _game.Content.Load<SpriteSheet>("Torute.sf", new JsonContentLoader());
            _Tortue = new AnimatedSprite(spriteSheetTortue);
        }
        public void DeplacementsTortue(float deltaSeconds, TiledMap _tiledMap, TiledMapTileLayer _mapLayer)
        {
            float walkSpeedTortue = deltaSeconds * _vitesseTortue; // Vitesse de déplacement du sprite
            _Tortue.Update(deltaSeconds); // time écoulé


            //-->> Gauche
            if (_sensTortue == 1)
            {
                ushort txTortue = (ushort)(_positionTortue.X / _tiledMap.TileWidth - 0.5);
                ushort tyTortue = (ushort)(_positionTortue.Y / _tiledMap.TileHeight);

                if (!IsCollisionEnnemies(txTortue, tyTortue, _mapLayer))
                {
                    _positionTortue.X -= walkSpeedTortue;
                    _Tortue.Play("walkWest");
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
                if (!IsCollisionEnnemies(txTortue, tyTortue, _mapLayer))
                {
                    _positionTortue.X += walkSpeedTortue;
                    _Tortue.Play("walkEast");
                }
                else
                {
                    _sensTortue = 1;
                }
            }
        }
        private bool IsCollisionEnnemies(ushort x, ushort y, TiledMapTileLayer mapLayer)
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
