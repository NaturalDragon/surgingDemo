

using MicroService.Core;
using MicroService.Data.Common;
using MicroService.Data.Enums;
using MicroService.Common.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;


 namespace MicroService.IApplication.Order.Dto
 {
	/// <summary>
	/// OrderInfo --dto
	/// </summary>
    [ProtoContract]
    [Serializable]
	public class OrderInfoPageRequestDto:PageData
	{

	}
}