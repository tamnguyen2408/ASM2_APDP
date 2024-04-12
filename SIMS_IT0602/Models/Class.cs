
namespace SIMS_IT0602.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string Major { get; set; }
        public string Lecturer { get; set; }

        internal object FirstOrDefault(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
