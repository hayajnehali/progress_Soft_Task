using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardInformation.Core.Sheard
{
    public class OperationResult<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; } // To store errors
        public T? Data { get; set; }
        public int TotalNumberOf {  get; set; }= 0;
    }
}
