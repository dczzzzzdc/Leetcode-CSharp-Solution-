using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Security.Cryptography;
using System.Threading.Tasks.Dataflow;

namespace DP_Path_Problems
{
    class Program
    {
        static void Main(string[] args)
        {

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
        #region Leetcode 72  Edit Distance
        // Our goal is edit both words until one of their length is zero
        public int MinDistance(string w1, string w2)
        {
            word1 = w1;
            word2 = w2;
            int l1 = w1.Length;
            int l2 = w2.Length;
            MEDdp = new int[l1 + 1][];
            // d[i][j] stores the Minimum Edit Distance from word1[0-i] to word2[0-j]
            for (int i = 0; i < l1+1; i++)
            {
                MEDdp[i] = new int[l2 + 1];
                Array.Fill(MEDdp[i], -1);
            }
            return MEDfind(l1, l2);

        }
        int[][] MEDdp;
        string word1;
        string word2;
        public int MEDfind(int i1, int i2) // These two params indicates the length of the remaining word
        {
            if(i1 == 0) 
            {
                return i2;
            }
            else if (i2 == 0)
            {
                return i1;
            }
            // We only need the (remaining length of another word) steps by deleting
            else if (MEDdp[i1][i2]!= -1)
            {
                return MEDdp[i1][i2];
            }
            int result = 0;
            if(word1[i1-1] == word2[i2 - 1])// If the next character is the same, then we can skip it
            {
                result = MEDfind(i1 - 1, i2 - 1);
            }
            else
            {
                result = Math.Min(Math.Min(MEDfind(i1 - 1, i2 - 1), MEDfind(i1 - 1, i2)), MEDfind(i1, i2 - 1));
                // Replace: If we replace a character, the next character is the same. Then we can skip it
                // Delete : If we delete a character in word1, then  we can skip a character in word1
                // Insert：If we insert a character into word1, then we can skip a character in word2
                // while the length of word1 remains the same(skip one insert one)
            }
            return MEDdp[i1][i2] = result;
        }
        #endregion
        #region Leetcode 79  Word Seach
        public bool Exist(char[][] board, string word)
        {
            int n = board.Length;
            if (n == 0) { return false; }
            int m = board[0].Length;
            char[] search = word.ToCharArray();
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < m; ++j)
                {
                    if (board[i][j] == search[0])
                    {
                        if (Search(j, i, 0, search, ref board))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool Search(int x, int y, int index, char[] search, ref char[][] board)
        {
            if (y < 0 || y >= board.Length || x < 0 || x >= board[0].Length || board[y][x] != search[index])
            {
                return false;
            }
            else if (index == search.Length - 1) { return true; } // We successfully searched through the entire word
            else
            {
                char cur = board[y][x];
                board[y][x] = '$'; // Mark as visited
                bool result = Search(x + 1, y, index + 1, search, ref board) || 
                    Search(x - 1, y, index + 1, search, ref board) || 
                    Search(x, y - 1, index + 1, search, ref board) || 
                    Search(x, y + 1, index + 1, search, ref board);
                board[y][x] = cur; // Reset to default;
                return result;
            }
        }
        #endregion
        #region Leetcode 741  Cherry Pickup
        public int CherryPickup(int[][] grid)
        {
            int n = grid.Length;
            CPdp = new int[n][][];
            for (int i = 0; i < n; i++)
            {
                CPdp[i] = new int[n][];
                for (int j = 0; j < n; j++)
                {
                    CPdp[i][j] = new int[n];
                    Array.Fill(CPdp[i][j], int.MinValue);
                }
            }
            return Math.Max(0, CPfind(n - 1, n - 1, n - 1, grid));
        }
        int[][][] CPdp;
        public int CPfind(int x1, int y1, int x2, int[][] grid)
        {
            int y2 = x1 + y1 - x2;
            // They started at the same pointer and move same steps
            if (y2 < 0 || y1 < 0 || x1 < 0 || x2 < 0)
            // These two figures started from bottom right and they can only move up or left
            {
                return -1;
            }
            else if (grid[y1][x1] == -1 || grid[y2][x2] == -1)
            // They all met an obstacle
            {
                return -1;
            }
            else if (x1 == 0 && y1 == 0) // They reached an end
            {
                return grid[0][0];
                // Return the value itself because the end might be an obstacle,cherry or nothing
            }
            else if (CPdp[x1][y1][x2] != int.MinValue)
            {
                return CPdp[x1][y1][x2];
            }
            int res = Math.Max(Math.Max(CPfind(x1 - 1, y1, x2 - 1, grid), CPfind(x1 - 1, y1, x2, grid)),
                Math.Max(CPfind(x1, y1 - 1, x2 - 1, grid), CPfind(x1, y1 - 1, x2, grid)));
            if (res < 0)
            // All paths have been blocked
            {
                return CPdp[x1][y1][x2] = -1;
            }
            res += grid[y1][x1];
            if (x1 != x2)
            // Note that the same cherry cannot be picked up twice
            {
                res += grid[y2][x2];
            }
            return CPdp[x1][y1][x2] = res;
        }
        #endregion
    }
}
