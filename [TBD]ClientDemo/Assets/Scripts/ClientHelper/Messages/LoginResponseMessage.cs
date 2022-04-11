
namespace Assets.Scripts.ClientHelper.Messages
{
    public class LoginResponseMessage : ResponseMessage
    {
        public LoginResponseMessage(bool result, object data) : base(result, data){}

        public override int GetMessageType()
        {
            return LoginResponseMessage;
        }
    }
}