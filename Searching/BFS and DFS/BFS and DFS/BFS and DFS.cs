using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BFS_and_DFS
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
        #region Leetcode 662  Maximum Width of Binary Tree

        public int WidthOfBinaryTree(TreeNode root)
        {
            List<(TreeNode, int)> level = new List<(TreeNode, int)>();
            level.Add((root, 0));
            int ans = 1;
            while (level.Count > 0)
            {
                ans = Math.Max(ans, -level[0].Item2 + level[level.Count - 1].Item2 + 1);
                List<(TreeNode, int)> cur = new List<(TreeNode, int)>();
                foreach (var item in level)
                {
                    TreeNode left = item.Item1.left;
                    TreeNode right = item.Item1.right;
                    int d = item.Item2;
                    if (left != null)
                    {
                        cur.Add((left, d * 2));
                    }
                    if (right != null)
                    {
                        cur.Add((right, d * 2 + 1));
                    }
                }
                level = cur;
            }
            return ans;
        }

        #endregion
        #region Leetcode 797  All Paths From Source to Target
        public IList<IList<int>> AllPathsSourceTarget(int[][] graph)
        {
            APTfind(0, graph, new List<int>());
            return APTans;
        }
        IList<IList<int>> APTans = new List<IList<int>>();
        public void APTfind(int cur, int[][] graph, List<int> path)
        {
            path.Add(cur);
            if (cur == graph.Length - 1)
            {
                APTans.Add(new List<int>(path));
            }

            foreach (int next in graph[cur])
            {
                APTfind(next, graph, path);
            }
            path.RemoveAt(path.Count - 1);
        }

        #endregion
        #region Leetcode 78  Subsets
        public IList<IList<int>> Subsets(int[] nums)
        {
            int n = nums.Length;
            for (int i = 0; i <= n; ++i)
            { //We try out every possible length of our subset
                IList<int> cur = new List<int>();
                Subset_dfs(0, ref cur, nums, i);
            }
            return Subset_ans;
        }
        IList<IList<int>> Subset_ans = new List<IList<int>>();
        public void Subset_dfs(int index, ref IList<int> path, int[] nums, int k)
        {
            if (path.Count == k)
            {
                // We have reached our destination
                Subset_ans.Add(new List<int>(path));
                return;
            }
            for (int i = index; i < nums.Length; ++i)
            {
                // We try every combination by letting it add all the number that is before it 
                path.Add(nums[i]);
                Subset_dfs(i + 1, ref path, nums, k);
                path.RemoveAt(path.Count - 1);
                // We reverse the previous action
            }

        }
        #region Extension: Find all the subarrays in an array
        public IList<IList<int>> Subarrays(int[] nums)
        {
            int n = nums.Length;
            for (int i = 1; i <= n; i++) // Enumerate every possible length
            {
                for (int index = 0; index <= n-i; index++) // Enumerate every possible starting point
                {
                    IList<int> cur = new List<int>();
                    Subarray_dfs(i, nums, ref cur, index);
                }
            }
            return Subarray_ans;
        }
        public IList<IList<int>> Subarray_ans = new List<IList<int>>();
        public void Subarray_dfs(int k, int[]nums,ref IList<int> path,int index)
        {
            if(path.Count == k)
            {
                Subarray_ans.Add(path);
                return;
            }
            path.Add(nums[index]);
            Subarray_dfs(k, nums, ref path, index + 1);
        }
        #endregion
        #endregion
        #region Leetcode 987  Vertical Order Traversal of a Binary Tree
        public IList<IList<int>> VerticalTraversal(TreeNode root)
        {
            Queue<(int, int, TreeNode)> q = new Queue<(int, int, TreeNode)>();
            List<(int, int, int)> info = new List<(int, int, int)>();
            // (x, y, node value)
            q.Enqueue((0, 0, root));
            while (q.Count != 0)
            {
                var cur = q.Dequeue();
                int x = cur.Item1;
                int y = cur.Item2;
                TreeNode node = cur.Item3;
                info.Add((x, y, node.val));
                if (node.left != null)
                {
                    q.Enqueue((x - 1, y + 1, node.left));
                }
                if (node.right != null)
                {
                    q.Enqueue((x + 1, y + 1, node.right));
                }

            }
            info.Sort();
            IList<IList<int>> ans = new List<IList<int>>();
            int index = 0;
            while (index < info.Count)
            {
                IList<int> level = new List<int>();
                int cur_level = info[index].Item1;
                while (index < info.Count && cur_level == info[index].Item1)
                {
                    level.Add(info[index++].Item3);
                }
                ans.Add(level);
            }
            return ans;
        }

        #endregion
    }
}
