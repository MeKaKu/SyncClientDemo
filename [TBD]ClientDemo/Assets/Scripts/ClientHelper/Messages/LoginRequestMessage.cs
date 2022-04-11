
namespace Assets.Scripts.ClientHelper.Messages
{
    public class LoginRequestMessage : Message
    {
        public string username;
        public string password;
        public override int GetMessageType(){
            return LoginRequestMessage;
        }
        public LoginRequestMessage(string username, string password){
            this.username = username;
            this.password = password;
        }
    }

    public class ReconnectRequestMessage : Message
    {
        public override int GetMessageType()
        {
            return ReconnectRequestMessage;
        }
    }
}