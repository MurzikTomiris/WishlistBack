namespace WishlistBack.Model
{
    public class Giftcard
    {
        public int id { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public decimal price { get; set; }

        public string link { get; set; }

        public string image { get; set; }

        public int wishlistId { get; set; }

        public int isReserved { get; set; }
    }

    public class Giftcard2
    {

        public string title { get; set; }

        public string description { get; set; }

        public decimal price { get; set; }

        public string link { get; set; }

        public string image { get; set; }

        public int wishlistId { get; set; }

    }
}
