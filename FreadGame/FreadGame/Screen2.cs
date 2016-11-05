using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;



namespace ScreenManager
{
    class Screen2 : Screen
    {
        #region ATTRIBUTS

        //ATTRIBUTS 

        static public List<Rectangle> listSquid; //Liste de rectangle pour les collisions

        static public float timerS;

        static public int currentFace;

        Rectangle realGameMap;
        Rectangle background;
        Rectangle map;
        Rectangle line;

        static string timerText;
        static string mapText;
        static string mapCentre;
        static string mapDroite;
        static string mapGauche;
        static string mapHaut;
        static string mapBas;
        static string mapBasBas;

        Vector2 posTexteTimer;
        Vector2 posTimerTime;
        Vector2 posTexteMap;
        Vector2 posTexteS;

        Vector2 posMapGauche;
        Vector2 posMapDroite;
        Vector2 posMapHaut;
        Vector2 posMapCentre;
        Vector2 posMapBas;
        Vector2 posMapBasBas;

        static public FreadGame.Block[, ,] mapGrid;

        static public int[, ,] mapGridRotation;
        static public char[, ,] mapGridLink;

        static public SpriteEffects flip;
        static public float rotation;


        #endregion


        #region CONSTRUCTOR
        public Screen2(GraphicsDevice device)
            : base(device, "screen2")
        {
            timerS = 0;

            background = new Rectangle(200, 0, 600, 600);
            map = new Rectangle(0, 50, 200, 200);

            line = new Rectangle(198, 0, 3, 600);
            timerText = "TIMER:";
            mapText = "MAP:";
            posTexteMap = new Vector2(5, 50);
            posTexteTimer = new Vector2(5, 0);
            posTimerTime = new Vector2(85, 0);
            posTexteS = new Vector2(160, 0);

            mapCentre = "1";
            mapBas = "2";
            mapDroite = "3";
            mapHaut = "4";
            mapGauche = "5";
            mapBasBas = "6";


            posMapHaut = new Vector2(95, 75);
            posMapBas = new Vector2(95, 160);
            posMapGauche = new Vector2(53, 118);
            posMapDroite = new Vector2(142, 118);
            posMapBasBas = new Vector2(95, 203);
            posMapCentre = new Vector2(100, 118);

            currentFace = 1;

            mapGrid = new FreadGame.Block[6, 20, 20];
            mapGridRotation = new int[6, 20, 20];
            mapGridLink = new char[6, 20, 20];

            flip = SpriteEffects.None;
            rotation = 0;
            listSquid = new List<Rectangle>();
        }

        #endregion
        public override bool Init()
        {
            FreadGame.Ressources.StopMusic();//Arret de la musique de la fenetre précedente
            FreadGame.Ressources.ListenMusicGame();//Debut de la nouvelle musique

            FileLoad();//Lecture du fichier texte et chargement des variables
            return base.Init();
        }

        public override void Shutdown()
        {
            base.Shutdown();
        }

        #region METHODES

