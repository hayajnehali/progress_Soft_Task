
using System.Collections.Generic;

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
            CreateStamp = DateTime.UtcNow;
            UpdateStamp = DateTime.UtcNow;
        }
    }
    public class BaseFilter
    {
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 1; 

    }
    
    public class PageResult<T> where T : class
    { 
        public int TotalNumberOf { get; set; } = 0;
        public List<T>? Collection { get; set; }
        public List<string>? Error { get; set; }
    
    }

}
