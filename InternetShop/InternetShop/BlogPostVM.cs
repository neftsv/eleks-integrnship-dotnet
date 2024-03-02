namespace InternetShop
{
    public class BlogPostVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Author { get; set; }
        public IFormFile photo {get; set; }
}
}
