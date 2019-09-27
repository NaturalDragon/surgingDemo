///<summary>
/// The RedBlackNode class encapsulates a node in the tree
///</summary>

using System;
using System.Text;

namespace MicroService.Data.KetamaHash.RedBlackTree
{
	public class RedBlackNode<TKey,TValue> where TKey: IComparable
    {
        // tree node colors
		public static int	RED		= 0;
		public static int	BLACK	= 1;

		// key provided by the calling class
		private TKey ordKey;
		// the data or value associated with the key
		private TValue objData;
		// color - used to balance the tree
		private int intColor;
		// left node 
		private RedBlackNode<TKey, TValue> rbnLeft;
		// right node 
		private RedBlackNode<TKey, TValue> rbnRight;
        // parent node 
        private RedBlackNode<TKey, TValue> rbnParent;
		
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
				return objData;
			}
			
			set
			{
				objData = value;
			}
		}
		///<summary>
		///Color
		///</summary>
		public int Color
		{
			get
            {
				return intColor;
			}
			
			set
			{
				intColor = value;
			}
		}
		///<summary>
		///Left
		///</summary>
		public RedBlackNode<TKey, TValue> Left
		{
			get
            {
				return rbnLeft;
			}
			
			set
			{
				rbnLeft = value;
			}
		}
		///<summary>
		/// Right
		///</summary>
		public RedBlackNode<TKey, TValue> Right
		{
			get
            {
				return rbnRight;
			}
			
			set
			{
				rbnRight = value;
			}
		}
        public RedBlackNode<TKey, TValue> Parent
        {
            get
            {
                return rbnParent;
            }
			
            set
            {
                rbnParent = value;
            }
        }

		public RedBlackNode()
		{
			Color = RED;
		}
	}
}
