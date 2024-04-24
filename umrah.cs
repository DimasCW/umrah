using System;
using System.Data;
using System.Data.SqlClient;

namespace Menu_Utama
{
    class Program
    {
        static string connectionString = "Data Source=Dimas;Initial Catalog={0};User ID=sa;Password=1234";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Masukkan nama database tujuan:");
                string db = Console.ReadLine();
                Console.WriteLine("\nKetik K untuk Terhubung ke Database: ");
                char chr = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (chr)
                {
                    case 'K':
                        MenuUtama(db);
                        break;
                    default:
                        Console.WriteLine("\nInvalid option");
                        break;
                }
            }
        }

        static void MenuUtama(string db)
        {
            while (true)
            {
                Console.WriteLine("Menu Utama:");
                Console.WriteLine("1. Hotel");
                Console.WriteLine("2. Paket Umroh");
                Console.WriteLine("3. Data Jamaah");
                Console.WriteLine("4. Data Passport");
                Console.WriteLine("5. Grup Keberangkatan");
                Console.WriteLine("6. Keluar");
                Console.Write("Pilih menu (1-6): ");
                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (choice)
                {
                    case '1':
                        ManageHotel(db);
                        break;
                    case '2':
                        ManagePaketUmroh(db);
                        break;
                    case '3':
                        ManageDataJamaah(db);
                        break;
                    case '4':
                        MenuPassport(db);
                        break;
                    case '5':
                        GrupKeberangkatan(db);
                        break;
                    case '6':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid!");
                        break;
                }
            }
        }

        static void GrupKeberangkatan(string db)
        {
            while (true)
            {
                Console.WriteLine("Menu Utama:");
                Console.WriteLine("1. Lihat Data Grup Keberangkatan");
                Console.WriteLine("2. Tambah Data Grup Keberangkatan");
                Console.WriteLine("3. Edit Data Grup Keberangkatan");
                Console.WriteLine("4. Hapus Data Grup Keberangkatan");
                Console.WriteLine("5. Kembali ke Menu Utama");
                Console.Write("Pilih menu (1-5) : ");

                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (choice)
                {
                    case '1':
                        ReadGrupKeberangkatan(db);
                        break;
                    case '2':
                        AddGrupKeberangkatan(db);
                        break;
                    case '3':
                        EditGrupKeberangkatan(db);
                        break;
                    case '4':
                        DeleteGrupKeberangkatan(db);
                        break;
                    case '5':
                        return;
                    default:
                        Console.WriteLine("Pilihan tidak valid. Silakan masukkan pilihan yang benar.");
                        break;
                }
            }
        }

        

        static void ReadGrupKeberangkatan(string db)
        {
            using (SqlConnection connection = new SqlConnection(string.Format(connectionString, db)))
            {
                string query = "SELECT * FROM Grup_Keberangkatan";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"ID Grup: {reader["Id_Grup"]}, Grup: {reader["Grup"]}, ID Jamaah: {reader["Id_Jamaah"]}, ID Paket: {reader["Id_Paket"]}");
                }
                reader.Close();
            }
        }

        static void AddGrupKeberangkatan(string db)
        {
            Console.WriteLine("Masukkan Id Grup Keberangkatan baru:");
            string idgrup = Console.ReadLine();
            Console.WriteLine("Grup:");
            string grup = Console.ReadLine();
            Console.WriteLine("ID Jamaah:");
            string idJamaah = Console.ReadLine();
            Console.WriteLine("ID Paket:");
            string idPaket = Console.ReadLine();

            Console.WriteLine("Apakah data yang anda masukkan sudah benar? (Y/N)");
            string confirm = Console.ReadLine().ToUpper();

            if (confirm == "Y")
            {
                using (SqlConnection connection = new SqlConnection(string.Format(connectionString, db)))
                {
                    string query = "INSERT INTO Grup_Keberangkatan (Id_Grup, Grup, Id_Jamaah, Id_Paket) VALUES (@id, @grup, @idJamaah, @idPaket)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", idgrup);
                    command.Parameters.AddWithValue("@grup", grup);
                    command.Parameters.AddWithValue("@idJamaah", idJamaah);
                    command.Parameters.AddWithValue("@idPaket", idPaket);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} baris telah ditambahkan.");
                }
            }
            else if (confirm == "N")
            {
                Console.WriteLine("Data dibatalkan.");
            }
            else
            {
                Console.WriteLine("Pilihan tidak valid.");
            }
        }

        static void EditGrupKeberangkatan(string db)
        {
            Console.WriteLine("Masukkan ID grup keberangkatan yang ingin diedit:");
            int idGrup = int.Parse(Console.ReadLine());
            Console.WriteLine("Masukkan grup baru:");
            string grup = Console.ReadLine();
            Console.WriteLine("Masukkan ID Jamaah baru:");
            string idJamaah = Console.ReadLine();
            Console.WriteLine("Masukkan ID Paket baru:");
            string idPaket = Console.ReadLine();

            Console.WriteLine("Apakah data yang anda masukkan sudah benar? (Y/N)");
            string confirm = Console.ReadLine().ToUpper();

            if (confirm == "Y")
            {
                using (SqlConnection connection = new SqlConnection(string.Format(connectionString, db)))
                {
                    string query = "UPDATE Grup_Keberangkatan SET Grup = @grup, Id_Jamaah = @idJamaah, Id_Paket = @idPaket WHERE Id_Grup = @idGrup";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idGrup", idGrup);
                    command.Parameters.AddWithValue("@grup", grup);
                    command.Parameters.AddWithValue("@idJamaah", idJamaah);
                    command.Parameters.AddWithValue("@idPaket", idPaket);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} baris telah diubah.");
                }
            }
            else if (confirm == "N")
            {
                Console.WriteLine("Data dibatalkan.");
            }
            else
            {
                Console.WriteLine("Pilihan tidak valid.");
            }
        }

        static void DeleteGrupKeberangkatan(string db)
        {
            Console.WriteLine("Masukkan ID grup keberangkatan yang ingin dihapus:");
            int idGrup = int.Parse(Console.ReadLine());

            Console.WriteLine("Apakah anda ingin menghapus data ini? (Y/N)");
            string confirm = Console.ReadLine().ToUpper();

            if (confirm == "Y")
            {
                using (SqlConnection connection = new SqlConnection(string.Format(connectionString, db)))
                {
                    string query = "DELETE FROM Grup_Keberangkatan WHERE Id_Grup = @idGrup";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idGrup", idGrup);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} baris telah dihapus.");
                }
            }
            else if (confirm == "N")
            {
                Console.WriteLine("Penghapusan data dibatalkan.");
            }
            else
            {
                Console.WriteLine("Pilihan tidak valid.");
            }
        }

        static void MenuPassport(string db)
        {
            while (true)
            {
                Console.WriteLine("\nMenu Passport:");
                Console.WriteLine("1. Lihat Semua Data");
                Console.WriteLine("2. Tambah Passport");
                Console.WriteLine("3. Edit");
                Console.WriteLine("4. Hapus");
                Console.WriteLine("5. Kembali ke Menu Utama");
                Console.Write("Pilih tindakan (1-5): ");

                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (choice)
                {
                    case '1':
                        ReadPassport(db);
                        break;
                    case '2':
                        AddPassport(db);
                        break;
                    case '3':
                        EditPassport(db);
                        break;
                    case '4':
                        DeletePassport(db);
                        break;
                    case '5':
                        return;
                    default:
                        Console.WriteLine("Pilihan tidak valid. Silakan masukkan pilihan yang benar.");
                        break;
                }
            }
        }

        static void AddPassport(string db)
        {
            Console.WriteLine("Tambah Passport");

            Console.WriteLine("Masukkan Nomor Passport (9 Karakter):");
            string nomorPassport = Console.ReadLine();

            Console.WriteLine("Masukkan Tanggal Terbit (YYYY-MM-DD):");
            string tanggalTerbitString = Console.ReadLine();
            DateTime tanggalTerbit;
            if (!DateTime.TryParse(tanggalTerbitString, out tanggalTerbit))
            {
                Console.WriteLine("Format tanggal tidak valid. Pastikan Anda memasukkan tanggal dalam format YYYY-MM-DD.");
                return;
            }

            Console.WriteLine("Masukkan Tanggal Berakhir (YYYY-MM-DD):");
            string tanggalBerakhirString = Console.ReadLine();
            DateTime tanggalBerakhir;
            if (!DateTime.TryParse(tanggalBerakhirString, out tanggalBerakhir))
            {
                Console.WriteLine("Format tanggal tidak valid. Pastikan Anda memasukkan tanggal dalam format YYYY-MM-DD.");
                return;
            }

            Console.WriteLine("Masukkan ID Jamaah:");
            string idJamaah = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(string.Format(connectionString, db)))
            {
                string query = "INSERT INTO Passport (Nomor_Passport, Tanggal_Terbit, Tanggal_Berakhir, Id_Jamaah) VALUES (@nomorPassport, @tanggalTerbit, @tanggalBerakhir, @idJamaah)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nomorPassport", nomorPassport);
                command.Parameters.AddWithValue("@tanggalTerbit", tanggalTerbit);
                command.Parameters.AddWithValue("@tanggalBerakhir", tanggalBerakhir);
                command.Parameters.AddWithValue("@idJamaah", idJamaah);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} baris telah ditambahkan.");

                // Validasi penginputan
                Console.WriteLine("Apakah data yang anda masukkan sudah benar? (Y/N)");
                char confirmation = Console.ReadKey().KeyChar;
                if (confirmation == 'N' || confirmation == 'n')
                {
                    // Rollback jika data tidak benar
                    string rollbackQuery = "DELETE FROM Passport WHERE Nomor_Passport = @nomorPassport";
                    SqlCommand rollbackCommand = new SqlCommand(rollbackQuery, connection);
                    rollbackCommand.Parameters.AddWithValue("@nomorPassport", nomorPassport);
                    rollbackCommand.ExecuteNonQuery();
                    Console.WriteLine("\nData telah dihapus.");
                }
                else if (confirmation != 'Y' && confirmation != 'y')
                {
                    Console.WriteLine("\nPilihan tidak valid.");
                }
            }
        }


        static void ReadPassport(string db)
        {
            using (SqlConnection connection = new SqlConnection(string.Format(connectionString, db)))
            {
                string query = "SELECT * FROM Passport";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"\nNomor Passport: {reader["Nomor_Passport"]}, \nTanggal Terbit: {reader["Tanggal_Terbit"]}, \nTanggal Berakhir: {reader["Tanggal_Berakhir"]}, \nID Jamaah: {reader["Id_Jamaah"]}");
                }
                reader.Close();
            }
        }


        static void EditPassport(string db)
        {
            Console.WriteLine("Masukkan Nomor Passport yang ingin diedit:");
            string nomorPassport = Console.ReadLine();
            Console.WriteLine("Masukkan Tanggal Terbit baru (YYYY-MM-DD HH:mm:ss):");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime tanggalTerbit))
            {
                Console.WriteLine("Format tanggal tidak valid. Pastikan formatnya adalah YYYY-MM-DD HH:mm:ss.");
                return;
            }
            Console.WriteLine("Masukkan Tanggal Berakhir baru (YYYY-MM-DD HH:mm:ss):");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime tanggalBerakhir))
            {
                Console.WriteLine("Format tanggal tidak valid. Pastikan formatnya adalah YYYY-MM-DD HH:mm:ss.");
                return;
            }
            Console.WriteLine("Masukkan ID Jamaah baru:");
            string idJamaah = Console.ReadLine();

            Console.Write("Apakah data yang Anda masukkan sudah benar? (Y/N): ");
            string confirmation = Console.ReadLine();

            if (confirmation.ToUpper() == "Y")
            {
                using (SqlConnection connection = new SqlConnection(string.Format(connectionString, db)))
                {
                    string query = "UPDATE Passport SET Tanggal_Terbit = @tanggalTerbit, Tanggal_Berakhir = @tanggalBerakhir, Id_Jamaah = @idJamaah WHERE Nomor_Passport = @nomorPassport";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nomorPassport", nomorPassport);
                    command.Parameters.AddWithValue("@tanggalTerbit", tanggalTerbit);
                    command.Parameters.AddWithValue("@tanggalBerakhir", tanggalBerakhir);
                    command.Parameters.AddWithValue("@idJamaah", idJamaah);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} baris telah diubah.");
                }
            }
            else if (confirmation.ToUpper() == "N")
            {
                Console.WriteLine("Pengguna membatalkan pengeditan data.");
            }
            else
            {
                Console.WriteLine("Pilihan tidak valid.");
            }
        }


        static void DeletePassport(string db)
        {
            Console.WriteLine("Masukkan Nomor Passport yang ingin dihapus:");
            string nomorPassport = Console.ReadLine();

            Console.Write("Apakah Anda yakin ingin menghapus data ini? (Y/N): ");
            string confirmation = Console.ReadLine();

            if (confirmation.ToUpper() == "Y")
            {
                using (SqlConnection connection = new SqlConnection(string.Format(connectionString, db)))
                {
                    string query = "DELETE FROM Passport WHERE Nomor_Passport = @nomorPassport";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nomorPassport", nomorPassport);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} baris telah dihapus.");
                }
            }
            else if (confirmation.ToUpper() == "N")
            {
                Console.WriteLine("Pengguna membatalkan penghapusan data.");
            }
            else
            {
                Console.WriteLine("Pilihan tidak valid.");
            }
        }



        static void ManageHotel(string db)
        {
            while (true)
            {
                Console.WriteLine("\nMenu Manajemen Hotel:");
                Console.WriteLine("1. Lihat Semua Data");
                Console.WriteLine("2. Tambah Hotel");
                Console.WriteLine("3. Edit");
                Console.WriteLine("4. Hapus");
                Console.WriteLine("5. Kembali ke Menu Utama");
                Console.Write("Pilih menu (1-5): ");
                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (choice)
                {
                    case '1':
                        ReadHotel(db);
                        break;
                    case '2':
                        AddHotel(db);
                        break;
                    case '3':
                        EditHotel(db);
                        break;
                    case '4':
                        DeleteHotel(db);
                        break;
                    case '5':
                        return;
                    default:
                        Console.WriteLine("Pilihan tidak valid!");
                        break;
                }
            }
        }

        static void ReadHotel(string db)
        {
            using (SqlConnection connection = new SqlConnection(string.Format(connectionString, db)))
            {
                string query = "SELECT * FROM Hotel";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"\nID Hotel: {reader["Id_Hotel"]}, \nNama Hotel: {reader["Nama_Hotel"]}, \nLokasi Hotel: {reader["Lokasi_Hotel"]}, \nFasilitas Hotel: {reader["Fasilitas_Hotel"]}");
                }
                reader.Close();
            }
        }


        static void AddHotel(string db)
        {
            Console.WriteLine("Masukkan ID hotel: (Maksimal 4 karakter)");
            string idHotel = Console.ReadLine();
            Console.WriteLine("Masukkan nama hotel:");
            string namaHotel = Console.ReadLine();
            Console.WriteLine("Masukkan lokasi hotel:");
            string lokasiHotel = Console.ReadLine();
            Console.WriteLine("Masukkan fasilitas hotel:");
            string fasilitasHotel = Console.ReadLine();

            Console.Write("Apakah data yang anda masukkan sudah benar? (Y/N): ");
            string confirmation = Console.ReadLine();

            if (confirmation.ToUpper() == "Y")
            {
                using (SqlConnection connection = new SqlConnection(string.Format(connectionString, db)))
                {
                    string query = "INSERT INTO Hotel (Id_Hotel, Nama_Hotel, Lokasi_Hotel, Fasilitas_Hotel) VALUES (@idHotel, @namaHotel, @lokasiHotel, @fasilitasHotel)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idHotel", idHotel);
                    command.Parameters.AddWithValue("@namaHotel", namaHotel);
                    command.Parameters.AddWithValue("@lokasiHotel", lokasiHotel);
                    command.Parameters.AddWithValue("@fasilitasHotel", fasilitasHotel);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} baris telah ditambahkan.");
                }
            }
            else if (confirmation.ToUpper() == "N")
            {
                Console.WriteLine("Pengguna membatalkan penambahan data.");
            }
            else
            {
                Console.WriteLine("Pilihan tidak valid.");
            }
        }




        static void EditHotel(string db)
        {
            Console.WriteLine("Masukkan ID hotel yang ingin diedit:");
            string idHotel = Console.ReadLine();
            Console.WriteLine("Masukkan nama hotel baru:");
            string namaHotel = Console.ReadLine();
            Console.WriteLine("Masukkan lokasi hotel baru:");
            string lokasiHotel = Console.ReadLine();
            Console.WriteLine("Masukkan fasilitas hotel baru:");
            string fasilitasHotel = Console.ReadLine();

            Console.Write("Apakah data yang anda masukkan sudah benar? (Y/N): ");
            string confirmation = Console.ReadLine();

            if (confirmation.ToUpper() == "Y")
            {
                using (SqlConnection connection = new SqlConnection(string.Format(connectionString, db)))
                {
                    string query = "UPDATE Hotel SET Nama_Hotel = @namaHotel, Lokasi_Hotel = @lokasiHotel, Fasilitas_Hotel = @fasilitasHotel WHERE Id_Hotel = @idHotel";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idHotel", idHotel);
                    command.Parameters.AddWithValue("@namaHotel", namaHotel);
                    command.Parameters.AddWithValue("@lokasiHotel", lokasiHotel);
                    command.Parameters.AddWithValue("@fasilitasHotel", fasilitasHotel);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} baris telah diubah.");
                }
            }
            else if (confirmation.ToUpper() == "N")
            {
                Console.WriteLine("Pengguna membatalkan pengeditan data.");
            }
            else
            {
                Console.WriteLine("Pilihan tidak valid.");
            }
        }




        static void DeleteHotel(string db)
        {
            Console.WriteLine("Masukkan ID hotel yang ingin dihapus:");
            string idHotel = Console.ReadLine();

            Console.Write("Apakah anda ingin menghapus data ini? (Y/N): ");
            string confirmation = Console.ReadLine();

            if (confirmation.ToUpper() == "Y")
            {
                using (SqlConnection connection = new SqlConnection(string.Format(connectionString, db)))
                {
                    string query = "DELETE FROM Hotel WHERE Id_Hotel = @idHotel";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idHotel", idHotel);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} baris telah dihapus.");
                }
            }
            else if (confirmation.ToUpper() == "N")
            {
                Console.WriteLine("Pengguna membatalkan penghapusan data.");
            }
            else
            {
                Console.WriteLine("Pilihan tidak valid.");
            }
        }



        static void ManagePaketUmroh(string db)
        {
            while (true)
            {
                Console.WriteLine("\nMenu Manajemen Paket Umroh:");
                Console.WriteLine("1. Lihat Semua Data");
                Console.WriteLine("2. Tambah Paket Umroh");
                Console.WriteLine("3. Edit");
                Console.WriteLine("4. Hapus");
                Console.WriteLine("5. Kembali ke Menu Utama");
                Console.Write("Pilih menu (1-5): ");
                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (choice)
                {
                    case '1':
                        ReadPaketUmroh(db);
                        break;
                    case '2':
                        AddPaketUmroh(db);
                        break;
                    case '3':
                        EditPaketUmroh(db);
                        break;
                    case '4':
                        DeletePaketUmroh(db);
                        break;
                    case '5':
                        return;
                    default:
                        Console.WriteLine("Pilihan tidak valid!");
                        break;
                }
            }
        }

        static void ReadPaketUmroh(string db)
        {
            using (SqlConnection connection = new SqlConnection(string.Format(connectionString, db)))
            {
                string query = "SELECT * FROM Paket_Umroh";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"\nID Paket: {reader["Id_Paket"]}, \nNama Paket: {reader["Nama_Paket"]}, \nRute: {reader["Rute_Perjalanan"]}, \nLama Perjalanan: {reader["Lama_Perjalanan"]},\nFasilitas: {reader["Fasilitas_yang_disediakan"]}, \nHarga: {reader["Harga_Paket"]}");
                }
                reader.Close();
            }
        }


        static void AddPaketUmroh(string db)
        {
            Console.WriteLine("Masukkan ID paket umroh (4 Karakter):");
            string idPaket = Console.ReadLine();
            Console.WriteLine("Masukkan nama paket umroh:");
            string namaPaket = Console.ReadLine();
            Console.WriteLine("Masukkan rute perjalanan:");
            string rutePerjalanan = Console.ReadLine();
            Console.WriteLine("Masukkan lama perjalanan (dalam hari):");
            int lamaPerjalanan;
            while (!int.TryParse(Console.ReadLine(), out lamaPerjalanan))
            {
                Console.WriteLine("Masukkan angka yang valid untuk lama perjalanan:");
            }
            Console.WriteLine("Masukkan fasilitas:");
            string fasilitas = Console.ReadLine();
            Console.WriteLine("Masukkan harga paket:");
            int hargaPaket;
            while (!int.TryParse(Console.ReadLine(), out hargaPaket))
            {
                Console.WriteLine("Masukkan angka yang valid untuk harga paket:");
            }

            Console.Write("Apakah data yang anda masukkan sudah benar? (Y/N): ");
            string confirmation = Console.ReadLine();

            if (confirmation.ToUpper() == "Y")
            {
                using (SqlConnection connection = new SqlConnection(string.Format(connectionString, db)))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = connection.CreateCommand();
                        command.CommandText = "INSERT INTO Paket_Umroh (Id_Paket, Nama_Paket, Rute_Perjalanan, Lama_Perjalanan, Fasilitas_yang_disediakan, Harga_Paket) VALUES (@idPaket, @namaPaket, @rutePerjalanan, @lamaPerjalanan, @fasilitas, @hargaPaket)";
                        command.Parameters.AddWithValue("@idPaket", idPaket);
                        command.Parameters.AddWithValue("@namaPaket", namaPaket);
                        command.Parameters.AddWithValue("@rutePerjalanan", rutePerjalanan);
                        command.Parameters.AddWithValue("@lamaPerjalanan", lamaPerjalanan);
                        command.Parameters.AddWithValue("@fasilitas", fasilitas);
                        command.Parameters.AddWithValue("@hargaPaket", hargaPaket);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} baris telah ditambahkan.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            else if (confirmation.ToUpper() == "N")
            {
                Console.WriteLine("Pengguna membatalkan penambahan data.");
            }
            else
            {
                Console.WriteLine("Pilihan tidak valid.");
            }
        }




        static void EditPaketUmroh(string db)
        {
            Console.WriteLine("Masukkan ID paket umroh yang ingin diedit:");
            string idPaket = Console.ReadLine();
            Console.WriteLine("Masukkan nama paket umroh baru:");
            string namaPaket = Console.ReadLine();
            Console.WriteLine("Masukkan rute perjalanan baru:");
            string rutePerjalanan = Console.ReadLine();
            Console.WriteLine("Masukkan lama perjalanan baru (dalam hari):");
            int lamaPerjalanan;
            while (!int.TryParse(Console.ReadLine(), out lamaPerjalanan))
            {
                Console.WriteLine("Masukkan angka yang valid untuk lama perjalanan:");
            }
            Console.WriteLine("Masukkan fasilitas baru:");
            string fasilitas = Console.ReadLine();
            Console.WriteLine("Masukkan harga paket baru:");
            int hargaPaket;
            while (!int.TryParse(Console.ReadLine(), out hargaPaket))
            {
                Console.WriteLine("Masukkan angka yang valid untuk harga paket:");
            }

            Console.WriteLine("Apakah data yang anda masukkan sudah benar? (Y/N)");
            string confirm = Console.ReadLine().ToUpper();

            if (confirm == "Y")
            {
                using (SqlConnection connection = new SqlConnection(string.Format(connectionString, db)))
                {
                    string query = "UPDATE Paket_Umroh SET Nama_Paket = @namaPaket, Rute_Perjalanan = @rutePerjalanan, Lama_Perjalanan = @lamaPerjalanan, Fasilitas_yang_disediakan = @fasilitas, Harga_Paket = @hargaPaket WHERE Id_Paket = @idPaket";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idPaket", idPaket);
                    command.Parameters.AddWithValue("@namaPaket", namaPaket);
                    command.Parameters.AddWithValue("@rutePerjalanan", rutePerjalanan);
                    command.Parameters.AddWithValue("@lamaPerjalanan", lamaPerjalanan);
                    command.Parameters.AddWithValue("@fasilitas", fasilitas);
                    command.Parameters.AddWithValue("@hargaPaket", hargaPaket);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} baris telah diubah.");
                }
            }
            else if (confirm == "N")
            {
                Console.WriteLine("Data dibatalkan.");
            }
            else
            {
                Console.WriteLine("Pilihan tidak valid.");
            }
        }



        static void DeletePaketUmroh(string db)
        {
            Console.WriteLine("Masukkan ID paket umroh yang ingin dihapus:");
            string idPaket = Console.ReadLine();

            Console.WriteLine("Apakah anda ingin menghapus data ini? (Y/N)");
            string confirm = Console.ReadLine().ToUpper();

            if (confirm == "Y")
            {
                using (SqlConnection connection = new SqlConnection(string.Format(connectionString, db)))
                {
                    string query = "DELETE FROM Paket_Umroh WHERE Id_Paket = @idPaket";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idPaket", idPaket);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} baris telah dihapus.");
                }
            }
            else if (confirm == "N")
            {
                Console.WriteLine("Penghapusan data dibatalkan.");
            }
            else
            {
                Console.WriteLine("Pilihan tidak valid.");
            }
        }




        static void ManageDataJamaah(string db)
        {
            while (true)
            {
                Console.WriteLine("\nMenu Manajemen Data Jamaah:");
                Console.WriteLine("1. Lihat Semua Data");
                Console.WriteLine("2. Tambah Data Jamaah");
                Console.WriteLine("3. Edit");
                Console.WriteLine("4. Hapus");
                Console.WriteLine("5. Kembali ke Menu Utama");
                Console.Write("Pilih menu (1-5): ");
                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (choice)
                {
                    case '1':
                        ReadDataJamaah(db);
                        break;
                    case '2':
                        AddDataJamaah(db);
                        break;
                    case '3':
                        EditDataJamaah(db);
                        break;
                    case '4':
                        DeleteDataJamaah(db);
                        break;
                    case '5':
                        return;
                    default:
                        Console.WriteLine("Pilihan tidak valid!");
                        break;
                }
            }
        }

        static void ReadDataJamaah(string db)
        {
            using (SqlConnection connection = new SqlConnection(string.Format(connectionString, db)))
            {
                string query = "SELECT * FROM Jamaah";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"\nID Jamaah: {reader["Id_Jamaah"]} \nNama Lengkap: {reader["Nama_Lengkap"]} \nJenis Kelamin: {reader["Jenis_Kelamin"]} \nTanggal Lahir: {reader["Tanggal_Lahir"]}" +
                        $" \nAlamat: {reader["Alamat"]} \nNomor Telepon: {reader["Nomor_Telepon"]} \nTanggal Order: {reader["Tanggal_Order"]} \nTanggal Keberangkatan: {reader["Tanggal_Keberangkatan"]} " +
                        $"\nStatus Keberangkatan: {reader["Status_Keberangkatan"]} \nStatus Jamaah: {reader["Status_Jamaah"]} \nID Hotel: {reader["Id_Hotel"]}");
                }
                reader.Close();
            }
        }


        static void AddDataJamaah(string db)
{
    Console.WriteLine("Masukkan ID Jamaah (Maksimal 8 karakter):");
    string idJamaah = Console.ReadLine();
    Console.WriteLine("Masukkan Nama Lengkap:");
    string namaLengkap = Console.ReadLine();
    Console.WriteLine("Masukkan Jenis Kelamin (L/P):");
    string jenisKelamin = Console.ReadLine();
    Console.WriteLine("Masukkan Tanggal Lahir (yyyy-MM-dd):");
    DateTime tanggalLahir = DateTime.Parse(Console.ReadLine());
    Console.WriteLine("Masukkan Alamat:");
    string alamat = Console.ReadLine();
    Console.WriteLine("Masukkan Nomor Telepon:");
    string nomorTelepon = Console.ReadLine();
    Console.WriteLine("Masukkan Tanggal Order (yyyy-MM-dd HH:mm:ss):");
    DateTime tanggalOrder = DateTime.Parse(Console.ReadLine());
    Console.WriteLine("Masukkan Tanggal Keberangkatan (yyyy-MM-dd HH:mm:ss):");
    DateTime tanggalKeberangkatan = DateTime.Parse(Console.ReadLine());
    Console.WriteLine("Masukkan Status Keberangkatan (Belum Berangkat/Berangkat/Pulang):");
    string statusKeberangkatan = Console.ReadLine();
    Console.WriteLine("Masukkan Status Jamaah (Ketua Kelompok/Anggota):");
    string statusJamaah = Console.ReadLine();
    Console.WriteLine("Masukkan ID Hotel:");
    string idHotel = Console.ReadLine();

    Console.WriteLine("Apakah data yang Anda masukkan sudah benar? (Y/N)");
    string inputConfirmation = Console.ReadLine().ToUpper();

    if (inputConfirmation == "Y")
    {
        using (SqlConnection connection = new SqlConnection(string.Format(connectionString, db)))
        {
            string query = "INSERT INTO Jamaah (Id_Jamaah, Nama_Lengkap, Jenis_Kelamin, Tanggal_Lahir, Alamat, Nomor_Telepon, Tanggal_Order, Tanggal_Keberangkatan, Status_Keberangkatan, Status_Jamaah, Id_Hotel)" +
                        " VALUES (@idJamaah, @namaLengkap, @jenisKelamin, @tanggalLahir, @alamat, @nomorTelepon, @tanggalOrder, @tanggalKeberangkatan, @statusKeberangkatan, @statusJamaah, @idHotel)";
            SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idJamaah", idJamaah);
                    command.Parameters.AddWithValue("@namaLengkap", namaLengkap);
                    command.Parameters.AddWithValue("@jenisKelamin", jenisKelamin);
                    command.Parameters.AddWithValue("@tanggalLahir", tanggalLahir);
                    command.Parameters.AddWithValue("@alamat", alamat);
                    command.Parameters.AddWithValue("@nomorTelepon", nomorTelepon);
                    command.Parameters.AddWithValue("@tanggalOrder", tanggalOrder);
                    command.Parameters.AddWithValue("@tanggalKeberangkatan", tanggalKeberangkatan);
                    command.Parameters.AddWithValue("@statusKeberangkatan", statusKeberangkatan);
                    command.Parameters.AddWithValue("@statusJamaah", statusJamaah);
                    command.Parameters.AddWithValue("@idHotel", idHotel);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} baris telah ditambahkan.");
                }
    }
    else
    {
        Console.WriteLine("Silakan periksa kembali data yang dimasukkan.");
    }
}



        static void EditDataJamaah(string db)
        {
            Console.WriteLine("Masukkan ID jamaah yang ingin diedit:");
            string idJamaah = Console.ReadLine();

            Console.WriteLine("Masukkan Nama Lengkap:");
            string namaLengkap = Console.ReadLine();
            Console.WriteLine("Masukkan Jenis Kelamin (L/P):");
            string jenisKelamin = Console.ReadLine();
            Console.WriteLine("Masukkan Tanggal Lahir (yyyy-MM-dd):");
            DateTime tanggalLahir = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Masukkan Alamat:");
            string alamat = Console.ReadLine();
            Console.WriteLine("Masukkan Nomor Telepon:");
            string nomorTelepon = Console.ReadLine();
            Console.WriteLine("Masukkan Tanggal Order (yyyy-MM-dd HH:mm:ss):");
            DateTime tanggalOrder = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Masukkan Tanggal Keberangkatan (yyyy-MM-dd HH:mm:ss):");
            DateTime tanggalKeberangkatan = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Masukkan Status Keberangkatan (Belum Berangkat/Berangkat/Pulang):");
            string statusKeberangkatan = Console.ReadLine();
            Console.WriteLine("Masukkan Status Jamaah (Ketua Kelompok/Anggota):");
            string statusJamaah = Console.ReadLine();
            Console.WriteLine("Masukkan ID Hotel:");
            string idHotel = Console.ReadLine();

            Console.WriteLine("Apakah data yang Anda masukkan sudah benar? (Y/N)");
            string inputConfirmation = Console.ReadLine().ToUpper();

            if (inputConfirmation == "Y")
            {
                using (SqlConnection connection = new SqlConnection(string.Format(connectionString, db)))
                {
                    string query = "UPDATE Jamaah SET Nama_Lengkap = @namaLengkap, Jenis_Kelamin = @jenisKelamin, Tanggal_Lahir = @tanggalLahir, Alamat = @alamat," +
                        " Nomor_Telepon = @nomorTelepon, Tanggal_Order = @tanggalOrder, Tanggal_Keberangkatan = @tanggalKeberangkatan, Status_Keberangkatan = @statusKeberangkatan," +
                        " Status_Jamaah = @statusJamaah, Id_Hotel = @idHotel WHERE Id_Jamaah = @idJamaah";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idJamaah", idJamaah);
                    command.Parameters.AddWithValue("@namaLengkap", namaLengkap);
                    command.Parameters.AddWithValue("@jenisKelamin", jenisKelamin);
                    command.Parameters.AddWithValue("@tanggalLahir", tanggalLahir);
                    command.Parameters.AddWithValue("@alamat", alamat);
                    command.Parameters.AddWithValue("@nomorTelepon", nomorTelepon);
                    command.Parameters.AddWithValue("@tanggalOrder", tanggalOrder);
                    command.Parameters.AddWithValue("@tanggalKeberangkatan", tanggalKeberangkatan);
                    command.Parameters.AddWithValue("@statusKeberangkatan", statusKeberangkatan);
                    command.Parameters.AddWithValue("@statusJamaah", statusJamaah);
                    command.Parameters.AddWithValue("@idHotel", idHotel);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} baris telah diubah.");
                }
            }
            else
            {
                Console.WriteLine("Silakan periksa kembali data yang dimasukkan.");
            }
        }



        static void DeleteDataJamaah(string db)
        {
            Console.WriteLine("Masukkan ID jamaah yang ingin dihapus:");
            string idJamaah = Console.ReadLine();

            Console.WriteLine("Apakah Anda yakin ingin menghapus data ini? (Y/N)");
            string confirmation = Console.ReadLine().ToUpper();

            if (confirmation == "Y")
            {
                using (SqlConnection connection = new SqlConnection(string.Format(connectionString, db)))
                {
                    string query = "DELETE FROM Jamaah WHERE Id_Jamaah = @idJamaah";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idJamaah", idJamaah);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} baris telah dihapus.");
                }
            }
            else
            {
                Console.WriteLine("Penghapusan data dibatalkan.");
            }
        }



    }
}
