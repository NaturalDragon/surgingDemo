using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Data.Enums
{
   public enum OcrType
    {
        /// <summary>
        /// 行程单
        /// </summary>
        Air=0,
        /// <summary>
        /// 火车票
        /// </summary>
        Train=1,
        /// <summary>
        /// 专票
        /// </summary>
        SpecialTicket=2,
        /// <summary>
        /// 普票
        /// </summary>
        GeneralVote=3
    }
}
