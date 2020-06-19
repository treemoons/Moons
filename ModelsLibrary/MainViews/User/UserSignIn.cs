using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.MainViews.User
{
    /// <summary>
    /// 用户登录表
    /// </summary>
    [DisplayName("用户登录表")]
    [Table("UserSignIn")]
    public class UserSignIn
    {
        public UserSignIn()
        {
            
        }
        [StringLength(10)]
        [DisplayName("用户名")]
        public string UserName { get; set; } 
        [StringLength(10)]
        [DisplayName("昵称")]
        public string UserAlias { get; set; }
        [StringLength(20)]
        [DisplayName("账号")]
        [Key]
        public int? Account { get; set; }
        [StringLength(16, MinimumLength = 1)]
        [Required]
        [DisplayName("密码")]
        public string Password { get; set; }
        public string LastLoginTime { get; set; }

        [StringLength(30,MinimumLength=1)]
        public string EMail{ get; set; }
        public string IPAddress { get; set; }
        public string IsRemembered { get; set; }
    }
}
