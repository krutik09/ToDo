using System.ComponentModel.DataAnnotations;

namespace AngularAuthAPI.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserID {  get; set; }  
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastEdited { get; set; }

    }
}
