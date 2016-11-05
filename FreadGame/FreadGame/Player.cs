using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace FreadGame
{

    public enum Direction
    {
        Up, Down, Left, Right, JumpLeft, JumpRight, JumpFace, RunRight, RunLeft
    };

    class Player
    {
        //Attributs

        #region ATTRIBUTS

        static public Rectangle texturePlayer;

        Direction Direction;

        public static int speed;
        int currentSpeed;
        int frameLine;
        int frameColumn;
        int timer;
        int animationSpeed;
        int timeJump;

        public static char numb;

        SpriteEffects Reverse;

        Boolean Anime;
        Boolean pushSpace;
        Boolean jumpBool;
        public static Boolean itemInThePocket;

        Vector2 jumpSpeed;

        double timerRunLeft;
        double timerRunRight;

        #endregion

        #region CONSTRUCTOR
        //Constructeur
        public Player()
        {
            Block.BlockLoad();
            numb = '\x0000';
            itemInThePocket = false;
            speed = 3;
            jumpSpeed = new Vector2(4, 3);//7,3
            animationSpeed = 6;
            Direction = Direction.Down;
            frameColumn = 2;
            frameLine = 3;
            Reverse = SpriteEffects.None;
            Anime = true;
            pushSpace = false;
            timer = 0;
            timeJump = 0;
            jumpBool = false;
            timerRunLeft = 0;
            timerRunRight = 0;
            currentSpeed = speed;
        }
        #endregion

        #region METHODES
        //METHODES ******************************************************************************





        public void ChangeRectangle(int _width, int _height)//Pour corriger le problème des sprites de taille différentes
        {
            texturePlayer = new Rectangle(texturePlayer.X, texturePlayer.Y, _width, _height);
        }


        public void AnimateDown()
        {
            if (texturePlayer.Y + speed <= 545)
            {
                texturePlayer.Y += (speed);
            }

        }
        public void AnimateAction()//Si le joueur appuie sur E
        {

            Rectangle hitBox;
            Collision.WallCollision(5, 0);
            hitBox = new Rectangle(Player.texturePlayer.X + (Player.texturePlayer.Width / 2), Player.texturePlayer.Y + (Player.texturePlayer.Height - 15), Player.texturePlayer.Width, Player.texturePlayer.Height);

            if (Collision.isCollideLever)//Si le joueur est sur un levier
            {

                foreach (Rectangle rectangle in ScreenManager.Screen2.listSquid) // Pour chaque rectangle de l'ecran
                {
                    if (hitBox.Intersects(rectangle))//Si il l'intersecte
                    {
                        numb = ScreenManager.Screen2.mapGridLink[(ScreenManager.Screen2.currentFace) - 1, (rectangle.X - 200) / 30, (rectangle.Y) / 30];//On enregistre le numero qui est dans le mapgridlink
                        break;
                    }
                    else
                    {
                        numb = '\x0000';
                    }
                }

                for (int f = 0; f < 6; f++)
                {
                    for (int l = 0; l < 20; l++)
                    {
                        for (int c = 0; c < 20; c++)
                        {
                            if (ScreenManager.Screen2.mapGridLink[f, c, l] == numb && ScreenManager.Screen2.mapGrid[(ScreenManager.Screen2.currentFace) - 1, c, l].id != '\x0004')//Si une ou plusieurs portes a le même numero que le levier
                            {
                                ScreenManager.Screen2.mapGrid[f, c, l] = new Block('\x0000', "neant", FreadGame.Ressources.tuyau, 0);//On l'efface de la carte
                            }
                        }
                    }
                }
            }
            else if (Collision.isCollideItem)//Si le joueur est sur l'objet a recuperer
            {
                itemInThePocket = true; //On met le boolean qui le represente a true
                for (int l = 0; l < 20; l++)
                {
                    for (int c = 0; c < 20; c++)
                    {
                        if (ScreenManager.Screen2.mapGrid[(ScreenManager.Screen2.currentFace) - 1, c, l].id == '\x0008')//Si c'est l'id de l'objet
                        {
                            ScreenManager.Screen2.mapGrid[(ScreenManager.Screen2.currentFace) - 1, c, l] = new Block('\x0000', "neant", FreadGame.Ressources.tuyau, 0);//On l'efface de la carte
                        }
                    }
                }

            }
            else if (Collision.isCollideGoal && itemInThePocket)//Si le joueur est sur l'entree avec l'objet
            {
                //GG WIN!
                ScreenManager.SCREEN_MANAGER.goto_screen("screen3"); //On passe a l'ecran de victoire
                GameMain.IsGameStart = false;
            }
        }
        
        public void AnimateFall()
        {

            ChangeRectangle(37, 55);
         
                frameLine = 4;
                frameColumn = 3;
                if (Direction == Direction.JumpLeft)
                {
                    Reverse = SpriteEffects.FlipHorizontally;
                }
            
            
        }
        
      

        public void AnimateRightLeft()
        {
            timer++;

            if (timer == animationSpeed)
            {
                timer = 0;
                if (Anime)
                {
                    frameColumn++;
                    if (frameColumn > 3)
                    {
                        frameColumn = 2;
                        Anime = false;
                    }
                }
                else
                {
                    frameColumn--;
                    if (frameColumn < 1)
                    {
                        frameColumn = 2;
                        Anime = true;
                    }
                }
            }

        }
        public void AnimateUp()
        {
            timer++;

            if (timer == animationSpeed)
            {
                timer = 0;
                if (Anime)
                {
                    timer = 0;
                    frameColumn++;
                    if (frameColumn > 3)
                    {
                        frameColumn = 2;
                        Anime = false;
                    }
                }
                else
                {
                    frameColumn--;

                    if (frameColumn < 1)
                    {
                        frameColumn = 2;
                        Anime = true;
                    }
                }
            }
            ChangeRectangle(25, 55);
        }


        public void JumpWay()
        {
            if (timeJump < 13)
            {
                
                frameColumn = 2;
                frameLine = 4;

                Collision.WallCollision(3, (int)jumpSpeed.Y);
                if (!Collision.isCollideWall)
                {
                    if (!Collision.isCollideScreenUp && !Collision.isCollideScreenDown)
                    {
                        texturePlayer.Y -= (int)jumpSpeed.Y;
                    }
                }
                if (Direction == Direction.JumpLeft)
                {
                    Collision.WallCollision(1, (int)jumpSpeed.X);
                    if (!Collision.isCollideWall)
                    {
                        if (!Collision.isCollideScreenJumpRL)
                        {
                            texturePlayer.X -= (int)jumpSpeed.X;
                        }
                    }
                }
                else if (Direction == Direction.JumpRight)
                {
                    Collision.WallCollision(2, (int)jumpSpeed.X);
                    if (!Collision.isCollideWall)
                    {
                        if (!Collision.isCollideScreenJumpRL)
                        {
                            texturePlayer.X += (int)jumpSpeed.X;
                        }
                    }
                }
                
                timeJump++;
            }
            else
            {
                timeJump = 0;
                jumpBool = false;
                pushSpace = false;
                jumpSpeed.X = 4;
 
            }
        }

        #endregion

        #region UPDATE & DRAW

        //UPDATE AND DRAW **********************************************************************


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressources.playermap, texturePlayer,

             new Rectangle(((frameColumn - 1) * texturePlayer.Width), ((frameLine - 1) * texturePlayer.Height), texturePlayer.Width, texturePlayer.Height),//

             Color.White, 0f, new Vector2(0, 0), Reverse, 0f);
        }
        public void Update(KeyboardState keyboard)
        {
            Collision.UpdateCollision();

            if (jumpBool)
            {
                JumpWay();//On active la methode pour les sauts
            }
            else
            {
                if (Collision.isFalling && !Collision.isCollideCable)
                {
                    currentSpeed = speed;
                    timerRunLeft = 0;
                    timerRunRight = 0;
                    texturePlayer.Y += 2 * speed;

                    Direction = Direction.Down;   //Fonction a améliorer
                    AnimateFall();

                  
                if (Collision.isCollideScreenDown)
                    {
                        ScreenManager.Screen2.changeScreen(4);
                        texturePlayer.Y = 10;
                    }
                }
                else
                {
                    if (keyboard.IsKeyDown(Keys.Up) && pushSpace == false)
                    {
                        Collision.WallCollision(3, speed);

                        if (!Collision.isCollideWall)
                        {
                            Collision.WallCollision(6, speed);

                            if (Collision.isCollideCable)
                            {
                                timerRunLeft = 0;
                                timerRunRight = 0;
                                currentSpeed = speed;
                                if (!Collision.isCollideScreenUp)//800-30 environ
                                {
                                    texturePlayer.Y -= speed;
                                    Direction = Direction.Up;
                                    AnimateUp();
                                }
                                else
                                {
                                    ScreenManager.Screen2.changeScreen(3); //1 gauche - 2 droite - 3 haut - 4 bas
                                    texturePlayer.Y = 540;
                                }
                            }
                        }
                    }
                    else if (keyboard.IsKeyDown(Keys.Down) && pushSpace == false)
                    {
                        timerRunLeft = 0;
                        timerRunRight = 0;
                        currentSpeed = speed;
                        Collision.WallCollision(4, speed);
                        Direction = Direction.Down;
                        if (!Collision.isCollideWall)
                        {
                            if (!Collision.isCollideScreenUp)//800-30 environ
                            {
                                AnimateDown();
                            }
                            else
                            {
                                ScreenManager.Screen2.changeScreen(4); //1 gauche - 2 droite - 3 haut - 4 bas
                                texturePlayer.Y = 10;
                            }
                        }
                    }
                    else if (keyboard.IsKeyDown(Keys.Left) && pushSpace == false)
                    {
                        timerRunRight = 0;
                        Direction = Direction.Left;
                        ChangeRectangle(24, 54);
                        Collision.WallCollision(1, currentSpeed);

                        if (!Collision.isCollideWall)
                        {
                            if (!Collision.isCollideScreenRL)
                            {
                                Collision.WallCollision(6, 0);
                                if (!Collision.isCollideCable)
                                {

                                    if (timerRunLeft >= 25)
                                    {
                                        Direction = Direction.RunLeft;
                                        AnimateRightLeft();
                                       texturePlayer.X -= 3 * speed;
                                         currentSpeed =  3 * speed;
                                        ChangeRectangle(37, 55);
                                    }
                                    else
                                    {
                                        texturePlayer.X -= speed;
                                        currentSpeed = speed;
                                        Direction = Direction.Left;
                                        AnimateRightLeft();
                                        ChangeRectangle(24, 54);
                                        timerRunLeft++;
                                    }
                                }
                                else //Collide with cable
                                {
                                    texturePlayer.X -= 2 * speed;
                                }
                            }
                            else
                            {
                                ScreenManager.Screen2.changeScreen(1); //1 gauche - 2 droite - 3 haut - 4 bas
                                texturePlayer.X = 765;
                            }
                        }

                    }
                    else if (keyboard.IsKeyDown(Keys.Right) && pushSpace == false)
                    {
                        timerRunLeft = 0;
                        Direction = Direction.Right;
                        ChangeRectangle(24, 54);
                        Collision.WallCollision(2, currentSpeed);

                        if (!Collision.isCollideWall)
                        {
                            if (!Collision.isCollideScreenRL)//800-30 environ
                            {
                                if (timerRunRight >= 25)
                                {
                                    Direction = Direction.RunRight;
                                    AnimateRightLeft();
                                    texturePlayer.X += 3 * speed;
                                    currentSpeed = 3 * speed;
                                    ChangeRectangle(37, 55);
                                }
                                else
                                {
                                    texturePlayer.X += speed;
                                    currentSpeed = speed;
                                    Direction = Direction.Right;
                                    AnimateRightLeft();
                                    ChangeRectangle(24, 54);
                                    timerRunRight++;
                                }
                            }
                            else
                            {
                                ScreenManager.Screen2.changeScreen(2); //1 gauche - 2 droite - 3 haut - 4 bas
                                texturePlayer.X = 215;
                            }
                        }

                    }
                    else if (keyboard.IsKeyDown(Keys.E))
                    {
                        timerRunLeft = 0;
                        timerRunRight = 0;
                        currentSpeed = speed;

                        Direction = Direction.Down;
                        AnimateAction();

                    }
                    else if (keyboard.IsKeyUp(Keys.Right) && keyboard.IsKeyUp(Keys.Left) && keyboard.IsKeyUp(Keys.Up) && Direction != Direction.Up && keyboard.IsKeyUp(Keys.Down) && keyboard.IsKeyUp(Keys.Space) && pushSpace == false)
                    {

                        if (Direction == Direction.Right || Direction == Direction.RunRight || Direction == Direction.JumpRight)
                        {
                            frameColumn = 2;
                            Direction = Direction.Right;
                        }
                        else if (Direction == Direction.Left || Direction == Direction.RunLeft || Direction == Direction.JumpLeft)
                        {
                            frameColumn = 2;
                            Direction = Direction.Left;
                        }
                        else if (Direction == Direction.JumpFace)
                        {
                            frameColumn = 2;
                            Direction = Direction.Down;
                        }
                        timerRunLeft = 0;
                        timerRunRight = 0;
                        currentSpeed = speed;
                        ChangeRectangle(24, 54);
                    }

                    if (keyboard.IsKeyDown(Keys.Space) && pushSpace == false)
                    {
                        
                            pushSpace = true;//Pour eviter les abus
                            if (keyboard.IsKeyUp(Keys.Right) && keyboard.IsKeyUp(Keys.Left))
                            {
                                timerRunLeft = 0;
                                timerRunRight = 0;
                            }
                            //On assigne la direction du saut
                            if (keyboard.IsKeyDown(Keys.Right))
                            {
                                Direction = Direction.JumpRight;
                            }
                            else if (keyboard.IsKeyDown(Keys.Left))
                            {
                                Direction = Direction.JumpLeft;
                            }
                            else
                            {
                                Direction = Direction.JumpFace;
                            }
                            ChangeRectangle(37, 55);//On change la taille des rectangles car les sprites ne sont pas de la meme taille
                            if (currentSpeed > speed)
                            {
                                jumpSpeed.X = 7;
                            }
                            else
                            {
                                jumpSpeed.X = 4;
                            }
                            jumpBool = true; //On lance la procédure de saut
                        }
                    

                    switch (Direction)
                    {
                        case Direction.Up:
                            frameLine = 2;
                            Reverse = SpriteEffects.None;
                            break;

                        case Direction.Down:
                            frameColumn = 1;
                            frameLine = 3;
                            Reverse = SpriteEffects.None;
                            break;

                        case Direction.Right:
                            frameLine = 1;
                            Reverse = SpriteEffects.None;
                            break;

                        case Direction.Left:
                            frameLine = 1;
                            Reverse = SpriteEffects.FlipHorizontally;
                            break;

                        case Direction.JumpRight:
                            frameLine = 4;
                            Reverse = SpriteEffects.None;
                            break;

                        case Direction.JumpLeft:
                            frameLine = 4;
                            Reverse = SpriteEffects.FlipHorizontally;
                            break;

                        case Direction.JumpFace:
                            frameLine = 4;
                            Reverse = SpriteEffects.None;
                            break;

                        case Direction.RunLeft:
                            frameLine = 5;
                            Reverse = SpriteEffects.FlipHorizontally;
                            break;

                        case Direction.RunRight:
                            frameLine = 5;
                            Reverse = SpriteEffects.None;
                            break;




                    }


                }
            }//END OF FALLISTRUE
        }// end of update

        #endregion
    }

}
