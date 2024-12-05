namespace TaskVisualizerWeb.Domain;

public class DateProvider : IDateProvider
{
    public DateTime Now() => DateTime.Now;
}
