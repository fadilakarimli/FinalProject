﻿namespace FinalProjectConsume.Models.Blog
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string? Content { get; set; }
        //public string Author { get; set; }
        //public int CommentCount { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;

    }
}
