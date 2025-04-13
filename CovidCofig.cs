using System;
using System.IO;
using System.Text.Json;

public class CovidConfig
{
    // Nama file konfigurasi
    private const string ConfigFile = "covid_config.json";

    // Struktur data konfigurasi
    public class ConfigData
    {
        public string satuan_suhu { get; set; }
        public int batas_hari_deman { get; set; }
        public string pesan_ditolak { get; set; }
        public string pesan_diterima { get; set; }
    }

    private ConfigData config;

    // Nilai default
    private readonly ConfigData defaultConfig = new ConfigData
    {
        satuan_suhu = "celcius",
        batas_hari_deman = 14,
        pesan_ditolak = "Anda tidak diperbolehkan masuk ke dalam gedung ini",
        pesan_diterima = "Anda dipersilahkan untuk masuk ke dalam gedung ini"
    };

    // Constructor: load konfigurasi
    public CovidConfig()
    {
        LoadConfig();
    }

    // Load config dari file, atau pakai default kalau tidak ada
    private void LoadConfig()
    {
        if (File.Exists(ConfigFile))
        {
            try
            {
                string json = File.ReadAllText(ConfigFile);
                config = JsonSerializer.Deserialize<ConfigData>(json);
            }
            catch (Exception)
            {
                UseDefaultConfig();
            }
        }
        else
        {
            UseDefaultConfig();
        }
    }

    // Pakai default config dan simpan ke file
    private void UseDefaultConfig()
    {
        config = defaultConfig;
        SaveConfig();
    }

    // Simpan config ke file JSON
    private void SaveConfig()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(config, options);
        File.WriteAllText(ConfigFile, json);
    }

    // Getter
    public string SatuanSuhu => config.satuan_suhu;
    public int BatasHariDemam => config.batas_hari_deman;
    public string PesanDitolak => config.pesan_ditolak;
    public string PesanDiterima => config.pesan_diterima;

    // Method untuk ubah satuan suhu celcius ↔ fahrenheit
    public void UbahSatuan()
    {
        if (config.satuan_suhu.ToLower() == "celcius")
            config.satuan_suhu = "fahrenheit";
        else
            config.satuan_suhu = "celcius";

        SaveConfig(); // simpan ke file
    }
}
