using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace FreadGame
{
    class Collision
    {
        #region ATTRIBUTS
        //ATTRIBUTS *******************************************
        static public Boolean isFalling;
        static public Boolean isCollideScreenRL;
        static public Boolean isCollideScreenUp;
        static public Boolean isCollideScreenDown;
        static public Boolean isCollideScreenJumpRL;
        static public Boolean isCollideScreenJumpUD;
        static public Boolean isCollideWall;
        static public Boolean isCollideLever;
        static public Boolean isCollideCable;
        static public Boolean isCollideItem;
        static public Boolean isCollideGoal;
        static Rectangle hitBox;

        #endregion

        #region CONSTRUCTOR


        public Collision()
        {

            hitBox = new Rectangle();
            isFalling = false;
            isCollideScreenRL = true;
            isCollideScreenUp = true;
            isCollideScreenDown = true;
            isCollideLever = false;
            isCollideWall = false;

        }

        #endregion

        #region METHODES

        //METHODES  *******************************************


        static public Boolean WallCollision(int sens, int speed)
        {
            Boolean isOnLever = false;
            Boolean isOnCable = false;
            Boolean isOnItem = false;
            Boolean isOnGoal = false;

            switch (sens)
            {
                case 1: // Left
                    hitBox = new Rectangle(Player.texturePlayer.X - speed, Player.texturePlayer.Y, 25, 54);
                    break;

                case 2: //Right
                    hitBox = new Rectangle(Player.texturePlayer.X + speed, Player.texturePlayer.Y, 25, 54);
                    break;
                case 3: // Up
                    hitBox = new Rectangle(Player.texturePlayer.X, Player.texturePlayer.Y - speed, 25, 54);
                    break;

                case 4: // Down
                    hitBox = new Rectangle(Player.texturePlayer.X, Player.texturePlayer.Y + (Player.texturePlayer.Height - 1) + speed, 25, 54);
                    break;

                case 5: // On place Lever
                    isOnLever = true;
                    isOnItem = true;
                    isOnGoal = true;
                    hitBox = new Rectangle(Player.texturePlayer.X + (Player.texturePlayer.Width / 2), Player.texturePlayer.Y + (Player.texturePlayer.Height - 15), 25, 54);
                    break;

                case 6: // On place Cable
                    isOnCable = true;
                    hitBox = new Rectangle(Player.texturePlayer.X, Player.texturePlayer.Y, 10, 10);
                    break;

                default:
                    hitBox = new Rectangle(Player.texturePlayer.X + (Player.texturePlayer.Width / 2), Player.texturePlayer.Y + (Player.texturePlayer.Height - 15), 25, 54);
                    break;

            }


            foreach (Rectangle rectangle in ScreenManager.Screen2.listSquid)
            {
                isCollideCable = false;
                isCollideLever = false;
                isCollideItem = false;
                isCollideWall = false;
                isCollideGoal = false;

                Block shortCut = ScreenManager.Screen2.mapGrid[ScreenManager.Screen2.currentFace - 1, ((rectangle.X - 200) / 30), rectangle.Y / 30];

                if (hitBox.Intersects(rectangle) && shortCut.name == "levier" && isOnLever == true)
                {
                    isCollideLever = true;
                    break;
                }

                else if (hitBox.Intersects(rectangle) && shortCut.name == "cable_blanc" && isOnItem == true)
                {
                    isCollideItem = true;
                    break;
                }

                else if (hitBox.Intersects(rectangle) && shortCut.name == "fils" && isOnCable == true)
                {
                    isCollideCable = true;
                    break;
                }
                else if (hitBox.Intersects(rectangle) && shortCut.name == "entree" && isOnGoal == true)
                {
                    isCollideGoal = true;
                    break;
                }
                else
                {
                    if (hitBox.Intersects(rectangle) && shortCut.type == 1)
                    {
                        isCollideWall = true;
                        break;
                    }
                }
            }
            isOnLever = false;
            isOnCable = false;
            isOnItem = false;
            isOnGoal = false;

            if (isCollideLever == true)
            {
                return isCollideLever;
            }
            else if (isCollideCable == true)
            {
                return isCollideCable;
            }
            else if (isCollideItem == true)
            {
                return isCollideItem;
            }
            else if (isCollideGoal == true)
            {
                return isCollideGoal;
            }
            else
            {
                return isCollideWall;
            }

        }




        static public void UpdateCollision()
        {


            hitBox = new Rectangle(Player.texturePlayer.X, Player.texturePlayer.Y + 2 * Player.speed, 25, 54);

            foreach (Rectangle rectangle in ScreenManager.Screen2.listSquid)
            {
                Block shortCut = ScreenManager.Screen2.mapGrid[ScreenManager.Screen2.currentFace - 1, ((rectangle.X - 200) / 30), rectangle.Y / 30];

                if (hitBox.Intersects(rectangle) && shortCut.name != "fils" && shortCut.name != "levier" && shortCut.name != "entree" && shortCut.name != "cable_blanc")
                {
                    isFalling = false;
                    break;
                }
                else
                {
                    isFalling = true;

                }
            }


            if ((Player.texturePlayer.X >= 195 && Player.texturePlayer.X <= 785)) //|| (Player.texturePlayer.X >= -30 && Player.texturePlayer.X <= 770))
            {
                isCollideScreenRL = false;
            }
            else
            {
                isCollideScreenRL = true;
            }
            if ((Player.texturePlayer.Y >= 10)) //543 pour pas tomber direct|| (Player.texturePlayer.X >= -30 && Player.texturePlayer.X <= 543))
            {
                isCollideScreenUp = false;
            }
            else
            {
                isCollideScreenUp = true;
            }
            if ((Player.texturePlayer.Y <= 545))
            {
                isCollideScreenDown = false;
            }
            else
            {
                isCollideScreenDown = true;
            }

            if ((Player.texturePlayer.Y >= 50) && (Player.texturePlayer.Y <= 500)) //543 pour pas tomber direct|| (Player.texturePlayer.X >= -30 && Player.texturePlayer.X <= 543))
            {
                isCollideScreenJumpUD = false;
            }
            else
            {
                isCollideScreenJumpUD = true;
            }
            if ((Player.texturePlayer.X >= 250 && Player.texturePlayer.X <= 750)) //|| (Player.texturePlayer.X >= -30 && Player.texturePlayer.X <= 770))
            {
                isCollideScreenJumpRL = false;
            }
            else
            {
                isCollideScreenJumpRL = true;
            }
        }

        #endregion

        
    }
}
