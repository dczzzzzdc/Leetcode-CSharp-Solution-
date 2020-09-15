using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;

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
    public class Node
    {
        public int val;
        public Node next;
        public Node left;
        public Node right;

        public Node(int val = 0, Node next = null, Node left = null, Node right = null)
        {
            this.val = val;
            this.next = next;
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
            // 2. When trying to find the path to a label, start tracking from the label and go from the bottom
            // 3. level_max = Math.Pow(2,level) - 1; level_min = Math.Pow(2,level- 1)
            // 4. Label is on the Math.Log(label,2) + 1 level
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

        #region Leetcode 1104  Path In Zigzag Labelled Binary Tree
        public IList<int> PathInZigZagTree(int label)
        {
            List<int> ans = new List<int>();
            int level = (int)Math.Log(label, 2) + 1;
            while (label > 0)
            // Go from the label to the root
            {
                ans.Add(label);
                int level_max = (int)Math.Pow(2, level) - 1;
                int level_min = (int)Math.Pow(2, level - 1);
                label = level_max + level_min - label;
                label /= 2;
                level--;
            }
            ans.Reverse();
            return new List<int>(ans);
        }
        #endregion
        #region Leetcode 226  Invert Binary Tree
        // Simply swap the left children with the right one
        public TreeNode InvertTree(TreeNode root)
        {
            if (root == null)
            {
                return null;
            }

            TreeNode temp = root.left;
            root.left = root.right;
            root.right = temp;

            InvertTree(root.left);
            InvertTree(root.right);

            return root;
        }
        #endregion
        #region Leetcode 116  Populating Next Right Pointers in Each Node
        public Node Connect(Node root)
        {
            if (root == null)
            {
                return null;
            }
            ConnectTwoNodes(root.left, root.right);
            return root;
        }
        public void ConnectTwoNodes(Node root1, Node root2)
        {
            if (root2 == null || root1 == null)
            {
                return;
            }

            root1.next = root2;

            // Connect two nodes under the same parent
            ConnectTwoNodes(root1.left, root1.right);
            ConnectTwoNodes(root2.left, root2.right);

            // Connect the bordering nodes under different parents
            ConnectTwoNodes(root1.right, root2.left);
        }
        #endregion
    }
}
