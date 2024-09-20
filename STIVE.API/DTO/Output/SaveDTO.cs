using STIVE.API.DTO.Core;

namespace STIVE.API.DTO.Output
{
    public class SaveDTO : EntityDTO
    {
        public string Name { get; set; }
        public int Gold { get; set; }
        public int idUser { get; set; }
        public SaveDTO() { }

    }
}
