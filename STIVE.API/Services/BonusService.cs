using STIVE.API.Database;
using STIVE.API.DTO.Input;
using STIVE.API.DTO.Output;
using STIVE.API.Models;

namespace STIVE.API.Services
{
    public class BonusService
    {
        private readonly stive_potion_seller_context Database;

        public BonusService() { }
        public BonusService(stive_potion_seller_context database)
        {
            Database = database;
        }

        public List<BonusDTO> GetAll()
        {
            var bonusesList = Database.bonus.ToList();
            return bonusesList.Select(x => new BonusDTO
            {
                Id = x.Id,
                Name = x.Name,
                Duration = x.Duration,
            }).ToList();
        }
        public BonusDTO? Get(int id)
        {
            var bonus = Database.bonus.FirstOrDefault(x => x.Id == id);
            if (bonus != null) 
            {
                var bonusDTO = new BonusDTO
                {
                    Id = bonus.Id,
                    Name = bonus.Name,
                    Duration = bonus.Duration,
                };
                return bonusDTO;
            }
            return null;
            
        }
        public void Add(BonusToSaveDTO bonusSaveDTO)
        {
            var bonus = new Bonus
            {
                Name = bonusSaveDTO.Name,
                Duration = bonusSaveDTO.Duration,
            };
            Database.bonus.Add(bonus);
            Database.SaveChanges();

        }
        public void Update(int id, BonusToSaveDTO bonusToSaveDTO)
        {
            Bonus? bonusToUpdate = Database.bonus.FirstOrDefault(x => x.Id == id);
            if (bonusToUpdate != null)
            {
                var saves = Database.save.ToList();
                bonusToUpdate.Name = bonusToSaveDTO.Name;
                bonusToUpdate.Duration = bonusToSaveDTO.Duration;
                bonusToUpdate.BonusHasSaves = bonusToSaveDTO.BonusHasSaves.Select(id => new SaveHasBonus
                {
                    SaveId = id
                }).ToList();
                Database.bonus.Update(bonusToUpdate);
                Database.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            Bonus? bonusToDelete = Database.bonus.FirstOrDefault(x => x.Id == id);
            if (bonusToDelete != null)
            {
                Database.bonus.Remove(bonusToDelete);
                Database.SaveChanges();
            }
        }
    }
}
