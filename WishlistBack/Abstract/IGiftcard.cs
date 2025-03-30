using WishlistBack.Model;

namespace WishlistBack.Abstract
{
    public interface IGiftcard
    {
        IEnumerable<Giftcard> GetAllGiftcards();
        IEnumerable<Giftcard> GetGiftcardByWishlistsId(int wishlistId);
        IEnumerable<Giftcard> GetGiftcardByLink(string link);
        Giftcard GetGiftcardById(int id);
        Result CreateGiftcard(Giftcard giftcard);
        Result UpdateGiftcard(Giftcard giftcard);
        Result DeleteGiftcard(int id);
        Result ReserveGiftcard(int id);
    }
}
