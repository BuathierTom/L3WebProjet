using L3WebProjet.Common.DAO;

namespace L3WebProjet.Common.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Pseudo { get; set; } = string.Empty;
    }

    public static class UserDtoExtensions
    {
        public static UserDto ToDto(this UserDao userDao)
        {
            return new UserDto
            {
                Id = userDao.Id,
                Pseudo = userDao.Pseudo
            };
        }
    }
}