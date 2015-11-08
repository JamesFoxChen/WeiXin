
namespace WeiXinBaseInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = OAuthHelper.GetAccessToken(Config.AppID, Config.AppSecret);
        }
    }
}
