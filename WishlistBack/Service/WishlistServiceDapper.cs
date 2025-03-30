using AutoMapper;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Security.Principal;
using WishlistBack.Abstract;
using WishlistBack.Model;
using static WishlistBack.Model.Result;

namespace WishlistBack.Service
{
    public class WishlistServiceDapper : IWishlist
    {
        public IConfiguration Configuration { get; }
        public WishlistServiceDapper(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Result CreateWishlist(Wishlist wishlist)
        {
            string error = null;
            string result = null;

            // Проверка входного объекта
            if (wishlist == null)
            {
                return new Result
                {
                    error = "Wishlist object is null.",
                    result = null,
                    status = Status.Error
                };
            }

            // Генерация listLink, если он пуст
            wishlist.listLink = GenerateRandomString();


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
                using (SqlConnection db = new SqlConnection(connectionString))
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Wishlist, Wishlist2>());
                    var mapper = new Mapper(config);
                    var myModel2 = mapper.Map<Wishlist2>(wishlist);

                    DynamicParameters p = new DynamicParameters(myModel2);
                    result = db.ExecuteScalar<string>("pCreateWishlist", p, commandType: CommandType.StoredProcedure);
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

        public Result DeactivateWishlist(int id)
        {
            using (SqlConnection db = new SqlConnection(Configuration["db"]))
            {
                db.Execute("pDeactivateWishlist", new { id = id }, commandType: CommandType.StoredProcedure);
                return new Result
                {
                    status = Status.Ok,
                    result = "ok",
                };
            }
        }

        public Result DeleteWishlist(int id)
        {
            using (SqlConnection db = new SqlConnection(Configuration["db"]))
            {
                db.Execute("pDeleteWishlist", new { id = id }, commandType: CommandType.StoredProcedure);
                return new Result
                {
                    status = Status.Ok,
                    result = "ok",
                };
            }
        }

        public IEnumerable<Wishlist> GetAllWishlists()
        {
            using (SqlConnection db = new SqlConnection(Configuration["db"]))
            {
                return db.Query<Wishlist>("pGetAllWishlists", commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Wishlist> GetWishlistByAccountId(int accountId)
        {
            using (SqlConnection db = new SqlConnection(Configuration["db"]))
            {
                return db.Query<Wishlist>("pGetWishlistByAccountId", new { accountId = accountId }, commandType: CommandType.StoredProcedure);
            }
        }

        public Wishlist GetWishlistById(int id)
        {
            using (SqlConnection db = new SqlConnection(Configuration["db"]))
            {
                return db.Query<Wishlist>("pGetWishlistById", new { id = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public Result UpdateWishlist(Wishlist wishlist)
        {
            string error = null;
            string result = null;
            try
            {
                using (SqlConnection db = new SqlConnection(Configuration["db"]))
                {
                    DynamicParameters p = new DynamicParameters(wishlist);
                    db.Execute("pUpdateWishlist", p, commandType: CommandType.StoredProcedure);
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

        private string GenerateRandomString()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 12);
        }
    }
}
