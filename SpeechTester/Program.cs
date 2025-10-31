using System;
using System.IO;
using Vosk; // Vosk kütüphanesi

public class VoskDemo
{
    public static void Main()
    {
        Vosk.Vosk.SetLogLevel(-1); // Gereksiz Vosk loglarını kapatır

        // 1. Kullanıcıdan Dil Seçimini Al
        Console.WriteLine("Lütfen bir dil seçin (Please select a language):");
        Console.WriteLine("  1: Türkçe (tr)");
        Console.WriteLine("  2: English (en)");
        Console.Write("Seçiminiz (1 veya 2): ");

        string choice = Console.ReadLine();
        string modelPath = "";
        string dilAdi = "";

        if (choice == "1")
        {
            modelPath = "vosk-model-small-tr-0.3";
            dilAdi = "Türkçe";
        }
        else if (choice == "2")
        {
            modelPath = "vosk-model-small-en-us-0.15";
            dilAdi = "İngilizce";
        }
        else
        {
            Console.WriteLine("Geçersiz seçim. Program kapatılıyor.");
            return;
        }

        // 2. Seçilen Modelin Hata Kontrolü
        if (!Directory.Exists(modelPath))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nHATA: '{dilAdi}' modeli için klasör bulunamadı: {modelPath}");
            Console.WriteLine("Lütfen bu model klasörünü indirdiğinizden,");
            Console.WriteLine("proje klasörüne eklediğinizden ve içindeki tüm dosyaların");
            Console.WriteLine("'Copy to Output Directory' ayarını 'Copy if newer' yaptığınızdan emin olun.");
            Console.ResetColor();
            Console.ReadKey();
            return;
        }

        // 3. Modeli Yükle ve Tanıyıcıyı Oluştur
        Console.WriteLine($"\n'{dilAdi}' modeli yükleniyor...");
        Model model = new Model(modelPath);
        SpeechRecognizer recognizer = new SpeechRecognizer(model, 16000.0f);

        // 4. Olayları (Events) bağla
        recognizer.PartialResultDecoded += (s, e) =>
        {
            if (!string.IsNullOrEmpty(e.Result.Partial))
            {
                Console.Write($"\r[Dinleniyor...]: {e.Result.Partial}     ");
            }
        };

        recognizer.ResultDecoded += (s, e) =>
        {
            if (!string.IsNullOrEmpty(e.Result.Text))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\r[SONUÇ]: {e.Result.Text}                ");
                Console.ResetColor();
            }
        };

        // 5. Dinlemeyi Başlat
        recognizer.StartListening();

        Console.WriteLine($"\nLütfen {dilAdi} konuşun...");
        Console.WriteLine("(Uygulamayı durdurmak için 'Enter' tuşuna basın)");
        Console.ReadLine();

        // 6. Durdur ve Kaynakları Temizle
        recognizer.StopListening();
        recognizer.Dispose();
        model.Dispose();
    }
}