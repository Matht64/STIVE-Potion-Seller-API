using STIVE.API.Database;
using STIVE.API.DTO.Input;
using STIVE.API.DTO.Output;
using STIVE.API.Models;

namespace STIVE.API.Services
{
    public class SaveService
    {
        private readonly stive_potion_seller_context Database;

        public SaveService() { }
        public SaveService(stive_potion_seller_context database)
        {
            Database = database;
        }

        public List<SaveDTO> GetAll()
        {
            var savesList = Database.save.ToList();
            return savesList.Select(x => new SaveDTO
            {
                Id = x.Id,
                Name = x.Name,
                Gold = x.Gold,
                UserId = x.UserId
            }).ToList();
        }
        public SaveDTO? Get(int id)
        {
            var save = Database.save.FirstOrDefault(x => x.Id == id);
            if (save != null) 
            {
                var saveDTO = new SaveDTO
                {
                    Id = save.Id,
                    Name = save.Name,
                    Gold = save.Gold,
                    UserId = save.UserId
                };
                return saveDTO;
            }
            return null;
            
        }
        public void Add(SaveToSaveDTO saveToSaveDTO)
        {
            var potions = Database.potion.ToList();
            var bonuses = Database.bonus.ToList();
            var suppliers = Database.supplier.ToList();
            var save = new Save
            {
                Name = saveToSaveDTO.Name,
                Gold = saveToSaveDTO.Gold,
                UserId = saveToSaveDTO.UserId,
                SaveHasBonuses = saveToSaveDTO.SaveHasBonuses.Select(id => new SaveHasBonus
                {
                    BonusId = id
                }).ToList(),
                SaveHasPotions = saveToSaveDTO.SaveHasPotions.Select(id => new SaveHasPotion
                {
                    PotionId = id
                }).ToList(),
                SaveHasSuppliers = saveToSaveDTO.SaveHasSuppliers.Select(id => new SaveHasSupplier
                {
                    SupplierId = id
                }).ToList(),

            };
            Database.save.Add(save);
            Database.SaveChanges();

        }
        public void Update(int id, SaveToSaveDTO saveToSaveDTO)
        {
            Save? saveToUpdate = Database.save.FirstOrDefault(x => x.Id == id);
            if (saveToUpdate != null)
            {
                var saves = Database.save.ToList();
                saveToUpdate.Name = saveToSaveDTO.Name;
                saveToUpdate.Gold = saveToSaveDTO.Gold;
                saveToUpdate.SaveHasBonuses = saveToSaveDTO.SaveHasBonuses.Select(id => new SaveHasBonus
                {
                    BonusId = id
                }).ToList();
                saveToUpdate.SaveHasPotions = saveToSaveDTO.SaveHasPotions.Select(id => new SaveHasPotion
                {
                    PotionId = id
                }).ToList();
                saveToUpdate.SaveHasSuppliers = saveToSaveDTO.SaveHasSuppliers.Select(id => new SaveHasSupplier
                {
                    SupplierId = id
                }).ToList();
                Database.save.Update(saveToUpdate);
                Database.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            Save? saveToDelete = Database.save.FirstOrDefault(x => x.Id == id);
            if (saveToDelete != null)
            {
                Database.save.Remove(saveToDelete);
                Database.SaveChanges();
            }
        }
    }
}
