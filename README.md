# StackExchangeRedis
.Net Core Web API ile StackExchange.Redis kullanılarak cache işlemlerinin yapıldığı basit bir demo. Uygulamada ülke ve ülkelere ait şehir bilgileri üzerinden redis ile cacheleme yapılmaktadır.

# Endpoint
Örnek endpointler aşağıdaki gibidir.

GetCountry => /api/cities/GetCountry/countryName=turkey

GetCity => /api/cities/GetCity/countryName=turkey&cityId=34

SetCity => /api/cities/SetCity?countryName=turkey (FromBody olarak City nesnesi gönderilmesi gerekmektedir.)


# Redis Hakkında
Redis(Remote Dictionary Server), en temel tanımıyla anahtar-değer (key-value) mantığıyla çalışan bir cache uygulaması. Unique bir Id’ye (key) karşılık bir değer (value) tutar. Veriyi RAM’de tutuyor olması sunucunun kapanması ile verinin tümden yok olması anlamına geliyor. Tabi bunu diske yazmanız mümkün fakat en temel haliyle Redis, veriyi RAM’de kullanmak için tasarlandı.

# Kaynaklar
Redis döküman = https://redis.io/documentation

Redis komutlarının tam listesi. = https://redis.io/commands

Redis veri türleri = http://redis.io/topics/data-types-intro


# Redis Kurulumu
İlk önce https://github.com/MSOpenTech/redis/releases adresine giderek Microsoft Open Tech tarafından çıkarılan son Redis sürümünü indirelim. Yazının yazıldığı tarihte son sürüm 3.2.100 olarak gözükmektedir. 

Kurulum dosyasını indirdikten sonra ilk olarak redis-server.exe programını çalıştırıp Redis serverı aktif hale getireceksiniz bu işlemi yaptıktan sonra aynı klasör içindeki redis-cli.exe eklentisini çalıştırdığınız zaman da artık Redis veri tabanı sistemini kullanabilir veri tabanlarınızı oluşturabilirsiniz.

Redis çalışıp çalışmadığına dair kontrol için redis-cli.exe'yi çalıştıralım ve set value1 key1 yazarak kayıt yapabilir daha sonrada get value1 diyerek key1 değerini elde edebilirsiniz
