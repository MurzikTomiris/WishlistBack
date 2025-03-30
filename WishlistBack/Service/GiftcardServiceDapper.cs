using AutoMapper;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using WishlistBack.Abstract;
using WishlistBack.Model;
using static WishlistBack.Model.Result;

namespace WishlistBack.Service
{
    public class GiftcardServiceDapper : IGiftcard
    {
        public IConfiguration Configuration { get; }
        public GiftcardServiceDapper(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Result CreateGiftcard(Giftcard giftcard)
        {
            string error = null;
            string result = null;

            // Проверка входного объекта
            if (giftcard == null)
            {
                return new Result
                {
                    error = "Wishlist object is null.",
                    result = null,
                    status = Status.Error
                };
            }

            // Проверка строки подключения
            string connectionString = Configuration["db"];
            if (string.IsNullOrEmpty(connectionString))
            {
                return new Result
                {
                    error = "Database connection string is null or empty.",
                    result = null,
                    status = Status.Error
                };
            }

            try
            {
                using (SqlConnection db = new SqlConnection(Configuration["db"]))
                {

                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Giftcard, Giftcard2>());
                    var mapper = new Mapper(config);
                    var myModel2 = mapper.Map<Giftcard2>(giftcard);

                    DynamicParameters p = new DynamicParameters(myModel2);
                    result = db.ExecuteScalar<string>("pCreateGiftcard", p, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception err)
            {
                error = err.Message;
            }
            return new Result
            {
                error = error,
                result = result,
                status = result == "ok" ? Status.Ok : Status.Error
            };
        }

        public Result DeleteGiftcard(int id)
        {
            using (SqlConnection db = new SqlConnection(Configuration["db"]))
            {
                db.Execute("pDeleteGiftcard", new { id = id }, commandType: CommandType.StoredProcedure);
                return new Result
                {
                    status = Status.Ok,
                    result = "ok",
                };
            }
        }

        public IEnumerable<Giftcard> GetAllGiftcards()
        {
            using (SqlConnection db = new SqlConnection(Configuration["db"]))
            {
                return db.Query<Giftcard>("pGetAllGiftcards", commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Giftcard> GetGiftcardByWishlistsId(int wishlistId)
        {
            using (SqlConnection db = new SqlConnection(Configuration["db"]))
            {
                return db.Query<Giftcard>("pGetGiftcardByWishlistsId", new { wishlistId = wishlistId }, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Giftcard> GetGiftcardByLink(string link)
        {
            using (SqlConnection db = new SqlConnection(Configuration["db"]))
            {
                return db.Query<Giftcard>("pGetGiftcardByLink", new { listLink = link }, commandType: CommandType.StoredProcedure);
            }
        }

        public Giftcard GetGiftcardById(int id)
        {
            using (SqlConnection db = new SqlConnection(Configuration["db"]))
            {
                return db.Query<Giftcard>("pGetGiftcardById", new { id = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public Result ReserveGiftcard(int id)
        {
            using (SqlConnection db = new SqlConnection(Configuration["db"]))
            {
                db.Execute("pReserveGiftcard", new { id = id }, commandType: CommandType.StoredProcedure);
                return new Result
                {
                    status = Status.Ok,
                    result = "ok",
                };
            }
        }

        public Result UpdateGiftcard(Giftcard giftcard)
        {
            string error = null;
            string result = null;
            try
            {
                using (SqlConnection db = new SqlConnection(Configuration["db"]))
                {
                    DynamicParameters p = new DynamicParameters(giftcard);
                    db.Execute("pUpdateGiftcard", p, commandType: CommandType.StoredProcedure);
                    result = "ok";
                }
            }
            catch (Exception err)
            {
                error = err.Message;
            }
            return new Result
            {
                error = error,
                result = result,
                status = result == "ok" ? Status.Ok : Status.Error
            };
        }
    }
}
