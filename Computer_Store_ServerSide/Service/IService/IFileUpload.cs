
using Microsoft.AspNetCore.Components.Forms;

namespace Computer_Store_ServerSide.Service.IService
{
    public interface IFileUpload
    {
        Task<string> UploadFile(IBrowserFile file);
        bool DeleteFile(string filePath);
    }
}
