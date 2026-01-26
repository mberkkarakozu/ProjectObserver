# 🛠️ TECH STACK & ARCHITECTURE PARADIGMS

## 1. Core Technology (Çekirdek)
* **Engine:** Unity 2022 LTS (Long Term Support)
* **Pipeline:** URP (Universal Render Pipeline) - *Mobil performans odaklı lighting ve post-processing.*
* **Language:** C# (Latest supported version)
* **Target Platform:** Mobile (Android/iOS) & PC Standalone (Debug)

## 2. Architectural Paradigms (Mimari Paradigmalar)
Bu proje rastgele kod yazımı (Spaghetti Code) yerine şu mühendislik prensiplerine sıkı sıkıya bağlıdır:

### A. Composition Over Inheritance (Kalıtım Yerine Kompozisyon)
* **Kural:** Derin `BaseClass : ChildClass` zincirleri yasaktır.
* **Yöntem:** Özellikler "Component" ve "Interface" olarak kazanılır.
    * *Yanlış:* `Door` sınıfı `ScannableObject`'ten türer.
    * *Doğru:* `Door` sınıfı `IScannable` ve `IInteractable` arayüzlerini (interface) imzalar (`Implement`).

### B. Event-Driven Architecture (Olay Güdümlü Mimari)
* **Kural:** Sistemler birbirine doğrudan referans (Hard Dependency) vermez.
* **Yöntem:** Observer Pattern (C# Events / Actions) kullanılır.
    * *Örnek:* `LidarScanner` kapıyı açmaya çalışmaz. Sadece `OnScanHit` eventini fırlatır. Kapı bu eventi dinler ve kendini açar.

### C. Interface-Based Programming (Arayüz Tabanlı Kodlama)
* **Kural:** `GetComponent<ConcreteClass>()` kullanımı minimize edilir.
* **Yöntem:** İletişim her zaman kontratlar (Interface) üzerinden yapılır.
    * Kod `TnkMovement` tanımaz, `IMovable` tanır.
    * Kod `Chest` tanımaz, `IScannable` tanır.

### D. Reactive Logic (Reaktif Mantık)
* **Kural:** Her frame çalışan `Update()` döngüleri (Polling) yasaktır/minimize edilir.
* **Yöntem:** Sadece input dinlemek için `Update` kullanılır. Geri kalan tüm mantık (Kapı hareketi, Lidar taraması) **Coroutines** veya **Async/Await** ile zaman tabanlı yönetilir.

## 3. Coding Standards (Anayasa)
1.  **SSOT (Single Source of Truth):** `ai-context` klasöründeki dosyalar projenin tek gerçeğidir. Hafıza değil, doküman esastır.
2.  **SOLID Principles:**
    * **S:** Her scriptin tek bir işi olur (Lidar sadece tarar, efekt oynatmaz; efekti `LidarFX` oynatır).
    * **O:** Kodlar değişime kapalı, gelişime açık olmalıdır (Yeni bir taranabilir obje ekleyince `LidarScanner.cs` değişmemelidir).
3.  **Naming Conventions:**
    * `PublicVariable` (PascalCase)
    * `ISomeInterface` (I ile başlar)

## 4. Visual Style & Art Direction
* **Keywords:** Matrix Wireframe, Lidar Point Cloud, Dark, Claustrophobic, Industrial.
* **Tech:** Shader Graph, Particle Systems (Grid/Dot styled), Volumetric Lighting (Fake/Optimized).
