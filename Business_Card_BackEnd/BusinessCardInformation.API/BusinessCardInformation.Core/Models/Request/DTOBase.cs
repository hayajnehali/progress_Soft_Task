
namespace BusinessCardInformation.Core.Models.Request
{
    public abstract class DTOBase
    {
        public int? Id { get; set; }
        public DateTime? CreateStamp { get; set; }
        public DateTime? UpdateStamp { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdateBy { get; set; } 
        public bool IsDeleted { get; set;}=false;
        public bool? IsActive { get; set;}

        protected DTOBase()
        {
            CreateStamp = DateTime.UtcNow; // Set to current UTC time
            UpdateStamp = DateTime.UtcNow; // Set to current UTC time
        }
    }
    public abstract class ModelBaseFilter
    {
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 1;
    
    }
}
