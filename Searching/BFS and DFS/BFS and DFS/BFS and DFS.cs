﻿using System;
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
            Console.WriteLine("Hello World!");
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
    }
}
