using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Implementation
{
    /// <summary>
    /// 服务类型
    /// </summary>
    public enum EService
    {
        /// <summary>
        /// 临时服务
        /// </summary>
        [Description("暂时服务")]
        AddTransient,
        /// <summary>
        /// 分区服务
        /// </summary>
        [Description("分区服务")]
        AddScoped,
        /// <summary>
        /// 单例服务
        /// </summary>
        [Description("单例服务")]
        AddSingleton
    }
    /// <summary>
    /// 服务容器&#60;服务,上下文&#62;
    /// <br/>服务：继承类
    /// <br/>上下文：继承DbContext预定义类型
    /// </summary>
    /// <typeparam name="TService">服务，继承类</typeparam>
    /// <typeparam name="TContext">上下文，继承DbContext预定义类型</typeparam>
    public class DBServiceProvider<TService, TContext> where TService : class where TContext : DbContext
    {
        private IServiceCollection Service { get; set; } = new ServiceCollection();
        private static IServiceCollection GetTransientService() => new DBServiceProvider<TService, TContext>().Service.AddTransient<TService>();
        private static IServiceCollection GetSingletonService() => new DBServiceProvider<TService, TContext>().Service.AddSingleton<TService>();
        private static IServiceCollection GetScopedService() => new DBServiceProvider<TService, TContext>().Service.AddScoped<TService>();
        /// <summary>
        /// 根据服务类型，获取相对应的服务
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="sqlString"> 数据库连接字符串</param>
        /// <returns>服务提供的容器</returns>
        public static IServiceProvider GetDbService(EService serviceType, string sqlString) =>
        (serviceType switch
        {
            EService.AddTransient => GetTransientService(),
            EService.AddScoped => GetScopedService(),
            EService.AddSingleton => GetSingletonService(),
            _ => throw new Exception(),
        })
            .AddEntityFrameworkSqlServer()
            .AddDbContext<TContext>(op => op.UseSqlServer(sqlString))
            .BuildServiceProvider();
    }
    public static class ServiceContainer
    {
        public static TService GetDbService<TService, TTable, TModelBuilder>(EService service, string sqlString)
                 where TService : DBService<DBContext<TTable, TModelBuilder>>
                 where TTable : class
                 where TModelBuilder : IEntityTypeConfiguration<TTable>, new()
                  => DBServiceProvider<TService, DBContext<TTable, TModelBuilder>>.GetDbService(service, sqlString).GetService<TService>();

    }
    /// <summary>
    /// 模型表
    /// </summary>
    public class c
    {
        void s()
        {
            ServiceContainer.GetDbService<DBS, c, b>(EService.AddScoped, "");
        }
    }
    /// <summary>
    /// 模型biulder，添加字段等
    /// </summary>
    public class b : IEntityTypeConfiguration<c>
    {
        public void Configure(EntityTypeBuilder<c> builder)
        { }
    }

    /// <summary>
    /// 添加模型参数
    /// </summary>
    public class DBS : DBService<DBContext<c, b>>
    {
        public DBS(DBContext<c, b> putContext) : base(putContext) { }
    }
    public class DBService<TContext> where TContext : DbContext
    {
        private readonly TContext context;
        public DBService(TContext putContext) => context = putContext;

        public async Task<bool> CreateDataBase() =>
            await context.Database.EnsureCreatedAsync();
        public async Task<bool> DeleteDataBase() =>
            await context.Database.EnsureDeletedAsync();
    }
    public class DBContext<TDbTable, TModelsBuilder> : DbContext where TDbTable : class where TModelsBuilder : IEntityTypeConfiguration<TDbTable>, new()
    {
        public DBContext(DbContextOptions<DBContext<TDbTable, TModelsBuilder>> options) : base(options) { }
        public DbSet<TDbTable> Table { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new TModelsBuilder());
        }
    }
}