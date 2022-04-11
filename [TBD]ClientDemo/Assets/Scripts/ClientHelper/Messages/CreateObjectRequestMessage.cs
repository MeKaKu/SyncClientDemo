
namespace Assets.Scripts.ClientHelper.Messages
{
    public class CreateObjectRequestMessage : Message
    {
        public override int GetMessageType()
        {
            return SyncCreateRequestMessage;
        }
    }
}