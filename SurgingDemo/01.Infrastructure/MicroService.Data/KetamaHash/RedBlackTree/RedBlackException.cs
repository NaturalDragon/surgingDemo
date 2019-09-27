///<summary>
/// The RedBlackException class distinguishes read black tree exceptions from .NET
/// exceptions. 
///</summary>
using System;

namespace MicroService.Data.KetamaHash.RedBlackTree
{
    public enum ReadBlackCode
    {
        DataNull,
        KeyExists,
        KeyNotExists,
        KeyNull,
        ElementNull,
        ElementNotFound,
        Empty,
        TreeNull
    }
	public class RedBlackException : Exception
	{
        public ReadBlackCode Error
        {
            get;set;
        }
            

        public RedBlackException()
        {
    	}
        public RedBlackException(ReadBlackCode code)
        {
            this.Error = code;
        }
        public RedBlackException(string msg) : base(msg) 
        {
		}			
	}
}
