using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vlctest.Models
{
    /// <summary>
    /// 设备类型
    /// </summary>
    public enum ClientStyle
    {
        PC = 1,
        Android = 2
    }

    /// <summary>
    /// 登陆用户类型
    /// </summary>
    public enum ClientRole
    {
        Teacher = 1,
        Assistant = 2,
        Student = 3
    }

    public enum ChatType
    {
        PrivateChat,
        GroupChat,
        TeamChat
    }

    /// <summary>
    /// 聊天信息类型
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        ///  文字
        /// </summary>
        String,
        /// <summary>
        /// 下载链接
        /// </summary>
        Sound,
        Image,
        Video
    }

    public enum ProgramType
    {
        Teacher,
        Student
    }
}
