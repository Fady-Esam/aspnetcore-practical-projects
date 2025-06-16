using Actions.Models.OldModels;

namespace Actions.Data.Services.OldServices
{
    public interface IListingService
    {
        IQueryable<Listing> GetAllListings();
        Task Add(Listing listing);
        Task<Listing> GetListingById(int? Id);
    }
}
