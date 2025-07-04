using System;
using UnityEngine;

[System.Serializable]
public class GameDate : ICloneable
{
    [SerializeField] private int years;
    [SerializeField] private int months;
    [SerializeField] private int days;
    [SerializeField] private int hours;
    [SerializeField] private int minutes;
    [SerializeField] private DayOfWeek dayOfWeek;

    public int Years => years;
    public int Months => months;
    public int Days => days;
    public int Hours => hours;
    public int Minutes => minutes;
    public DayOfWeek DayOfWeek => dayOfWeek;



    private static readonly int[] DaysInMonth = {
        31,
        28,
        31,
        30,
        31,
        30,
        31,
        31,
        30,
        31,
        30,
        31 
    };



    public void AdvanceMinutes(int amount)
    {
        minutes += amount;
        NormalizeTime();
    }



    public void AdvanceHours(int amount)
    {
        hours += amount;
        NormalizeTime();
    }



    public void AdvanceDays(int amount = 1)
    {
        days += amount;
        AdvanceDayOfWeek(amount);
        NormalizeTime();
    }



    private void AdvanceDayOfWeek(int amount = 1)
    {
        int total = ((int)dayOfWeek + amount) % 7;
        if (total < 0) total += 7;
        dayOfWeek = (DayOfWeek)total;
    }



    public void NormalizeTime()
    {
        if (minutes >= 60)
        {
            hours += minutes / 60;
            minutes %= 60;
        }

        if (hours >= 24)
        {
            int extraDays = hours / 24;
            hours %= 24;
            days += extraDays;
            AdvanceDayOfWeek(extraDays);
        }

        while (true)
        {
            int daysThisMonth = DaysInMonth[months - 1];

            if (days > daysThisMonth)
            {
                days -= daysThisMonth;
                months++;

                if (months > 12)
                {
                    months = 1;
                    years++;
                }
            }
            else break;
        }
    }


    public int GetTotalMinutes()
    {
        int totalDays = 0;
        for (int y = 0; y < years; y++)
        {
            totalDays += 365;
        }
        for (int m = 1; m < months; m++)
        {
            totalDays += DaysInMonth[m - 1];
        }
        totalDays += days - 1;

        int totalMinutes = totalDays * 24 * 60 + hours * 60 + minutes;
        return totalMinutes;
    }



    public object Clone()
    {
        return (GameDate)this.MemberwiseClone();
    }

    public static int operator -(GameDate date1, GameDate date2)
    {
        return Mathf.Abs(date1.GetTotalMinutes() - date2.GetTotalMinutes());
    }



    public static bool operator >(GameDate date1, GameDate date2)
    {
        return date1.GetTotalMinutes() > date2.GetTotalMinutes();
    }



    public static bool operator <(GameDate date1, GameDate date2)
    {
        return date1.GetTotalMinutes() < date2.GetTotalMinutes();
    }



    public static bool operator >=(GameDate date1, GameDate date2)
    {
        return date1.GetTotalMinutes() >= date2.GetTotalMinutes();
    }



    public static bool operator <=(GameDate date1, GameDate date2)
    {
        return date1.GetTotalMinutes() <= date2.GetTotalMinutes();
    }



    public string DateString => $"{years:D4}-{months:D2}-{days:D2}";
    public string TimeString => $"{hours:D2}:{minutes:D2}";
    public string FullString => $"{DateString} {TimeString}";
}



public enum DayOfWeek
{
    MON, TUE, WED, THU, FRI, SAT, SUN
}