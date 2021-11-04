namespace Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PropertyModel
    {
        [Key]
        public int PropertyId { get; set; }

        
        [ForeignKey("RegisterModel")]
        public int? UserId { get; set; }
        public virtual RegisterModel RegisterModel { get; set; }

        
        [ForeignKey("FormModel")]
        public int? FormId { get; set; }
        public virtual FormModel FormModel { get; set; }

        [Required]
        public string PropertyName { get; set; }
        
        [Required]
        public string PropertyWorth { get; set; }

    }
}
