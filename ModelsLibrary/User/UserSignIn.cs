using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.User
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
        [StringLength(10, ErrorMessage = "{0}的长度在{2}至{1}个字符间")]
        [DisplayName("用户名")]
        public string UserName { get; set; } = "defualt";
        [StringLength(10, ErrorMessage = "{0}的长度在{2}至{1}个字符间")]
        [DisplayName("昵称")]
        public string UserAlias { get; set; }
        [StringLength(20, ErrorMessage = "{0}的长度在{2}至{1}个字符间")]
        [DisplayName("账号")]
        [Key]
        public int? Account { get; set; }
        [StringLength(16, MinimumLength = 1, ErrorMessage = "{0}的长度在{2}至{1}个字符间")]
        [Required(ErrorMessage = "{0}不能为空。")]
        [DisplayName("密码")]
        public string Password { get; set; }
        public string LastLoginTime { get; set; }
        public string IPAddress { get; set; }
        public string IsRemembered { get; set; }
    }
}
