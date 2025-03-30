using AutoMapper;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using WishlistBack.Abstract;
using WishlistBack.Model;
using static WishlistBack.Model.Result;

namespace WishlistBack.Service
{
    public class AccountServiceDapper : IAccount
    {

        public IConfiguration Configuration { get; }
        public AccountServiceDapper(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Result CreateAccount(Account account)
        {
            string error = null;
            string result = null;

            // Проверка входного объекта
            if (account == null)
            {
                return new Result
                {
                    error = "Account object is null.",
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

                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Account, Account2>());
                    var mapper = new Mapper(config);
                    var myModel2 = mapper.Map<Account2>(account);

                    DynamicParameters p = new DynamicParameters(myModel2);
                    result = db.ExecuteScalar<string>("pCreateAccount", p, commandType: CommandType.StoredProcedure);

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

        public Result DeleteAccount(int id)
        {
            using (SqlConnection db = new SqlConnection(Configuration["db"]))
            {
                db.Execute("pDeleteAccount", new { id = id }, commandType: CommandType.StoredProcedure);
                return new Result
                {
                    status = Status.Ok,
                    result = "ok",
                };
            }
        }

        public Account GetAccountById(int id)
        {
            using (SqlConnection db = new SqlConnection(Configuration["db"]))
            {
                return db.Query<Account>("pGetAccountById", new { id = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            using (SqlConnection db = new SqlConnection(Configuration["db"]))
            {
                return db.Query<Account>("pGetAllAccounts", commandType: CommandType.StoredProcedure);
            }
        }

        public Result UpdateAccount(Account account)
        {
            string error = null;
            string result = null;
            try
            {
                using (SqlConnection db = new SqlConnection(Configuration["db"]))
                {
                    DynamicParameters p = new DynamicParameters(account);
                    db.Execute("pUpdateAccount", p, commandType: CommandType.StoredProcedure);
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

        public Account Login(string login, string password)
        {
            using (SqlConnection db = new SqlConnection(Configuration["db"]))
            {
                return db.Query<Account>("pLogin", new { login, password }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
