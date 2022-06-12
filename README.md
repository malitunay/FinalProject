## Kurulum

İlk çalıştırmada seeding ile veritabanına eklenen admin kullanıcısı ile sisteme giriş yapılarak metotlar kullanılabilir.

![](https://github.com/malitunay/FinalProject/blob/master/screenshots/vt.jpg)

## Metotlar

### Kullanıcı Ekleme, Güncelleme ve Silme

Kullanıcı eklenirken random üretilen bir parola kullanıcının mail adresine gönderilir ve hash'lenen parola MsSql veritabanına kaydedilir.
Kullanıcı eklenirken random üretilen kredi kartı bilgileri ödemeyi alacak api nin kullandığı mongo db ye kaydedilir. (Kullanıcı ödeme yaparken yan tarafta kullanıcıya ait kredi kartı bilgilerini görür)
Kullanıcı silme işleminde gerçekte nesne silinmez, isActive bilgisi false olarak güncellenir.

### Daire Ekleme, Güncelleme ve Silme

Daire eklenirken veya güncellenirken sistemde kayıtlı olan bir kullanıcıya atanabilir.
Daire silme işleminde gerçekte nesne silinmez, isActive bilgisi false olarak güncellenir.

### Fatura Oluşturma

Fatura bir daireye veya tüm dairelere (aynı fatura tipi ve aynı miktarda) oluşturulabilir.
Admin dairelere oluşturuduğu faturaları ödenenler ve ödenmeyenler şeklinde iki farklı kategoride görebilir.
Kullanıcı kendi dairesine oluşturulan faturaları ödedikleri ve ödemedikleri şeklinde iki farklı kategoride görebilir.

### Fatura Ödeme

Daireye atanan kullanıcı daireye kesilen faturaları ödenenler ve ödenmeyenler olarak iki farklı kategoride görüntüler ve ödeme yapabilir.
Ödeme yapılırken forma girilen kredi kartı bilgileri ödemeyi alan API'ye gönderilir. Ödemeyi alan API kredi kartı bilgilierini ve kredi kartı limitini kontrol ederek ödemeyi alır ve limiti günceller. Başarılı geri dönüş olan ödemelerde MsSql de ilgili faturanın durumu ödendi olarak güncellenir.

### Mesajlaşma

Admin ile kullanıcılar arasında mesajlaşılabilir.
İki tarafta okumadığı mesaj sayısını span içerisinde görebilir.
Admin, ekranın sağ kısmında bulunan kullanıcı listesini açarak kullanıcılara mesaj atabilir. Okumadığı mesaj sayısını kullanıcı isminin yan tarafında görebilir.
Kullanıcı tarafında ise admin ile mesajlaşma yapılabilir ve okunmayan mesaj sayısı görüntülenebilir.

## Ekran Görüntüleri

### Veritabanı Diagramı

![][(https://github.com/malitunay/FinalProject/blob/master/screenshots/vt.jpg)]

### Admin (Site Yöneticisi)

Login Ekranı:
![][(https://github.com/malitunay/FinalProject/blob/master/screenshots/login.png)]

Kullanıcı Listesi:
![][(https://github.com/malitunay/FinalProject/blob/master/screenshots/users.png)]

Kullanıcı Ekleme:
![][(https://github.com/malitunay/FinalProject/blob/master/screenshots/adduser.png)]

Kullanıcı Güncelleme:
![][(https://github.com/malitunay/FinalProject/blob/master/screenshots/updateuser.png)]

Daire Ekleme / Daireyi Kullanıcıya Atama:
![][(https://github.com/malitunay/FinalProject/blob/master/screenshots/addapartment.png)]

Bir daireye bir fatura gönderme:
![][(https://github.com/malitunay/FinalProject/blob/master/screenshots/addinvoice.png)]

Tüm dairelere birer fatura gönderme:
![][(https://github.com/malitunay/FinalProject/blob/master/screenshots/addinvoicetoall.png)]

Daire Listesi:
![][(https://github.com/malitunay/FinalProject/blob/master/screenshots/apartmentlist.png)]

Daire Güncelleme:
![][(https://github.com/malitunay/FinalProject/blob/master/screenshots/updateapartment.png)]

Fatura Listesi:
![][(https://github.com/malitunay/FinalProject/blob/master/screenshots/invoicelistadmin.png)]

Mesajlaşma:
![][(https://github.com/malitunay/FinalProject/blob/master/screenshots/messages.png)]



### User (Daire Sakini)

Daireye Ait Fatura Listesi:
![][(https://github.com/malitunay/FinalProject/blob/master/screenshots/invoiceofuser.png)]

Ödeme Ekranı:
![][(https://github.com/malitunay/FinalProject/blob/master/screenshots/payment.png)]

Mesajlaşma:
![][(https://github.com/malitunay/FinalProject/blob/master/screenshots/messagesofuser.png)]
