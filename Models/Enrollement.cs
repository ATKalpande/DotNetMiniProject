using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DotNetMiniProject.Models
{
    public class Enrollement
    {
        [Key]
        public int Reservation_Id { get; set; }
        public int Customer_Id { get; set; }
        public int Meal_Id { get; set; }
        public string PaymentType { get; set; }
        public int Quantity { get; set; }
        public bool Delivery { get; set; }

        [ForeignKey("User_Id")]
        public virtual User User { get; set; }

        [ForeignKey("Subject_Id")]
        public virtual Subjects Subjects { get; set; }
    }
}
