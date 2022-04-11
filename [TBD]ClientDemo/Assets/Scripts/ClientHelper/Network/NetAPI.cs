using System;
using Assets.Scripts.ClientHelper.Messages;
using UnityEngine;

namespace Assets.Scripts.ClientHelper.Network
{
    public class NetAPI
    {
        event Action<ResponseMessage> onResult;
        private int _protocolCode;
        public Action<Action<ResponseMessage>> Request(Message requestMessage)
        {
            ClientLancher.instance.onTcpPacket -= OnResult;
            ClientLancher.instance.onTcpPacket += OnResult;
            _protocolCode = requestMessage.GetMessageType();
            ClientLancher.instance.SendMsg(requestMessage);
            return Then;
        }

        public void OnResult(NetData netData)
        {
            if (netData.protocolCode == _protocolCode)
            {
                ResponseMessage responseMessage = JsonHelper.DeserializeObject<ResponseMessage>(netData.msg);
                onResult?.Invoke(responseMessage);
                ClientLancher.instance.onTcpPacket -= OnResult;
            }
        }

        void Then(Action<ResponseMessage> onResultAction){
            onResult = onResultAction;
        }
    }
}