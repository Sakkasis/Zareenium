using System;

public class SystemTime : ISystemTime
{

    public string FullDateTime()
    {

        string fullDateTime;

        var currentHour = DateTime.Now.ToLocalTime().Hour;
        var currentMinute = DateTime.Now.ToLocalTime().Minute;
        var currentSecond = DateTime.Now.ToLocalTime().Second;
        var currentDay = DateTime.Now.ToLocalTime().Day;
        var currentMonth = DateTime.Now.ToLocalTime().Month;
        var currentYear = DateTime.Now.ToLocalTime().Year;

        fullDateTime = currentHour.ToString("00") + ":" + currentMinute.ToString("00") + "." + currentSecond.ToString("00") + " | " + currentDay.ToString("00") + "/" + currentMonth.ToString("00") + "/" + currentYear.ToString("0000");

        return fullDateTime;

    }

    public string FullDigitalClockTime()
    {

        string fullClockTime;

        var currentHour = DateTime.Now.ToLocalTime().Hour;
        var currentMinute = DateTime.Now.ToLocalTime().Minute;
        var currentSecond = DateTime.Now.ToLocalTime().Second;
        var currentMilliSecond = DateTime.Now.ToLocalTime().Millisecond;

        fullClockTime = currentHour.ToString("00") + ":" + currentMinute.ToString("00") + "." + currentSecond.ToString("00") + "." + currentMilliSecond.ToString();

        return fullClockTime;

    }

    public string FullCalendarDateTime()
    {

        string fullCalendarTime;

        var currentDay = DateTime.Now.ToLocalTime().Day;
        var currentMonth = DateTime.Now.ToLocalTime().Month;
        var currentYear = DateTime.Now.ToLocalTime().Year;

        fullCalendarTime = currentDay.ToString("00") + "/" + currentMonth.ToString("00") + "/" + currentYear.ToString("0000");

        return fullCalendarTime;

    }

}