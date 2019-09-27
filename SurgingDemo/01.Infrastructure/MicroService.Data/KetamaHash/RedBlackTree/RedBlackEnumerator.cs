///<summary>
/// The RedBlackEnumerator class returns the keys or data objects of the treap in
/// sorted order. 
///</summary>
using System;
using System.Collections;
	
namespace MicroService.Data.KetamaHash.RedBlackTree
{
	public class RedBlackEnumerator<TKey, TValue>where TKey:IComparable
    {
		// the treap uses the stack to order the nodes
		private Stack stack;
		// return the keys
		private bool keys;
		// return in ascending order (true) or descending (false)
		private bool ascending;
		
		// key
		private TKey ordKey;
		// the data or value associated with the key
		private TValue objValue;

        public  string  Color;             // testing only, don't use in live system
        public  TKey parentKey;     // testing only, don't use in live system
		
		///<summary>
		///Key
		///</summary>
		public TKey Key
		{
			get
            {
				return ordKey;
			}
			
			set
			{
				ordKey = value;
			}
		}
		///<summary>
		///Data
		///</summary>
		public TValue Value
		{
			get
            {
				return objValue;
			}
			
			set
			{
				objValue = value;
			}
		}
		
		public RedBlackEnumerator() 
        {
		}
		///<summary>
		/// Determine order, walk the tree and push the nodes onto the stack
		///</summary>
		public RedBlackEnumerator(RedBlackNode<TKey, TValue> tnode, bool keys, bool ascending) 
        {
			
			stack           = new Stack();
			this.keys       = keys;
			this.ascending  = ascending;
			
            // use depth-first traversal to push nodes into stack
            // the lowest node will be at the top of the stack
            if(ascending)
			{   // find the lowest node
				while(tnode != RedBlack<TKey, TValue>.sentinelNode)
				{
					stack.Push(tnode);
					tnode = tnode.Left;
				}
			}
			else
			{
                // the highest node will be at top of stack
				while(tnode != RedBlack<TKey, TValue>.sentinelNode)
				{
					stack.Push(tnode);
					tnode = tnode.Right;
				}
			}
			
		}
		///<summary>
		/// HasMoreElements
		///</summary>
		public bool HasMoreElements()
		{
			return (stack.Count > 0);
		}
		///<summary>
		/// NextElement
		///</summary>
		public object NextElement()
		{
			if(stack.Count == 0)
				throw(new RedBlackException("Element not found") {  Error= ReadBlackCode.ElementNotFound});
			
			// the top of stack will always have the next item
			// get top of stack but don't remove it as the next nodes in sequence
			// may be pushed onto the top
			// the stack will be popped after all the nodes have been returned
			RedBlackNode<TKey, TValue> node = (RedBlackNode<TKey, TValue>) stack.Peek();	//next node in sequence
			
            if(ascending)
            {
                if(node.Right == RedBlack<TKey, TValue>.sentinelNode)
                {	
                    // yes, top node is lowest node in subtree - pop node off stack 
                    RedBlackNode<TKey, TValue> tn = (RedBlackNode<TKey, TValue>) stack.Pop();
                    // peek at right node's parent 
                    // get rid of it if it has already been used
                    while(HasMoreElements()&& ((RedBlackNode<TKey, TValue>) stack.Peek()).Right == tn)
                        tn = (RedBlackNode<TKey, TValue>) stack.Pop();
                }
                else
                {
                    // find the next items in the sequence
                    // traverse to left; find lowest and push onto stack
                    RedBlackNode<TKey, TValue> tn = node.Right;
                    while(tn != RedBlack<TKey, TValue>.sentinelNode)
                    {
                        stack.Push(tn);
                        tn = tn.Left;
                    }
                }
            }
            else            // descending, same comments as above apply
            {
                if(node.Left == RedBlack<TKey, TValue>.sentinelNode)
                {
                    // walk the tree
                    RedBlackNode<TKey, TValue> tn = (RedBlackNode<TKey, TValue>) stack.Pop();
                    while(HasMoreElements() && ((RedBlackNode<TKey, TValue>)stack.Peek()).Left == tn)
                        tn = (RedBlackNode<TKey, TValue>) stack.Pop();
                }
                else
                {
                    // determine next node in sequence
                    // traverse to left subtree and find greatest node - push onto stack
                    RedBlackNode<TKey, TValue> tn = node.Left;
                    while(tn != RedBlack<TKey, TValue>.sentinelNode)
                    {
                        stack.Push(tn);
                        tn = tn.Right;
                    }
                }
            }
			
			// the following is for .NET compatibility (see MoveNext())
			Key     = node.Key;
			Value   = node.Value;
            // ******** testing only ********
            try
            {
            parentKey = node.Parent.Key;            // testing only
            
            }
            catch(Exception e)
            {
                object o = e;                       // stop compiler from complaining
                parentKey = default(TKey);
            }
			if(node.Color == 0)                     // testing only
                Color = "Red";
            else
                Color = "Black";
            // ******** testing only ********
            if(keys)
            {
                return node.Key;
            }
            else
            {
                return node.Value;
            }
            //return keys == true ? node.Key : node.Data;			
		}
		///<summary>
		/// MoveNext
		/// For .NET compatibility
		///</summary>
		public bool MoveNext()
		{
			if(HasMoreElements())
			{
				NextElement();
				return true;
			}
			return false;
		}
	}
}
