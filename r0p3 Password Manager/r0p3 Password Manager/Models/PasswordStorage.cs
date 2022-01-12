using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Essentials;

namespace r0p3_Password_Manager.Models
{
    public static class PasswordStorage
    {
        static SQLiteAsyncConnection db;
        static async Task Init()
        {
            if (db != null)
                return;
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Passwords.db");
            db = new SQLiteAsyncConnection(path);
            await db.CreateTableAsync<Password>();
            try
            {
                if (await SecureStorage.GetAsync("PasswordEncryptor") == null)
                    await SecureStorage.SetAsync("PasswordEncryptor", PasswordGenerator.generate(64, true, true, true, true));
            }
            catch
            {

            }
        }

        public static async Task addPassword(Password password)
        {
            await Init();
            await db.InsertAsync(password);
        }

        public static async Task removeAllPasswords()
        {
            await Init();
            await db.DeleteAllAsync<Password>();
        }

        public static async Task removePassword(int id)
        {
            await Init();
            await db.DeleteAsync<Password>(id);
        }

        public static async Task<IEnumerable<Password>> getPasswords()
        {
            await Init();
            var passwords = await db.Table<Password>().ToArrayAsync();
            return passwords;
        }

        public static async Task<Password> getPassword(int id)
        {
            await Init();
            var password = await db.Table<Password>().FirstOrDefaultAsync(p => p.ID == id);
            return password;
        }

        public static async Task updatePassword(Password password)
        {
            await Init();
            await db.UpdateAsync(password);
        }
    }
}
