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
            
        }
    }
}
