using MicroService.Common.Core.Enums;
using ProtoBuf;
using Surging.Core.System.Intercept;
using System;

namespace MicroService.Common.Models
{
    [ProtoContract]
    public class BaseDto : RequestData
    {
        [ProtoMember(1)]
        [CacheKey(1)]
        public string Id { set; get; }

        public bool IsDelete { set; get; }

        public DateTime CreateDate { set; get; } = DateTime.Now;

        public string CreateUserId { set; get; }


        public string CreateUserName { set; get; }


        public string ModifyUserId { set; get; }


        public Nullable<DateTime> ModifyDate { set; get; }


        public string ModifyUserName { set; get; }

        public string Token { get; set; }
        public OperationModel OperationModel { set; get; }
    }
}
