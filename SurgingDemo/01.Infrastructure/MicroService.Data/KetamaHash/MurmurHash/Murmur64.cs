#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：MicroService.Data.KetamaHash.MurmurHash
* 项目描述 ：
* 类 名 称 ：Murmur64
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
using System.Runtime.CompilerServices;
using System.Text;

namespace MicroService.Data.KetamaHash.MurmurHash
{
    /* ============================================================================== 
    * 功能描述：Murmur64 
    * 创 建 者：jinyu 
    * 修 改 者：jinyu 
    * 创建日期：2018 
    * 修改日期：2018 
    * ==============================================================================*/

   internal class Murmur64: Murmur32
    {
        public Murmur64(uint seed = 0)
            : base(seed)
        {
        }

        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            Length += cbSize;
            Body(array, ibStart, cbSize);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Body(byte[] data, int start, int length)
        {
            int remainder = length & 3;
            int alignedLength = start + (length - remainder);

            for (int i = start; i < alignedLength; i += 4)
                H1 = (((H1 ^ (((data.ToUInt32(i) * C1).RotateLeft(15)) * C2)).RotateLeft(13)) * 5) + 0xe6546b64;

            if (remainder > 0)
                Tail(data, alignedLength, remainder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        private void Tail(byte[] tail, int position, int remainder)
        {
            // create our keys and initialize to 0
            uint k1 = 0;

            // determine how many bytes we have left to work with based on length
            switch (remainder)
            {
                case 3: k1 ^= (uint)tail[position + 2] << 16; goto case 2;
                case 2: k1 ^= (uint)tail[position + 1] << 8; goto case 1;
                case 1: k1 ^= tail[position]; break;
            }

            H1 ^= (k1 * C1).RotateLeft(15) * C2;
        }
    }
}
