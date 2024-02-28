using Microsoft.EntityFrameworkCore;

namespace ISERV.Persistence.EF.Entities
{
    [PrimaryKey("Id")]
    public class University
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Sites { get; set; }
    }
}
