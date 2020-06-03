using ModelsLibrary.User;
using Implementation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Implementation.Table.User
{
    /// <summary>
    /// 添加上下文配置(使用ModelBuilder，则无需添加任何内容，请使用UserModelBuilder进行属性配置)
    /// </summary>
    public class UserContext:DBContext<UserProfile,ModelsBuilder.UserProfile>//添加泛型引用，是否使用modelBuilder建立
    {
        public UserContext(DbContextOptions<DBContext<UserProfile,ModelsBuilder.UserProfile>> options) : base(options) { }
    }
}