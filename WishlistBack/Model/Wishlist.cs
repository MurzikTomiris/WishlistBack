namespace WishlistBack.Model
{
    public class Wishlist
    {
        public int id { get; set; }

        public string name { get; set; } 

        public string description { get; set; }

        public string listLink { get; set; }

        public int accountId { get; set; }

        public int isActive { get; set; } 
    }

    public class Wishlist2
    {
        public string name { get; set; }

        public string description { get; set; }
        public string listLink { get; set; }

        public int accountId { get; set; }

    }
}
