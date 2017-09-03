using Common;

namespace Model
{
    public class LoginRequest : IsendRequest
    {
      
        public string userName { get; set; }
        public string nickName { get; set; }
        public string password { get; set; }
        public ClientRole ClientRole { get; set; }
    }
}
