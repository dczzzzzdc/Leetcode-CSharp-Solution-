using System;
using System.Collections.Generic;

namespace Linked_List
{
    #region Node Classes
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
    public class Node
    {
        public int val;
        public Node next;
        public Node random;

        public Node(int _val)
        {
            val = _val;
            next = null;
            random = null;
        }
    }
    #endregion
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        #region Leetcode 206  Reverse Linked List
        public ListNode ReverseList(ListNode head)
        {
            ListNode nHead = null;
            while (head != null)
            {
                // We have to store its value, because it is going to be changed
                ListNode next = head.next;
                // Reverse
                // For example, in 2->3 and 1
                // If we want to move 2 into the new list, we have to make its "next" 1
                head.next = nHead;
                // We shift nHead and head
                nHead = head;
                head = next;
            }
            return nHead;
        }
        #endregion
        #region Leetcode 92  Reverse Linked List II
        public ListNode ReverseBetween(ListNode head, int m, int n)
        {
            int count = n - m + 1;
            ListNode prehead = null;
            ListNode result = head;
            while (head != null && m > 1) // We push head forward for m-1 units
            {
                prehead = head; // Record the predescor of head
                head = head.next;
                --m;
            }
            // After the reverse, nhead becomes the head of the new linked list
            // head becomes the tail of the new linked list
            ListNode nListTail = head;
            ListNode nHead = null;
            while (head != null && count > 0) // We reverse count amount of nodes 
            {
                // Normal Reversing Process
                ListNode next = head.next;
                head.next = nHead;
                nHead = head;
                head = next;
                --count;
            }
            nListTail.next = head; // We connect the post-tail with the current tail
            if (prehead != null)// If the prehead is not null, then we did not start from the first element
            // As a reuslt, we can just connect the pre-head with the current head
            {
                prehead.next = nHead;
            }
            else
            // If it do start from the first element,
            // then the result is actually the new head
            {
                result = nHead;
            }
            return result;
        }
        #endregion
        #region  Leetcode 160  Intersection of Two Linked Lists
        public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
        {
            int len1 = GetLength(headA);
            int len2 = GetLength(headB);
            if (len1 > len2)
            {
                Forward(len1 - len2, ref headA);
            }
            else
            {
                Forward(len2 - len1, ref headB);
            }
            while (headA != null && headB != null)
            {
                if (headA == headB)
                {
                    return headA;
                }
                headB = headB.next;
                headA = headA.next;
            }
            return null;
        }
        public int GetLength(ListNode head)
        {
            int len = 0;
            while (head != null)
            {
                ++len;
                head = head.next;
            }
            return len;
        }
        public void Forward(int delta, ref ListNode head)
        {
            while (delta > 0 && head != null)
            {
                head = head.next;
                --delta;
            }

        }
        #endregion
        #region Leetcode 141/142  Linked List Cycle
        //Detect Cycle using Fast&Slow Pointer
        public ListNode DetectCycle(ListNode head)
        {
            ListNode fast = head;
            ListNode slow = head;
            ListNode meet = null;
            while (fast != null) // We cycle until they meet
            {
                slow = slow.next;
                fast = fast.next;
                if (fast != null) { return null; }
                fast = fast.next;
                if (fast == slow) // Two Pointers meet each other
                {
                    meet = fast;
                    break;
                }
            }
            if (meet == null) { return null; } // They did not meet each other
            while (head != null && meet != null) // When head and meet, we find the starting point of the cycle
            // using math
            {
                if (head == fast) { return head; }
                meet = meet.next;
                head = head.next;
            }
            return null;
        }
        #endregion
        #region Leetcode 86  Partition List
        public ListNode Partition(ListNode head, int x)
        {
            ListNode more_head = new ListNode(0, null);
            ListNode less_head = new ListNode(0, null);
            // These build the List that start with a null
            ListNode more = more_head;
            ListNode less = less_head;
            while (head != null) // Traverse through the entire linked list
            {
                if (head.val >= x)
                {
                    more.next = head; // We put head in the more linked list
                    more = head; // Make more linked list's pointer pointing to the head, which is the last node
                }
                else
                {
                    less.next = head;
                    less = head;
                }
                head = head.next;
            }
            less.next = more_head.next; // We trim the first null of the more head
            more.next = null; // We trim the more linked list
            return less_head.next; // We also need to cut the first element, which is null, of the less link
        }
        #endregion
        #region Leetcode 138  Copy List with Random Pointers
        public Node CopyRandomList(Node head)
        {
            if (head == null)
            {
                return null;
            }
            Dictionary<Node, int> position = new Dictionary<Node, int>();
            //This stores the position of every node in the original linked list
            Node temp = head;
            List<Node> nodemap = new List<Node>();
            // nodemap[i] stores the node that the ith node is pointing to
            int index = 0;
            while (temp != null)
            {
                nodemap.Add(new Node(temp.val));
                position.Add(temp, index);
                temp = temp.next; // Traverse through the whole linked list
                ++index;
            }
            int length = index;
            temp = head;
            index = 0;
            while (temp != null)
            {
                if (index < length - 1)
                {
                    nodemap[index].next = nodemap[index + 1];
                    //We first connect the normal pointers 
                }
                if (temp.random != null) // If it do have a random pointer
                {
                    int id = position[temp.random]; // We get the id of it random pointer
                    nodemap[index].random = nodemap[id];
                }
                temp = temp.next;
                ++index;
            }
            return nodemap[0];
        }
        #endregion
    }
}
