using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Implementation
{
    public enum EService
    {
        AddTransient,
        AddScoped,
        AddSingleton
    }
    public class DBServiceProvider<TService, TContext> where TService : class where TContext : DbContext
    {
        private  IServiceCollection Service { get; set; } = new ServiceCollection();
        private static IServiceCollection GetTransientService() =>new DBServiceProvider<TService, TContext>().Service.AddTransient<TService>();
        private static IServiceCollection GetSingletonService() => new DBServiceProvider<TService, TContext>().Service.AddSingleton<TService>();
        private static IServiceCollection GetScopedService() => new DBServiceProvider<TService, TContext>().Service.AddScoped<TService>();

        public static IServiceProvider GetDbService(EService serviceType) =>
        (serviceType switch
        {
            EService.AddTransient => GetTransientService(),
            EService.AddScoped => GetScopedService(),
            EService.AddSingleton => GetSingletonService(),
            _ => throw new Exception(),
        })
            .AddEntityFrameworkSqlServer()
            .AddDbContext<TContext>(op => op.UseSqlServer(""))
            .BuildServiceProvider();
    }
    public  static class ServiceContainer
    {
       public static TService GetDbService<TService,TTable, TModelBuilder>(EService service)where TService :DBService<DBContext<TTable, TModelBuilder>> where TTable:class where TModelBuilder:IEntityTypeConfiguration<TTable>,new()
        => DBServiceProvider<TService, DBContext<TTable,TModelBuilder>>.GetDbService(service).GetService<TService>();
   
   }
    /// <summary>
    /// 模型表
    /// </summary>
    public class c {
        void s(){
            ServiceContainer.GetDbService<DBS, c,b>(EService.AddScoped);
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
    public class DBS:DBService<DBContext<c, b>>{
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