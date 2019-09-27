#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：MicroService.Data.KetamaHash.MurmurHash
* 项目描述 ：
* 类 名 称 ：Murmur32
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
    * 功能描述：Murmur32 
    * 创 建 者：jinyu 
    * 修 改 者：jinyu 
    * 创建日期：2018 
    * 修改日期：2018 
    * ==============================================================================*/

    public abstract class Murmur32 : HashAlgorithm
    {
        protected const uint C1 = 0xcc9e2d51;
        protected const uint C2 = 0x1b873593;

        private readonly uint _Seed;

        protected Murmur32(uint seed)
        {
            _Seed = seed;
            Reset();
        }

        public override int HashSize { get { return 32; } }
        public uint Seed { get { return _Seed; } }

        protected uint H1 { get; set; }

        protected int Length { get; set; }

        private void Reset()
        {
            H1 = Seed;
            Length = 0;
        }

        public override void Initialize()
        {
            Reset();
        }

        protected override byte[] HashFinal()
        {
            H1 = (H1 ^ (uint)Length).FMix();

            return BitConverter.GetBytes(H1);
        }
    }
}
