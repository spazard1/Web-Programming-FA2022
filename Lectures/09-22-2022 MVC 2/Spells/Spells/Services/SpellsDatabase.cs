using Spells.Models;

namespace Spells.Services
{
    public class SpellsDatabase
    {
        private List<SpellModel> spells = new() {
            new SpellModel() { Spell = "Steven" },
            new SpellModel() { Spell = "Amanda" },
            new SpellModel() { Spell = "Benji" },
        };

        public SpellModel Get(int index)
        {
            return spells[index];
        }

        public int Count()
        {
            return spells.Count();
        }

        public void Add(string newSpell)
        {
            spells.Add(new SpellModel() { Spell = newSpell });
        }

        public void Delete(int id)
        {
            spells.RemoveAt(id);
        }
    }
}
