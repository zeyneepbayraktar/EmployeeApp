using System;
using System.IO;

// 1. Üst Sınıf: Employee (Soyut Sınıf)
public abstract class Employee : IComparable<Employee>
{
    private int id;
    private string name;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    // Constructor
    public Employee(string name, int id)
    {
        this.name = name;
        this.id = id;
    }

    // IComparable: id değerine göre küçükten büyüğe sıralama
    public int CompareTo(Employee other)
    {
        if (other == null) return 1;
        return this.id.CompareTo(other.id);
    }

    // Çok biçimlilik için soyut metot
    public abstract decimal Earnings();
}

// 2. Alt Sınıf: CommissionEmployee (Soyut DEĞİL, nesne üretilebilir)
public class CommissionEmployee : Employee
{
    private decimal grossSales;
    private double commissionRate;

    public decimal GrossSales
    {
        get { return grossSales; }
        set { grossSales = value >= 0 ? value : 0; }
    }

    public double CommissionRate
    {
        get { return commissionRate; }
        set { commissionRate = (value >= 0 && value <= 1) ? value : 0; }
    }

    // Constructor: base() ile üst sınıfa veri yollanır, kalanlar kendi field'larına atanır
    public CommissionEmployee(string name, int id, decimal grossSales, double commissionRate) 
        : base(name, id)
    {
        GrossSales = grossSales;
        CommissionRate = commissionRate;
    }

    public override decimal Earnings()
    {
        return (decimal)commissionRate * grossSales;
    }
}

// 3. Alt Sınıfın Altı: BasePlusCommissionEmployee
public class BasePlusCommissionEmployee : CommissionEmployee
{
    private decimal baseSalary;

    public decimal BaseSalary
    {
        get { return baseSalary; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(BaseSalary)} must be >= 0");
            }
            baseSalary = value; // Atama işlemi eklendi
        }
    }

    // Constructor
    public BasePlusCommissionEmployee(string name, int id, decimal grossSales, double commissionRate, decimal baseSalary) 
        : base(name, id, grossSales, commissionRate)
    {
        BaseSalary = baseSalary;
    }

    // Kod tekrarı yapmadan üst sınıfın metodunu (base.Earnings) çağırma
    public override decimal Earnings()
    {
        return BaseSalary + base.Earnings();
    }
}

// 4. Çalıştırıcı Sınıf
class Program
{
    static void Main()
    {
        // Try-Catch Hata Yönetimi Bloğu
        try
        {
            // Polimorfizm (Çok Biçimlilik) Gösterimi için Employee Dizisi
            Employee[] employees = new Employee[2];

            // Doğru veri tipleriyle nesnelerin üretilmesi (String yerine Int ve Decimal değerler)
            employees[0] = new CommissionEmployee("Zeynep", 2222, 50000m, 0.10);
            employees[1] = new BasePlusCommissionEmployee("Alya", 1111, 30000m, 0.05, 15000m);

            // Diziyi IComparable (id değerlerine göre) sıralama
            Array.Sort(employees);

            Console.WriteLine("--- Çalışanların Kazanç Listesi (ID Sıralı) ---\n");
            
            // Foreach ile polimorfik çağrı
            foreach (Employee emp in employees)
            {
                Console.WriteLine($"Çalışan ID: {emp.Id}");
                Console.WriteLine($"Adı: {emp.Name}");
                Console.WriteLine($"Toplam Kazanç: {emp.Earnings():C}\n"); // :C yerel para birimi formatı yapar
            }

            // Senaryoda yer alan hataları simüle etmek veya tetiklemek istersen test edebilirsin:
            // Örnek FormatException tetikleme simülasyonu:
            // int.Parse("HatalıSayı"); 

            // Örnek FileNotFoundException tetikleme simülasyonu:
            // File.Open("olmayan_dosya.txt", FileMode.Open);
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Veri biçimlendirme hatası oluştu: {ex.Message}");
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Aranan dosya sistemde bulunamadı: {ex.FileName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Beklenmedik bir hata oluştu: {ex.Message}");
        }
    }
}