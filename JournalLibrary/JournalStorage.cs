using JournalLibrary.Models;
using System.Xml;

namespace JournalLibrary
{

    internal class JournalStorage
    {
        public List<JournalEntry> _allEntries = new List<JournalEntry>();
        public void LoadEntrye(object JsonConvert)
        {
            if (File.Exists(Constants.PATH_INGREDIENTS))
            {
                string serializedData = File.ReadAllText(Constants.PATH_INGREDIENTS);
                List<JournalEntry> loadedEntries = JsonConvert. <List<JournalEntry>>(serializedData);

                if (loadedEntries != null)
                {
                    _allEntries = loadedEntries;
                }
            }
        }



        private void SaveIngredients(object JsonConvert)
        {
            string serializedIngredients = JsonConvert.SerializeObject(_allEntries, Formatting.Indented);
            File.WriteAllText(Constants.PATH_INGREDIENTS, serializedIngredients);
        }

    }

}
