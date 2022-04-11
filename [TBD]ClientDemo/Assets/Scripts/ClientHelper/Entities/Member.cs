namespace Assets.Scripts.ClientHelper.Entities
{
    [System.Serializable]
    public class Member
    {
        public string username;
        public bool isReady;

        public Member(string username, bool isReady){
            this.username = username;
            this.isReady = isReady;
        }
    }
}