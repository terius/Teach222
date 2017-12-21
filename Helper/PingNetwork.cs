using System;
using System.Net.NetworkInformation;

namespace Helpers
{
    public class PingNetwork
    {
        /// <summary>
        /// 获取网络状态
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool GetNetworkStatus(out string message)
        {
            string url = "www.baidu.com;www.163.com;www.csdn.com;www.360.com;www.sohu.com";
            string[] urls = url.Split(new char[] { ';' });
            bool networkStatus = CheckServeStatus(urls, out message);
            return networkStatus;
        }

        /// <summary>
        /// 获取服务器网络状态
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool GetServerStatus(string serverUrl, out string message)
        {
            try
            {
                if (!LocalConnectionStatus())
                {
                    message = "网络连接异常，请检查本地网络！";
                    return false;
                }
                Ping ping = new Ping();
                //ping时长设为3秒
                PingReply pr = ping.Send(serverUrl, 3000);
                if (pr.Status != IPStatus.Success)
                {
                    message = "当前与服务器相连的网络不通，请稍后重试或者联系客服人员！";
                    return false;
                }
                message = "服务器连接正常！";
                return true;
            }
            catch
            {
                message = "当前与服务器相连的网络不通，请稍后重试或者联系客服人员！";
                return false;
            }

        }

        /// <summary>
        /// 检测网络连接状态
        /// </summary>
        /// <param name="urls"></param>
        private static bool CheckServeStatus(string[] urls, out string message)
        {
            int errCount = 0; //ping时连接失败个数

            if (!LocalConnectionStatus())
            {
                message = "网络异常，无连接！";
                return false;
            }
            else if (!MyPing(urls, out errCount))
            {
                if ((double)errCount / urls.Length >= 0.8)
                {
                    message = "网络状况差！";
                    return false;
                }
                message = "网络不稳定！";
                return true;
                //else
                //{
                //    message = "网络不稳定！";
                //    return false;
                //}
            }
            else
            {
                message = "网络连接正常！";
            }
            return true;
        }

        #region 网络检测

        private const int INTERNET_CONNECTION_MODEM = 1;
        private const int INTERNET_CONNECTION_LAN = 2;

        [System.Runtime.InteropServices.DllImport("winInet.dll")]
        private static extern bool InternetGetConnectedState(ref int dwFlag, int dwReserved);

        /// <summary>
        /// 判断本地的连接状态
        /// </summary>
        /// <returns></returns>
        private static bool LocalConnectionStatus()
        {
            System.Int32 dwFlag = new Int32();
            if (!InternetGetConnectedState(ref dwFlag, 0))
            {
                //Console.WriteLine("LocalConnectionStatus--未连网!");
                return false;
            }
            else
            {
                if ((dwFlag & INTERNET_CONNECTION_MODEM) != 0)
                {
                    //Console.WriteLine("LocalConnectionStatus--采用调制解调器上网。");
                    return true;
                }
                else if ((dwFlag & INTERNET_CONNECTION_LAN) != 0)
                {
                    //Console.WriteLine("LocalConnectionStatus--采用网卡上网。");
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Ping命令检测网络是否畅通
        /// </summary>
        /// <param name="urls">URL数据</param>
        /// <param name="errorCount">ping时连接失败个数</param>
        /// <returns></returns>
        private static bool MyPing(string[] urls, out int errorCount)
        {
            bool isconn = true;
            Ping ping = new Ping();

            errorCount = 0; //ping时连接失败个数
            try
            {
                PingReply pr;
                for (int i = 0; i < urls.Length; i++)
                {
                    pr = ping.Send(urls[i]);
                    if (pr.Status != IPStatus.Success)
                    {
                        isconn = false;
                        errorCount++;
                    }
                    //Console.WriteLine("Ping " + urls[i] + "    " + pr.Status.ToString());
                }
            }
            catch
            {
                isconn = false;
                errorCount = urls.Length;
            }
            return isconn;
        }
        #endregion
    }
}
