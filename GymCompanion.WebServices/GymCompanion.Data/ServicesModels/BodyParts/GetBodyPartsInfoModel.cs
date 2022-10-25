using GymCompanion.Data.Models.General;

namespace GymCompanion.Data.Models.BodyParts
{
    public class GetBodyPartsInfoModel
    {
        public List<BodyPartModel> BodyParts { get; set; } = new();
    }
}
