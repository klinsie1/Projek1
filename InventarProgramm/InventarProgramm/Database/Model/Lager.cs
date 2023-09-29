using System.Data.SQLite;

namespace InventarProgramm.Database.Model {
    class Lager : IModel {

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Ort { get; private set; }

        public Lager(SQLiteDataReader dataReader) {
            if (dataReader != null) {
                this.Id = dataReader.GetInt32(0);
                this.Name = dataReader.GetString(1);
                this.Ort = dataReader.GetString(2);
            }
        }

        public Lager(int id, string name, string ort) {
            Id = id;
            Name = name;
            Ort = ort;
        }

        public string GetDeleteEntry(int id) {
            return $"delete from lager where id={id};";
        }

        public int GetID() {
            return this.Id;
        }

        public string GetInsertEntry() {
            return $"insert into lager (id, name, ort) values ({Id}, '{Name}', '{Ort}');";
        }

        public string GetSelectEntries() {
            return "SELECT * FROM lager;";
        }

        public ModelTyp ModelTyp() {
            return InventarProgramm.Database.ModelTyp.lager;
        }

        public void SetID(int id) {
            this.Id = id;
        }

        public string GetUpdateEntry() {
            return $"UPDATE lager SET name='{this.Name}', ort='{this.Ort}' WHERE ID={this.Id};";
        }
    }
}
