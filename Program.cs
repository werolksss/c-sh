using System;
using System.IO;

class SchetDlyaOplaty
{
    public static bool SohranyatVychislyaemyePolya { get; set; }

// обычные поля
    public double OplataZaDen;
    public int KolichestvoDney;
    public double ShtrafZaDen;
    public int KolichestvoDneyZaderzhki;

// вычисляемые поля
    public double SummaBezShtrafa;
    public double Shtraf;
    public double ObshayaSumma;

    public SchetDlyaOplaty()
    {
    }

    public SchetDlyaOplaty(double oplataZaDen, int kolichestvoDney, double shtrafZaDen, int kolichestvoDneyZaderzhki)
    {
        OplataZaDen = oplataZaDen;
        KolichestvoDney = kolichestvoDney;
        ShtrafZaDen = shtrafZaDen;
        KolichestvoDneyZaderzhki = kolichestvoDneyZaderzhki;

        Raschitat();
    }

// метод считает вычисляемые поля
    public void Raschitat()
    {
        SummaBezShtrafa = OplataZaDen * KolichestvoDney;
        Shtraf = ShtrafZaDen * KolichestvoDneyZaderzhki;
        ObshayaSumma = SummaBezShtrafa + Shtraf;
    }

// вывод информации
    public void Pokazat()
    {
        Console.WriteLine("Оплата за день: " + OplataZaDen);
        Console.WriteLine("Количество дней: " + KolichestvoDney);
        Console.WriteLine("Штраф за один день задержки: " + ShtrafZaDen);
        Console.WriteLine("Количество дней задержки: " + KolichestvoDneyZaderzhki);
        Console.WriteLine("Сумма без штрафа: " + SummaBezShtrafa);
        Console.WriteLine("Штраф: " + Shtraf);
        Console.WriteLine("Общая сумма к оплате: " + ObshayaSumma);
    }

// сохранение в файл
    public void SohranitVFayl(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            writer.WriteLine(OplataZaDen);
            writer.WriteLine(KolichestvoDney);
            writer.WriteLine(ShtrafZaDen);
            writer.WriteLine(KolichestvoDneyZaderzhki);

            if (SohranyatVychislyaemyePolya == true)
            {
                writer.WriteLine(SummaBezShtrafa);
                writer.WriteLine(Shtraf);
                writer.WriteLine(ObshayaSumma);
            }
        }

        Console.WriteLine("Данные сохранены в файл.");
    }

// чтение из файла
    public static SchetDlyaOplaty ZagruzitIzFayla(string fileName)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine("Файл не найден.");
            return null;
        }

        string[] lines = File.ReadAllLines(fileName);

        SchetDlyaOplaty schet = new SchetDlyaOplaty();

        schet.OplataZaDen = Convert.ToDouble(lines[0]);
        schet.KolichestvoDney = Convert.ToInt32(lines[1]);
        schet.ShtrafZaDen = Convert.ToDouble(lines[2]);
        schet.KolichestvoDneyZaderzhki = Convert.ToInt32(lines[3]);

        if (SohranyatVychislyaemyePolya == true && lines.Length >= 7)
        {
            schet.SummaBezShtrafa = Convert.ToDouble(lines[4]);
            schet.Shtraf = Convert.ToDouble(lines[5]);
            schet.ObshayaSumma = Convert.ToDouble(lines[6]);
        }
        else
        {
            schet.Raschitat();
        }

        Console.WriteLine("Данные загружены из файла.");
        return schet;
    }
}

class Program
{
    static void Main()
    {
        string fileName = "schet.txt";

        SchetDlyaOplaty schet = new SchetDlyaOplaty(1000, 10, 200, 3);

        Console.WriteLine("Исходный счет:");
        schet.Pokazat();

        Console.WriteLine();

    // первый вариант: сохраняем все поля
        SchetDlyaOplaty.SohranyatVychislyaemyePolya = true;

        Console.WriteLine("Сохранение с вычисляемыми полями:");
        schet.SohranitVFayl(fileName);

        Console.WriteLine();

        SchetDlyaOplaty schet1 = SchetDlyaOplaty.ZagruzitIzFayla(fileName);

        Console.WriteLine("Считанный счет из файла:");
        schet1.Pokazat();

    // второй вариант: вычисляемые поля не сохраняются
        SchetDlyaOplaty.SohranyatVychislyaemyePolya = false;

        Console.WriteLine("Сохранение без вычисляемых полей:");
        schet.SohranitVFayl(fileName);

        Console.WriteLine();

        SchetDlyaOplaty schet2 = SchetDlyaOplaty.ZagruzitIzFayla(fileName);

        Console.WriteLine("Считанный счет из файла:");
        schet2.Pokazat();
    }
}