

namespace VIVU.Intergration.Interface
{
    public interface IUserService
    {
        Task<CommonResponseModel<UserLoginDataTransferObject>> Login(LoginRequest request);
    }
}
