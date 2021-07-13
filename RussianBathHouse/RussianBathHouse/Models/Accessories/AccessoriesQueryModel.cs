using System.Collections.Generic;

namespace RussianBathHouse.Models.Accessories
{
    public class AccessoriesQueryModel
    {
        public const int AccessoriesPerPage = 3;

        public IEnumerable<AccessoriesAllViewModel> Accessories { get; set; }

        public int TotalAccessories { get; set; }

        public int CurrentPage { get; set; } = 1;

        public AccessoriesSorting Sorting { get; set; }
        
        public string SearchTerm { get; set; }
    }
}
