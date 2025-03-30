using WishlistBack.Model;

namespace WishlistBack.Abstract
{
    public interface IWishlist
    {
        IEnumerable<Wishlist> GetAllWishlists();
        IEnumerable<Wishlist> GetWishlistByAccountId(int accountId);
        Wishlist GetWishlistById(int id);
        Result CreateWishlist(Wishlist wishlist);
        Result UpdateWishlist(Wishlist wishlist);
        Result DeleteWishlist(int id);
        Result DeactivateWishlist(int id);
    }
}
