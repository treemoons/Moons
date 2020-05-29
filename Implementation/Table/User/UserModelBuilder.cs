using Implementation;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModelsLibrary.User;
namespace Implementation.Table.User
{
    /// <summary>
    /// 添加所有模型属性，阴影属性等
    /// </summary>
    public class UserModelBuilder:IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        { }
    }
}