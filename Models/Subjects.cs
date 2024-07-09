using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetMiniProject.Models
{
    public class Subjects
    {
        public int Id { get; set; }
        public string SubName { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int Course_Id { get; set; }

        [ForeignKey("Course_Id")]
        public virtual Course Course { get; set; }
    }
}
