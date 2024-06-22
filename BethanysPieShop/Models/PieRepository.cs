
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    public class PieRepository:IPieRepository
    {
        public readonly BethanysPieShopDbContext bethanysPieShopDbContext;

        public PieRepository(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            this.bethanysPieShopDbContext = bethanysPieShopDbContext;
        }

        public IEnumerable<Pie> AllPies
        {
            get {
                return bethanysPieShopDbContext.Pies.Include(c => c.Category);
            
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get {
            return bethanysPieShopDbContext.Pies.Include(c=>c.Category).Where(p=>p.IsPieOfTheWeek);
            }
        }

        public Pie? GetPieById(int id)
        {
            return bethanysPieShopDbContext.Pies.FirstOrDefault(p=>p.PieId == id);  
        }
    }
}
