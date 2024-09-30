namespace Mvc.Repositories.Data.Entities
{
    public class Movies{
        public int Id { get; set; }
        public string? nama { get; set; }
        public string? genre { get; set; }
        public int? users_id { get; set; }
    }
}