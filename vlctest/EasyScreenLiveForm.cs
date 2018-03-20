using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vlctest
{
    public partial class EasyScreenLiveForm : Form
    {
        public EasyScreenLiveForm()
        {
            InitializeComponent();
        }

        string EASY_RTSP_KEY = "79397037795969576B5A7341596A5261706375647066464659584E355548567A614756794C6D56345A534E58444661672F365867523246326157346D516D466962334E68514449774D545A4659584E355247467964326C75564756686257566863336B3D";

        string EASY_RTMP_KEY = "79397037795969576B5A75416D7942617064396A4575314659584E3555324E795A57567554476C325A53356C65475570567778576F50365334456468646D6C754A6B4A68596D397A595541794D4445325257467A65555268636E6470626C526C5957316C59584E35";

        string EASY_IPC_KEY = "6D72754B7A4969576B5A75416D7942617064396A4575314659584E3555324E795A57567554476C325A53356C65475570567778576F50365334456468646D6C754A6B4A68596D397A595541794D4445325257467A65555268636E6470626C526C5957316C59584E35";

        public enum tagSOURCE_TYPE
        {

            /// SOURCE_LOCAL_CAMERA -> 0
            SOURCE_LOCAL_CAMERA = 0,

            /// SOURCE_SCREEN_CAPTURE -> 1
            SOURCE_SCREEN_CAPTURE = 1,

            /// SOURCE_FILE_STREAM -> 2
            SOURCE_FILE_STREAM = 2,

            /// SOURCE_RTSP_STREAM -> 3
            SOURCE_RTSP_STREAM = 3,

            /// SOURCE_RTMP_STREAM -> 4
            SOURCE_RTMP_STREAM = 4,
        }

        public enum tagPUSH_TYPE
        {

            /// PUSH_NONE -> 0
            PUSH_NONE = 0,

            PUSH_RTSP,

            PUSH_RTMP,
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        public struct tagEASYLIVE_DEVICE_INFO_T
        {

            /// char[64]
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 64)]
            public string friendlyName;

            /// tagEASYLIVE_DEVICE_INFO_T*
            public System.IntPtr pNext;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct tagEASYLIVE_DEVICE_LIST_T
        {

            /// int
            public int count;

            /// EASYLIVE_DEVICE_INFO_T*
            public System.IntPtr pDevice;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        public struct EASYLIVE_CHANNEL_INFO_T
        {

            /// int
            public int id;

            /// char[64]
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 64)]
            public string name;

            /// int
            public int enable_multicast;

            /// char[36]
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 36)]
            public string multicast_addr;

            /// unsigned char
            public byte ttl;
        }



        /// Return Type: void*
        ///EasyIPCamera_Key: char*
        ///EasyRTMP_Key: char*
        ///EasyRTSP_Key: char*
        [System.Runtime.InteropServices.DllImportAttribute("libEasyScreenLive.dll", EntryPoint = "?EasyScreenLive_Create@@YAPAXPAD00@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern System.IntPtr EasyScreenLive_Create(string EasyIPCamera_Key, string EasyRTMP_Key, string EasyRTSP_Key);


        /// Return Type: void
        ///handler: void*
        [System.Runtime.InteropServices.DllImportAttribute("libEasyScreenLive.dll", EntryPoint = "EasyScreenLive_Release")]
        public static extern void EasyScreenLive_Release(System.IntPtr handler);

        [System.Runtime.InteropServices.DllImportAttribute("libEasyScreenLive.dll", EntryPoint = "?EasyScreenLive_StartCapture@@YAHPAXW4tagSOURCE_TYPE@@HH0HHHHHPADHH@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern System.IntPtr EasyScreenLive_StartCapture(
            System.IntPtr handler, tagSOURCE_TYPE eSourceType,
            int nCamId,
            int nAudioId,
            IntPtr hCapWnd,
            int nEncoderType,
            int nVideoWidth = 640,
            int nVideoHeight = 480,
            int nFps = 25,
            int nBitRate = 2048,
            string szDataType = "YUY2",  //VIDEO PARAM
            int nSampleRate = 44100,
            int nChannel = 2
            );//AUDIO PARAM



        /// Return Type: void
        ///handler: void*
        [System.Runtime.InteropServices.DllImportAttribute("libEasyScreenLive.dll", EntryPoint = "EasyScreenLive_StopCapture")]
        public static extern void EasyScreenLive_StopCapture(System.IntPtr handler);



        [System.Runtime.InteropServices.DllImportAttribute("libEasyScreenLive.dll", EntryPoint = "?EasyScreenLive_StartPush@@YAHPAXW4tagPUSH_TYPE@@PADH2H@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern void EasyScreenLive_StartPush(System.IntPtr handler, tagPUSH_TYPE pushType, string ServerIp, int nPushPort, string sPushName, int nPushBufSize = 1024);




        /// Return Type: void
        ///handler: void*
        ///pushType: PUSH_TYPE->tagPUSH_TYPE
        [System.Runtime.InteropServices.DllImportAttribute("libEasyScreenLive.dll", EntryPoint = "EasyScreenLive_StopPush")]
        public static extern void EasyScreenLive_StopPush(System.IntPtr handler, tagPUSH_TYPE pushType);


        /// Return Type: int
        ///handler: void*
        ///listenport: int
        ///username: char*
        ///password: char*
        ///channelInfo: EASYLIVE_CHANNEL_INFO_T*
        ///channelNum: int
        [System.Runtime.InteropServices.DllImportAttribute("libEasyScreenLive.dll", EntryPoint = "EasyScreenLive_StartServer")]
        public static extern int EasyScreenLive_StartServer(System.IntPtr handler, int listenport, System.IntPtr username, System.IntPtr password, ref EASYLIVE_CHANNEL_INFO_T channelInfo, int channelNum);


        /// Return Type: void
        ///handler: void*
        [System.Runtime.InteropServices.DllImportAttribute("libEasyScreenLive.dll", EntryPoint = "EasyScreenLive_StopServer")]
        public static extern void EasyScreenLive_StopServer(System.IntPtr handler);


        /// Return Type: EASYLIVE_DEVICE_LIST_T*
        ///handler: void*
        [System.Runtime.InteropServices.DllImportAttribute("libEasyScreenLive.dll", EntryPoint = "EasyScreenLive_GetAudioInputDevList")]
        public static extern System.IntPtr EasyScreenLive_GetAudioInputDevList(System.IntPtr handler);


        /// Return Type: EASYLIVE_DEVICE_LIST_T*
        ///handler: void*
        [System.Runtime.InteropServices.DllImportAttribute("libEasyScreenLive.dll", EntryPoint = "EasyScreenLive_GetCameraList")]
        public static extern System.IntPtr EasyScreenLive_GetCameraList(System.IntPtr handler);

        IntPtr m_pusher;

        private void EasyScreenLiveForm_Load(object sender, EventArgs e)
        {
            m_pusher = EasyScreenLive_Create(EASY_IPC_KEY, EASY_RTMP_KEY, EASY_RTSP_KEY);

            var ret = EasyScreenLive_StartCapture(m_pusher, tagSOURCE_TYPE.SOURCE_SCREEN_CAPTURE, 0, 0, this.panel2.Handle, 0, 640, 480, 25, 2048, "RGB24");
        }

        private void btnStartPush_Click(object sender, EventArgs e)
        {
            EasyScreenLive_StartPush(m_pusher, tagPUSH_TYPE.PUSH_RTSP, "139.196.217.92", 10554, "terius");
        }
    }
}
