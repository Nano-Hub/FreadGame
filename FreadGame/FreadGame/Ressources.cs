using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;


namespace FreadGame
{
    class Ressources
    {

        #region ATTRIBUTS
        //Attributs


        //IMAGE PAGE D'ACCEUIL
        //*********************************************BUTTON****************************
        public static Texture2D button_play;
        public static Texture2D button_parametre;
        public static Texture2D button_title;
        public static Texture2D button_credit;

        //**********************************************PICTURE**************************
        public static Texture2D imageHome;
        public static Texture2D playermap;
        public static Texture2D background;
        public static Texture2D map;
        public static Texture2D line;
        public static Texture2D block_item;
        public static Texture2D mapzone;
        public static Texture2D endScreen;

        //--------------------------------------Blocks----------------------
        public static Texture2D tuyau;
        public static Texture2D tuyau_bout;
        public static Texture2D porte;
        public static Texture2D levier;
        public static Texture2D fils;
        public static Texture2D entree;
        public static Texture2D circuits;
        public static Texture2D cable_blanc;

        //**********************************************SONG**************************
        public static Song musicGame;
        public static Song musicHome;
        //**********************************************FONT************************
        public static SpriteFont myFont;


        #endregion


        #region METHODES
        //Methodes
        public static void LoadContent(ContentManager Content)
        {
            //IMAGES********************************
            playermap = Content.Load<Texture2D>("test_image2");


            //BLOCK*********************************
            tuyau = Content.Load<Texture2D>("tuyaux");
            tuyau_bout = Content.Load<Texture2D>("tuyaux_bout");
            porte = Content.Load<Texture2D>("porte");
            levier = Content.Load<Texture2D>("levier");
            fils = Content.Load<Texture2D>("fils");
            entree = Content.Load<Texture2D>("entree");
            circuits = Content.Load<Texture2D>("circuits");
            cable_blanc = Content.Load<Texture2D>("cables_blanc");


            //IMAGES MENU***************************
            imageHome = Content.Load<Texture2D>("cube1");
            background = Content.Load<Texture2D>("fond 2");
            map = Content.Load<Texture2D>("map");
            line = Content.Load<Texture2D>("barre");
            block_item = Content.Load<Texture2D>("block_item");
            mapzone = Content.Load<Texture2D>("mapzone");
            endScreen = Content.Load<Texture2D>("End_screen");

            //BUTTON*****************************
            button_play = Content.Load<Texture2D>("button_play");
            button_parametre = Content.Load<Texture2D>("button_parametre");
            button_credit = Content.Load<Texture2D>("button_credit");
            button_title = Content.Load<Texture2D>("button_title");

            //FONT
            myFont = Content.Load<SpriteFont>("myFont");

            //MUSIQUES
            musicHome = Content.Load<Song>("Pamgaea");
            musicGame = Content.Load<Song>("DreamCulture");
        }

        public static void ListenMusicHome()
        {
            MediaPlayer.Play(musicHome);
            MediaPlayer.Volume = 0.1f;
        }

        public static void ListenMusicGame()
        {
            MediaPlayer.Play(musicGame);
            MediaPlayer.Volume = 0.1f;
        }
        public static void StopMusic()
        {
            MediaPlayer.Stop();
        }


        #endregion
    }
}
