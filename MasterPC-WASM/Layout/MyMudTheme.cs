using MudBlazor;

public class MyMudTheme : MudTheme
{
    public MudTheme GetTheme()
    {
        var theme = new MudTheme()
        {
            Palette = new PaletteLight()
            {
                Primary = "#594AE2",
                AppbarBackground = "#594AE2",
                Background = Colors.Grey.Lighten4,
            },
            PaletteDark = new PaletteDark()
            {
                Primary = "#594AE2",
                AppbarBackground = "#594AE2"
            }
        };
        return theme;
    }
}
