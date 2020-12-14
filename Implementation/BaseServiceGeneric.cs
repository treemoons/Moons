using System.Diagnostics;
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
using Microsoft.Extensions.Logging;

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

    /// <summary> 服务容器&#60;服务,上下文&#62;
    /// <br/>服务：继承自类
    /// <br/>上下文：继承自DbContext预定义类型
    /// </summary>
    /// <typeparam name="TService">服务，继承类</typeparam>
    /// <typeparam name="TContext">上下文，继承DbContext预定义类型</typeparam>
    public class DBServiceProvider<TService, TContext> where TService : class where TContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public DBServiceProvider()
        {
            Service = new ServiceCollection();
        }
        /// <summary>
        /// Get DBServiceProvider with sqlstring
        /// </summary>
        /// <param name="sqlString">connection sqlstring</param>
        public DBServiceProvider(string sqlString)
        {
            ConnectionString = sqlString;
            Service = new ServiceCollection();
        }
        /// <summary>
        /// SQLServer Connetion String
        /// </summary>
        /// <value>const</value>
        public string ConnectionString { set; get; } = default;
        private IServiceCollection Service = default;
        private IServiceCollection GetTransientService() => this.Service.AddTransient<TService>();
        private IServiceCollection GetSingletonService() => this.Service.AddSingleton<TService>();
        private IServiceCollection GetScopedService() => this.Service.AddScoped<TService>();


        /// <summary>
        /// logging  to  console
        /// </summary>
        /// <returns>ILoggerFactory</returns>
        private static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(op => { }); });

        /// <summary>根据服务类型，获取相对应的服务
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns>服务</returns>
        public IServiceCollection GetDbService(EService serviceType) =>
            (serviceType switch
            {
                EService.AddTransient => GetTransientService(),
                EService.AddScoped => GetScopedService(),
                EService.AddSingleton => GetSingletonService(),
                _ => throw new Exception()
            })
            .AddEntityFrameworkSqlServer()
            .AddDbContext<TContext>(
                op =>
                {
                    if (string.IsNullOrEmpty(ConnectionString))
                        throw new NullReferenceException("ConnectionString is null or empty.");
                    op.UseSqlServer(ConnectionString);
                    op.UseLoggerFactory(MyLoggerFactory);
                });

        /// <summary>根据服务类型，获取相对应的服务
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="sqlString"> 数据库连接字符串</param>
        /// <returns>服务</returns>
        public IServiceCollection GetDbService(EService serviceType, string sqlString) =>
            (serviceType switch
            {
                EService.AddTransient => GetTransientService(),
                EService.AddScoped => GetScopedService(),
                EService.AddSingleton => GetSingletonService(),
                _ => throw new Exception()
            })
            .AddEntityFrameworkSqlServer()
            .AddDbContext<TContext>(
                op =>
                {
                    op.UseSqlServer(sqlString);
                    op.UseLoggerFactory(MyLoggerFactory);
                });

    }

    /// <summary>
    /// 服务容器
    /// </summary>
    public static class ServiceContainer
    {
        #region Get Services of MultiTables
        /// <summary>
        /// Create service,which can  add other services
        /// </summary>
        /// <param name="service">orginal sercive type</param>
        /// <param name="sqlString">connection string of database[sqlserver] </param>
        /// <typeparam name="TService"> service that class ,which contains all functions and models of data</typeparam>
        /// <typeparam name="TDbContext"></typeparam>
        /// <returns>IService</returns>
        public static IServiceCollection GetService<TService, TDbContext>(EService service, string sqlString)
            where TService : DBService<TDbContext>
            where TDbContext : DbContext
            => new DBServiceProvider<TService, TDbContext>().GetDbService(service, sqlString);
        



        /// <summary>
        /// Get service,which built service of database and dealing with tables of database.
        /// </summary>
        /// <param name="service">Enum,which is type of Service</param>
        /// <param name="sqlString"> SQL string  of connection</param>
        /// <typeparam name="TService">Service ,which is class including functions of dealing with database</typeparam>
        /// <typeparam name="TDbContext">DbContext of service,which built the models of database</typeparam>
        /// <returns></returns>
        public static TService GetServiceContext<TService, TDbContext>(EService service, string sqlString)
        where TService : DBService<TDbContext>
        where TDbContext : DbContext
        => GetService<TService, TDbContext>(service,sqlString).BuildServiceProvider().GetService<TService>();

        #endregion
    }

    #region Sample
    /// <summary>
    /// 模型表
    /// </summary>
    public class c
    {
        async void s()
        {

            var user = ServiceContainer.GetServiceContext<Implementation.Tables.MainViews.User.UserService, Implementation.Tables.MainViews.User.UserContext>(EService.AddScoped, "");
            await user.CheckingLogin("", "");
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
    #endregion


    /// <summary> 用户服务基类，在使用服务容器时必须继承此类
    /// <br/>TContext为继承DBContext泛型基类的派生类
    /// <br/>作用：
    /// <br/>添加服务方法和服务功能
    /// </summary>
    /// <typeparam name="TContext">上下文</typeparam>
    public class DBService<TContext> where TContext : DbContext
    {
        /// <summary>
        /// 服务上下文context
        /// </summary>
        protected readonly TContext context;
        /// <summary>
        /// basic service of dbcontext
        /// </summary>
        /// <param name="_context"></param>
        protected DBService(TContext _context) => context = _context;

        /// <summary>
        /// CREATE DATABASE
        /// </summary>
        /// <returns></returns>
        protected async Task<bool> CreateDataBaseAsync() =>
            await context.Database.EnsureCreatedAsync();
        /// <summary>
        /// DELETE DATABASE
        /// </summary>
        /// <returns></returns>
        protected async Task<bool> DeleteDataBaseAsync() =>
            await context.Database.EnsureDeletedAsync();
    }

    #region 单表上下文基类，sigle table DBcontext
    /// <summary> 【单表】上下文基类，用于加载配置数据表和数据表字段属性
    /// <br/>DBContext&#60;TDbTable, TModelsBuilder&#62;
    /// <br/>DBContext：继承自DbContext预定义类型
    /// <br/>TModelsBuilder：继承自接口IEntityTypeConfiguration&#60;T&#62;
    /// <br/>(其中接口IEntityTypeConfiguration&#60;T&#62;需要：
    /// <br/>using Microsoft.EntityFrameworkCore;
    /// <br/>using Microsoft.EntityFrameworkCore.Metadata.Builders;)
    /// </summary>
    /// <typeparam name="TDbTable">数据表模型类</typeparam>
    /// <typeparam name="TModelsBuilder">配置加载数据模型属性 
    /// <br/><i>继承自接口IEntityTypeConfiguration&#60;TDbTable&#62;</i>
    /// </typeparam>
    public class DBContext<TDbTable, TModelsBuilder> : DbContext
     where TDbTable : class
     where TModelsBuilder : IEntityTypeConfiguration<TDbTable>, new()
    {
        public DBContext(DbContextOptions<DBContext<TDbTable, TModelsBuilder>> options) : base(options) { }
        /// <summary>
        /// 控制的表
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable> Table { get; set; }
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


    /// <summary> 上下文基类，用于加载配置数据表和数据表字段属性
    /// <br/>DBContext：继承自DbContext预定义类型
    /// <br/>TDbTable：模型表类
    /// <br/>继承并重写OnModelCreating(ModelBuilder builder)方法，设置flexibility api配置TDTable的属性
    /// </summary>
    /// <typeparam name="TDbTable">数据表模型类</typeparam>
    public class DBContext<TDbTable> : DbContext where TDbTable : class
    {
        public DBContext(DbContextOptions<DBContext<TDbTable>> options) : base(options) { }
        /// <summary>
        /// 控制的表
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable> Table { get; set; }
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

    #endregion

    #region 多表上下文基类，multi tabels DBcontext
    /// <summary> 上下文基类，用于加载配置数据表和数据表字段属性
    /// <br/>DBContext：继承自DbContext预定义类型
    /// <br/>TDbTable：模型表类
    /// <br/>可继承并重写OnModelCreating(ModelBuilder builder)方法，设置flexibility api配置TDTable的属性
    /// </summary>
    /// <typeparam name="TDbTable1">数据表模型类1</typeparam>
    /// <typeparam name="TDbTable2">数据表模型类2</typeparam>
    public class DBContexts<TDbTable1, TDbTable2> : DbContext
     where TDbTable1 : class
     where TDbTable2 : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public DBContexts(DbContextOptions<DBContexts<TDbTable1, TDbTable2>> options) : base(options) { }
        /// <summary>
        /// 控制的表,对应泛型的第一个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable1> Table1 { get; set; }
        /// <summary>
        /// 控制的表，对应泛型的第二个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable2> Table2 { get; set; }
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

    /// <summary> 上下文基类，用于加载配置数据表和数据表字段属性
    /// <br/>DBContext：继承自DbContext预定义类型
    /// <br/>TDbTable：模型表类
    /// <br/>可继承并重写OnModelCreating(ModelBuilder builder)方法，设置flexibility api配置TDTable的属性
    /// </summary>
    /// <typeparam name="TDbTable1">数据表模型类1</typeparam>
    /// <typeparam name="TDbTable2">数据表模型类2</typeparam>
    /// <typeparam name="TDbTable3">数据表模型类3</typeparam>
    public class DBContexts<TDbTable1, TDbTable2, TDbTable3> : DbContext
     where TDbTable1 : class
     where TDbTable2 : class
     where TDbTable3 : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public DBContexts(DbContextOptions<DBContexts<TDbTable1, TDbTable2, TDbTable3>> options) : base(options) { }
        /// <summary>
        /// 控制的表,对应泛型的第一个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable1> Table1 { get; set; }
        /// <summary>
        /// 控制的表，对应泛型的第二个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable2> Table2 { get; set; }
        /// <summary>
        /// 控制的表，对应泛型的第二个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable3> Table3 { get; set; }
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



    /// <summary> 上下文基类，用于加载配置数据表和数据表字段属性
    /// <br/>DBContext：继承自DbContext预定义类型
    /// <br/>TDbTable：模型表类
    /// <br/>可继承并重写OnModelCreating(ModelBuilder builder)方法，设置flexibility api配置TDTable的属性
    /// </summary>
    /// <typeparam name="TDbTable1">数据表模型类1</typeparam>
    /// <typeparam name="TDbTable2">数据表模型类2</typeparam>
    /// <typeparam name="TDbTable3">数据表模型类3</typeparam>
    /// <typeparam name="TDbTable4">数据表模型类4</typeparam>
    public class DBContexts<TDbTable1, TDbTable2, TDbTable3, TDbTable4> : DbContext
      where TDbTable1 : class
      where TDbTable2 : class
      where TDbTable3 : class
      where TDbTable4 : class
    {
        public DBContexts(DbContextOptions<DBContexts<TDbTable1, TDbTable2, TDbTable3, TDbTable4>> options) : base(options) { }
        /// <summary>
        /// 控制的表,对应泛型的第1个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable1> Table1 { get; set; }
        /// <summary>
        /// 控制的表，对应泛型的第2个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable2> Table2 { get; set; }
        /// <summary>
        /// 控制的表，对应泛型的第3个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable3> Table3 { get; set; }
        /// <summary>
        /// 控制的表，对应泛型的第4个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable4> Table4 { get; set; }
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



    /// <summary> 上下文基类，用于加载配置数据表和数据表字段属性
    /// <br/>DBContext：继承自DbContext预定义类型
    /// <br/>TDbTable：模型表类
    /// <br/>可继承并重写OnModelCreating(ModelBuilder builder)方法，设置flexibility api配置TDTable的属性
    /// </summary>
    /// <typeparam name="TDbTable1">数据表模型类1</typeparam>
    /// <typeparam name="TDbTable2">数据表模型类2</typeparam>
    /// <typeparam name="TDbTable3">数据表模型类3</typeparam>
    /// <typeparam name="TDbTable4">数据表模型类4</typeparam>
    /// <typeparam name="TDbTable5">数据表模型类5</typeparam>
    public class DBContextsWithModelsBuilder<TDbTable1, TDbTable2, TDbTable3, TDbTable4, TDbTable5> : DbContext
      where TDbTable1 : class
      where TDbTable2 : class
      where TDbTable3 : class
      where TDbTable4 : class
      where TDbTable5 : class
    {
        public DBContextsWithModelsBuilder(DbContextOptions<DBContextsWithModelsBuilder<TDbTable1, TDbTable2, TDbTable3, TDbTable4, TDbTable5>> options) : base(options) { }
        /// <summary>
        /// 控制的表,对应泛型的第1个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable1> Table1 { get; set; }
        /// <summary>
        /// 控制的表，对应泛型的第2个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable2> Table2 { get; set; }
        /// <summary>
        /// 控制的表，对应泛型的第3个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable3> Table3 { get; set; }
        /// <summary>
        /// 控制的表，对应泛型的第4个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable4> Table4 { get; set; }
        /// <summary>
        /// 控制的表，对应泛型的第5个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable4> Table5 { get; set; }
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

    /// <summary> 【多表】上下文基类，用于加载配置数据表和数据表字段属性
    /// <br/>DBContext&#60;TDbTable1, TDbTable2, TModelsBuilder1, TModelsBuilder2&#62;
    /// <br/>DBContext：继承自DbContext预定义类型
    /// <br/>TModelsBuilder：继承自接口IEntityTypeConfiguration&#60;T&#62;
    /// <br/>(其中接口IEntityTypeConfiguration&#60;T&#62;需要：
    /// <br/>using Microsoft.EntityFrameworkCore;
    /// <br/>using Microsoft.EntityFrameworkCore.Metadata.Builders;)
    /// </summary>
    /// <typeparam name="TDbTable1">数据表模型类1</typeparam>
    /// <typeparam name="TDbTable2">数据表模型类2</typeparam>
    /// <typeparam name="TModelsBuilder1">配置加载数据模型属性 1
    /// <br/><i>继承自接口IEntityTypeConfiguration&#60;TDbTable&#62;</i></typeparam>
    /// <typeparam name="TModelsBuilder2">配置加载数据模型属性 2
    /// <br/><i>继承自接口IEntityTypeConfiguration&#60;TDbTable&#62;</i>
    /// </typeparam>
    public class DBContextsWithModelsBuilder<TDbTable1, TDbTable2, TModelsBuilder1, TModelsBuilder2> : DbContext
    where TDbTable1 : class
    where TDbTable2 : class
    where TModelsBuilder1 : IEntityTypeConfiguration<TDbTable1>, new()
    where TModelsBuilder2 : IEntityTypeConfiguration<TDbTable2>, new()
    {
        public DBContextsWithModelsBuilder(DbContextOptions<DBContextsWithModelsBuilder<TDbTable1, TDbTable2, TModelsBuilder1, TModelsBuilder2>> options) : base(options) { }
        /// <summary>
        /// 控制的表,对应泛型的第一个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable1> Table1 { get; set; }
        /// <summary>
        /// 控制的表，对应泛型的第二个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable2> Table2 { get; set; }
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
            builder.ApplyConfiguration(new TModelsBuilder1());
            builder.ApplyConfiguration(new TModelsBuilder2());
        }
    }


    /// <summary>
    ///  【多表】上下文基类，用于加载配置数据表和数据表字段属性
    /// <br/>DBContext&#60;TDbTable1, TDbTable2, TModelsBuilder1, TModelsBuilder2&#62;
    /// <br/>DBContext：继承自DbContext预定义类型
    /// <br/>TModelsBuilder：继承自接口IEntityTypeConfiguration&#60;T&#62;
    /// <br/>(其中接口IEntityTypeConfiguration&#60;T&#62;需要：
    /// <br/>using Microsoft.EntityFrameworkCore;
    /// <br/>using Microsoft.EntityFrameworkCore.Metadata.Builders;)
    /// </summary>
    /// <typeparam name="TDbTable1">数据表模型类1</typeparam>
    /// <typeparam name="TDbTable2">数据表模型类2</typeparam>
    /// <typeparam name="TDbTable3">数据表模型类3</typeparam>
    /// <typeparam name="TModelsBuilder1">配置加载数据模型属性1</typeparam> 
    /// <typeparam name="TModelsBuilder2">配置加载数据模型属性2</typeparam> 
    /// <typeparam name="TModelsBuilder3">配置加载数据模型属性3
    /// <br/><i>继承自接口IEntityTypeConfiguration&#60;TDbTable&#62;</i>
    /// </typeparam>
    public class DBContextsWithModelsBuilder<TDbTable1, TDbTable2, TDbTable3, TModelsBuilder1, TModelsBuilder2, TModelsBuilder3> : DbContext
    where TDbTable1 : class
    where TDbTable2 : class
    where TDbTable3 : class
    where TModelsBuilder1 : IEntityTypeConfiguration<TDbTable1>, new()
    where TModelsBuilder2 : IEntityTypeConfiguration<TDbTable2>, new()
    where TModelsBuilder3 : IEntityTypeConfiguration<TDbTable3>, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public DBContextsWithModelsBuilder(DbContextOptions<DBContextsWithModelsBuilder<TDbTable1, TDbTable2, TDbTable3, TModelsBuilder1, TModelsBuilder2, TModelsBuilder3>> options) : base(options) { }
        /// <summary>
        /// 控制的表,对应泛型的第1个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable1> Table1 { get; set; }
        /// <summary>
        /// 控制的表，对应泛型的第2个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable2> Table2 { get; set; }
        /// <summary>
        /// 控制的表，对应泛型的第3个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable3> Table3 { get; set; }
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
            builder.ApplyConfiguration(new TModelsBuilder1());
            builder.ApplyConfiguration(new TModelsBuilder2());
            builder.ApplyConfiguration(new TModelsBuilder3());
        }
    }


    /// <summary> 【多表】上下文基类，用于加载配置数据表和数据表字段属性
    /// <br/>DBContext：继承自DbContext预定义类型
    /// <br/>TModelsBuilder：继承自接口IEntityTypeConfiguration&#60;T&#62;
    /// <br/>(其中接口IEntityTypeConfiguration&#60;T&#62;需要：
    /// <br/>using Microsoft.EntityFrameworkCore;
    /// <br/>using Microsoft.EntityFrameworkCore.Metadata.Builders;)
    /// </summary>
    /// <typeparam name="TDbTable1">数据表模型类1</typeparam>
    /// <typeparam name="TDbTable2">数据表模型类2</typeparam>
    /// <typeparam name="TDbTable3">数据表模型类3</typeparam>
    /// <typeparam name="TDbTable4">数据表模型类4</typeparam>
    /// <typeparam name="TModelsBuilder1">配置加载数据模型属性1 </typeparam>
    /// <typeparam name="TModelsBuilder2">配置加载数据模型属性2 </typeparam>
    /// <typeparam name="TModelsBuilder3">配置加载数据模型属性3 </typeparam>
    /// <typeparam name="TModelsBuilder4">配置加载数据模型属性4 
    /// <br/><i>继承自接口IEntityTypeConfiguration&#60;TDbTable&#62;</i>
    /// </typeparam>
    public class DBContextsWithModelsBuilder<TDbTable1, TDbTable2, TDbTable3, TDbTable4, TModelsBuilder1, TModelsBuilder2, TModelsBuilder3, TModelsBuilder4> : DbContext
      where TDbTable1 : class
      where TDbTable2 : class
      where TDbTable3 : class
      where TDbTable4 : class
      where TModelsBuilder1 : IEntityTypeConfiguration<TDbTable1>, new()
      where TModelsBuilder2 : IEntityTypeConfiguration<TDbTable2>, new()
      where TModelsBuilder3 : IEntityTypeConfiguration<TDbTable3>, new()
      where TModelsBuilder4 : IEntityTypeConfiguration<TDbTable4>, new()
    {
        public DBContextsWithModelsBuilder(DbContextOptions<DBContextsWithModelsBuilder<TDbTable1, TDbTable2, TDbTable3, TDbTable4, TModelsBuilder1, TModelsBuilder2, TModelsBuilder3, TModelsBuilder4>> options) : base(options) { }
        /// <summary>
        /// 控制的表,对应泛型的第1个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable1> Table1 { get; set; }
        /// <summary>
        /// 控制的表，对应泛型的第2个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable2> Table2 { get; set; }
        /// <summary>
        /// 控制的表，对应泛型的第3个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable3> Table3 { get; set; }
        /// <summary>
        /// 控制的表，对应泛型的第4个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable4> Table4 { get; set; }
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
            builder.ApplyConfiguration(new TModelsBuilder1());
            builder.ApplyConfiguration(new TModelsBuilder2());
            builder.ApplyConfiguration(new TModelsBuilder3());
            builder.ApplyConfiguration(new TModelsBuilder4());
        }
    }


    /// <summary> 【多表】上下文基类，用于加载配置数据表和数据表字段属性
    /// <br/>DBContext：继承自DbContext预定义类型
    /// <br/>TModelsBuilder：继承自接口IEntityTypeConfiguration&#60;T&#62;
    /// <br/>(其中接口IEntityTypeConfiguration&#60;T&#62;需要：
    /// <br/>using Microsoft.EntityFrameworkCore;
    /// <br/>using Microsoft.EntityFrameworkCore.Metadata.Builders;)
    /// </summary>
    /// <typeparam name="TDbTable1">数据表模型类1</typeparam>
    /// <typeparam name="TDbTable2">数据表模型类2</typeparam>
    /// <typeparam name="TDbTable3">数据表模型类3</typeparam>
    /// <typeparam name="TDbTable4">数据表模型类4</typeparam>
    /// <typeparam name="TDbTable5">数据表模型类5</typeparam>
    /// <typeparam name="TModelsBuilder1">配置加载数据模型属性 </typeparam>
    /// <typeparam name="TModelsBuilder2">配置加载数据模型属性 </typeparam>
    /// <typeparam name="TModelsBuilder3">配置加载数据模型属性 </typeparam>
    /// <typeparam name="TModelsBuilder4">配置加载数据模型属性  </typeparam>
    /// <typeparam name="TModelsBuilder5">配置加载数据模型属性 
    /// <br/><i>继承自接口IEntityTypeConfiguration&#60;TDbTable&#62;</i>
    /// </typeparam>
    public class DBContextsWithModelsBuilder<TDbTable1, TDbTable2, TDbTable3, TDbTable4, TDbTable5, TModelsBuilder1, TModelsBuilder2, TModelsBuilder3, TModelsBuilder4, TModelsBuilder5> : DbContext
      where TDbTable1 : class
      where TDbTable2 : class
      where TDbTable3 : class
      where TDbTable4 : class
      where TDbTable5 : class
      where TModelsBuilder1 : IEntityTypeConfiguration<TDbTable1>, new()
      where TModelsBuilder2 : IEntityTypeConfiguration<TDbTable2>, new()
      where TModelsBuilder3 : IEntityTypeConfiguration<TDbTable3>, new()
      where TModelsBuilder4 : IEntityTypeConfiguration<TDbTable4>, new()
      where TModelsBuilder5 : IEntityTypeConfiguration<TDbTable5>, new()
    {
        public DBContextsWithModelsBuilder(DbContextOptions<DBContextsWithModelsBuilder<TDbTable1, TDbTable2, TDbTable3, TDbTable4, TDbTable5, TModelsBuilder1, TModelsBuilder2, TModelsBuilder3, TModelsBuilder4, TModelsBuilder5>> options) : base(options) { }
        /// <summary>
        /// 控制的表,对应泛型的第1个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable1> Table1 { get; set; }
        /// <summary>
        /// 控制的表，对应泛型的第2个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable2> Table2 { get; set; }
        /// <summary>
        /// 控制的表，对应泛型的第3个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable3> Table3 { get; set; }
        /// <summary>
        /// 控制的表，对应泛型的第4个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable4> Table4 { get; set; }
        /// <summary>
        /// 控制的表，对应泛型的第5个类型
        /// </summary>
        /// <value>所有表的属性值及方法</value>
        public DbSet<TDbTable4> Table5 { get; set; }
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
            builder.ApplyConfiguration(new TModelsBuilder1());
            builder.ApplyConfiguration(new TModelsBuilder2());
            builder.ApplyConfiguration(new TModelsBuilder3());
            builder.ApplyConfiguration(new TModelsBuilder4());
            builder.ApplyConfiguration(new TModelsBuilder5());
        }
    }
    #endregion
}