        public static void changeScreen(int direction)
        {
            switch (direction)
            {
                case 1: // LEFT
                    switch (currentFace)
                    {
                        case 1:
                            currentFace = 5;
                            break;
                        case 2:
                            currentFace = 5;

                            break;
                        case 3:
                            currentFace = 1;

                            break;
                        case 4:
                            currentFace = 5;

                            break;
                        case 5:
                            currentFace = 3;

                            break;
                        case 6:
                            currentFace = 5;

                            break;
                    }
                    break;
                case 2://RIGHT
                    switch (currentFace)
                    {
                        case 1:
                            currentFace = 3;

                            break;
                        case 2:
                            currentFace = 3;

                            break;
                        case 3:
                            currentFace = 5;

                            break;
                        case 4:
                            currentFace = 3;

                            break;
                        case 5:
                            currentFace = 1;

                            break;
                        case 6:
                            currentFace = 3;

                            break;
                    }
                    break;
                case 3: //UP
                    switch (currentFace)
                    {
                        case 1:
                            currentFace = 4;

                            break;
                        case 2:
                            currentFace = 1;

                            break;
                        case 3:
                            currentFace = 4;

                            break;
                        case 4:
                            currentFace = 6;

                            break;
                        case 5:
                            currentFace = 4;

                            break;
                        case 6:
                            currentFace = 2;

                            break;
                    }
                    break;
                case 4://DOWN
                    switch (currentFace)
                    {
                        case 1:
                            currentFace = 2;

                            break;
                        case 2:
                            currentFace = 6;

                            break;
                        case 3:
                            currentFace = 2;

                            break;
                        case 4:
                            currentFace = 1;

                            break;
                        case 5:
                            currentFace = 2;

                            break;
                        case 6:
                            currentFace = 4;

                            break;
                    }
                    break;
            }
        }
        public static void FileLoad()
        {

            string[] fileContent;
            string fileName;
            string sourcePath;
            string filePath;

            fileName = "Map.f25";
            sourcePath = @"";

            filePath = System.IO.Path.Combine(sourcePath, fileName);
            fileContent = File.ReadAllLines(filePath);
            try
            {
                // Determine si le fichier existe

                for (int f = 0; f < 6; f++)
                {
                    for (int l = 0; l < 20; l++)
                    {
                        for (int c = 0; c < 60; c++)
                        {
                            mapGrid[f, c / 3, l] = FreadGame.Block.listBlock[(int)fileContent[21 * f + l][c]];


                            if (mapGrid[0, c / 3, l].id == '\x0006')//Position de départ pour le joueur (selon la position de l'entree)
                            {
                                FreadGame.Player.texturePlayer = new Rectangle(200 + c * 10, l * 30, 24, 54);
                            }
                            c++;

                            switch (fileContent[21 * f + l][c])
                            {
                                case '0':
                                    mapGridRotation[f, (c - 1) / 3, l] = 1;//rotation none-flip none
                                    break;
                                case '1':
                                    mapGridRotation[f, (c - 1) / 3, l] = 2;//rotate 270-flip XY
                                    break;
                                case '2':
                                    mapGridRotation[f, (c - 1) / 3, l] = 3;//rotate none-flip XY
                                    break;
                                case '3':
                                    mapGridRotation[f, (c - 1) / 3, l] = 4;//rotate90 - flip XY
                                    break;
                                default:
                                    mapGridRotation[f, (c - 1) / 3, l] = 1;//rotation none-flip none
                                    break;
                            }

                            c++;
                            mapGridLink[f, (c - 2) / 3, l] = fileContent[21 * f + l][c];



                        } // for (int c = 0; c < 60; c++)
                    } // for (int l = 0; l < 20; l++)
                } // for (int f = 0; f < 6; f++)

            } // if (openFileDialog1.ShowDialog() == DialogResult.OK)

            catch (Exception e)
            {
                MessageBox.Show(e.Message, e.ToString());
            }
            finally { }
        }

        static public void DrawFace(SpriteBatch spriteBatch)
        {
            Vector2 vector = new Vector2(0,0);
            int tailleSpecX;
            int tailleSpecY;

            listSquid.Clear();//On vide la liste a chaque affichage

            int rotation2Draw;


            for (int l = 0; l < 20; l++)
            {
                for (int c = 0; c < 20; c++)
                {

                    if (mapGrid[currentFace - 1, c, l].id != '\x0000') //Si ce n'est pas un bloc vide
                    {
                        tailleSpecX = 30; // taille habituelle en X
                        tailleSpecY = 30; //taille habituelle en Y

                        if (mapGrid[currentFace - 1, c, l].id == '\x0006' || mapGrid[currentFace - 1, c, l].id == '\x0003')//Si c'est des blocs comme la porte et l'entree
                        {
                            tailleSpecY = 60;//changement de la taille en Y
                        }

                        listSquid.Add(new Rectangle(200 + (30 * c), (30 * l), tailleSpecX, tailleSpecY)); // Creation de la liste des rectangle représentant nos blocs
                        rotation2Draw = mapGridRotation[currentFace - 1, c, l]; //Variable juste pour la simplification


                        switch (rotation2Draw)
                        {
                            case 1:
                                flip = SpriteEffects.None;
                                rotation = 0;//rotation none-flip none
                                vector = new Vector2(0, 0);
                                break;
                            case 2:
                                flip = SpriteEffects.None;
                                rotation = 4.71F; //rotate 270-flip XY
                                vector = new Vector2(30, 0);
                                break;
                            case 3:
                                flip = SpriteEffects.FlipHorizontally;//rotate none-flip XY // LEFT
                                rotation = 0;
                                vector = new Vector2(0, 0);
                                break;
                            case 4:
                                flip = SpriteEffects.None;
                                rotation = 1.57F; //rotate90 - flip XY
                                vector = new Vector2(0, 30);
                                break;
                            default:
                                flip = SpriteEffects.None;
                                rotation = 0; //rotation none-flip none
                                vector = new Vector2(0, 0);
                                break;
                        }


                        spriteBatch.Draw(mapGrid[currentFace - 1, c, l].blockImage, new Rectangle(200 + (30 * c), (30 * l), tailleSpecX, tailleSpecY), null, Color.White, rotation, vector, flip, 0f);


                    } // (mapGrid[currentFace - 1, c, l] != null)

                } // for (int c = 0; c < 20; c++)
            } // for (int l = 0; l < 20; l++)



        }

