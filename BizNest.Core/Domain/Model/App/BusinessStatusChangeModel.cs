using BizNest.Core.Domain.Entity.App;

namespace BizNest.Core.Domain.Model.App
{
    public class BusinessStatusChangeModel
    {
        public string Message { get; set; }

        public int BusinessId { get; set; }

        public RegistrationStatus Status { get; set; }
    }
}