#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：MicroService.Data.KetamaHash.MurmurHash
* 项目描述 ：
* 类 名 称 ：Murmur128
* 类 描 述 ：
* 命名空间 ：MicroService.Data.KetamaHash.MurmurHash
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
using System.Security.Cryptography;
using System.Text;

namespace MicroService.Data.KetamaHash.MurmurHash
{
    /* ============================================================================== 
    * 功能描述：Murmur128 
    * 创 建 者：jinyu 
    * 修 改 者：jinyu 
    * 创建日期：2018 
    * 修改日期：2018 
    * ==============================================================================*/

    public abstract class Murmur128 : HashAlgorithm
    {
        private readonly uint _Seed;
        protected Murmur128(uint seed)
        {
            _Seed = seed;
        }

        public uint Seed { get { return _Seed; } }

        public override int HashSize { get { return 128; } }
    }
}
