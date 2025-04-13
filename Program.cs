using System;

class Program
{
    static void Main(string[] args)
    {
        CovidConfig config = new CovidConfig();

        // Ganti satuan (bisa dihapus kalau nggak mau diganti)
        config.UbahSatuan();

        Console.WriteLine($"Berapa suhu badan anda saat ini? Dalam nilai {config.SatuanSuhu}: ");
        double suhu = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala demam?");
        int hari = Convert.ToInt32(Console.ReadLine());

        bool suhuNormal = false;

        if (config.SatuanSuhu == "celcius")
        {
            suhuNormal = suhu >= 36.5 && suhu <= 37.5;
        }
        else if (config.SatuanSuhu == "fahrenheit")
        {
            suhuNormal = suhu >= 97.7 && suhu <= 99.5;
        }

        if (suhuNormal && hari < config.BatasHariDemam)
        {
            Console.WriteLine(config.PesanDiterima);
        }
        else
        {
            Console.WriteLine(config.PesanDitolak);
        }
    }
}
