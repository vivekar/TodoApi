using System;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id {get;set;}
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public DateTime CreatedOn { get; set; }        
        public DateTime ModifiedOn { get; set; }        
    }
    
}