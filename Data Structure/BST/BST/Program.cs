using System;
using System.ComponentModel.Design.Serialization;

namespace BST
{
    public class TreeNode
    {
        public int val;
        public TreeNode right;
        public TreeNode left;
        
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
            // Basic Property of a BST
            
            // 0. left children smaller than the root and right children bigger than the root
            // 1. The height of the tree: Log(Number of nodes)
        }
        #region Leetcode 450  Delete Node in a BST
        public TreeNode DeleteNode(TreeNode root, int key)
        {
            if (root == null) return root;

            else if (root.val < key) { root.right = DeleteNode(root.right, key); }
            else if (root.val > key) { root.left = DeleteNode(root.left, key); }

            else
            {
                if (root.left == null && root.right == null) return null;

                else if (root.left == null) return root.right;

                else if (root.right == null) return root.left;

                root.val = getMinValNode(root.right);
                root.right = DeleteNode(root.right, root.val);
            }
            return root;
        }
        public int getMinValNode(TreeNode root)
        {
            int min = root.val;
            while (root != null)
            {
                min = root.val;
                root = root.left;
            }
            return min;
        }
        #endregion
    }
}
