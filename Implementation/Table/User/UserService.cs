using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
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
using ModelsLibrary.User;
namespace Implementation.Table.User
{
    
    /// <summary>
    /// 添加所有服务方法
    /// </summary>
    public class UserService:DBService<UserContext>
    {
        public UserService(UserContext putContext) : base(putContext) { }
        public async  Task<bool> CheckingLogin(string username, string password)
        {
           var find= await context.Table.FirstOrDefaultAsync();
            return find==null?false:true;
        }

       
    }
}