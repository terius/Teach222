using System.ComponentModel;

namespace Common
{
    /// <summary>
    /// 命令字
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// 用户登陆
        /// </summary>
        [Description("用户登陆")]
        UserLogin = 1,


        /// <summary>
        /// 用户登陆返回信息
        /// </summary>
        [Description("用户登陆返回信息")]
        UserLoginRes = 2,


        /// <summary>
        /// 在线列表
        /// </summary>
        [Description("在线列表")]
        OnlineList = 3,


        /// <summary>
        /// 开始点名
        /// </summary>
        [Description("开始点名")]
        BeginCall = 5,


        /// <summary>
        /// 结束点名
        /// </summary>
        [Description("结束点名")]
        EndCall = 6,


        /// <summary>
        /// 开始屏幕广播
        /// </summary>
        [Description("开始屏幕广播")]
        ScreenInteract = 7,

        /// <summary>
        /// 结束屏幕广播
        /// </summary>
        [Description("结束屏幕广播")]
        StopScreenInteract = 8,


        /// <summary>
        /// 屏幕肃静
        /// </summary>
        [Description("屏幕肃静")]
        Quiet = 9,

        [Description("结束屏幕肃静")]
        /// <summary>
        /// 结束屏幕肃静
        /// </summary>
        StopQuiet = 10,


        /// <summary>
        /// 开始锁屏
        /// </summary>
        [Description("开始锁屏")]
        LockScreen = 11,


        /// <summary>
        /// 结束锁屏
        /// </summary>
        [Description("结束锁屏")]
        StopLockScreen = 12,


        /// <summary>
        /// 私聊
        /// </summary>
        [Description("私聊")]
        PrivateChat = 13,


        /// <summary>
        /// 所有人对话
        /// </summary>
        [Description("所有人对话")]
        GroupChat = 14,

        /// <summary>
        /// 创建群组
        /// </summary>
        [Description("创建群组")]
        CreateTeam = 15,

        /// <summary>
        /// 群聊（分组聊天）
        /// </summary>
        [Description("群聊（分组聊天）")]
        TeamChat = 16,

        /// <summary>
        /// 一个用户登陆进来
        /// </summary>
        [Description("一个用户登陆进来")]
        OneUserLogIn = 17,

        /// <summary>
        /// 用户登出
        /// </summary>
        [Description("用户登出")]
        UserLoginOut = 18,

        /// <summary>
        /// 客户端提交点名
        /// </summary>
        [Description("客户端提交点名")]
        StudentCall = 19,

        [Description("")]
        StudentInMainForm = 20,

        /// <summary>
        /// 开始客户端演示
        /// </summary>
        [Description("开始客户端演示")]
        CallStudentShow = 21,


        ///// <summary>
        ///// 客户端启动演示
        ///// </summary>
        //StudentBeginShow = 22,

        /// <summary>
        /// 停止客户端演示
        /// </summary>
        [Description("停止客户端演示")]
        StopStudentShow = 23,


        ///// <summary>
        ///// 视频直播
        ///// </summary>
        //VideoInteract = 24,
        ///// <summary>
        ///// 结束视频直播
        ///// </summary>
        //StopVideoInteract = 25,

        /// <summary>
        /// 禁止私聊
        /// </summary>
        [Description("禁止私聊")]
        ForbidPrivateChat = 26,

        /// <summary>
        /// 允许私聊
        /// </summary>
        [Description("允许私聊")]
        AllowPrivateChat = 27,

        /// <summary>
        /// 禁止群聊
        /// </summary>
        [Description("禁止群聊")]
        ForbidTeamChat = 28,

        /// <summary>
        /// 允许群聊
        /// </summary>
        [Description("允许群聊")]
        AllowTeamChat = 29,

        /// <summary>
        /// 发送文件信息
        /// </summary>
        [Description("发送文件信息")]
        SendMessageWithFile = 30,

        /// <summary>
        /// 主机端登录
        /// </summary>
        [Description("主机端登录")]
        TeacherLoginIn = 80,

        /// <summary>
        /// 主机端登出
        /// </summary>
        [Description("主机端登出")]
        TeacherLoginOut = 81,

        /// <summary>
        /// 开始客户端对主机端演示
        /// </summary>
        [Description("开始客户端对主机端演示")]
        CallStudentShowForMySelf = 31,


        /// <summary>
        /// 开始客户端对主机端演示
        /// </summary>
        [Description("开始客户端对主机端演示")]
        StudentShowToTeacher = 32,

        /// <summary>
        /// 通知客户端发送视频直播
        /// </summary>
        [Description("通知客户端发送视频直播")]
        CallStudentShowVideoToTeacher = 33,

        /// <summary>
        /// 通知客户端发送视频直播
        /// </summary>
        [Description("连接服务端心跳命令")]
        XinTiao = 34,

        /// <summary>
        /// 从群组中删除某个成员
        /// </summary>
        [Description("删除群组成员")]
        DeleteUserInGroup = 35

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


    public enum CaptureType
    {
        SCREEN_CAPTURE,
        LOCAL_CAMERA
    }
}
