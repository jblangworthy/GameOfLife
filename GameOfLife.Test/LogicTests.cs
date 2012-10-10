using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using GameOfLife.Model;

namespace GameOfLife.Test
{
    [TestClass]
    public class LogicTests
    {
        [TestMethod]
        public void AnyLiveCellWithFewerThanTwoLiveNeighboursDies()
        {
            var testCell = new Cell(1, true);
            Assert.AreEqual(false, testCell.CellShouldLive());
        }

        [TestMethod]
        public void AnyLiveCellWithTwoOrThreeNeighboursLives()
        {
            var testCellWith2 = new Cell(2, true);
            var testCellWith3 = new Cell(3, true);
            Assert.AreEqual(true, testCellWith2.CellShouldLive());
            Assert.AreEqual(true, testCellWith3.CellShouldLive());
        }

        [TestMethod]
        public void AnyDeadCellWithExactlyThreeNeighboursShouldLive()
        {
            var testCellDeadWith3 = new Cell(3, false);
            Assert.AreEqual(true, testCellDeadWith3.CellShouldLive());
        }
    }
}


/*Any live cell with fewer than two live neighbours dies, as if caused by under-population.
Any live cell with two or three live neighbours lives on to the next generation.
Any live cell with more than three live neighbours dies, as if by overcrowding.
Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.*/