using AutoMapper;
using MicroService.Common.Models;
using MicroService.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MicroService.Data.Ext
{
    public static class AutoMapHelper
    {
        /// <summary>
        /// 集合列表类型映射,默认字段名字一一对应
        /// </summary>
        /// <typeparam name="TDestination">转化之后的model，可以理解为viewmodel</typeparam>
        /// <typeparam name="TSource">要被转化的实体，Entity</typeparam>
        /// <param name="source">可以使用这个扩展方法的类型，任何引用类型</param>
        /// <returns>转化之后的实体列表</returns>
        public static IEnumerable<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source)
            where TDestination : class
            where TSource : class
        {
            if (source == null) return new List<TDestination>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            return mapper.Map<List<TDestination>>(source);
        }
        public static TDestination MapToEntity<TSource, TDestination>(this TSource source,TDestination destination)
         where TDestination : Entity<string>
         where TSource : RequestData
        {

            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TSource, TDestination>()
             //.ForMember(dest => dest., opt => opt.Ignore())
             .ForAllMembers(opt => opt.Condition((src, dest, sourceMember) => sourceMember != null )));

            var mapper = config.CreateMapper();

            return mapper.Map<TSource,TDestination>(source,destination);
        }

        public static TDestination MapToModifyEntity<TSource, TDestination>(this TSource source, TDestination destination)
        where TDestination : Entity<string>
        where TSource : RequestData
        {

            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TSource, TDestination>()
             .ForMember(dest =>dest.CreateDate, opt => opt.Ignore())
             .ForAllMembers(opt => opt.Condition((src, dest, sourceMember) => sourceMember != null)));

            var mapper = config.CreateMapper();

            return mapper.Map<TSource, TDestination>(source, destination);
        }
        public static TDestination MapEntity<TSource, TDestination>(this TSource source)
  where TDestination : RequestData
  where TSource : Entity<string>
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>()
             .ForMember(dest => dest.Payload, opt => opt.Ignore()));
            var mapper = config.CreateMapper();

            return mapper.Map<TDestination>(source);
        }
      
      
        public static TDestination Map<TSource, TDestination>(this TSource source, TDestination destination,
           Expression<Func<TSource, object>> sourceMember, Action<ISourceMemberConfigurationExpression> memberOptions)
        {
            var configuration = new MapperConfiguration(x => x.CreateMap<TSource, TDestination>().ForSourceMember(sourceMember, memberOptions));
            var mapper = configuration.CreateMapper();
            var result = mapper.Map<TSource, TDestination>(source, destination);

            return result;
        }
    } 


}
