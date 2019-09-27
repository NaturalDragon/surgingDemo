using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
namespace MicroService.EntityFramwork
{
    /// <summary>
    /// 设计时数据上下文实例工厂基类，用于执行数据迁移
    /// </summary>
    public abstract class DesignTimeDbContextFactoryBase<TDbContext> : IDesignTimeDbContextFactory<TDbContext>
        where TDbContext : DbContext
    {
        public TDbContext CreateDbContext(string[] args)
        {
            string connString = GetConnectionString();
          //  IEntityConfigurationTypeFinder typeFinder = GetEntityConfigurationTypeFinder();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder<TDbContext>();
            builder = UseSql(builder, connString);
            return (TDbContext)Activator.CreateInstance(typeof(TDbContext), new object[] { builder.Options
            });
        }
        /// <summary>
        /// 重写以获取数据上下文数据库连接字符串
        /// </summary>
        public abstract string GetConnectionString();

        /// <summary>
        /// 重写以获取数据实体类配置类型查找器
        /// </summary>
        /// <returns></returns>
       // public abstract IEntityConfigurationTypeFinder GetEntityConfigurationTypeFinder();

        /// <summary>
        /// 重写以实现数据上下文选项构建器加载数据库驱动程序
        /// </summary>
        /// <param name="builder">数据上下文选项构建器</param>
        /// <param name="connString">数据库连接字符串</param>
        /// <returns>数据上下文选项构建器</returns>
        public abstract DbContextOptionsBuilder UseSql(DbContextOptionsBuilder builder, string connString);
    }
}
