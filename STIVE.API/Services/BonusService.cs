using STIVE.API.Database;
using STIVE.API.DTO.Both;
using STIVE.API.DTO.ClientLeger;
using STIVE.API.DTO.ClientLourd;
using STIVE.API.Models;
using BonusDTO = STIVE.API.DTO.ClientLeger;

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

        public List<BonusDetailDTO> GetAll()
        {
            var bonusesList = Database.bonus.ToList();
            return bonusesList.Select(x => new BonusDetailDTO
            {
                Id = x.Id,
                Name = x.Name,
                Duration = x.Duration,
                Price = x.Price,
            }).ToList();
        }
        public BonusDetailDTO? Get(int id)
        {
            var bonus = Database.bonus.FirstOrDefault(x => x.Id == id);
            if (bonus != null) 
            {
                var bonusDto = new BonusDetailDTO
                {
                    Id = bonus.Id,
                    Name = bonus.Name,
                    Duration = bonus.Duration,
                    Price = bonus.Price,
                };
                return bonusDto;
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
