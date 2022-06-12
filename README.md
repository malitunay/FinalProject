## Kurulum

İlk çalıştırmada seeding ile veritabanına eklenen admin kullanıcısı ile sisteme giriş yapılarak metotlar kullanılabilir.

![](https://github.com/malitunay/FinalProject/blob/master/screenshots/vt.jpg)

## Metotlar

### Kullanıcı Ekleme, Güncelleme ve Silme

Kullanıcı eklenirken random üretilen bir parola kullanıcının mail adresine gönderilir ve hash'lenen parola MsSql veritabanına kaydedilir.
Kullanıcı eklenirken random üretilen kredi kartı bilgileri ödemeyi alacak api nin kullandığı mongo db ye kaydedilir. (Kullanıcı ödeme yaparken yan tarafta kullanıcıya ait kredi kartı bilgilerini görür)
Kullanıcı silme işleminde gerçekte nesne silinmez, isActive bilgisi false olarak güncellenir.

![][(https://github.com/malitunay/FinalProject/blob/master/screenshots/vt.jpg)]
![][(https://github.com/GelecekVarlik-FullStack-Bootcamp/odev-hafta4-dnet-malitunay/blob/main/diagram.png)
![](https://github.com/GelecekVarlik-FullStack-Bootcamp/odev-hafta4-dnet-malitunay/blob/main/diagram.png)
![](https://github.com/GelecekVarlik-FullStack-Bootcamp/odev-hafta4-dnet-malitunay/blob/main/diagram.png)
![](https://github.com/GelecekVarlik-FullStack-Bootcamp/odev-hafta4-dnet-malitunay/blob/main/diagram.png)

### Daire Ekleme, Güncelleme ve Silme

Daire eklenirken veya güncellenirken sistemde kayıtlı olan bir kullanıcıya atanabilir.
Daire silme işleminde gerçekte nesne silinmez, isActive bilgisi false olarak güncellenir.

![](https://github.com/GelecekVarlik-FullStack-Bootcamp/odev-hafta4-dnet-malitunay/blob/main/diagram.png)
![](https://github.com/GelecekVarlik-FullStack-Bootcamp/odev-hafta4-dnet-malitunay/blob/main/diagram.png)
![](https://github.com/GelecekVarlik-FullStack-Bootcamp/odev-hafta4-dnet-malitunay/blob/main/diagram.png)
![](https://github.com/GelecekVarlik-FullStack-Bootcamp/odev-hafta4-dnet-malitunay/blob/main/diagram.png)
![](https://github.com/GelecekVarlik-FullStack-Bootcamp/odev-hafta4-dnet-malitunay/blob/main/diagram.png)

### Fatura Oluşturma

Fatura bir daireye veya tüm dairelere (aynı fatura tipi ve aynı miktarda) oluşturulabilir.
Admin dairelere oluşturuduğu faturaları ödenenler ve ödenmeyenler şeklinde iki farklı kategoride görebilir.
Kullanıcı kendi dairesine oluşturulan faturaları ödedikleri ve ödemedikleri şeklinde iki farklı kategoride görebilir.

![](https://github.com/GelecekVarlik-FullStack-Bootcamp/odev-hafta4-dnet-malitunay/blob/main/diagram.png)
![](https://github.com/GelecekVarlik-FullStack-Bootcamp/odev-hafta4-dnet-malitunay/blob/main/diagram.png)
![](https://github.com/GelecekVarlik-FullStack-Bootcamp/odev-hafta4-dnet-malitunay/blob/main/diagram.png)
![](https://github.com/GelecekVarlik-FullStack-Bootcamp/odev-hafta4-dnet-malitunay/blob/main/diagram.png)
![](https://github.com/GelecekVarlik-FullStack-Bootcamp/odev-hafta4-dnet-malitunay/blob/main/diagram.png)

### Fatura Ödeme

Daireye atanan kullanıcı daireye kesilen faturaları ödenenler ve ödenmeyenler olarak iki farklı kategoride görüntüler ve ödeme yapabilir.
Ödeme yapılırken forma girilen kredi kartı bilgileri ödemeyi alan API'ye gönderilir. Ödemeyi alan API kredi kartı bilgilierini ve kredi kartı limitini kontrol ederek ödemeyi alır ve limiti günceller. Başarılı geri dönüş olan ödemelerde MsSql de ilgili faturanın durumu ödendi olarak güncellenir.

![](https://github.com/GelecekVarlik-FullStack-Bootcamp/odev-hafta4-dnet-malitunay/blob/main/diagram.png)
![](https://github.com/GelecekVarlik-FullStack-Bootcamp/odev-hafta4-dnet-malitunay/blob/main/diagram.png)
![](https://github.com/GelecekVarlik-FullStack-Bootcamp/odev-hafta4-dnet-malitunay/blob/main/diagram.png)

### Mesajlaşma

Admin ile kullanıcılar arasında mesajlaşılabilir.
İki tarafta okumadığı mesaj sayısını span içerisinde görebilir.

Admin, ekranın sağ kısmında bulunan kullanıcı listesini açarak kullanıcılara mesaj atabilir. Okumadığı mesaj sayısını kullanıcı isminin yan tarafında görebilir.

Kullanıcı tarafında ise admin ile mesajlaşma yapılabilir ve okunmayan mesaj sayısı görüntülenebilir.

![](https://github.com/GelecekVarlik-FullStack-Bootcamp/odev-hafta4-dnet-malitunay/blob/main/diagram.png)
![](https://github.com/GelecekVarlik-FullStack-Bootcamp/odev-hafta4-dnet-malitunay/blob/main/diagram.png)
