using InventarProgramm.Database.Model;
using InventarProgramm.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace InventarProgramm.Database {
    class Database {
        public static Database Instance {
            get {
                if (_Instance == null)
                    return new Database();
                return _Instance;
            }
        }
        private static Database _Instance;
        private static string DbFile = "data";

        private SQLiteConnection connection;
        public Dictionary<int, Artikel> Artikels { get; private set; }
        public Dictionary<int, Lager> Lagers { get; private set; }
        public Dictionary<int, Regal> Regals { get; private set; }
        public Dictionary<int, User> Users { get; private set; }
        private Dictionary<ModelTyp, int> nextId;
        private List<string> dbStatementQueue;

        public Database() {
            _Instance = this;
            this.OpenConnection();

            this.Artikels = new Dictionary<int, Artikel>();
            this.Lagers = new Dictionary<int, Lager>();
            this.Regals = new Dictionary<int, Regal>();
            this.Users = new Dictionary<int, User>();
            this.nextId = new Dictionary<ModelTyp, int>();
            this.dbStatementQueue = new List<string>();

            this.ReadAllData();

            new Thread(() => {
                while (true) {
                    Thread.Sleep(10000); //alle 10 sekunden DB sync!
                    while (this.dbStatementQueue.Count != 0) {
                        //try {
                        new SQLiteCommand(this.dbStatementQueue[0], this.connection).ExecuteNonQuery();
                        //} catch (Exception e) {
                        //    LoggerService.Instance.AddLog($"Error in sql command execution. sql command: {this.dbStatementQueue[0]}. Error: {e.ToString()}");
                        //}
                        this.dbStatementQueue.RemoveAt(0);
                    }
                }
            }).Start();
        }

        private void AddSqlCommand(string command) {
            //try {
            this.dbStatementQueue.Add(command);
            //} catch (Exception e) {
            //    LoggerService.Instance.AddLog($"Error in AddSqlCommand. sql command: {command}. Error: {e.ToString()}");
            //}
        }

        public void Insert(IModel model) {
            //try {
            model.SetID(this.nextId[model.ModelTyp()]);

            switch (model.ModelTyp()) {
                case ModelTyp.artikel:
                    this.Artikels.Add(model.GetID(), model as Artikel);
                    break;
                case ModelTyp.regal:
                    this.Regals.Add(model.GetID(), model as Regal);
                    break;
                case ModelTyp.lager:
                    this.Lagers.Add(model.GetID(), model as Lager);
                    break;
            }
            this.AddSqlCommand(model.GetInsertEntry());
            this.nextId[model.ModelTyp()]++;

            //} catch (Exception e) {
            //    LoggerService.Instance.AddLog($"Error in Insert. sql command: {model.GetInsertEntry()}. Error: {e.ToString()}");
            //}
        }

        public void Update(IModel model) {
            switch (model.ModelTyp()) {
                case ModelTyp.artikel:
                    this.Artikels[model.GetID()] = model as Artikel;
                    break;
                case ModelTyp.regal:
                    this.Regals[model.GetID()] = model as Regal;
                    break;
                case ModelTyp.lager:
                    this.Lagers[model.GetID()] = model as Lager;
                    break;
            }
            this.AddSqlCommand(model.GetUpdateEntry());
        }

        public void Delete(ModelTyp modelTyp, int id) {
            switch (modelTyp) {
                case ModelTyp.artikel:
                    //if (this.Artikels.ContainsKey(id)) {
                        this.Artikels.Remove(id);
                        this.AddSqlCommand(new Artikel(null).GetDeleteEntry(id));
                    //} else {
                    //    LoggerService.Instance.AddLog($"Wanted to remove {id} from {modelTyp.ToString()} but didnt exists");
                    //}
                    break;
                case ModelTyp.regal:
                    //if (this.Regals.ContainsKey(id)) {
                        this.Regals.Remove(id);
                        this.AddSqlCommand(new Regal(null).GetDeleteEntry(id));
                    //} else {
                    //    LoggerService.Instance.AddLog($"Wanted to remove {id} from {modelTyp.ToString()} but didnt exists");
                    //}
                    break;
                case ModelTyp.lager:
                    //if (this.lagers.ContainsKey(id)) {
                        this.Lagers.Remove(id);
                        this.AddSqlCommand(new Lager(null).GetDeleteEntry(id));
                    //} else {
                    //    LoggerService.Instance.AddLog($"Wanted to remove {id} from {modelTyp.ToString()} but didnt exists");
                    //}
                    break;
            }
        }

        private void OpenConnection() {
            this.connection = new SQLiteConnection($"Data Source={DbFile};Version=3;New=True;Compress=True;");
            this.connection.Open();
        }

        private void ReadAllData() {
            this.nextId.Add(ModelTyp.artikel, 0);
            this.nextId.Add(ModelTyp.regal, 0);
            this.nextId.Add(ModelTyp.lager, 0);

            SQLiteDataReader artikelReader = new SQLiteCommand(new Artikel(null).GetSelectEntries(), this.connection).ExecuteReader();
            SQLiteDataReader regalReader = new SQLiteCommand(new Regal(null).GetSelectEntries(), this.connection).ExecuteReader();
            SQLiteDataReader lagerReader = new SQLiteCommand(new Lager(null).GetSelectEntries(), this.connection).ExecuteReader();
            SQLiteDataReader userReader = new SQLiteCommand(new User(null).GetSelectEntries(), this.connection).ExecuteReader();

            while (artikelReader.Read()) {
                var artikel = new Artikel(artikelReader);
                this.Artikels.Add(artikel.Id, artikel);
            }
            while (regalReader.Read()) {
                var regal = new Regal(regalReader);
                this.Regals.Add(regal.Id, regal);
            }
            while (lagerReader.Read()) {
                var lager = new Lager(lagerReader);
                this.Lagers.Add(lager.Id, lager);
            }
            while (userReader.Read()) {
                var user = new User(userReader);
                this.Users.Add(user.Id, user);
            }

            foreach (var a in this.Artikels) {
                if (a.Key > this.nextId[ModelTyp.artikel])
                    this.nextId[ModelTyp.artikel] = a.Key;
            }
            foreach (var a in this.Regals) {
                if (a.Key > this.nextId[ModelTyp.regal])
                    this.nextId[ModelTyp.regal] = a.Key;
            }
            foreach (var a in this.Lagers) {
                if (a.Key > this.nextId[ModelTyp.lager])
                    this.nextId[ModelTyp.lager] = a.Key;
            }

            this.nextId[ModelTyp.artikel] = this.nextId[ModelTyp.artikel] + 1;
            this.nextId[ModelTyp.regal] = this.nextId[ModelTyp.regal] + 1;
            this.nextId[ModelTyp.lager] = this.nextId[ModelTyp.lager] + 1;
        }

        public List<LagerViewElement> ExtractLagerViewElements() {
            var returner = new List<LagerViewElement>();
            this.Lagers.Values.ToList().ForEach(lager => {
                returner.Add(new LagerViewElement(lager.Id, lager.Name, lager.Ort));
            });
            return returner;
        }

        public List<RegalViewElement> ExtractRegalViewElements() {
            var returner = new List<RegalViewElement>();
            this.Regals.Values.ToList().ForEach(regal => {
                returner.Add(new RegalViewElement(regal.Id, regal.Name, this.ConvertInternLagerToChoosableString(this.Lagers[regal.Lager_id])));
            });
            return returner;
        }

        private string ConvertInternLagerToChoosableString(Lager lager) {
            return $"{lager.Name} (Ort: {lager.Ort}, ID: {lager.Id})";
        }

        public List<string> ExtractAllChoosableLagerElements() {
            var returner = new List<string>();
            this.Lagers.Values.ToList().ForEach(lager => {
                returner.Add(this.ConvertInternLagerToChoosableString(lager));
            });
            return returner;
        }

        private string ConvertInternRegalToChoosableString(Regal regal) {
            return $"{regal.Name} [Lager: {this.ConvertInternLagerToChoosableString(this.Lagers[regal.Lager_id])}, ID: {regal.Id}]";
        }

        public List<ArtikelViewElement> ExtractArtikelViewElements() {
            var returner = new List<ArtikelViewElement>();
            this.Artikels.Values.ToList().ForEach(artikel => {
                returner.Add(new ArtikelViewElement(artikel.Id, artikel.Name, artikel.Bestand, artikel.Beschreibung, this.ConvertInternRegalToChoosableString(this.Regals[artikel.RegalId])));
            });
            return returner;
        }

        public List<string> ExtractAllChoosableRegalElements() {
            var returner = new List<string>();
            this.Regals.Values.ToList().ForEach(regal => {
                returner.Add(this.ConvertInternRegalToChoosableString(regal));
            });
            return returner;
        }
    }
}
