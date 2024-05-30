using Smart.Core.Interface;

namespace Smart.Core.Entities.RefreshTokenEntity
{
    public class RefreshToken : IBaseEntity
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
