using JournalLibrary.Models;
using Newtonsoft.Json;

namespace JournalLibrary
{

    public class JournalStorage
    {
        public List<JournalEntry> _allEntries = new List<JournalEntry>();

        public JournalStorage()
        {
            LoadEntries();
        }

        public void AddJournalEntry(JournalEntry entry)
        {
            JournalEntry? existingJournalEntry = FindJournalEntryByDate(entry.Date);
            if (existingJournalEntry != null)
            {
                throw new Exception("Запись с такой датой уже существует!");
            }
            _allEntries.Add(entry);

            SaveJournalEntries();
        }

        private JournalEntry? FindJournalEntryByDate(DateTime date)
        {
            return _allEntries.Find(i => i.Date == date);
        }

        private JournalEntry GetJournalEntryById(string id)
        {
            return _allEntries.First(i => i.Id == id);
        }

        public List<JournalEntry> GetAllJournalEntrys()
        {
            return _allEntries;
        }
        public void LoadEntries()
        {
            if (!File.Exists(Constants.PATH_JOURNAL_ENTRIES))
            {
                return;
            }
            string serializedData = File.ReadAllText(Constants.PATH_JOURNAL_ENTRIES);
            List<JournalEntry> loadedEntries = JsonConvert.DeserializeObject<List<JournalEntry>>(serializedData)!;

            if (loadedEntries == null)
            {
                return;
            }
            _allEntries = loadedEntries;
        }
        private void SaveJournalEntries()
        {
            string serializedJournalEntrys = JsonConvert.SerializeObject(_allEntries, Formatting.Indented);
            File.WriteAllText(Constants.PATH_JOURNAL_ENTRIES, serializedJournalEntrys);
        }
    }

}
