using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WishlistBack.Abstract;
using WishlistBack.Model;
using WishlistBack.Service;

namespace WishlistBack.Controllers
{
    [Route("api/wishlist")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        IWishlist WishlistServiceDapper;
        public WishlistController(IWishlist wishlistServiceDapper)
        {
            WishlistServiceDapper = wishlistServiceDapper ?? throw new ArgumentNullException(nameof(wishlistServiceDapper));
        }

        [HttpGet, Route("getAllWishlists")]
        public IEnumerable<Wishlist> GetAllWishlists()
        {
            return WishlistServiceDapper.GetAllWishlists();
        }

        [HttpGet, Route("getWishlistByAccountId/{accountId}")]
        public IEnumerable<Wishlist> GetWishlistByAccountId(int accountId)
        {
            return WishlistServiceDapper.GetWishlistByAccountId(accountId);
        }

        [HttpGet, Route("getWishlistById/{id}")]
        public Wishlist GetWishlistById(int id)
        {
            return WishlistServiceDapper.GetWishlistById(id);
        }

        [HttpPost, Route("createWishlist")]
        public Result CreateWishlist(Wishlist wishlist)
        {
            return WishlistServiceDapper.CreateWishlist(wishlist);
        }

        [HttpPut, Route("updateWishlist")]
        public Result UpdateWishlist(Wishlist wishlist)
        {
            return WishlistServiceDapper.UpdateWishlist(wishlist);
        }

        [HttpDelete, Route("deleteWishlist/{id}")]
        public Result DeleteWishlist(int id)
        {
            return WishlistServiceDapper.DeleteWishlist(id);
        }

        [HttpGet, Route("deactivateWishlist/{id}")]
        public Result DeactivateWishlist(int id)
        {
            return WishlistServiceDapper.DeactivateWishlist(id);
        }
    }
}
