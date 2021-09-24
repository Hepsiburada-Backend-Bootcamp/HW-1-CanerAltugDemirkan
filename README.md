# Student API

## How to use

**Student Listesini Özellikleri**

-Id

-Number

-FirstName

-LastName

-TcNo

-Gender

-Department


**Get**

api/v1/students bütün öğrencileri döndürür.

api/v1/students? url'sinden sonra Student'ları attribute'larına göre filtreleyebilirsiniz.

& ile ekstra parametre ekleyebilirsiniz.

Mimarlık okuyan kadın öğrencileri öğrenmek için:
```bash
  api/v1/students?gender=f&department=architect
```

Listeyi istediğiniz sortby parametresi ile istediğiniz özelliğe göre sıralayabilirsiniz.

Öğrencileri cinsiyete göre sıralamak için:
```bash
  api/v1/students?sortby=gender
```

Sıralamayı büyükten küçüğe yapmak için özellikten sonra virgül(,) ile desc yazmanız lazım:
```bash
  api/v1/students?sortby=gender,desc
```

Birden fazla özelliğe göre de sıralama yapabilirsiniz.

Öğrencileri önce cinsiyete sonra bölümüne göre sıralamak için iki özelliği arasına noktalı virgül(;) koymanız lazım:
```bash
  api/v1/students?sortby=gender;department
```


**Post**

Body'de json olarak yolladığınız özelliklere uygun öğrenci yaratıp listeye ekler

Id otomatik olarak üretilir. Verdiğiniz Id değeri işleme sokulmaz



**Put**

Body'den gelen id'ye ait öğrencinin özelliklerini body'den gelen diğer özelliklerle değiştirir



**Delete**

Query'den gelen id'ye ait kaydı listeden kaldırır
