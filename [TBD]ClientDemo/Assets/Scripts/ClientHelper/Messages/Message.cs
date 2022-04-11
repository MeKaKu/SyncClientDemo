
namespace Assets.Scripts.ClientHelper.Messages
{
    public abstract class Message
    {
        public abstract int GetMessageType();

        //心跳
        public static int PumpMessage = 0;

        //-用户模块
        public static int LoginRequestMessage         =  0x00010001; //登录
        public static int LoginResponseMessage        = -0x00010001;
        public static int RegisterRequestMessage      =  0x00010002; //注册
        public static int RegisterResponseMessage     = -0x00010002;

        //-房间模块
        public static int GetRoomListRequestMessage   =  0x00020000; //获取房间列表
        public static int GetRoomListResponseMessage  = -0x00020000;
        public static int CreateRoomRequestMessage    =  0x00020001; //创建房间
        public static int CreateRoomResponseMessage   = -0x00020001;
        public static int RoomReadyRequestMessage     =  0x00020002; //准备
        public static int RoomReadyResponseMessage    = -0x00020002;
        public static int EnterRoomRequestMessage     =  0x00020004; //加入房间
        public static int EnterRoomResponseMessage    = -0x00020004;
        public static int ExitRoomRequestMessage      =  0x00020005; //离开房间
        public static int ExitRoomResponseMessage     = -0x00020005;

        //-场景同步模块
        public static int EnterSceneRequestMessage    =  0x00030001; //进入场景
        public static int EnterSceneResponseMessage   = -0x00030001;
        public static int ItemAuthorityRequestMessage =  0x00030002; //物体操作权限
        public static int EnterSceneInformMessage     =  0x00030003;
        public static int RecordSceneRequestMessage   =  0x00030006; //保存场景
        public static int OfflineMessage              =  0x00030007; //掉线
        public static int ReconnectRequestMessage     =  0x00030008; //重连

        // 物品同步
        public static int SyncCreateRequestMessage    =  0x00031001; //创建物体
        public static int SyncCreateResponseMessage   = -0x00031001;
        public static int SyncAttributeRequestMessage =  0x00031002; //物体属性同步
        public static int SyncPositionRequestMessage  =  0x00031003; //位置同步
        public static int SyncPositionResponseMessage = -0x00031003;

        public static int SceneSnapshotRequestMessage =  0x00040001;
        public static int ObserveRequestMessage       =  0x00040002; //观看请求
        public static int UnobserveRequestMessage     =  0x00040003; //取消观看

        public static int RecordedSceneRequestMessage =  0x00050001; //获取保存的场景列表
        public static int AllSnapshotRequestMessage   =  0x00050002; //获取全部的场景快照
        
    }
}