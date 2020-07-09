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
        #region Leetcode 64 Maximum Path Sum
        public int MinPathSum(int[][] grid)
        {
            int y = grid.Length;
            if (y == 0) { return 0; }
            int x = grid[0].Length;
            int[][] dp = new int[y][];
            // dp[i][j] stores the minimum path sum on (j,i)
            for (int i = 0; i < y; i++)
            {
                dp[i] = new int[x];
            }
            #region We initiallize the border
            dp[0][0] = grid[0][0];
            for (int i = 1; i < x; i++)
            {
                dp[0][i] = dp[0][i - 1] + grid[0][i];
            }
            for (int i = 1; i < y; i++)
            {
                dp[i][0] = dp[i - 1][0] + grid[i][0];
            }
            #endregion
            for (int i = 1; i < y; i++)
            {
                for (int j = 1; j < x; j++)
                {
                    // We can come from the top or the left
                    dp[i][j] = Math.Min(dp[i - 1][j], dp[i][j - 1]) + grid[i][j];
                }
            }
            return dp[y - 1][x - 1];
        }
        #endregion
    }
}
