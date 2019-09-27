

using MicroService.Data.KetamaHash.TencentYoutu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
* 命名空间: MicroService.Data.KetamaHash 
* 类 名： HashNode
* 版本 ：v1.0
* Copyright (c) year 
*/

namespace MicroService.Data.KetamaHash
{
    /// <summary>
    /// 功能描述    ：HashRingNode  选取节点
    /// 创 建 者    ：jinyu
    /// 创建日期    ：2018/10/12 15:55:07 
    /// 最后修改者  ：jinyu
    /// 最后修改日期：2018/10/12 15:55:07 
    /// </summary>
    public class ConsistencyRing
    {

        static Random rand = new Random(Environment.TickCount);

        /** key's count */
        private const int EXE_TIMES = 100000;

        private const int NODE_COUNT = 4;

        private const int VIRTUAL_NODE_COUNT = 160;


      private IKetamaHash locator = null;

       
       
       
        /**
         * Gets the mock node by the material parameter
         *  测试使用的
         * @param nodeCount 
         * 		the count of node wanted
         * @return
         * 		the node list
         */
        private static List<StoreNode> GetNodes(int nodeCount)
        {
            List<StoreNode> nodes = new List<StoreNode>();
            var configs= Configuration.ConfigManager.GetEntity<List<Config>>("ServiceConfig");
            for (int k = 1; k <= configs.Count(); k++)
            {
                StoreNode node = new StoreNode() { Name = "Node", IP = "192.168.3." + (k+100), Port = 7123 + k,
                Config=configs[k-1]};
                nodes.Add(node);
            }
           
            return nodes;
        }

        /**
         *	All the keys	
         */
        private static List<string> GetAllStrings()
        {
            List<string> allStrings = new List<string>(EXE_TIMES);
            for (int i = 0; i < EXE_TIMES; i++)
            {
                allStrings.Add(GenerateRandomString(rand.Next(200)));
            }

            return allStrings;
        }

        /// <summary>
        /// 随机产生一个key
        /// </summary>
        /// <returns></returns>
        public string GetCurrentKey()
        {
            return GenerateRandomString(50);
        }

        /**
         * To generate the random string by the random algorithm
         * <br>
         * The char between 32 and 127 is normal char
         * 
         * @param length
         * @return
         */
        private static string GenerateRandomString(int length)
        {
           
            StringBuilder sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                sb.Append((char)(rand.Next(95) + 32));
            }
            return sb.ToString();
        }

        /// <summary>
      /// 添加真实节点
      /// </summary>
      /// <param name="nodes"></param>
        public void AddNode(StoreNode[] nodes=null)
        {
            // allKeys = GetAllStrings();
            List<StoreNode> lstNodes = null;
            if (nodes != null)
            {
                lstNodes = nodes.ToList();

            }
            else
            {
                //测试代码
                lstNodes = GetNodes(NODE_COUNT);
            }
             locator = new KetamaNodeLocator();
            //
            locator.AddNode(lstNodes, VIRTUAL_NODE_COUNT);
           
        }

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public StoreNode GetStoreNode(string key)
        {
            return locator.GetPrimary(key);
        }

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public StoreNode GetStoreNode(byte[]key)
        {
            return locator.GetPrimary(key);
        }

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <returns></returns>
        public StoreNode GetCurrent()
        {
            if(locator==null)
            {
                AddNode(null);
            }
            return GetStoreNode(GetCurrentKey());
        }

    }
}
