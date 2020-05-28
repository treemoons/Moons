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
        [Description("临时服务")]
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
        public DBServiceProvider()
        {
            Service = new ServiceCollection();
        }
        private IServiceCollection Service;
        private IServiceCollection GetTransientService() => this.Service.AddTransient<TService>();
        private IServiceCollection GetSingletonService() => this.Service.AddSingleton<TService>();
        private IServiceCollection GetScopedService() => this.Service.AddScoped<TService>();
        /// <summary>
        /// 根据服务类型，获取相对应的服务
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="sqlString"> 数据库连接字符串</param>
        /// <returns>服务提供的容器</returns>
        public IServiceProvider GetDbService(EService serviceType, string sqlString) =>
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

    /// <summary>
    /// 服务容器
    /// </summary>
    public static class ServiceContainer
    {
        /// <summary> 获取服务容器（带ModelBuider）</summary>
        /// <param name="service">服务类型</param>
        /// <param name="sqlString">连接服务器数据库连接字符串</param>
        /// <typeparam name="TService">服务类</typeparam>
        /// <typeparam name="TTable">数据表类</typeparam>
        /// <typeparam name="TModelBuilder">模型类</typeparam>
        /// <returns>服务的容器</returns>
        public static TService GetDbService<TService, TTable, TModelBuilder>(EService service, string sqlString)
            where TService : DBService<DBContext<TTable, TModelBuilder>>
            where TTable : class
            where TModelBuilder : IEntityTypeConfiguration<TTable>, new()
            => new DBServiceProvider<TService, DBContext<TTable, TModelBuilder>>().GetDbService(service, sqlString).GetService<TService>();

        /// <summary> 获取服务容器（不带ModelBuider） </summary>
        /// <param name="service">服务类型</param>
        /// <param name="sqlString">连接服务器数据库连接字符串</param>
        /// <typeparam name="TService">服务类</typeparam>
        /// <typeparam name="TTable">数据表类</typeparam>
        /// <returns>服务的容器</returns>
        public static TService GetDbService<TService, TTable>(EService service, string sqlString)
            where TService : DBService<DBContext<TTable>>
            where TTable : class
            => new DBServiceProvider<TService, DBContext<TTable>>().GetDbService(service, sqlString).GetService<TService>();
    }

    /// <summary>
    /// 模型表
    /// </summary>
    public class c
    {
        void s()
        {
            var service = ServiceContainer.GetDbService<DBS, c, b>(EService.AddScoped, "");// 获取服务

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
    /// <summary>
    /// 用户服务基类，在使用服务容器时必须继承此类
    /// </summary>
    /// <typeparam name="TContext">上下文</typeparam>
    public class DBService<TContext> where TContext : DbContext
    {
        /// <summary>
        /// 服务上下文context
        /// </summary>
        protected readonly TContext context;
        protected DBService(TContext putContext) => context = putContext;

        protected async Task<bool> CreateDataBaseAsync() =>
            await context.Database.EnsureCreatedAsync();
        protected async Task<bool> DeleteDataBaseAsync() =>
            await context.Database.EnsureDeletedAsync();
    }
    /// <summary>
    /// 上下文基类，用于加载配置数据表和数据表字段属性
    /// <br/>DBContext&#60;TDbTable, TModelsBuilder&#62;
    /// <br/>DBContext：继承自DbContext预定义类型
    /// <br/>TModelsBuilder：继承自接口IEntityTypeConfiguration&#60;T&#62;
    /// </summary>
    /// <typeparam name="TDbTable">数据表模型类</typeparam>
    /// <typeparam name="TModelsBuilder">配置加载数据模型属性 
    /// <br/><i>继承自接口IEntityTypeConfiguration&#60;TDbTable&#62;</i>
    /// </typeparam>
    public class DBContext<TDbTable, TModelsBuilder> : DbContext where TDbTable : class where TModelsBuilder : IEntityTypeConfiguration<TDbTable>, new()
    {
        protected DBContext(DbContextOptions<DBContext<TDbTable, TModelsBuilder>> options) : base(options) { }
        /// <summary>
        /// 控制的表
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        protected DbSet<TDbTable> Table { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        /// <summary>
        /// 流利api可以配置加载属性，通过ModelBuilder添加属性
        /// </summary>
        /// <param name="builder">添加模型属性配置</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new TModelsBuilder());
        }
    }

    /// <summary>
    /// 上下文基类，用于加载配置数据表和数据表字段属性
    /// <br/>DBContext&#60;TDbTable&#62;
    /// <br/>DBContext：继承自DbContext预定义类型
    /// <br/>继承并重写OnModelCreating(ModelBuilder builder)方法，设置flexibility api配置TDTable的属性
    /// </summary>
    /// <typeparam name="TDbTable">数据表模型类</typeparam>
    public class DBContext<TDbTable> : DbContext where TDbTable : class
    {
        protected DBContext(DbContextOptions<DBContext<TDbTable>> options) : base(options) { }
        /// <summary>
        /// 控制的表
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        protected DbSet<TDbTable> Table { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        /// <summary>
        /// 流利api可以配置加载属性，通过ModelBuilder添加属性
        /// </summary>
        /// <param name="builder">添加模型属性配置</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}