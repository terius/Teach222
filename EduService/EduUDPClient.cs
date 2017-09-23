using Common;
using Helpers;
using Model;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace EduService
{
    public class EduUDPClient
    {
        readonly int udpPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["UDPPort"]);
        readonly string serverIP = System.Configuration.ConfigurationManager.AppSettings["serverIP"];
        readonly string teacherIP = System.Configuration.ConfigurationManager.AppSettings["TeacherIP"];
        UdpClient studentUdpClient;
        UdpClient teacherUdpClient;
        IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        string remoteIp;
        int remotePort;
        ManualResetEvent sendDone = new ManualResetEvent(false);
        ProgramType _programType;
        public Action<ScreenCaptureInfo> OnTeacherReceiveUDP;

        /// <summary>
        /// 服务端是否为局域网
        /// </summary>
        public bool IsLocalNetwork
        {
            get
            {
                return true;//只用于测试
                if (serverIP.StartsWith("192.168"))
                {
                    return true;
                }
                return false;
            }
        }

        public EduUDPClient(ProgramType programType)
        {
            _programType = programType;
            if (_programType == ProgramType.Teacher)
            {
                CreateTeacherUDPClient();
            }
            else
            {
                CreateStudentUDPClient();
            }
            remoteIp = serverIP;
            remotePort = 55556;

        }

        #region 主机端UDP
        private void CreateTeacherUDPClient()
        {
            if (teacherUdpClient == null)
            {
                IPEndPoint fLocalIPEndPoint = new IPEndPoint(IPAddress.Any, udpPort);
                teacherUdpClient = new UdpClient(fLocalIPEndPoint);
                teacherUdpClient.Client.ReceiveBufferSize = 4096;
                uint IOC_IN = 0x80000000;
                uint IOC_VENDOR = 0x18000000;
                uint SIO_UDP_CONNRESET = IOC_IN | IOC_VENDOR | 12;
                teacherUdpClient.Client.IOControl((int)SIO_UDP_CONNRESET, new byte[] { Convert.ToByte(false) }, null);
            }

        }

        public void CloseTeacherUDP()
        {
            if (teacherUdpClient != null)
            {
                teacherUdpClient.Close();
            }
        }

        public void CreateUDPTeacherHole()
        {

            try
            {
                // if (!IsLocalNetwork)
                // {
                byte[] fHelloData = Encoding.UTF8.GetBytes("TEACHER");
                teacherUdpClient.Send(fHelloData, fHelloData.Length, remoteIp, remotePort);
                // }
                ScreenCaptureInfo screenInfo;
                while (true)
                {
                    var receiveBytes = teacherUdpClient.Receive(ref RemoteIpEndPoint);
                    //   Loger.LogMessage("接收到udp信息，长度：" + receiveBytes.Length);
                    if (receiveBytes.Length > 100)
                    {
                        screenInfo = GetScreen(receiveBytes);
                        OnTeacherReceiveUDP?.Invoke(screenInfo);
                    }
                    Thread.Sleep(200);
                }
                //    teacherUdpClient.BeginReceive(new AsyncCallback(TeacherReceiveUDPCallback), null);
            }
            catch (Exception ex)
            {
                Loger.LogMessage(ex.ToString());
            }


        }

        private ScreenCaptureInfo GetScreen(byte[] receiveBytes)
        {
            int tempOffset = 0;
            var lenBytes = new byte[4];
            Array.Copy(receiveBytes, tempOffset, lenBytes, 0, 4);
            int alllength = BitConverter.ToInt32(lenBytes, 0);
            tempOffset += 4;
            Array.Copy(receiveBytes, tempOffset, lenBytes, 0, 4);
            int userLength = BitConverter.ToInt32(lenBytes, 0);
            var userBytes = new byte[userLength];
            tempOffset += 4;
            Array.Copy(receiveBytes, tempOffset, userBytes, 0, userLength);
            string userString = Encoding.UTF8.GetString(userBytes);
            ScreenCaptureInfo info = JsonHelper.DeserializeObj<ScreenCaptureInfo>(userString);
            tempOffset += userLength;
            Array.Copy(receiveBytes, tempOffset, lenBytes, 0, 4);
            int imgLength = BitConverter.ToInt32(lenBytes, 0);
            var imgBytes = new byte[imgLength];
            tempOffset += 4;
            Array.Copy(receiveBytes, tempOffset, imgBytes, 0, imgLength);
            info.Image = FileHelper.ByteArrayToImage(imgBytes);
            return info;
        }

        #endregion


        #region 客户端UDP
        private void CreateStudentUDPClient()
        {
            if (studentUdpClient == null)
            {
                IPEndPoint fLocalIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
                studentUdpClient = new UdpClient(fLocalIPEndPoint);
                studentUdpClient.Client.ReceiveBufferSize = 4096;
                uint IOC_IN = 0x80000000;
                uint IOC_VENDOR = 0x18000000;
                uint SIO_UDP_CONNRESET = IOC_IN | IOC_VENDOR | 12;
                studentUdpClient.Client.IOControl((int)SIO_UDP_CONNRESET, new byte[] { Convert.ToByte(false) }, null);
                //if (IsLocalNetwork)
                //{
                //    remoteIp = teacherIP;
                //    remotePort = udpPort;
                //}
            }



        }

        public void SendDesktopPic(byte[] fileBytes)
        {
            //if (studentUdpClient == null)
            //{
            //    studentUdpClient = new UdpClient();
            //    var remoteEP = new IPEndPoint(IPAddress.Parse(teacherIp), 10888);
            //    studentUdpClient.Connect(remoteEP);
            //}
            //var fileBytes = FileHelper.FileToByteArray(fileName);
            //  var fHelloData = Encoding.UTF8.GetBytes("hello1111" + DateTime.Now.Ticks);
            //    Loger.LogMessage("发送图片，地址：" + remoteIp + ":" + remotePort + "   长度：" + fileBytes.Length);
            studentUdpClient.Send(fileBytes, fileBytes.Length, remoteIp, remotePort);
            // Loger.LogMessage("SendDesktopPic-------------------");
            //studentUdpClient.BeginSend(fileBytes, fileBytes.Length, remoteIp, remotePort, (result) =>
            //  {
            //      if (result.IsCompleted)
            //      {
            //          Thread.Sleep(2000);
            //          Loger.LogMessage("SendDesktopPic:");
            //          sendDone.Set();
            //      }
            //  }, null);
            //   sendDone.WaitOne();
        }

        public void CloseStudentUDP()
        {
            if (studentUdpClient != null)
            {
                studentUdpClient.Close();
            }
        }

        public void CreateUDPStudentHole()
        {

            if (IsLocalNetwork)
            {
                return;
            }

            byte[] fHelloData = Encoding.UTF8.GetBytes("STUDENT");
            studentUdpClient.Send(fHelloData, fHelloData.Length, serverIP, udpPort);
            Byte[] fData = studentUdpClient.Receive(ref RemoteIpEndPoint);

            if (fData.Length > 0)
            {//数据接收成功,放入缓存
             // IPEndPoint fClientIPEndPoint = null;
                string fContent = Encoding.UTF8.GetString(fData);
                string fIPAddress = RemoteIpEndPoint.Address.ToString() + ":" + RemoteIpEndPoint.Port;
                Loger.LogMessage("源地址： " + fIPAddress + "   内容：" + fContent);
                if (fContent.Contains(":"))
                {
                    remoteIp = fContent.Substring(1, fContent.LastIndexOf(":") - 1);
                    remotePort = Convert.ToInt32(fContent.Substring(fContent.LastIndexOf(":") + 1));
                    Loger.LogMessage("主机端地址： " + remoteIp + ":" + remotePort);

                    fHelloData = Encoding.UTF8.GetBytes("hello" + DateTime.Now.Ticks);
                    studentUdpClient.Send(fHelloData, fHelloData.Length, remoteIp, remotePort);
                    Loger.LogMessage("发送地址： " + remoteIp + ":" + remotePort + " 内容长度：" + fHelloData.Length);
                    //while (true)
                    //{
                    //    fHelloData = Encoding.UTF8.GetBytes("hello" + DateTime.Now.Ticks);
                    //    studentUdpClient.Send(fHelloData, fHelloData.Length, remoteIp, remotePort);
                    //    Loger.LogMessage("发送udp信息： " + fHelloData.Length);
                    //    Thread.Sleep(5000);
                    //}


                }

            }

            //studentUdpClient.BeginReceive(new AsyncCallback(StudentReceiveUDPCallback), null);

            //var uClient = new UdpClient();
            //var remoteEP = new IPEndPoint(IPAddress.Parse(serverIP), udpPort);
            //uClient.Connect(remoteEP);
            //var bt = Encoding.UTF8.GetBytes("STUDENT");
            //uClient.Send(bt, bt.Length);
        }
        #endregion




        private void SendMessage(string data, string ip, int port)
        {
            byte[] fData = Encoding.UTF8.GetBytes(data);
            if (_programType == ProgramType.Student)
            {
                studentUdpClient.Send(fData, fData.Length, ip, port);
            }
            else
            {
                teacherUdpClient.Send(fData, fData.Length, ip, port);
            }
            Console.WriteLine("发送地址" + ip + ":" + port + "   " + (fData.Length > 1000 ? "内容长度：" + fData.Length : "内容：" + data));
        }
        public void WaitUdp()
        {
            sendDone.WaitOne();
        }


        #region OLD
        //private void TeacherReceiveUDPCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        if (teacherUdpClient.Client != null)
        //        {
        //            IPEndPoint fClientIPEndPoint = null;
        //            byte[] fData = teacherUdpClient.EndReceive(ar, ref fClientIPEndPoint);
        //            if (fData.Length > 0)
        //            {//数据接收成功,放入缓存
        //                Loger.LogMessage("接收到udp信息，长度：" + fData.Length);
        //                if (fData.Length > 100)
        //                {
        //                    var info = GetScreen(fData);
        //                    OnTeacherReceiveUDP(info);
        //                }
        //                //string fContent = Encoding.UTF8.GetString(fData);
        //                //string fIPAddress = fClientIPEndPoint.Address.ToString() + ":" + fClientIPEndPoint.Port;
        //                //Loger.LogMessage("源地址： " + fIPAddress + "   内容：" + fContent);
        //                //if (fContent.Contains(":"))
        //                //{
        //                //    remoteIp = fContent.Substring(1, fContent.LastIndexOf(":") - 1);
        //                //    remotePort = Convert.ToInt32(fContent.Substring(fContent.LastIndexOf(":") + 1));
        //                //    byte[] fHelloData = Encoding.UTF8.GetBytes("hello");
        //                //    teacherUdpClient.Send(fHelloData, fHelloData.Length, remoteIp, remotePort);
        //                //}

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Loger.LogMessage(ex.ToString());
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            teacherUdpClient.BeginReceive(new AsyncCallback(TeacherReceiveUDPCallback), null);
        //        }
        //        catch (Exception ex)
        //        {
        //            Loger.LogMessage(ex.ToString());
        //        }
        //    }
        //}

        //private void StudentReceiveUDPCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        if (studentUdpClient.Client != null)
        //        {
        //            IPEndPoint fClientIPEndPoint = null;
        //            byte[] fData = studentUdpClient.EndReceive(ar, ref fClientIPEndPoint);
        //            if (fData.Length > 0)
        //            {//数据接收成功,放入缓存
        //                string fContent = Encoding.UTF8.GetString(fData);
        //                string fIPAddress = fClientIPEndPoint.Address.ToString() + ":" + fClientIPEndPoint.Port;
        //                Loger.LogMessage("源地址： " + fIPAddress + "   内容：" + fContent);
        //                if (fContent.Contains(":"))
        //                {
        //                    remoteIp = fContent.Substring(1, fContent.LastIndexOf(":") - 1);
        //                    remotePort = Convert.ToInt32(fContent.Substring(fContent.LastIndexOf(":") + 1));
        //                    Loger.LogMessage("主机端地址： " + remoteIp + ":" + remotePort);
        //                    //while (true)
        //                    //{
        //                    //    byte[] fHelloData = Encoding.UTF8.GetBytes("hello" + DateTime.Now.Ticks);
        //                    //    studentUdpClient.Send(fHelloData, fHelloData.Length, remoteIp, remotePort);
        //                    //    Thread.Sleep(5000);
        //                    //}


        //                }

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Loger.LogMessage(ex.ToString());
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            studentUdpClient.BeginReceive(new AsyncCallback(TeacherReceiveUDPCallback), null);
        //        }
        //        catch (Exception ex)
        //        {
        //            Loger.LogMessage(ex.ToString());
        //        }
        //    }
        //}

        //public ScreenCaptureInfo GetReceieveDesktopInfo()
        //{
        //    if (teacherUdpClient == null)
        //    {
        //        teacherUdpClient = new UdpClient(10888);
        //    }

        //    Byte[] receiveBytes = teacherUdpClient.Receive(ref RemoteIpEndPoint);
        //    return GetScreen(receiveBytes);
        //}

        #endregion
    }
}
