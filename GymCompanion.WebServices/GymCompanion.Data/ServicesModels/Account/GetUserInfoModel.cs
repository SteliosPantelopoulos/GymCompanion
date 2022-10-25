using GymCompanion.Data.Models.General;
using GymCompanion.Data.ServicesModels.Account;

namespace GymCompanion.Data.Models.Account
{
    public class GetUserInfoModel : BaseModel
    {
        public UserInfoModel UserInfo { get; set; }
    }
}
