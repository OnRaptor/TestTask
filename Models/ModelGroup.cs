using TestTask.Data.Models;

namespace TestTask.Models
{
    public class ModelGroup
    {
        public Brand Brand { get; set; }
        public ICollection<Model>? Models { get; set; }
    }
}
