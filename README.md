# Pratik - JWT Kimlik Doğrulama Sistemi ReadMe

Bu doküman, bir JWT (JSON Web Token) tabanlı kimlik doğrulama sistemi oluşturmak için izlenmesi gereken adımları özetler.

---

## 1. Kullanıcı Modeli Oluşturma
- **User Modeli**:
  - `Id`: Benzersiz kullanıcı kimliği (int, primary key).
  - `Email`: Kullanıcı e-posta adresi (string, benzersiz).
  - `Password`: Kullanıcı şifresi (string).

---

## 2. Veritabanı Ayarları
- **DbContext**: 
  - Bir `JwtDbContext` sınıfı oluşturun.
  - `User` modelini DbContext’e ekleyin (`DbSet<User> Users`).
- **Migration ve Veritabanı**:
  - Entity Framework kullanarak migration oluşturun ve veritabanını güncelleyin.

---

## 3. JWT Oluşturma
- **Controller**: `AuthController` adında bir kontrolör oluşturun.
  - `Login` Metodu:
    - Kullanıcının `Email` ve `Password` bilgilerini alır.
    - Kimlik doğrulama başarısızsa `BadRequest` döner.
    - Doğruysa JWT oluşturur ve döner.
- **JWT Helper**:
  - JWT oluşturmak için bir `JwtHelper` sınıfı yazın.
  - Token içeriği: Kullanıcı `Id`, `Email`, `Role`, vb.
  - Token süresi ve gizli anahtarı `appsettings.json` üzerinden yapılandırın.

---

## 4. JWT Doğrulama
- **Authentication Middleware**:
  - `AddAuthentication` ve `AddJwtBearer` ile JWT doğrulama mekanizması ekleyin.
  - `TokenValidationParameters` ile doğrulama kurallarını belirleyin (ör. `Issuer`, `Audience`, `Lifetime`, vb.).
- **Authorize**:
  - JWT doğrulaması gereken endpointlere `[Authorize]` niteliği ekleyin.

---

## Gelişmiş Özellikler
- **Swagger JWT Desteği**:
  - Swagger için JWT giriş alanı ekleyin (`AddSwaggerGen`).
  - Header’da JWT gönderimi için gerekli ayarları yapın.
- **Veri Çekme Örnekleri**:
  - Kimlik doğrulaması gerektiren endpointlerden veri almak için `[Authorize]` kullanın.
  - Örneğin, veritabanındaki tüm kullanıcı e-posta adreslerini listeleme endpointi ekleyin.

