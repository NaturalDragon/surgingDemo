#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：MicroService.Data.KetamaHash.MurmurHash
* 项目描述 ：
* 类 名 称 ：MurmurHashFactory
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
    * 功能描述：MurmurHashFactory 
    * 创 建 者：jinyu 
    * 修 改 者：jinyu 
    * 创建日期：2018 
    * 修改日期：2018 
    * ==============================================================================*/

    public class MurmurHashFactory
    {
        public enum AlgorithmPreference
        {
            Auto,
            X64,
            X86
        }

        public enum AlgorithmType
        {
            Murmur32,
            Murmur128
        }

      //  private static readonly uint Ticks= 0xee6b27eb;

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="digest"></param>
        /// <param name="nTime"></param>
        /// <returns></returns>
        public static ulong Hash(byte[] digest, int nTime=0)
        {

            //long rv = ((long)(digest[3 + nTime * 4] & 0xFF) << 24)
            //        | ((long)(digest[2 + nTime * 4] & 0xFF) << 16)
            //        | ((long)(digest[1 + nTime * 4] & 0xFF) << 8)
            //        | ((long)digest[0 + nTime * 4] & 0xFF);

            //return rv & 0xffffffffL; /* Truncate to 32-bits */
            var a = BitConverter.ToUInt64(digest, 0);
            var b = BitConverter.ToUInt64(digest, 8);
            ulong hashCode = a ^ b;
            return hashCode;
        }
       

        /// <summary>
        /// MurmurHash
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public static byte[] ComputeMurmur(string k)
        {
            Murmur128 murmur =MurmurHash.Create128((uint)Environment.TickCount);
            byte[] digest = murmur.ComputeHash(Encoding.UTF8.GetBytes(k));
            murmur.Clear();
            murmur.Dispose();
            return digest;
        }
        public static class MurmurHash
        {
            public static Murmur32 Create32(uint seed = 0)
            {
               
                    return new Murmur64(seed);
            }

            public static Murmur128 Create128(uint seed = 0,AlgorithmPreference preference = AlgorithmPreference.Auto)
            {
                var algorithm = Pick(seed, preference, s => new Murmur128_86(s), s => new Murmur128_64(s));
                  

                return algorithm as Murmur128;
            }

            private static HashAlgorithm Pick<T32, T64>(uint seed, AlgorithmPreference preference, Func<uint, T32> factory32, Func<uint, T64> factory64)
                where T32 : HashAlgorithm
                where T64 : HashAlgorithm
            {
                switch (preference)
                {
                    case AlgorithmPreference.X64: return factory64(seed);
                    case AlgorithmPreference.X86: return factory32(seed);
                    default:
                        {
                            if (Is64BitProcess())
                                return factory64(seed);

                            return factory32(seed);
                        }
                }
            }

            static bool Is64BitProcess()
            {
                return Environment.Is64BitProcess;
            }
        }
    }
}
