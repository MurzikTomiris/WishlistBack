using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WishlistBack.Abstract;
using WishlistBack.Model;
using WishlistBack.Service;

namespace WishlistBack.Controllers
{
    [Route("api/giftcard")]
    [ApiController]
    public class GiftcardController : ControllerBase
    {
        IGiftcard GiftcardServiceDapper;
        public GiftcardController(IGiftcard giftcardServiceDapper)
        {
            GiftcardServiceDapper = giftcardServiceDapper ?? throw new ArgumentNullException(nameof(giftcardServiceDapper));
        }

        [HttpGet, Route("getAllGiftcards")]
        public IEnumerable<Giftcard> GetAllGiftcards()
        {
            return GiftcardServiceDapper.GetAllGiftcards();
        }

        [HttpGet, Route("getGiftcardByWishlistsId/{wishlistId}")]
        public IEnumerable<Giftcard> GetGiftcardByWishlistsId(int wishlistId)
        {
            return GiftcardServiceDapper.GetGiftcardByWishlistsId(wishlistId);
        }

        [HttpGet, Route("GetGiftcardByLink/{link}")]
        public IEnumerable<Giftcard> GetGiftcardByLink(string link)
        {
            return GiftcardServiceDapper.GetGiftcardByLink(link);
        }

        [HttpGet, Route("getGiftcardById/{id}")]
        public Giftcard GetGiftcardById(int id)
        {
            return GiftcardServiceDapper.GetGiftcardById(id);
        }

        [HttpPost, Route("createGiftcard")]
        public Result CreateGiftcard(Giftcard giftcard)
        {
            return GiftcardServiceDapper.CreateGiftcard(giftcard);
        }

        [HttpPut, Route("updateGiftcard")]
        public Result UpdateGiftcard(Giftcard giftcard)
        {
            return GiftcardServiceDapper.UpdateGiftcard(giftcard);
        }

        [HttpDelete, Route("deleteGiftcard/{id}")]
        public Result DeleteGiftcard(int id)
        {
            return GiftcardServiceDapper.DeleteGiftcard(id);
        }

        [HttpGet, Route("reserveGiftcard/{id}")]
        public Result ReserveGiftcard(int id)
        {
            return GiftcardServiceDapper.ReserveGiftcard(id);
        }
    }
}
