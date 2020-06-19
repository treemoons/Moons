using ModelsLibrary.MainViews.User;
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

namespace Implementation.Tables.MainViews.User
{
    /// <summary>
    /// 添加上下文配置(使用ModelBuilder，则无需添加任何内容，请使用UserModelBuilder进行属性配置)
    /// </summary>
    public class UserContext:DBContextsWithModelsBuilder<UserSignIn,UserProfile,ModelsBuilder.UserSignIn,ModelsBuilder.UserProfile>//添加泛型引用，是否使用modelBuilder建立
    {
        public UserContext(DbContextOptions<DBContextsWithModelsBuilder<UserSignIn, UserProfile, ModelsBuilder.UserSignIn, ModelsBuilder.UserProfile>> options) : base(options) { }
    }
}