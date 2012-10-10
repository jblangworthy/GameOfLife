using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Model
{
    public class Grid
    {
        public IDictionary<CoOrds,Cell> CurrentLiveCells { get; private set; }

        public Grid(Dictionary<CoOrds, Cell> seed)
        {
            CurrentLiveCells = seed;
        }

        public void Tick() 
        {
            var cellsMidTick = new Dictionary<CoOrds, Cell>();

            // loop through each currently live cell and decide if it should live or die
            // while looping keep track of number of live neighbours

            foreach (var kvp in CurrentLiveCells)
            {
                if (kvp.Value.CellShouldLive())
                {
                    
                }
            }
        }



        private List<CoOrds> GetNeighbouringCoOrds(CoOrds currentCoOrds)
        {
            var neighbouringCoOrds = new List<CoOrds>();

            var neighbourY = currentCoOrds.Y - 1;  // row above
            while (neighbourY <= currentCoOrds.Y + 1)
            {
                var neighbourX = currentCoOrds.X - 1;  // column to the left
                while (neighbourX <= currentCoOrds.X + 1)
                {
                    var neighbourCoOrds = new CoOrds(neighbourX, neighbourY);
                    if (!neighbourCoOrds.Equals(currentCoOrds)) // don't add the original coords to the list of neighbours
                    {
                        neighbouringCoOrds.Add(neighbourCoOrds);
                    }
                    neighbourX++;
                }
                neighbourY++;
            }

            return neighbouringCoOrds;
        }
    }
}
