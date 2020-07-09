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
        #region Leetcode 1340  Jump Game V
        public int MaxJumps(int[] arr, int d)
        {
            int n = arr.Length;
            max = d;
            // This represents the largest distance the figure can jump to
            mjdp = new int[n];
            int ans = 0;
            for (int i = 0; i < n; i++) // We find every starting point and then call the dp function
            {
                ans = Math.Max(ans, MJFind(i, arr));
            }
            return ans;
        }
        int max;
        int[] mjdp;
        public int MJFind(int index, int[] nums)
        {
            if (mjdp[index] != 0) { return mjdp[index]; }// If already calculated
            int ans = 1; // It is set to one because doing nothing is considered one step
            for (int i = index + 1; i < Math.Min(nums.Length, index + max + 1) && nums[index] > nums[i]/*Making sure that the player can cross*/; i++)
            // We first look at the right side of the index
            // This loop must also make sure that the jumping range do not surpass index + max or index - max
            {
                ans = Math.Max(ans, MJFind(i, nums) + 1);
                // Adding one meaning making a move
            }
            for (int i = index - 1; i >= Math.Max(0, index - max) && nums[index] > nums[i]; i--)
            // We then search the left side of the index
            {
                ans = Math.Max(ans, MJFind(i, nums) + 1);
            }
            return mjdp[index] = ans;
        }
        #endregion
        #region Leetcode 576  Out of Boundary Paths
        public static int[][][] OBdp;
        public const int mod = 1000000007;
        private int Find(int x, int y, int k, int m, int n)
        {
            if (x < 0 || y < 0 || y >= n || x >= m) { return 1; } // We successfully reached the boundary
            else if (OBdp[x][y][k] != -1) { return OBdp[x][y][k] % mod; }// We already calculated it 
            else if (k == 0) { return 0; } // We used all of our moves
            // We extend to four ways
            int tempVal = 0;
            tempVal = (tempVal + Find(x, y - 1, k - 1, m, n)) % mod;
            tempVal = (tempVal + Find(x - 1, y, k - 1, m, n)) % mod;
            tempVal = (tempVal + Find(x + 1, y, k - 1, m, n)) % mod;
            tempVal = (tempVal + Find(x, y + 1, k - 1, m, n)) % mod;
            OBdp[x][y][k] = tempVal;
            return tempVal % mod;
        }
        public int FindPaths(int m, int n, int N, int x, int y)
        {
            // Here m is the x boundary
            // And n is the y boundary
            // There is a little problem with the description of the second example
            OBdp = new int[m][][];
            for (int i = 0; i < m; ++i)
            {
                OBdp[i] = new int[n][];
                for (int j = 0; j < n; ++j)
                {
                    OBdp[i][j] = new int[N + 1];
                    Array.Fill(OBdp[i][j], -1);
                }
            }
            return Find(x, y, N, m, n);
        }
        #endregion
    }
}
