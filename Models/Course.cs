using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetMiniProject.Models
{
    public class Course
    {
        public int Id { get; set; }

        public int Field_Id { get; set; }

        [ForeignKey("Field_Id ")]
        public virtual Field Field { get; set; }

        public virtual ICollection<Subjects> Subjects { get; set; } = new HashSet<Subjects>();
    }
}
