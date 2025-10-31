# C# ile Çift Dilli (TR/EN) Çevrimdışı Speech-to-Text Projesi

Bu proje, Vosk kütüphanesini kullanarak C#'ta çalışan bir konsol uygulamasıdır. Kullanıcıdan Türkçe veya İngilizce seçmesini ister ve mikrofondan alınan sesi gerçek zamanlı olarak metne dönüştürür.

---

##  Özellikler

* **Çift Dil Desteği:** Türkçe (TR) ve İngilizce (EN).
* **Çevrimdışı (Offline):** İnternet bağlantısı gerektirmez, tüm tanıma işlemi yerel olarak yapılır.
* **Gerçek Zamanlı:** Konuşurken anlık olarak kısmi sonuçları (`[Dinleniyor...]`) ve duraksayınca tam sonucu (`[SONUÇ]`) gösterir.

---

##  Kullanılan Teknolojiler

* **C#** (.NET 7.0 / .NET 6.0)
* **Vosk (v0.3.38):** Çevrimdışı konuşma tanıma kütüphanesi.
* **NAudio (v2.2.1):** Mikrofon girişi için .NET ses kütüphanesi (Vosk'un `SpeechRecognizer` sınıfı tarafından arka planda kullanılır).

---

## 🚀 Projeyi Çalıştırma

### 1. Adım: Gerekli Modelleri İndirin

Bu projenin çalışması için Vosk'un Türkçe ve İngilizce dil modellerine ihtiyacı vardır. Bu modeller depoya dahil **değildir**, manuel olarak indirilip projeye eklenmesi gerekir.

1.  Aşağıdaki iki küçük (small) modeli indirin:
    * **İngilizce Model (EN):** [vosk-model-small-en-us-0.15](https://alphacephei.com/vosk/models/vosk-model-small-en-us-0.15.zip) (40 MB)
    * **Türkçe Model (TR):** [vosk-model-small-tr-0.3](https://alphacephei.com/vosk/models/vosk-model-small-tr-0.3.zip) (43 MB)

2.  İndirdiğiniz `.zip` dosyalarını bir klasöre çıkartın. Elinizde `vosk-model-small-en-us-0.15` ve `vosk-model-small-tr-0.3` adında iki klasör olmalıdır.

### 2. Adım: Projeyi Visual Studio'da Açın

1.  Bu depoyu klonlayın veya ZIP olarak indirin.
2.  `SpeechTester.sln` dosyasını Visual Studio 2022 ile açın.
3.  Visual Studio, `Vosk` ve `NAudio` paketlerini NuGet üzerinden otomatik olarak geri yükleyecektir.

### 3. Adım: Modelleri Projeye Ekleyin (En Önemli Adım)

1.  Visual Studio'da **"Çözüm Gezgini" (Solution Explorer)** penceresini açın.
2.  1. Adım'da zip'ten çıkardığınız **iki model klasörünü de** (`vosk-model-small-en-us-0.15` ve `vosk-model-small-tr-0.3`) sürükleyip "Çözüm Gezgini"ndeki **`SpeechTester`** proje adınızın üzerine bırakın.
3.  Proje dosyası (`.csproj`) bu dosyaların kopyalanması için zaten ayarlanmıştır. Ayarların doğru olduğundan emin olmak için:
    * Projedeki `vosk-model-small-en-us-0.15` klasörünün içindeki tüm dosyaları seçin.
    * **"Özellikler" (Properties)** penceresinde, **"Derleme Dizinine Kopyala" (Copy to Output Directory)** ayarının **"Daha yeniyse kopyala" (Copy if newer)** olduğunu doğrulayın.
    * Aynı doğrulamayı `vosk-model-small-tr-0.3` klasörünün içindekiler için de yapın.

### 4. Adım: Çalıştırın

1.  Projeyi (Yeşil 'Start' ‣ butonu ile) çalıştırın.
2.  Program sizden dil seçmenizi isteyecek (1: TR, 2: EN).
3.  Seçim yaptıktan sonra konuşmaya başlayabilirsiniz.

---

## 🔧 Kod Nasıl Çalışır?

1.  **Model Yükleme:** Kullanıcının seçimine göre (`tr` veya `en`) ilgili model klasörünün yolu (`modelPath`) bir `Model` nesnesine yüklenir.
2.  **Tanıyıcı Oluşturma:** `SpeechRecognizer` sınıfı, bu `Model` nesnesi ile başlatılır. Bu sınıf, `NAudio`'yu arka planda kullanarak mikrofonu yönetir.
3.  **Olay (Event) Dinleme:** İki ana olay (event) dinlenir:
    * `PartialResultDecoded`: Kullanıcı konuşurken tetiklenir ve anlık metni (`[Dinleniyor...]`) ekrana yazar.
    * `ResultDecoded`: Kullanıcı duraksadığında tetiklenir ve cümlenin tamamlanmış halini (`[SONUÇ]`) ekrana yazar.
4.  **Başlatma/Durdurma:** `recognizer.StartListening()` komutu mikrofonu açar ve `Console.ReadLine()` programın kapanmasını engeller.
