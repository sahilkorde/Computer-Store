using Microsoft.JSInterop;
namespace Computer_Store_ClientStore.Helper
{
    public static class IJsRuntileExtention
    {
        public static async ValueTask ToasterSuccess(this IJSRuntime jSRuntime, string message)
        {
            await jSRuntime.InvokeVoidAsync("ShowToastr", "success", message);
        }
        public static async ValueTask ToastrError(this IJSRuntime jSRuntime, string message)
        {
            await jSRuntime.InvokeVoidAsync("ShowToastr", "error", message);
        }
    }
}
