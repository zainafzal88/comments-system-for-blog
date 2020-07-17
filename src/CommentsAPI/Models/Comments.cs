namespace CommentsAPI.Models
{
    public class Comments
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Comment { get; set; }
        
        public string PostId { get; set; }
    }
}