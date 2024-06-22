using BethanysPieShop.Models;

namespace BethanysPieShop.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Pie> PiesOfWeek{ get; }

        public HomeViewModel(IEnumerable<Pie> piesOfWeek)
        {
            PiesOfWeek= piesOfWeek;
        }
    }
}
