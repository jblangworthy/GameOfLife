using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using GameOfLife.Model;

namespace GameOfLife.Test
{
    [TestClass]
    public class GridTests
    {
        private Dictionary<CoOrds, Cell> GetSimpleSeed()
        {
            var testSeed = new Dictionary<CoOrds, Cell>();
            testSeed.Add(new CoOrds(1, 1), new Cell(0, true));
            testSeed.Add(new CoOrds(2, 2), new Cell(0, true));
            testSeed.Add(new CoOrds(1, 3), new Cell(0, true));
            return testSeed;
        }
        
        [TestMethod]
        public void SeedIsStoredAndNeighboursCountedCorrectly()
        {
            var testSeed = GetSimpleSeed();
            var testGrid = new Grid(testSeed);
            // the cell at 2,2 should have two neighbours
            Assert.AreEqual(2, testGrid.CurrentLiveCells[new CoOrds(2, 2)].LiveNeighbourCount);
        }

        [TestMethod]
        public void GridAccurateAfterOneTick()
        {
            var testGrid = new Grid(GetSimpleSeed());
            testGrid.Tick();

            var newGridState = new Dictionary<CoOrds, Cell>();
            newGridState.Add(new CoOrds(1, 2), new Cell(1, true));
            newGridState.Add(new CoOrds(2, 2), new Cell(1, true));

            Assert.IsTrue(testGrid.Equals(newGridState));
        }



    }
}
