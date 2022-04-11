namespace Assets.Scripts.ClientHelper.Messages
{
    public class PumpMessage : Message
    {
        public override int GetMessageType()
        {
            return PumpMessage;
        }
    }
}