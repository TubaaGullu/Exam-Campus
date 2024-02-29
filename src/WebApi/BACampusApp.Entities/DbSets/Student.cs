namespace BACampusApp.Entities.DbSets
{
    public class Student : BaseUser
    {
        public Student()
        {

            StudentExams = new HashSet<StudentExam>();
            
        }


        //Navigation property
        public virtual ICollection<StudentExam> StudentExams { get; set; }


    }
}
