namespace CakeCrafter.DataAccess.Entites
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; } //Конечно потом на хэш переделать
    }
}
