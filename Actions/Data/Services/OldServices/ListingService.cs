//using Actions.Models.OldModels;
//using Microsoft.EntityFrameworkCore;
////uUjYQSy9b-U8M.s
//namespace Actions.Data.Services
//{
//    public class ListingService : IListingService
//    {
//        private readonly ApplicationDbContext _context;
//        public ListingService(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task Add(Listing listing)
//        {
//            _context.Listings.Add(listing);
//            await _context.SaveChangesAsync();
//        }

//        public IQueryable<Listing> GetAllListings()
//        {
//            var listData = _context.Listings.Include(l => l.User);
//            return listData;
//        }

//        public async Task<Listing> GetListingById(int? Id)
//        {
//            var item = await _context.Listings.Include(i => i.User)
//            .Include(l => l.User)
//            .Include(l => l.Comments)
//            .Include(l => l.Bids)
//            .ThenInclude(l => l.User)
//            .FirstOrDefaultAsync(l => l.Id == Id);
//            return item;
//            // FirstOrDefault(l => l.Id == Id);
//        }


//        //public Listing GetListingById(int? Id)
//        //{
//        //    return _context.Listings.Include(l => l.User).FirstOrDefault(m => m.Id == Id);
//        //}
//    }
//}
