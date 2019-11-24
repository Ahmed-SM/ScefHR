using System.ComponentModel.DataAnnotations;

namespace ScefHR.Models
{
    public class FormField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}