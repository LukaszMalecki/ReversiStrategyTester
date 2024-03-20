﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyReversiVS.ReversiClasses
{
    internal class StrategyEndgame : IStrategyReversi
    {
        public StrategyEndgame() { }


        override public string ToString()
        {
            return "StrategyEndgame";
        }

        public double EvaluationFunction(Player player, GameState gameState)
        {
            if (gameState.GameOver)
            {
                return gameState.ResultScore(player);
            }
            double retValue = 0;

            int discCount = gameState.DiscNumber();

            //bool isEndgame = false;
            /*if( discCount > 44)
                isEndgame = true;*/

            //Early game it's better to have lower number of discs

            double tempValue = (gameState.DiscCount[player] / discCount - 0.5) * 10.0;
            /*if ( !isEndgame ) 
            {
                tempValue -= 1;
            }*/
            retValue += tempValue;

            //FOR ENDGAME
            //Other parameters lose a bit of their importance in relation to discCount

            //it's good to have center pieces
            double tempValue2;
            (tempValue, tempValue2) = gameState.CountCenterPieces(player);

            retValue += (tempValue / (tempValue + tempValue2) - 0.5) * 3.0;

            //it's good to have more legal moves

            tempValue = gameState.LegalMoves.Count - 2;

            if (tempValue > 0)
            {
                tempValue *= 0.5;
            }
            if (player != gameState.CurrentPlayer)
                tempValue *= -1;
            
            retValue += tempValue;

            //Corners are very good to have

            (tempValue, tempValue2) = gameState.CornersCount(player);

            retValue += (tempValue - tempValue2) * 7.5;

            //You shouldn't have pieces adjacent to empty corner

            (tempValue, tempValue2) = gameState.AdjacentToEmptyCornersCount(player);

            retValue -= (tempValue - tempValue2) * 6.0;

            return retValue;
        }
    }
}
