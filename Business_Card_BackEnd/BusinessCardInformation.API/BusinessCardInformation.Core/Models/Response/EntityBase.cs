
namespace BusinessCardInformation.Core.Models.Response
{
    public abstract class EntityBase
    {
        public int? Id { get; }
        public DateTime? CreateStamp { get; private set; }
        public DateTime? UpdateStamp { get; set; } 
        public int? CreatedBy { get; set; }
        public int? UpdateBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool? IsActive { get; set;}

        protected EntityBase()
        {
            CreateStamp = DateTime.UtcNow; // Set to current UTC time
            UpdateStamp = DateTime.UtcNow; // Set to current UTC time
        }
    }
}
