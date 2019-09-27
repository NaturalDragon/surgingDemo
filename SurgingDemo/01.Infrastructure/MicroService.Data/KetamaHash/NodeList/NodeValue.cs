#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：MicroService.Data.KetamaHash.NodeList
* 项目描述 ：
* 类 名 称 ：NodeValue
* 类 描 述 ：
* 命名空间 ：MicroService.Data.KetamaHash.NodeList
* CLR 版本 ：4.0.30319.42000
* 作    者 ：jinyu
* 创建时间 ：2018
* 更新时间 ：2018
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ jinyu 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion


using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Data.KetamaHash.NodeList
{
    /* ============================================================================== 
    * 功能描述：NodeValue 
    * 创 建 者：jinyu 
    * 修 改 者：jinyu 
    * 创建日期：2018 
    * 修改日期：2018 
    * ==============================================================================*/

   public class NodeData<TKey,TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
}
