using System.ComponentModel;

namespace Common
{
    /// <summary>
    /// 命令字
    /// </summary>
    public enum CommandType
    {
        [Description("用户登陆")]
        /// <summary>
        /// 用户登陆
        /// </summary>
        UserLogin = 1,

        [Description("用户登陆返回信息")]
        /// <summary>
        /// 用户登陆返回信息
        /// </summary>
        UserLoginRes = 2,

        [Description("在线列表")]
        /// <summary>
        /// 在线列表
        /// </summary>
        OnlineList = 3,

        [Description("开始点名")]
        /// <summary>
        /// 开始点名
        /// </summary>
        BeginCall = 5,

        [Description("结束点名")]
        /// <summary>
        /// 结束点名
        /// </summary>
        EndCall = 6,

        [Description("开始屏幕广播")]
        /// <summary>
        /// 开始屏幕广播
        /// </summary>
        ScreenInteract = 7,

        [Description("结束屏幕广播")]
        /// <summary>
        /// 结束屏幕广播
        /// </summary>
        StopScreenInteract = 8,

        [Description("屏幕肃静")]
        /// <summary>
        /// 屏幕肃静
        /// </summary>
        Quiet = 9,

        [Description("结束屏幕肃静")]
        /// <summary>
        /// 结束屏幕肃静
        /// </summary>
        StopQuiet = 10,

        [Description("开始锁屏")]
        /// <summary>
        /// 开始锁屏
        /// </summary>
        LockScreen = 11,

        [Description("结束锁屏")]
        /// <summary>
        /// 结束锁屏
        /// </summary>
        StopLockScreen = 12,

        [Description("私聊")]
        /// <summary>
        /// 私聊
        /// </summary>
        PrivateChat = 13,

        [Description("所有人对话")]
        /// <summary>
        /// 所有人对话
        /// </summary>
        GroupChat = 14,

        [Description("创建群组")]
        /// <summary>
        /// 创建群组
        /// </summary>
        CreateTeam = 15,

        [Description("群聊（分组聊天）")]
        /// <summary>
        /// 群聊（分组聊天）
        /// </summary>
        TeamChat = 16,

        [Description("一个用户登陆进来")]
        /// <summary>
        /// 一个用户登陆进来
        /// </summary>
        OneUserLogIn = 17,

        [Description("用户登出")]
        /// <summary>
        /// 用户登出
        /// </summary>
        UserLoginOut = 18,

        [Description("客户端提交点名")]
        /// <summary>
        /// 客户端提交点名
        /// </summary>
        StudentCall = 19,

        [Description("")]
        StudentInMainForm = 20,

        [Description("开始客户端演示")]
        /// <summary>
        /// 开始客户端演示
        /// </summary>
        CallStudentShow = 21,


        ///// <summary>
        ///// 客户端启动演示
        ///// </summary>
        //StudentBeginShow = 22,

        [Description("停止客户端演示")]
        /// <summary>
        /// 停止客户端演示
        /// </summary>
        StopStudentShow = 23,


        ///// <summary>
        ///// 视频直播
        ///// </summary>
        //VideoInteract = 24,
        ///// <summary>
        ///// 结束视频直播
        ///// </summary>
        //StopVideoInteract = 25,

        [Description("禁止私聊")]
        /// <summary>
        /// 禁止私聊
        /// </summary>
        ForbidPrivateChat = 26,

        [Description("允许私聊")]
        /// <summary>
        /// 允许私聊
        /// </summary>
        AllowPrivateChat = 27,

        [Description("禁止群聊")]
        /// <summary>
        /// 禁止群聊
        /// </summary>
        ForbidTeamChat = 28,

        [Description("允许群聊")]
        /// <summary>
        /// 允许群聊
        /// </summary>
        AllowTeamChat = 29,

        [Description("发送文件信息")]
        /// <summary>
        /// 发送文件信息
        /// </summary>
        SendMessageWithFile = 30,

        [Description("主机端登录")]
        /// <summary>
        /// 主机端登录
        /// </summary>
        TeacherLoginIn = 80,

        [Description("主机端登出")]
        /// <summary>
        /// 主机端登出
        /// </summary>
        TeacherLoginOut = 81,

        [Description("开始客户端对主机端演示")]
        /// <summary>
        /// 开始客户端对主机端演示
        /// </summary>
        CallStudentShowForMySelf = 31,

        [Description("开始客户端对主机端演示")]
        /// <summary>
        /// 开始客户端对主机端演示
        /// </summary>
        StudentShowToTeacher = 32,

        [Description("通知客户端发送视频直播")]
        /// <summary>
        /// 通知客户端发送视频直播
        /// </summary>
        CallStudentShowVideoToTeacher = 33,

        [Description("连接服务端心跳命令")]
        /// <summary>
        /// 通知客户端发送视频直播
        /// </summary>
        XinTiao = 34,
        [Description("删除群组成员")]
        /// <summary>
        /// 从群组中删除某个成员
        /// </summary>
        DeleteUserInGroup=35

    }

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
