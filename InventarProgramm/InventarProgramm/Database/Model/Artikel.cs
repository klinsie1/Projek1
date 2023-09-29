using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarProgramm.Database.Model {
    class Artikel : IModel {

        public int Id { get; private set; }
        public int RegalId { get; private set; }
        public string Name { get; private set; }
        public string Beschreibung { get; private set; }
        public int Bestand { get; private set; }
        public string Ort { get; private set; }

        public Artikel(SQLiteDataReader dataReader) {
            if (dataReader != null) {
                this.Id = dataReader.GetInt32(0);
                this.RegalId = dataReader.GetInt32(1);
                this.Name = dataReader.GetString(2);
                this.Beschreibung = dataReader.GetString(3);
                this.Bestand = dataReader.GetInt32(4);
                this.Ort = dataReader.GetString(5);
            }
        }

        public Artikel(int id, int regalId, string name, string beschreibung, int bestand, string ort) {
            Id = id;
            RegalId = regalId;
            Name = name;
            Beschreibung = beschreibung;
            Bestand = bestand;
            Ort = ort;
        }

        public string GetDeleteEntry(int id) {
            return $"DELETE FROM artikel where id={id}";
        }

        public int GetID() {
            return this.Id;
        }

        public string GetInsertEntry() {
            return $"INSERT INTO artikel (id, regal_id, name, beschreibung, bestand, ort) VALUES " + 
                $"({this.Id}, {this.RegalId}, '{this.Name}', '{this.Beschreibung}', {this.Bestand}, '{this.Ort}');";
        }

        public string GetSelectEntries() {
            return "SELECT * FROM artikel;";
        }

        public ModelTyp ModelTyp() {
            return InventarProgramm.Database.ModelTyp.artikel;
        }

        public void SetID(int id) {
            this.Id = id;
        }

        public string GetUpdateEntry() {
            return $"UPDATE artikel SET regal_id={this.RegalId}, name='{this.Name}', beschreibung='{this.Beschreibung}', bestand={this.Bestand}, ort='{this.Ort}' WHERE ID={this.Id};";
        }
    }
}
