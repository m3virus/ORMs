using RepoDb.Attributes;

namespace RepoDBAPI
{
    [Map("Users")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
