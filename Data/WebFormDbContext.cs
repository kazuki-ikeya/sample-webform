using System.Data.Entity;

namespace WebForm.Data
{
    public class WebFormDbContext : DbContext
    {
        public WebFormDbContext()
            : base("name=DefaultConnection")
        {
        }
    }
}
