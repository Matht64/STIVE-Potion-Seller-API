using STIVE.API.DTO.Core;
using STIVE.API.DTO;

namespace STIVE.API.DTO
{
    public class SaveDTO : EntityDTO
    {
        public string Name { get; set; }
        public int Gold { get; set; }
        public int idUser { get; set; }
        public SaveDTO() {}

    }
}
