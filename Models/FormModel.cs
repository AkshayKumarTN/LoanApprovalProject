namespace Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class FormModel
    {
        [Key]
        public int FormId { get; set; }

        [ForeignKey("RegisterModel")]
        public int? UserId { get; set; }
        public virtual RegisterModel RegisterModel { get; set; }

        [Required]
        public string Reason { get; set; }

        [DefaultValue(0)]
        public double ApprovedAmount { get; set; }

        [DefaultValue("pending")]
        public string Status { get; set; }
    }
}
