//#region << 版 本 注 释 >>
///*----------------------------------------------------------------
//* 项目名称 ：MicroService.Data.KetamaHash
//* 项目描述 ：
//* 类 名 称 ：KetamaNodeLocatorList
//* 类 描 述 ：
//* 命名空间 ：MicroService.Data.KetamaHash
//* CLR 版本 ：4.0.30319.42000
//* 作    者 ：jinyu
//* 创建时间 ：2018
//* 更新时间 ：2018
//* 版 本 号 ：v1.0.0.0
//*******************************************************************
//* Copyright @ jinyu 2018. All rights reserved.
//*******************************************************************
////----------------------------------------------------------------*/
//#endregion


//using MicroService.Data.KetamaHash.MurmurHash;
//using MicroService.Data.KetamaHash.NodeList;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace MicroService.Data.KetamaHash
//{
//    /* ============================================================================== 
//    * 功能描述：KetamaNodeLocatorList 带虚拟节点SortedList实现
//    * 创 建 者：jinyu 
//    * 修 改 者：jinyu 
//    * 创建日期：2018 
//    * 修改日期：2018 
//    * ==============================================================================*/

//    public class KetamaNodeLocatorList:IKetamaHash
//    {
//       private IDictionary<ulong, StoreNode> ketamaNodes = null;
      
//        private int numReps = 160;

//        public KetamaNodeLocatorList()
//        {
//            // SortedDictionary<long, StoreNode>;
//            ketamaNodes = new SortedList<ulong, StoreNode>(0);
//        }

//        public void AddNode(List<StoreNode> nodes, int nodeCopies = 0)
//        {
          
            
//            if (nodeCopies != 0)
//            {
//                numReps = nodeCopies;
//            }
//            ketamaNodes = new SortedList<ulong, StoreNode>(numReps * nodes.Count);
//            //对所有节点，生成nCopies个虚拟结点
//            foreach (StoreNode node in nodes)
//            {
//                //每四个虚拟结点为一组
//                for (int i = 0; i < numReps / 4; i++)
//                {
//                    //getKeyForNode方法为这组虚拟结点得到惟一名称 

//                    // byte[] digest = Murmur3_64.StringToHashValue(node + i);
//                    /** Md5是一个16字节长度的数组，将16字节的数组每四个字节一组，分别对应一个虚拟结点，这就是为什么上面把虚拟结点四个划分一组的原因*/

//                    byte[] digest = MurmurHashFactory.ComputeMurmur(node.ToString() + i);
//                    for (int h = 0; h < 4; h++)
//                    {
//                        ulong m = MurmurHashFactory.Hash(digest, h);
//                        // ketamaNodes[m] = node;
//                        ketamaNodes.Add(m, node);
//                    }
//                }
//            }
//        }

//        public void AddNode(StoreNode node)
//        {
//            List<ulong> lst = new List<ulong>();
//            for (int i = 0; i < numReps / 4; i++)
//            {
//                //getKeyForNode方法为这组虚拟结点得到惟一名称 
//                /** Md5是一个16字节长度的数组，将16字节的数组每四个字节一组，分别对应一个虚拟结点，这就是为什么上面把虚拟结点四个划分一组的原因*/
//                try
//                {
//                    byte[] digest = MurmurHashFactory.ComputeMurmur(node.ToString() + i);
//                    for (int h = 0; h < 4; h++)
//                    {
//                        ulong m = MurmurHashFactory.Hash(digest, h);
//                        ketamaNodes.Add(m, node);
//                        lst.Add(m);
//                    }
//                }
//                catch
//                {
                    
//                    i--;
//                }
//            }
//        }

//        public StoreNode GetPrimary(string k)
//        {
//            Murmur128 murmur = MurmurHashFactory.MurmurHash.Create128((uint)Environment.TickCount);
//            byte[] digest = murmur.ComputeHash(Encoding.UTF8.GetBytes(k));
//            StoreNode rv = GetNodeForKey(MurmurHashFactory.Hash(digest, 0));
//            return rv;
//        }

//        public StoreNode GetPrimary(byte[] k)
//        {
//            throw new NotImplementedException();
//        }

//        public void Print()
//        {
//            throw new NotImplementedException();
//        }

//        public void Remove(StoreNode node)
//        {
//            List<ulong> keys = new List<ulong>(1000);
//            foreach(var kv in ketamaNodes)
//            {
//                 if(node==kv.Value)
//                {
//                    keys.Add(kv.Key);
//                }
//            }
//            foreach(ulong key in keys)
//            {
//                ketamaNodes.Remove(key);
//            }
//        }

//        public double Test()
//        {
//            throw new NotImplementedException();
//        }

//        StoreNode GetNodeForKey(ulong hash)
//        {
//            StoreNode rv =null;
//            ulong key = hash;

//            //如果找到这个节点，直接取节点，返回   
//            if (!ketamaNodes.ContainsKey(key))
//            {
//                //得到大于当前key的那个子Map，然后从中取出第一个key，就是大于且离它最近的那个key 说明详见: http://www.javaeye.com/topic/684087
//                var tailMap = from coll in ketamaNodes
//                              where coll.Key > hash
//                              select new { coll.Key };
//                if (tailMap == null || tailMap.Count() == 0)
//                    key = ketamaNodes.FirstOrDefault().Key;
//                else
//                    key = tailMap.FirstOrDefault().Key;
//                // var tialMap = ketamaNodes.AsParallel().Where((kv) => { if (kv.Key > hash) { return true; }; return false; });
//                // if(tialMap==null||tialMap.Count()==0)
//                // {
//                //     key = ketamaNodes.FirstOrDefault().Key;
//                // }
//                //else
//                // {
//                //     key = tialMap.AsParallel().AsOrdered().FirstOrDefault().Key;
//                // }

//            }
//            rv = ketamaNodes[key];
//            return rv;
           
//        }
//    }
//}
