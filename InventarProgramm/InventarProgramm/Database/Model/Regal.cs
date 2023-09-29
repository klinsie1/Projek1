using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarProgramm.Database.Model {
    class Regal : IModel {
        public int Id { get; set; }
        public int Lager_id { get; set; }
        public string Name { get; set; }

        public Regal(SQLiteDataReader dataReader) {
            if (dataReader != null) {
                this.Id = dataReader.GetInt32(0);
                this.Lager_id = dataReader.GetInt32(1);
                this.Name = dataReader.GetString(2);
                
            }
        }

        public Regal(int id, int lager_id, string name) {
            Id = id;
            Lager_id = lager_id;
            Name = name;
        }

        public string GetDeleteEntry(int id) {
            return $"DELETE FROM regal where id={id};";
        }

        public int GetID() {
            return this.Id;
        }

        public string GetInsertEntry() {
            return $"INSERT INTO regal (id, lager_id, name) VALUES " +
                $"({this.Id}, {this.Lager_id}, '{this.Name}');";
        }

        public string GetSelectEntries() {
            return "SELECT * FROM regal;";
        }

        public ModelTyp ModelTyp() {
            return InventarProgramm.Database.ModelTyp.regal;
        }

        public void SetID(int id) {
            this.Id = id;
        }

        public string GetUpdateEntry() {
            return $"UPDATE regal SET lager_id={this.Lager_id}, NAME='{this.Name}' WHERE ID={this.Id};";
        }
    }
}
