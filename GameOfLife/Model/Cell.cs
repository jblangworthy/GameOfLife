using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Model
{
    public class Cell
    {
        public int LiveNeighbourCount { get; private set; }
        public bool IsAlive { get; private set; }

        public Cell(int numLiveNeighbours, bool isAlive)
        {
            this.LiveNeighbourCount = numLiveNeighbours;
            this.IsAlive = isAlive;
        }

        public bool CellShouldLive()
        {
            // Any live cell with two or three live neighbours lives on to the next generation.
            if (IsAlive && (LiveNeighbourCount == 2 || LiveNeighbourCount == 3)) 
            {
                return true;
            }

            // Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
            if (!IsAlive && LiveNeighbourCount == 3)
            {
                return true;
            }
            // Any live cell with fewer than two live neighbours dies, as if caused by under-population.
            // Any live cell with more than three live neighbours dies, as if by overcrowding.
            return false;
        }


        public void IncrementLiveNeighbourCount()
        {
            LiveNeighbourCount++;
        }

        public void DecrementLiveNeighbourCount()
        {
            LiveNeighbourCount--;
        }
    }
}