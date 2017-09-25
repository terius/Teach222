using Common;

namespace Model
{
    public abstract class BaseChatRequest: SendFileView, IChatRequest
    {
        //   public string receivename { get; set; }

        public string SendUserName { get; set; }

        public string msg { get; set; }
        public string SendDisplayName { get; set; }

        public ClientRole clientRole { get; set; }
 

    }


    public abstract class SendFileView
    {
        public string DownloadFileUrl { get; set; }
        public MessageType MessageType { get; set; }

        public string LocalFilePath { get; set; }
    }
}
