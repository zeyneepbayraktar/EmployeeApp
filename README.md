# Employee Hierarchy — C# OOP Demo

Bu proje, C# ile nesne yönelimli programlamanın temel kavramlarını gösteren bir çalışan hiyerarşisi uygulamasıdır.

## Sınıf Yapısı
Employee (abstract, IComparable<Employee>)
└── CommissionEmployee
└── BasePlusCommissionEmployee

## Kullanılan OOP Kavramları

- **Encapsulation** — Private field'lar, public property'ler ve değer doğrulama (validation)
- **Inheritance** — `CommissionEmployee : Employee` ve `BasePlusCommissionEmployee : CommissionEmployee`
- **Polymorphism** — `abstract Earnings()` metodu, `Employee[]` dizisiyle polimorfik çağrı
- **Interface** — `IComparable<Employee>` ile `Array.Sort()` entegrasyonu (ID'ye göre sıralama)
- **Exception Handling** — `try-catch` bloğu; `FormatException`, `FileNotFoundException`, genel `Exception`
- **`base()` kullanımı** — Constructor zinciri ve `base.Earnings()` ile kod tekrarından kaçınma

## Çalıştırma

```bash
dotnet run
```

## Notlar

- `BaseSalary` setter'ında negatif değer için `ArgumentOutOfRangeException` fırlatılır.
- Para formatı için `{value:C}` (currency format specifier) kullanılmıştır.
- `FormatException` ve `FileNotFoundException` senaryoları yorum satırı olarak bırakılmıştır; test etmek için açılabilir.
