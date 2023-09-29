namespace InventarProgramm.Database {
    interface IModel {
        //string CreateTableStatement();
        string GetDeleteEntry(int id);
        string GetInsertEntry();
        string GetSelectEntries();
        string GetUpdateEntry();
        void SetID(int id);
        int GetID();
        ModelTyp ModelTyp();
    }
}
