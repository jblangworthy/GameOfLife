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
        public int TickCount { get; private set; }

        public Grid(Dictionary<CoOrds, Cell> seed)
        {
            TickCount = 0;
            CurrentLiveCells = seed;
            UpdateCurrentLiveCellsNeighbourCount();
        }

        public void Tick() 
        {
            // assume we know the neighbour count of each live cell, do this after adding seed to 
            // live cells plus neighbouring dead are "cells of interest"
            // loop through each and add ones that pass the "should survive" test to list of "future live cells"
            var cellsOfInterest = CurrentLiveCells;

            foreach (var kvp in CurrentLiveCells.ToList())
            {
                // loop thru each live cell and establish each dead neighbour's live count
                foreach (var neighbourCoOrd in GetNeighbouringCoOrds(kvp.Key))
                {
                    // if it is already 
                    if (cellsOfInterest.Keys.Contains(neighbourCoOrd))
                    {
                        if (!cellsOfInterest[neighbourCoOrd].IsAlive)
                        {
                            cellsOfInterest[neighbourCoOrd].IncrementLiveNeighbourCount();
                        }
                    }
                    else
                    {
                        cellsOfInterest.Add(neighbourCoOrd, new Cell(1, false));
                    }
                }
            }

            var cellsThatSurvived = new Dictionary<CoOrds, Cell>();
            foreach (var kvp in cellsOfInterest)
            {
                if (kvp.Value.CellShouldLive())
                {
                    cellsThatSurvived.Add(kvp.Key, new Cell(0, true));
                }
            }

            CurrentLiveCells = cellsThatSurvived;
            UpdateCurrentLiveCellsNeighbourCount();
            TickCount++;
        }

        /// <summary>
        /// Update the live cell's neighbour count
        /// </summary>
        private void UpdateCurrentLiveCellsNeighbourCount()
        {
            // populate with live neighbour count
            foreach (var kvp in CurrentLiveCells)
            {
                kvp.Value.ResetNeighbourCount();
                foreach (var neighbourCoOrd in GetNeighbouringCoOrds(kvp.Key))
                {
                    if (CurrentLiveCells.Keys.Contains(neighbourCoOrd))
                    {
                        kvp.Value.IncrementLiveNeighbourCount();
                    }
                }
            }
        }


        /// <summary>
        /// Find the coordinates of 8 neighbours based on coordinates of one cell
        /// </summary>
        /// <param name="currentCoOrds">CoOrdinates of cell of which you wish to find the neighbours</param>
        /// <returns>Collection of 8 CoOrds objects</returns>
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

        public bool Equals(Dictionary<CoOrds,Cell> comparisonCells)
        {
            if (comparisonCells.Count != CurrentLiveCells.Count)
                return false; // Different number of items

            foreach (var kvp in CurrentLiveCells)
            {
                Cell comparisonCell;
                if (!comparisonCells.TryGetValue(kvp.Key, out comparisonCell))
                    return false; // key missing in comparison
                if (!kvp.Value.Equals(comparisonCell))
                    return false; // value is different
            }
            return true;
        }
    }
}
