
namespace Assets.Scripts.ClientHelper.Messages
{
    public class ResponseMessage : Message
    {
        public bool result;
        public object data;

        public ResponseMessage(){}

        public ResponseMessage(bool result, object data){
            this.result = result;
            this.data = data;
        }

        public override int GetMessageType()
        {
            return 0;
        }

        public override string ToString(){
            return $"请求结果:{result},返回数据:{data}";
        }
    }
}