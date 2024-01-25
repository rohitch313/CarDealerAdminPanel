namespace Admin.UI.Utility
{
    public class ApiTypeSD
    {
        public static string UserInfoAPIBase {  get; set; }
        public static string StateAPIBase { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE,
        }
    }
}
