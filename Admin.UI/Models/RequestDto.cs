using Admin.UI.Utility;
using static Admin.UI.Utility.ApiTypeSD;

namespace Admin.UI.Models
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
    }
}
