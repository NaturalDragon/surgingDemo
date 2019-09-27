#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：MicroService.Data.KetamaHash.MurmurHash
* 项目描述 ：
* 类 名 称 ：Extensions
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
    * 功能描述：Extensions 
    * 创 建 者：jinyu 
    * 修 改 者：jinyu 
    * 创建日期：2018 
    * 修改日期：2018 
    * ==============================================================================*/

    internal static class Extensions
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static uint ToUInt32(this byte[] data, int start)
        {
            return BitConverter.IsLittleEndian
                    ? (uint)(data[start] | data[start + 1] << 8 | data[start + 2] << 16 | data[start + 3] << 24)
                    : (uint)(data[start] << 24 | data[start + 1] << 16 | data[start + 2] << 8 | data[start + 3]);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ulong ToUInt64(this byte[] data, int start)
        {
            if (BitConverter.IsLittleEndian)
            {
                uint i1 = (uint)(data[start] | data[start + 1] << 8 | data[start + 2] << 16 | data[start + 3] << 24);
                ulong i2 = (ulong)(data[start + 4] | data[start + 5] << 8 | data[start + 6] << 16 | data[start + 7] << 24);
                return (i1 | i2 << 32);
            }
            else
            {
                ulong i1 = (ulong)(data[start] << 24 | data[start + 1] << 16 | data[start + 2] << 8 | data[start + 3]);
                uint i2 = (uint)(data[start + 4] << 24 | data[start + 5] << 16 | data[start + 6] << 8 | data[start + 7]);
                return (i2 | i1 << 32);

                //int i1 = (*pbyte << 24) | (*(pbyte + 1) << 16) | (*(pbyte + 2) << 8) | (*(pbyte + 3));
                //int i2 = (*(pbyte + 4) << 24) | (*(pbyte + 5) << 16) | (*(pbyte + 6) << 8) | (*(pbyte + 7));
                //return (uint)i2 | ((long)i1 << 32);
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static uint RotateLeft(this uint x, byte r)
        {
            return (x << r) | (x >> (32 - r));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ulong RotateLeft(this ulong x, byte r)
        {
            return (x << r) | (x >> (64 - r));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        internal static uint FMix(this uint h)
        {
            // pipelining friendly algorithm
            h = (h ^ (h >> 16)) * 0x85ebca6b;
            h = (h ^ (h >> 13)) * 0xc2b2ae35;
            return h ^ (h >> 16);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ulong FMix(this ulong h)
        {
            // pipelining friendly algorithm
            h = (h ^ (h >> 33)) * 0xff51afd7ed558ccd;
            h = (h ^ (h >> 33)) * 0xc4ceb9fe1a85ec53;

            return (h ^ (h >> 33));
        }
    }
}
