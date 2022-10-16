using GymCompanion.Data.Models.General;

namespace GymCompanion.Data.Models.BodyParts
{
    public class GetBodyPartsInfoModel : BaseModel
    {
        public List<BodyPartModel> BodyParts { get; set; } = new();
    }
}
