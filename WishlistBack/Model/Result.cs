namespace WishlistBack.Model
{
    public class Result
    {
        public string result { get; set; }
        public string error { get; set; }
        public Status status { get; set; }

        public enum Status
        {
            Ok,
            Error
        }
    }
}
