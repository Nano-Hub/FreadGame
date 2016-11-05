using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;

using System.Windows.Forms;



namespace FreadGame
{
    class Block
    {



        #region ATTRIBUTS

        //ATTRIBUTS*************************************************************************************


        static public List<Block> listBlock;
        public String name;
        public char id;
        public Texture2D blockImage;
        public int type;

        #endregion

        #region CONSTRUCTOR
        //CONSTRUCTOR*************************************************************************************** 
        public Block()
        {
            id = '\x0000';
            name = "";
            blockImage = null;
            listBlock = new List<Block>();

            switch (id)
            {
                case '\x0000': type = 0;
                    break;
                case '\x0001': type = 1;
                    break;
                case '\x0002': type = 1;
                    break;
                case '\x0003': type = 1;
                    break;
                case '\x0007': type = 1;
                    break;
                default: type = 2;
                    break;

            }//Type 0: Bloc vide  Type 1: Bloc non traversable Type 2 : Bloc traversable
        }
        #endregion

        #region METHODES

        //METHODES*****************************************************************************************
        public Block(char newId, String newName, Texture2D newImage, int newType)
        {
            blockImage = newImage;
            name = newName;
            id = newId;
            type = newType;

        }

        //-----------------------------------------------------------------------------------------------------
        static public void BlockLoad()
        {
            listBlock = new List<Block>();
            listBlock.Add(new Block('\x0000', "neant", FreadGame.Ressources.tuyau, 0));// VOID BLOCK (NOT SO EMPTY)!
            listBlock.Add(new Block('\x0001', "tuyaux", FreadGame.Ressources.tuyau, 1));
            listBlock.Add(new Block('\x0002', "tuyaux_bout", FreadGame.Ressources.tuyau_bout, 1));
            listBlock.Add(new Block('\x0003', "porte", FreadGame.Ressources.porte, 1));
            listBlock.Add(new Block('\x0004', "levier", FreadGame.Ressources.levier, 2));
            listBlock.Add(new Block('\x0005', "fils", FreadGame.Ressources.fils, 2));
            listBlock.Add(new Block('\x0006', "entree", FreadGame.Ressources.entree, 2));
            listBlock.Add(new Block('\x0007', "circuit", FreadGame.Ressources.circuits, 1));
            listBlock.Add(new Block('\x0008', "cable_blanc", FreadGame.Ressources.cable_blanc, 2));


        }

        #endregion



    }
    //---------------------------------------------------------------------------------------------
}
    

