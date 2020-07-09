using System;

namespace DP_Path_Problems
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        #region Leetcode 62/62  Unique Path Series
        public int UniquePathsWithObstacles(int[][] obstacleGrid)
        {
            int n = obstacleGrid.Length;
            if(obstacleGrid == null || n == 0) { return 0; }
            int m = obstacleGrid[0].Length;
            UPmem = new int[n + 1][];
            for (int i = 0; i < n+1; i++)
            {
                UPmem[i] = new int[m + 1];
                Array.Fill(UPmem[i], -1);
            }
            return UPFind(obstacleGrid, 0, 0);
        }
        int[][] UPmem;
        public int UPFind(int[][] grid, int x, int y)
        {
            if (x < 0 || y < 0 || x >= grid[0].Length || y >= grid.Length) { return 0; }
            else if (x == grid[0].Length - 1 && y == grid.Length - 1) { return 1 - grid[y][x]; } // It is 1-grid[y][x] here
            // There is a possibility that the destination is also an obstacle
            else if (UPmem[y][x] != -1) { return UPmem[y][x]; } // We have already calculated this 
            else if (grid[y][x] == 1) { return 0; }
            return UPmem[y][x] = UPFind(grid, x + 1, y) + UPFind(grid, x, y + 1);
        }
        #endregion
    }
}
