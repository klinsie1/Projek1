using System;
using System.Data.SQLite;

namespace InventarProgramm.Database.Model {
    class User : IModel {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string password { get; private set; }

        public User(SQLiteDataReader dataReader) {
            if (dataReader != null) {
                this.Id = dataReader.GetInt32(0);
                this.Name = dataReader.GetString(1);
                this.password = dataReader.GetString(2);
            }
        }

        public string GetDeleteEntry(int id) {
            //wird nicht benötigt weil wir keine Nutzerpflege über die Applikation machen
            throw new NotImplementedException();
        }

        public int GetID() {
            return this.Id;
        }

        public string GetInsertEntry() {
            //wird auch nicht benötigt da die Nutzerpfelge nicht über die UI gemacht wird
            throw new NotImplementedException();
        }

        public string GetSelectEntries() {
            return "SELECT * FROM user;";
        }

        public ModelTyp ModelTyp() {
            return InventarProgramm.Database.ModelTyp.user;
        }

        public void SetID(int id) {
            //brauchen wir an der stelle auch nicht weil wir keine neuen nutzer über die UI anlegen wollen.
            throw new NotImplementedException();
        }

        public string GetUpdateEntry() {
            //brauchen wir an der stelle auch nicht weil wir keine neuen nutzer über die UI anlegen wollen.
            throw new NotImplementedException();
        }
    }
}
