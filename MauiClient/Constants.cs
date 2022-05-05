namespace MauiClient
{
    public static class Constants
    {
        public static string ApiUri { get; set; } = "https://ngrok...";

        public static string[] B2cScopes { get; set; } = new string[] { "https://goldiessw.onmicrosoft.com/maui-client/access_as_user" };

        public static string B2cTenantId { get; set; } = "22836d53-fdb2-46c1-be8b-5d81e4059afc";

        public static string B2cClientId { get; set; } = "f63ea4d6-3c35-4253-bb60-71097b4da999";

        public static string B2cPolicy { get; set; } = "B2C_1_v2SignupAndSignin";

        public static string AuthorityUri { get; set; } = "https://goldiessw.b2clogin.com/tfp/goldiessw.onmicrosoft.com/B2C_1_v2SignupAndSignin";

        public static string AccessToken { get; set; }
    }
}
