using Computer_Store_ClientStore.ViewModel;
using Computer_Store_Models;
using System.ComponentModel.Design;

namespace Computer_Store_ClientStore.Service.IService
{
    public interface IPaymentService
    {
        public Task<SuccessModelDTO> Checkout(StripePaymentDTO model);
    }
}