        #endregion

        #region DRAW & UPDATE
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _device.Clear(Color.DarkSeaGreen);
            //IMAGE SUR LE JEU
            spriteBatch.Draw(FreadGame.Ressources.background, background, Color.White);//le fond
            spriteBatch.Draw(FreadGame.Ressources.map, map, Color.White);//Affichage de la carte
            spriteBatch.Draw(FreadGame.Ressources.line, line, Color.White);//black line

            spriteBatch.Draw(FreadGame.Ressources.block_item, new Rectangle(75, 320, 50, 50), Color.White);//block item
            spriteBatch.DrawString(FreadGame.Ressources.myFont, "ITEM:", new Vector2(75, 300), Color.Black);//item text
            if (FreadGame.Player.itemInThePocket)
            {
                spriteBatch.Draw(FreadGame.Ressources.cable_blanc, new Rectangle(85, 330, 30, 30), Color.White);//item
            }

            switch (currentFace - 1)
            {
                case 0: spriteBatch.Draw(FreadGame.Ressources.mapzone, new Rectangle(82, 109, 40, 37), Color.White);
                    break;

                case 1: spriteBatch.Draw(FreadGame.Ressources.mapzone, new Rectangle(82, 153, 40, 37), Color.White);
                    break;

                case 2: spriteBatch.Draw(FreadGame.Ressources.mapzone, new Rectangle(127, 109, 40, 37), Color.White);
                    break;

                case 3: spriteBatch.Draw(FreadGame.Ressources.mapzone, new Rectangle(82, 67, 40, 37), Color.White);
                    break;

                case 4: spriteBatch.Draw(FreadGame.Ressources.mapzone, new Rectangle(38, 109, 40, 37), Color.White);
                    break;

                case 5: spriteBatch.Draw(FreadGame.Ressources.mapzone, new Rectangle(82, 196, 40, 37), Color.White);
                    break;

                default:
                    spriteBatch.Draw(FreadGame.Ressources.mapzone, new Rectangle(82, 59, 40, 37), Color.White);
                    break;
            }

            spriteBatch.DrawString(FreadGame.Ressources.myFont, timerText, posTexteTimer, Color.Black);//position timer text

            spriteBatch.DrawString(FreadGame.Ressources.myFont, mapText, posTexteMap, Color.Black);// map text

            spriteBatch.DrawString(FreadGame.Ressources.myFont, "" + timerS.ToString("0") + " s", posTimerTime, Color.Black);// le temps en seconde




            spriteBatch.DrawString(FreadGame.Ressources.myFont, mapBas, posMapBas, Color.Black);// map text
            spriteBatch.DrawString(FreadGame.Ressources.myFont, mapHaut, posMapHaut, Color.Black);// map text
            spriteBatch.DrawString(FreadGame.Ressources.myFont, mapDroite, posMapDroite, Color.Black);// map text
            spriteBatch.DrawString(FreadGame.Ressources.myFont, mapGauche, posMapGauche, Color.Black);// map text
            spriteBatch.DrawString(FreadGame.Ressources.myFont, mapCentre, posMapCentre, Color.Black);// map text
            spriteBatch.DrawString(FreadGame.Ressources.myFont, mapBasBas, posMapBasBas, Color.Black);// map text


            DrawFace(spriteBatch);//Affichage des elements du jeu

            //FIN IMAGE SUR LE JEU
            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime, MouseState Mouse, KeyboardState Keyboard)
        {
            if (FreadGame.GameMain.IsGameStart)
            {
                timerS += (float)gameTime.ElapsedGameTime.TotalSeconds;//Calcul du temps en seconde pour le timer
            }
            base.Update(gameTime, Mouse, Keyboard);

        }

        #endregion

    }
}