using Battleships.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    // Imagine a game of battleships.
    //   The player has to guess the location of the opponent's 'ships' on a 10x10 grid
    //   Ships are one unit wide and 2-4 units long, they may be placed vertically or horizontally
    //   The player asks if a given co-ordinate is a hit or a miss
    //   Once all cells representing a ship are hit - that ship is sunk.
    public class Game
    {
        // ships: each string represents a ship in the form first co-ordinate, last co-ordinate
        //   e.g. "3:2,3:5" is a 4 cell ship horizontally across the 4th row from the 3rd to the 6th column
        // guesses: each string represents the co-ordinate of a guess
        //   e.g. "7:0" - misses the ship above, "3:3" hits it.
        // returns: the number of ships sunk by the set of guesses
        public static int Play(string[] ships, string[] guesses)
        {
            try
            {
                List<ShipDetails> shipDetailsList = ParseShipDetailsAndCheckHit(ships, guesses);
                int shipNotSunkCount = shipDetailsList.Where(x => x.ShipCoordinateList.Any(y => !y.IsCoordinateHit)).ToList().Count;
                return shipDetailsList.Count - shipNotSunkCount;
            }
            catch (Exception)
            {

                return -1;
            }
        }
        private static List<ShipDetails> ParseShipDetailsAndCheckHit(string[] ships, string[] guesses)
        {
            try
            {
                List<ShipDetails> shipDetailsList = new List<ShipDetails>();
                List<string> coordinateList = new List<string>();
                if(ships.Any())
                {
                    foreach(var ship in ships)
                    {
                        ShipDetails shipDetails = new ShipDetails();    
                        string[] coordinate = ship.Split(','); //This will split the coordinate of ship starting and end

                        //below code to get start and end coordinate of ship . First Y and secode is x (y:x)
                        int startingYCoordinate = Convert.ToInt32(coordinate[0].Split(':')[0].Trim());
                        int startingXCoordinate = Convert.ToInt32(coordinate[0].Split(':')[1].Trim());
                        int endYCoordinate = Convert.ToInt32(coordinate[1].Split(':')[0].Trim());
                        int endXCoordinate = Convert.ToInt32(coordinate[1].Split(':')[1].Trim());

                        //As mention grid is of 10X10 so y start from 0 to 9 and x start from 0 to 9
                        if(startingYCoordinate <0 || startingYCoordinate>9
                            ||startingXCoordinate <0 || startingXCoordinate >9
                            ||endYCoordinate <0 || endYCoordinate>9
                            ||endXCoordinate <0 || endXCoordinate>9 )
                        {
                            throw new Exception("Ship Coordinate is not valid");
                        }

                        List<ShipCoordinateDetails> shipCoordinateDetailsList = new List<ShipCoordinateDetails>();
                        if(startingYCoordinate == endYCoordinate)
                        {
                            if((endXCoordinate - startingXCoordinate) !=1&&(endXCoordinate -startingXCoordinate)!=3)
                            {                                
                                throw new Exception("Length of ship is not valid");
                            }
                            shipDetails.ShipAlignment = EnumShipAlignment.Horizantal;

                            while(startingXCoordinate <= endXCoordinate)
                            {
                                ShipCoordinateDetails shipCordinateDetails = new ShipCoordinateDetails();
                                shipCordinateDetails.Coordinate = startingYCoordinate.ToString() + ":" + startingXCoordinate.ToString();
                                if(coordinateList.Any(x=>x==shipCordinateDetails.Coordinate))
                                {
                                    throw new Exception("Two ships crossed each other");
                                }
                                coordinateList.Add(shipCordinateDetails.Coordinate);
                                shipCordinateDetails.IsCoordinateHit = guesses.Any(x => x.Trim() == shipCordinateDetails.Coordinate);
                                shipCoordinateDetailsList.Add(shipCordinateDetails);
                                startingXCoordinate++;
                            }
                        }
                        else if(startingXCoordinate == endXCoordinate)
                        {
                            if ((endYCoordinate - startingYCoordinate) != 1 && (endYCoordinate - startingYCoordinate) != 3)
                            {
                                throw new Exception("Length of ship is not valid");
                            }
                            shipDetails.ShipAlignment = EnumShipAlignment.Vertical;
                            while (startingYCoordinate <= endYCoordinate)
                            {
                                ShipCoordinateDetails shipCordinateDetails = new ShipCoordinateDetails();
                                shipCordinateDetails.Coordinate = startingYCoordinate.ToString() + ":" + startingXCoordinate.ToString();
                                if (coordinateList.Any(x => x == shipCordinateDetails.Coordinate))
                                {
                                    throw new Exception("Two ships crossed each other");
                                }
                                coordinateList.Add(shipCordinateDetails.Coordinate);
                                shipCordinateDetails.IsCoordinateHit = guesses.Any(x => x.Trim() == shipCordinateDetails.Coordinate);
                                shipCoordinateDetailsList.Add(shipCordinateDetails);
                                startingXCoordinate++;
                            }
                        }
                        else
                        {
                            throw new Exception("Ship is not properly align ");
                        }
                        shipDetails.ShipCoordinateList = shipCoordinateDetailsList;
                        shipDetailsList.Add(shipDetails);

                    }
                }
                else
                {
                    throw new Exception("Ship details not entered.");
                }
                return shipDetailsList;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
