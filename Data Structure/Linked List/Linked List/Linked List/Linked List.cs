using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

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
        public Node prev;
        public Node next;
        public Node child;
        public Node random;
        public Node(int _val)
        {
            val = _val;
            next = null;

        }
       
    }
    #endregion
    class Program
    {
        static void Main(string[] args)
        {
        }
        #region Leetcode 206  Reverse Linked List
        public ListNode ReverseList(ListNode head)
        {
            ListNode nHead = null;
            while (head != null)
            {
                // For example, nHead = 1, head = 2->3
                ListNode next = head.next;
                // next = 3
                head.next = nHead;
                // head = 2->1
                nHead = head;
                // nHead = 2->1
                head = next;
                // head = 3
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
        #region Leetcode 23  Merge k Sorted List
        // We can put all the node into a list and then sort it
        // However, optimally, we can use divide and conquer
        public ListNode MergeKLists(ListNode[] lists)
        {
            return Merge(lists, 0, lists.Length - 1);
        }

        public ListNode Merge(ListNode[] lists, int L, int R)
        {
            if (L == R) return lists[L];
            if (L < R)
            {
                int M = (L + R) / 2;
                ListNode l1 = Merge(lists, L, M);
                ListNode l2 = Merge(lists, M + 1, R);
                return MergeTwoLists(l1, l2);
            }
            else
            {
                return null;
            }
                
        }
        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            ListNode head = new ListNode(0);
            ListNode prev = head;
            while (l1 != null && l2 != null)
            {
                if (l2.val > l1.val)
                {
                    prev.next = l1; // Connect
                    l1 = l1.next; // Keep the traverse
                }
                else
                {
                    prev.next = l2;
                    l2 = l2.next;
                }
                // Move the prev pointer forward
                prev = prev.next;
            }
            // If any of them is not used up
            // then we put them behind the already sorted array, since both of them are sorted
            if (l1 != null)
            {
                prev.next = l1;
            }
            else if (l2 != null)
            {
                prev.next = l2;
            }
            return head.next;
        }
        #endregion
        #region Leetcode 430  Flatten a Multilevel Doubly Linked List
        public Node Flatten(Node head)
        {
            if (head == null) { return head; }
            Stack<Node> nodes = new Stack<Node>();
            Node cur = head; // Setup the pointer
            while (cur != null)
            {
                if (cur.child != null)
                {
                    if (cur.next != null)
                    {
                        nodes.Push(cur.next);
                    }
                    cur.next = cur.child;
                    cur.next.prev = cur;
                    cur.child = null;
                }
                else if (cur.next == null && nodes.Count > 0)
                // We have reached an end here but we still has to put all the stuff in the stack to tail of the linked list
                {
                    cur.next = nodes.Pop();
                    cur.next.prev = cur;
                }
                cur = cur.next;
            }
            return head;
            #region Explanation for Leetcode 430
            /*
             * Very Very Useful Literal Explanation
             * 1---2---3---4---5---6--NULL
                       |
                        7---8---9---10--NULL
                            |
                            11--12--NULL
             * The pointer keeps traversing until it meets 3, which has a child
             * Therefore, we put the rest of the nodes on the first level in the stack [4---5---6--NULL]
             * And now our pointer is on 7 because we set cur.next to be its child
             * The pointer keeps traversing until it meets 8
             * We the rest of the node on the second level in the stack [4---5---6--NULL] + [9---10--NULL]
             * The pointer is one 11 and it keeps traversing until it meets null
             * Therefore, we put all the things in the stack at the tail of the linked list
             */
            #endregion
            
        }
        #endregion
        #region Leetcode 2  Adding two numbers
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode dummy = new ListNode(0);
            ListNode tail = dummy;
            int sum = 0;
            while(l1!= null || l2 != null || sum != 0)
            {
                sum += ((l1 == null) ? 0 : l1.val) + ((l2 == null) ? 0 : l2.val);
                tail.next = new ListNode(sum % 10);
                tail = tail.next;
                if(l1 != null) { l1 = l1.next; }
                if(l2 != null) { l2 = l2.next; }
                sum /= 10;
            }
            return dummy.next;
        }
        #endregion
        #region Leetcode 143  Reorder List
        public void ReorderList(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return;
            }
            // The splitting point of the linked list
            ListNode mid = FindMiddleNode(head);
            // The head of the second half linked list
            ListNode l2 = mid.next;
            mid.next = null;
            l2 = ReverseLinkedList(l2);
            // The head of the first half linked list 
            ListNode l1 = head;

            while (l1 != null && l2 != null)
            {
                // For example, l1 = 2->3->4 and l2 = 9->10
                ListNode next = l1.next;
                // next = 3->4
                l1.next = l2;
                // l1 = 2->9->10
                l2 = l2.next;
                // l2 = 10
                l1.next.next = next;
                // l1 = 2->9->3->4
                l1 = next;
            }
        }
        public ListNode FindMiddleNode(ListNode head)
        {
            ListNode fast = head;
            ListNode slow = head;
            while (fast != null && slow != null && fast.next != null)
            {
                fast = fast.next.next;
                slow = slow.next;
            }
            return slow;
        }
        public ListNode ReverseLinkedList(ListNode head)
        {
            ListNode nHead = null;
            while (head != null)
            {
                ListNode next = head.next;
                head.next = nHead;
                nHead = head;
                head = next;
            }
            return nHead;
        }
        #endregion
    }
}
