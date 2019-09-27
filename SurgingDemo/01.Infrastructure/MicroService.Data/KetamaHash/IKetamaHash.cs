using System.Collections.Generic;

namespace MicroService.Data.KetamaHash
{
    public interface IKetamaHash
    {

        /// <summary>
        /// 初始化添加节点
        /// </summary>
        /// <param name="nodes">真实节点</param>
        /// <param name="nodeCopies">每一个虚拟节点</param>
        void AddNode(List<StoreNode> nodes, int nodeCopies = 0);

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="node"></param>
        void AddNode(StoreNode node);

        /// <summary>
        /// 移除节点
        /// </summary>
        /// <param name="node"></param>
        void Remove(StoreNode node);

       /// <summary>
       /// 获取节点
       /// </summary>
       /// <param name="k"></param>
       /// <returns></returns>
         StoreNode GetPrimary(string k);

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        StoreNode GetPrimary(byte[] k);


    }
}
